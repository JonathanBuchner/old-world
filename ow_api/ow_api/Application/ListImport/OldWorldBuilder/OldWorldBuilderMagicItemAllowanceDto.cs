using System.Text.Json.Serialization;

namespace ow_api.Application.ListImport.OldWorldBuilder;

public class OldWorldBuilderMagicItemAllowanceDto : OldWorldBuilderLocalizedTextDto
{
    [JsonPropertyName("types")]
    public List<string> Types { get; set; } = [];

    [JsonPropertyName("maxPoints")]
    public int? MaxPoints { get; set; }

    [JsonPropertyName("notes")]
    public OldWorldBuilderLocalizedTextDto? Notes { get; set; }
}
