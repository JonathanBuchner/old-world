using System.Text.Json.Serialization;

namespace ow_api.Application.ListImport.OldWorldBuilder;

public class OldWorldBuilderCommandOptionDto : OldWorldBuilderLocalizedTextDto
{
    [JsonPropertyName("points")]
    public int Points { get; set; }

    [JsonPropertyName("alwaysActive")]
    public bool? AlwaysActive { get; set; }

    [JsonPropertyName("magic")]
    public OldWorldBuilderMagicAllowanceDto? Magic { get; set; }
}
