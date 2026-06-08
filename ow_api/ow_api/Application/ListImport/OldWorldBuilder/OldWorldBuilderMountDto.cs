using System.Text.Json.Serialization;

namespace ow_api.Application.ListImport.OldWorldBuilder;

public class OldWorldBuilderMountDto : OldWorldBuilderLocalizedTextDto
{
    [JsonPropertyName("points")]
    public int Points { get; set; }

    [JsonPropertyName("active")]
    public bool? Active { get; set; }

    [JsonPropertyName("perModel")]
    public bool? PerModel { get; set; }

    [JsonPropertyName("armyComposition")]
    public List<string>? ArmyComposition { get; set; }

    [JsonPropertyName("options")]
    public List<OldWorldBuilderMountOptionDto>? Options { get; set; }

    [JsonPropertyName("notes")]
    public OldWorldBuilderLocalizedTextDto? Notes { get; set; }
}
