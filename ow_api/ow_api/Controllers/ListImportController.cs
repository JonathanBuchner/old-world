using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ow_api.Application.ListImport;
using ow_api.Exceptions;
using ow_api.Infrastructure.Telemetry;
using ow_api.Models.Api;
using ow_api.Models.ListImport;

namespace ow_api.Controllers
{
    public class ListImportController : BaseController<ListImportController>
    {
        private readonly JsonListBuilderDetector _jsonListBuilderHelper;

        public ListImportController(ILogger<ListImportController> logger, IControllerTelemetry controllerTelemetry, JsonListBuilderDetector jsonListBuilderDetector) : base(logger, controllerTelemetry)
        {
            _jsonListBuilderHelper = jsonListBuilderDetector;
        }

        [HttpPost]
        public ActionResult<ApiResponse<ListImportAcceptedResponse>> Import([FromBody] JsonElement rawJson)
        {
            try
            {
                // Throws InvalidJsonListExceptions
                var jsonFormatType = _jsonListBuilderHelper.Detect(rawJson);

                TrackEvent("listimport.raw.succeeded", nameof(Import), new Dictionary<string, object?>
                {
                    ["json.format.type"] = jsonFormatType.ToString()
                });

                return Ok(ApiResponse<ListImportAcceptedResponse>.Ok(new ListImportAcceptedResponse()));
            }
            catch (InvalidJsonListException ex)
            {
                TrackLogException("listimport.raw.failed.unrecognizedtype", nameof(Import), ex);

                return BadRequest(ApiResponse<ListImportAcceptedResponse>.Fail(ex.Message));
            }
        }
    }
}
