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
            while (!stoppingToken.IsCancellationRequested)
            {                
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(TimeSpan.FromMinutes(_configuation.GetValue<int>("IntervalMinutes")), stoppingToken);
            }
        }
    }
}