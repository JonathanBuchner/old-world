using System.Text.Json.Serialization;

namespace ow_api.Application.ListImport.OldWorldBuilder;

public class OldWorldBuilderMountOptionDto : OldWorldBuilderLocalizedTextDto
{
    [JsonPropertyName("points")]
    public int Points { get; set; }

    [JsonPropertyName("notes")]
    public List<OldWorldBuilderLocalizedTextDto>? Notes { get; set; }
}
