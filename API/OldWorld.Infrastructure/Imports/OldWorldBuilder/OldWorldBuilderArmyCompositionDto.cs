using System.Text.Json.Serialization;

namespace OldWorld.Infrastructure.Imports.OldWorldBuilder;

public class OldWorldBuilderArmyCompositionDto
{
    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;

    [JsonPropertyName("notes")]
    public OldWorldBuilderLocalizedTextDto? Notes { get; set; }

    [JsonPropertyName("specialRules")]
    public OldWorldBuilderLocalizedTextDto? SpecialRules { get; set; }
}
