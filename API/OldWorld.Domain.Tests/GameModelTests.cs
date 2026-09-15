using System.Text.Json;
using System.Text.Json.Serialization;
using OldWorld.Domain.DataStructures;
using OldWorld.Domain.Enums.Army;
using OldWorld.Domain.Models;
using OldWorld.Domain.Models.Profile;

[assembly: Parallelize(Scope = ExecutionScope.MethodLevel)]

namespace OldWorld.Domain.Tests;

[TestClass]
public class GameModelTests
{
    [TestMethod]
    public void GameRoundTripPreservesArmyBookProfilesAndPoints()
    {
        var game = new Game
        {
            GameVersionEnum = GameVersionEnum.OldWorldV152Renegade,
            ArmyLists = [new ArmyBook
            {
                ModelEntries = [new Model
                {
                    ReadableName = "Test model",
                    Profiles = [new ModelProfile
                    {
                        PointCost = 10,
                        Equipment = [new Equipment { PointCost = 3 }],
                        SpecialRules = [new RuleList { PointCost = 2 }],
                        Spells = [new SpellProfile { PointCost = 7 }]
                    }]
                }]
            }]
        };
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        options.Converters.Add(new JsonStringEnumConverter());

        var json = JsonSerializer.Serialize(game, options);
        var restored = JsonSerializer.Deserialize<Game>(json, options)!;

        Assert.AreEqual(game.GuidId, restored.GuidId);
        Assert.AreEqual(GameVersionEnum.OldWorldV152Renegade, restored.GameVersionEnum);
        Assert.AreEqual("Test model", restored.ArmyLists[0].ModelEntries[0].ReadableName);
        // Preserve the existing points behavior: spell costs are not included.
        Assert.AreEqual(15, restored.ArmyLists[0].ModelEntries[0].TotalPointsValue);
    }

    [TestMethod]
    public void UnitPointsIncludePrimaryModelCountAndAdditionalModels()
    {
        var unit = new Unit
        {
            PrimaryModel = new Model { Profiles = [new ModelProfile { PointCost = 10 }] },
            PrimaryModelCount = 5,
            AdditionalModels = [new Model { Profiles = [new ModelProfile { PointCost = 20 }] }]
        };

        Assert.AreEqual(70, unit.TotalPointsValue);
        unit.PrimaryModel = null;
        Assert.AreEqual(20, unit.TotalPointsValue);
    }
}
