using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ow_api.Application.GameCatalog;
using ow_api.Application.GameRules.Catalog;
using ow_api.Exceptions;
using ow_api.Infrastructure.Telemetry;
using ow_api.Models.Api;
using ow_api.Models.GameCatalog;
using ow_gen_lib.Enums.Army;

namespace ow_api.Controllers
{
    [ApiVersion(1.0)]
    public class GameCatalogController : BaseController<GameCatalogController>
    {
        private readonly IGameCatalogStorageService _gameCatalogStorageService;

        public GameCatalogController(ILogger<GameCatalogController> logger, IControllerTelemetry controllerTelemetry, IGameCatalogStorageService gameCatalogStorageService) : base(logger, controllerTelemetry)
        {
            _gameCatalogStorageService = gameCatalogStorageService;
        }

        [HttpGet("{gameVersion}/rules")]
        public async Task<ActionResult<ApiResponse<RulesCatalogFile>>> GetRules(string gameVersion, CancellationToken cancellationToken)
        {
            try
            {
                var gameVersionEnum = ParseGameVersion(gameVersion);
                var rules = await _gameCatalogStorageService.GetRulesAsync(gameVersionEnum, cancellationToken);
                TrackEvent("gamecatalog.rules.read.succeeded", nameof(GetRules), BuildTags(gameVersionEnum));

                return Ok(ApiResponse<RulesCatalogFile>.Ok(rules));
            }
            catch (GameCatalogException ex)
            {
                TrackLogException("gamecatalog.rules.read.failed", nameof(GetRules), ex, BuildTags(gameVersion));

                return BadRequest(ApiResponse<RulesCatalogFile>.Fail(ex.Message));
            }
        }

        [HttpPut("{gameVersion}/rules")]
        public async Task<ActionResult<ApiResponse<GameCatalogUpdateResponse>>> UpdateRules(string gameVersion, [FromBody] RulesCatalogFile rulesCatalogFile, CancellationToken cancellationToken)
        {
            try
            {
                var gameVersionEnum = ParseGameVersion(gameVersion);
                await _gameCatalogStorageService.UpdateRulesAsync(gameVersionEnum, rulesCatalogFile, cancellationToken);
                TrackEvent("gamecatalog.rules.update.succeeded", nameof(UpdateRules), BuildTags(gameVersionEnum));

                return Ok(ApiResponse<GameCatalogUpdateResponse>.Ok(new GameCatalogUpdateResponse()));
            }
            catch (GameCatalogException ex)
            {
                TrackLogException("gamecatalog.rules.update.failed", nameof(UpdateRules), ex, BuildTags(gameVersion));

                return BadRequest(ApiResponse<GameCatalogUpdateResponse>.Fail(ex.Message));
            }
        }

        [HttpGet("{gameVersion}/equipment")]
        public async Task<ActionResult<ApiResponse<EquipmentCatalogFile>>> GetEquipment(string gameVersion, CancellationToken cancellationToken)
        {
            try
            {
                var gameVersionEnum = ParseGameVersion(gameVersion);
                var equipment = await _gameCatalogStorageService.GetEquipmentAsync(gameVersionEnum, cancellationToken);
                TrackEvent("gamecatalog.equipment.read.succeeded", nameof(GetEquipment), BuildTags(gameVersionEnum));

                return Ok(ApiResponse<EquipmentCatalogFile>.Ok(equipment));
            }
            catch (GameCatalogException ex)
            {
                TrackLogException("gamecatalog.equipment.read.failed", nameof(GetEquipment), ex, BuildTags(gameVersion));

                return BadRequest(ApiResponse<EquipmentCatalogFile>.Fail(ex.Message));
            }
        }

        [HttpPut("{gameVersion}/equipment")]
        public async Task<ActionResult<ApiResponse<GameCatalogUpdateResponse>>> UpdateEquipment(string gameVersion, [FromBody] EquipmentCatalogFile equipmentCatalogFile, CancellationToken cancellationToken)
        {
            try
            {
                var gameVersionEnum = ParseGameVersion(gameVersion);
                await _gameCatalogStorageService.UpdateEquipmentAsync(gameVersionEnum, equipmentCatalogFile, cancellationToken);
                TrackEvent("gamecatalog.equipment.update.succeeded", nameof(UpdateEquipment), BuildTags(gameVersionEnum));

                return Ok(ApiResponse<GameCatalogUpdateResponse>.Ok(new GameCatalogUpdateResponse()));
            }
            catch (GameCatalogException ex)
            {
                TrackLogException("gamecatalog.equipment.update.failed", nameof(UpdateEquipment), ex, BuildTags(gameVersion));

                return BadRequest(ApiResponse<GameCatalogUpdateResponse>.Fail(ex.Message));
            }
        }

        [HttpGet("{gameVersion}/lores")]
        public async Task<ActionResult<ApiResponse<LoresCatalogFile>>> GetLores(string gameVersion, CancellationToken cancellationToken)
        {
            try
            {
                var gameVersionEnum = ParseGameVersion(gameVersion);
                var lores = await _gameCatalogStorageService.GetLoresAsync(gameVersionEnum, cancellationToken);
                TrackEvent("gamecatalog.lores.read.succeeded", nameof(GetLores), BuildTags(gameVersionEnum));

                return Ok(ApiResponse<LoresCatalogFile>.Ok(lores));
            }
            catch (GameCatalogException ex)
            {
                TrackLogException("gamecatalog.lores.read.failed", nameof(GetLores), ex, BuildTags(gameVersion));

                return BadRequest(ApiResponse<LoresCatalogFile>.Fail(ex.Message));
            }
        }

        [HttpPut("{gameVersion}/lores")]
        public async Task<ActionResult<ApiResponse<GameCatalogUpdateResponse>>> UpdateLores(string gameVersion, [FromBody] LoresCatalogFile loresCatalogFile, CancellationToken cancellationToken)
        {
            try
            {
                var gameVersionEnum = ParseGameVersion(gameVersion);
                await _gameCatalogStorageService.UpdateLoresAsync(gameVersionEnum, loresCatalogFile, cancellationToken);
                TrackEvent("gamecatalog.lores.update.succeeded", nameof(UpdateLores), BuildTags(gameVersionEnum));

                return Ok(ApiResponse<GameCatalogUpdateResponse>.Ok(new GameCatalogUpdateResponse()));
            }
            catch (GameCatalogException ex)
            {
                TrackLogException("gamecatalog.lores.update.failed", nameof(UpdateLores), ex, BuildTags(gameVersion));

                return BadRequest(ApiResponse<GameCatalogUpdateResponse>.Fail(ex.Message));
            }
        }

        private static GameVersionEnum ParseGameVersion(string gameVersion)
        {
            return gameVersion switch
            {
                "v152" => GameVersionEnum.OldWorldV152,
                "v152r" => GameVersionEnum.OldWorldV152Renegade,
                _ => throw new GameCatalogException($"Unsupported game version '{gameVersion}'.")
            };
        }

        private static Dictionary<string, object?> BuildTags(GameVersionEnum gameVersion)
        {
            return new Dictionary<string, object?>()
            {
                ["game.version"] = gameVersion.ToString()
            };
        }

        private static Dictionary<string, object?> BuildTags(string gameVersion)
        {
            return new Dictionary<string, object?>()
            {
                ["game.version"] = gameVersion
            };
        }
    }
}
