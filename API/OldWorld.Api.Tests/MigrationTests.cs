using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using OldWorld.Api.Application;
using OldWorld.Api.Application.GameRules.DeferredEngine;
using OldWorld.Api.Controllers;
using OldWorld.Api.DependencyInjection;
using OldWorld.Api.Models.Api;
using OldWorld.Api.Models.ListImport;
using OldWorld.Api.Telemetry;
using OldWorld.Domain.Catalog;
using OldWorld.Domain.Enums.Army;
using OldWorld.Domain.Exceptions;
using OldWorld.Domain.Models;
using OldWorld.Domain.Rules;
using OldWorld.Infrastructure.Catalog;
using OldWorld.Infrastructure.Configuration;
using OldWorld.Infrastructure.Imports;
using OldWorld.Infrastructure.Imports.OldWorldBuilder;
using OldWorld.Infrastructure.Storage;

[assembly: Parallelize(Scope = ExecutionScope.MethodLevel)]

namespace OldWorld.Api.Tests;

[TestClass]
public class MigrationTests
{
    private const GameVersionEnum Version = GameVersionEnum.OldWorldV152Renegade;

    private static GameRulesSettings Settings() => new()
    {
        DefaultGameVersion = Version,
        SupportedGameVersions = [Version],
        RulesContainerName = "rules",
        VersionFolders = new() { [Version] = "v152r" }
    };

    [TestMethod]
    public async Task ApiRegistrationsResolveAndCatalogStartupPopulatesSharedProvider()
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            EnvironmentName = Environments.Production,
            ApplicationName = typeof(Program).Assembly.GetName().Name
        });
        builder.Configuration.Sources.Clear();
        builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["AzureStorageSettings:ConnectionString"] = "UseDevelopmentStorage=true",
            ["ApplicationInsightsSettings:ConnectionString"] = "InstrumentationKey=00000000-0000-0000-0000-000000000001;IngestionEndpoint=https://localhost/",
            ["GameRulesSettings:DefaultGameVersion"] = Version.ToString(),
            ["GameRulesSettings:SupportedGameVersions:0"] = Version.ToString(),
            ["GameRulesSettings:RulesContainerName"] = "rules",
            [$"GameRulesSettings:VersionFolders:{Version}"] = "v152r"
        });
        builder.Host.UseDefaultServiceProvider(options =>
        {
            options.ValidateOnBuild = true;
            options.ValidateScopes = true;
        });
        ServicesRegisterer.All(builder);
        ApplicationRegisterer.All(builder);
        ControllersRegisterer.Add(builder);

        var blobs = new InMemoryBlobStorage();
        var jsonStorage = new BlobJsonStorage(blobs);
        await jsonStorage.UpdateAsync("rules", "v152r/rules.json", new RulesCatalogFile { GameVersion = Version, Rules = [new Rule { NameId = "test-rule" }] });
        await jsonStorage.UpdateAsync("rules", "v152r/equipment.json", new EquipmentCatalogFile { GameVersion = Version });
        await jsonStorage.UpdateAsync("rules", "v152r/lores.json", new LoresCatalogFile { GameVersion = Version });
        builder.Services.Replace(ServiceDescriptor.Singleton<IBlobStorage>(blobs));

        await using var app = builder.Build();
        var services = app.Services;
        Assert.AreEqual(Version, services.GetRequiredService<IOptions<GameRulesSettings>>().Value.DefaultGameVersion);
        Assert.AreSame<object>(services.GetRequiredService<IGameRulesCatalogProvider>(), services.GetRequiredService<IGameRulesCatalogStore>());
        var startup = services.GetServices<IHostedService>().OfType<GameRulesCatalogHostedService>().Single();
        await startup.StartAsync(CancellationToken.None);
        Assert.IsTrue(services.GetRequiredService<IGameRulesCatalogProvider>().Get(Version).Rules.ContainsKey("test-rule"));
        Assert.AreEqual(Version, services.GetRequiredService<IGameRulesEngineProvider>().Get(Version).Catalog.GameVersion);
        _ = ActivatorUtilities.CreateInstance<AdminController>(services);
        _ = ActivatorUtilities.CreateInstance<ListImportController>(services);
        _ = ActivatorUtilities.CreateInstance<GameCatalogController>(services);
    }

    [TestMethod]
    public void ExistingImportFixturePreservesBaselineValidationErrors()
    {
        var json = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "TestData", "dark-elves-basic-test.owb.json"));
        var result = ImportController().Import(JsonSerializer.Deserialize<JsonElement>(json));
        var response = (ApiResponse<ListImportAcceptedResponse>) ((BadRequestObjectResult) result.Result!).Value!;

        CollectionAssert.AreEqual(new[]
        {
            "Property 'armyComposition' at #/characters[0].mounts[3].armyComposition: ArrayExpected.",
            "Property 'armyComposition' at #/characters[0].mounts[3].armyComposition: NullExpected.",
            "Property 'mounts' at #/characters[0].mounts: NullExpected."
        }, response.Errors.Select(error => error.Message).ToArray());
    }

    [TestMethod]
    public void ValidImportIsStillAccepted()
    {
        var json = "{\"game\":\"the-old-world\",\"characters\":[],\"core\":[],\"special\":[],\"rare\":[],\"mercenaries\":[],\"allies\":[],\"extraProperty\":true}";
        var result = ImportController().Import(JsonSerializer.Deserialize<JsonElement>(json));
        var response = (ApiResponse<ListImportAcceptedResponse>) ((OkObjectResult) result.Result!).Value!;
        Assert.IsTrue(response.Success);
        Assert.IsNotNull(response.Data);
    }

    [TestMethod]
    public void UnrecognizedImportKeepsApiErrorEnvelope()
    {
        var result = ImportController().Import(JsonSerializer.Deserialize<JsonElement>("{}"));
        var response = (ApiResponse<ListImportAcceptedResponse>) ((BadRequestObjectResult) result.Result!).Value!;
        var json = JsonSerializer.SerializeToElement(response, new JsonSerializerOptions(JsonSerializerDefaults.Web));

        Assert.IsFalse(json.GetProperty("success").GetBoolean());
        Assert.AreEqual("Missing required property 'game'.", json.GetProperty("errors")[0].GetProperty("message").GetString());
    }

    [TestMethod]
    public void SchemaErrorsAreMappedFromInfrastructureToApiMessages()
    {
        var result = ImportController().Import(JsonSerializer.Deserialize<JsonElement>("{\"game\":\"the-old-world\",\"core\":42}"));
        var response = (ApiResponse<ListImportAcceptedResponse>) ((BadRequestObjectResult) result.Result!).Value!;

        Assert.IsFalse(response.Success);
        Assert.IsNotEmpty(response.Errors);
        Assert.IsTrue(response.Errors.Any(error => error.Message.Contains("core", StringComparison.Ordinal)));
    }

    [TestMethod]
    public async Task CatalogRoundTripPreservesJsonShapeAndCancellation()
    {
        var blobs = new InMemoryBlobStorage();
        var service = new GameCatalogStorageService(new BlobJsonStorage(blobs), Options.Create(Settings()));
        using var cancellation = new CancellationTokenSource();
        await service.UpdateRulesAsync(Version, new RulesCatalogFile { GameVersion = Version, Rules = [new Rule { NameId = "test", PointCost = 5 }] }, cancellation.Token);

        Assert.AreEqual(cancellation.Token, blobs.LastCancellationToken);
        Assert.AreEqual("application/json", blobs.LastContentType);
        var json = JsonSerializer.Deserialize<JsonElement>(blobs.Blobs[("rules", "v152r/rules.json")].ToString());
        Assert.AreEqual(Version.ToString(), json.GetProperty("gameVersion").GetString());
        Assert.AreEqual("test", json.GetProperty("rules")[0].GetProperty("nameId").GetString());
        Assert.AreEqual(5, (await service.GetRulesAsync(Version, cancellation.Token)).Rules[0].PointCost);
    }

    [TestMethod]
    public async Task CatalogVersionMismatchDoesNotWrite()
    {
        var blobs = new InMemoryBlobStorage();
        var service = new GameCatalogStorageService(new BlobJsonStorage(blobs), Options.Create(Settings()));

        await Assert.ThrowsAsync<GameCatalogException>(() => service.UpdateRulesAsync(Version, new RulesCatalogFile { GameVersion = GameVersionEnum.OldWorldV152 }, CancellationToken.None));
        Assert.IsEmpty(blobs.Blobs);
    }

    [TestMethod]
    public async Task MissingCatalogStillReturnsBadRequest()
    {
        var storage = new GameCatalogStorageService(new BlobJsonStorage(new InMemoryBlobStorage()), Options.Create(Settings()));
        var controller = new GameCatalogController(NullLogger<GameCatalogController>.Instance, new NoTelemetry(), storage);
        controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };

        var result = await controller.GetRules("v152r", CancellationToken.None);
        var response = (ApiResponse<RulesCatalogFile>) ((BadRequestObjectResult) result.Result!).Value!;
        Assert.IsFalse(response.Success);
        Assert.AreEqual("Game catalog blob was not found: rules/v152r/rules.json.", response.Errors.Single().Message);
    }

    [TestMethod]
    public async Task CatalogLoaderRejectsMismatchedVersion()
    {
        var storage = new BlobJsonStorage(new InMemoryBlobStorage());
        await storage.UpdateAsync("rules", "v152r/rules.json", new RulesCatalogFile { GameVersion = GameVersionEnum.OldWorldV152 });
        await storage.UpdateAsync("rules", "v152r/equipment.json", new EquipmentCatalogFile { GameVersion = Version });
        await storage.UpdateAsync("rules", "v152r/lores.json", new LoresCatalogFile { GameVersion = Version });
        var loader = new GameRulesCatalogLoader(storage, NullLogger<GameRulesCatalogLoader>.Instance, Options.Create(Settings()));

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => loader.LoadAsync(Version, CancellationToken.None));
        StringAssert.Contains(exception.Message, "expected 'OldWorldV152Renegade'");
    }

    private static ListImportController ImportController()
    {
        var controller = new ListImportController(NullLogger<ListImportController>.Instance, new NoTelemetry(), new JsonListBuilderDetector(), new OldWorldBuilderJsonParser());
        controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
        return controller;
    }

    private sealed class NoTelemetry : IControllerTelemetry
    {
        public void TrackEvent<TController>(ControllerTelemetryContext<TController> context) { }
        public void TrackLogError<TController>(ControllerTelemetryContext<TController> context) { }
        public void TrackLogException<TController>(ControllerTelemetryContext<TController> context, Exception exception) { }
    }
}
