using System.Text.Json.Serialization;

namespace ow_api.Application.ListImport.OldWorldBuilder;

public class OldWorldBuilderUnitDto : OldWorldBuilderLocalizedTextDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("named")]
    public bool? Named { get; set; }

    [JsonPropertyName("points")]
    public int Points { get; set; }

    [JsonPropertyName("minimum")]
    public int Minimum { get; set; }

    [JsonPropertyName("maximum")]
    public int Maximum { get; set; }

    [JsonPropertyName("magicItemsArmy")]
    public string? MagicItemsArmy { get; set; }

    [JsonPropertyName("maxSignatureSpells")]
    public int MaxSignatureSpells { get; set; }

    [JsonPropertyName("army")]
    public string? Army { get; set; }

    [JsonPropertyName("armyComposition")]
    public Dictionary<string, OldWorldBuilderArmyCompositionDto>? ArmyComposition { get; set; }

    [JsonPropertyName("command")]
    public List<OldWorldBuilderCommandOptionDto>? Command { get; set; }

    [JsonPropertyName("equipment")]
    public List<OldWorldBuilderSelectableOptionDto>? Equipment { get; set; }

    [JsonPropertyName("armor")]
    public List<OldWorldBuilderSelectableOptionDto>? Armor { get; set; }

    [JsonPropertyName("options")]
    public List<OldWorldBuilderSelectableOptionDto>? Options { get; set; }

    [JsonPropertyName("mounts")]
    public List<OldWorldBuilderMountDto>? Mounts { get; set; }

    [JsonPropertyName("items")]
    public List<OldWorldBuilderMagicItemAllowanceDto>? Items { get; set; }

    [JsonPropertyName("lores")]
    public List<string>? Lores { get; set; }

    [JsonPropertyName("specialRules")]
    public OldWorldBuilderLocalizedTextDto? SpecialRules { get; set; }

    [JsonPropertyName("notes")]
    public OldWorldBuilderLocalizedTextDto? Notes { get; set; }
}
