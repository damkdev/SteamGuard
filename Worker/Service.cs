using SteamGuard.Worker.Configuration;
using SteamGuard.Worker.Util;
using System.Diagnostics;

namespace SteamGuard.Worker
{
    public class Service : BackgroundService
    {
        private readonly IConfiguration _configuation;
        private readonly ILogger<Service> _logger;

        public Service(IConfiguration configuration, ILogger<Service> logger)
        {
            _configuation = configuration;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SteamBackupConfiguration steamBackupConfig = new();
            _configuation.GetSection("SteamBackup").Bind(steamBackupConfig);
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Worker running at: {DateTime.Now}");
                try
                {
                    foreach (var game in steamBackupConfig.SteamGames)
                    {
                        if (Process.GetProcessesByName(game.Process).Any())
                        {
                            _logger.LogInformation($"Backing up {game.Name} at {DateTime.Now}");
                            FileUtility.CopyFiles(game.SourceDirectory, Path.Combine(steamBackupConfig.TargetDirectory, game.Name));
                        }
                        else continue;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex);
                }

                await Task.Delay(TimeSpan.FromMinutes(_configuation.GetValue<int>("IntervalMinutes")), stoppingToken);
            }
        }
    }
}