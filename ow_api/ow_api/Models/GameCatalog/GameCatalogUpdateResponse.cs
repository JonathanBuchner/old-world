namespace ow_api.Models.GameCatalog
{
    public class GameCatalogUpdateResponse
    {
        public bool Updated { get; set; } = true;
        public bool RequiresServerRestart { get; set; } = true;
        public string Message { get; set; } = "Catalog JSON was updated in storage. Restart or reload the server before using the updated in-memory catalog.";
    }
}
