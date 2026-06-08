using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ow_api.Application.ListImport;
using ow_api.Application.ListImport.OldWorldBuilder;
using ow_api.Enums.Json;
using ow_api.Exceptions;
using ow_api.Infrastructure.Telemetry;
using ow_api.Models.Api;
using ow_api.Models.ListImport;

namespace ow_api.Controllers
{
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
                    ["errors"] = string.Join(", ", ex.Errors.Select(error => error.Message)),
                    ["error.count"] = ex.Errors.Count,
                    ["json"] = rawJson.GetRawText(),
                    ["listtype"] = "old-world-builder",
                };
                TrackLogException("listimport.raw.failed.deserialize", nameof(Import), ex, additionalTags);

                return BadRequest(ApiResponse<ListImportAcceptedResponse>.Fail(ex.Errors));
            }
        }
    }
}
