using System.Text.Json;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using OldWorld.Infrastructure.Imports;
using OldWorld.Infrastructure.Imports.OldWorldBuilder;
using OldWorld.Infrastructure.Imports.Exceptions;
using OldWorld.Api.Telemetry;
using OldWorld.Api.Models.Api;
using OldWorld.Api.Models.ListImport;

namespace OldWorld.Api.Controllers
{
    [ApiVersion(1.0)]
    public class ListImportController : BaseController<ListImportController>
    {
        private readonly JsonListBuilderDetector _jsonListBuilderHelper;
        private readonly OldWorldBuilderJsonParser _oldWorldBuilderJsonParser;

        public ListImportController(ILogger<ListImportController> logger, IControllerTelemetry controllerTelemetry, JsonListBuilderDetector jsonListBuilderDetector, OldWorldBuilderJsonParser oldWorldBuilderJsonParser) : base(logger, controllerTelemetry)
        {
            _jsonListBuilderHelper = jsonListBuilderDetector;
            _oldWorldBuilderJsonParser = oldWorldBuilderJsonParser;
        }

        [HttpPost]
        public ActionResult<ApiResponse<ListImportAcceptedResponse>> Import([FromBody] JsonElement rawJson)
        {
            try
            {
                // Throws InvalidJsonListExceptions
                var jsonFormatType = _jsonListBuilderHelper.Detect(rawJson);

                switch (jsonFormatType)
                {
                    case JsonFormatType.OldWorldBuilder:
                        // Throws OldWorldBuilderJsonDeserializeException
                        _oldWorldBuilderJsonParser.Parse(rawJson);
                        break;

                    default:
                        throw new Exception($"Unhandled JSON format type: {jsonFormatType}. Need to add format type to import.");
                }

                var additionalTags = new Dictionary<string, object?>
                {
                    ["json.format.type"] = jsonFormatType.ToString()
                };

                TrackEvent("listimport.raw.succeeded", nameof(Import), additionalTags);

                return Ok(ApiResponse<ListImportAcceptedResponse>.Ok(new ListImportAcceptedResponse()));
            }
            catch (InvalidJsonListException ex)
            {
                TrackLogException("listimport.raw.failed.unrecognizedtype", nameof(Import), ex);

                return BadRequest(ApiResponse<ListImportAcceptedResponse>.Fail(ex.Message));
            }
            catch (OldWorldBuilderJsonDeserializeException ex)
            {
                var additionalTags = new Dictionary<string, object?>
                {
                    ["errors"] = string.Join(", ", ex.Errors),
                    ["error.count"] = ex.Errors.Count,
                    ["json"] = rawJson.GetRawText(),
                    ["listtype"] = "old-world-builder",
                };
                TrackLogException("listimport.raw.failed.deserialize", nameof(Import), ex, additionalTags);

                return BadRequest(ApiResponse<ListImportAcceptedResponse>.Fail(ex.Errors.ToArray()));
            }
        }
    }
}
