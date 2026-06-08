using System.Text.Json.Serialization;

namespace ow_api.Application.ListImport.OldWorldBuilder;

public class OldWorldBuilderSelectableOptionDto : OldWorldBuilderLocalizedTextDto
{
    [JsonPropertyName("points")]
    public int Points { get; set; }

    [JsonPropertyName("perModel")]
    public bool? PerModel { get; set; }

    [JsonPropertyName("active")]
    public bool? Active { get; set; }

    [JsonPropertyName("equippedDefault")]
    public bool? EquippedDefault { get; set; }

    [JsonPropertyName("alwaysActive")]
    public bool? AlwaysActive { get; set; }

    [JsonPropertyName("stackable")]
    public bool? Stackable { get; set; }

    [JsonPropertyName("minimum")]
    public int? Minimum { get; set; }

    [JsonPropertyName("maximum")]
    public int? Maximum { get; set; }

    [JsonPropertyName("armyComposition")]
    public List<string>? ArmyComposition { get; set; }

    [JsonPropertyName("requiredMagicItem")]
    public string? RequiredMagicItem { get; set; }

    [JsonPropertyName("notes")]
    public OldWorldBuilderLocalizedTextDto? Notes { get; set; }
}
