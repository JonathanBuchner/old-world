using System.Text.Json.Serialization;

namespace ow_api.Application.ListImport.OldWorldBuilder;

public class OldWorldBuilderDatasetDto
{
    [JsonPropertyName("game")]
    public string Game { get; set; } = string.Empty;

    [JsonPropertyName("characters")]
    public List<OldWorldBuilderUnitDto> Characters { get; set; } = [];

    [JsonPropertyName("core")]
    public List<OldWorldBuilderUnitDto> Core { get; set; } = [];

    [JsonPropertyName("special")]
    public List<OldWorldBuilderUnitDto> Special { get; set; } = [];

    [JsonPropertyName("rare")]
    public List<OldWorldBuilderUnitDto> Rare { get; set; } = [];

    [JsonPropertyName("mercenaries")]
    public List<OldWorldBuilderUnitDto> Mercenaries { get; set; } = [];

    [JsonPropertyName("allies")]
    public List<OldWorldBuilderUnitDto> Allies { get; set; } = [];
}
