namespace SteamGuard.Worker.Configuration
{
    public class SteamBackupConfiguration
    {
        public IEnumerable<SteamGameConfiguration> SteamGames { get; set; } = new List<SteamGameConfiguration>();

        public string TargetDirectory { get; set; }
    }
}