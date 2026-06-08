using System.Text.Json.Serialization;

namespace ow_api.Application.ListImport.OldWorldBuilder;

public class OldWorldBuilderLocalizedTextDto
{
    [JsonPropertyName("name_en")]
    public string NameEn { get; set; } = string.Empty;

    [JsonPropertyName("name_de")]
    public string NameDe { get; set; } = string.Empty;

    [JsonPropertyName("name_fr")]
    public string NameFr { get; set; } = string.Empty;

    [JsonPropertyName("name_es")]
    public string NameEs { get; set; } = string.Empty;

    [JsonPropertyName("name_it")]
    public string NameIt { get; set; } = string.Empty;

    [JsonPropertyName("name_pl")]
    public string NamePl { get; set; } = string.Empty;

    [JsonPropertyName("name_cn")]
    public string NameCn { get; set; } = string.Empty;
}
