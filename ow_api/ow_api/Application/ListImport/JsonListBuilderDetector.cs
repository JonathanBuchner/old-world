using System.Text.Json;
using ow_api.Enums.Json;
using ow_api.Exceptions;

namespace ow_api.Application.ListImport
{
    public class JsonListBuilderDetector
    {
        public JsonFormatType Detect(JsonElement json)
        {
            if (json.ValueKind != JsonValueKind.Object)
                throw new InvalidJsonListException("JSON payload must be an object.");

            if (!json.TryGetProperty("game", out var game))
                throw new InvalidJsonListException("Missing required property 'game'.");

            if (game.ValueKind != JsonValueKind.String)
                throw new InvalidJsonListException("Property 'game' must be a string.");

            switch (game.GetString())
            {
                case "the-old-world":
                        return JsonFormatType.OldWorldBuilder;
            };

            throw new InvalidJsonListException("JSON format is not recognized.");
        }
    }
}
