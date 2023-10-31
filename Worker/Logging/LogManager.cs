namespace SteamGuard.Worker.Logging
{
    public static class LogManager
    {
        public static void Archive()
        {            
            var logDir = Path.Combine(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent?.FullName ?? string.Empty, "Log");
            if (!string.IsNullOrEmpty(logDir))
            {
                if (!Directory.Exists(logDir))
                    Directory.CreateDirectory(logDir);
                var sourceDir = new DirectoryInfo(logDir);
                var targetDir = sourceDir.CreateSubdirectory($"Archive_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}");
                foreach (var log in sourceDir.GetFileSystemInfos("*.log", SearchOption.TopDirectoryOnly))
                {
                    File.Move(log.FullName, Path.Combine(targetDir.FullName, log.Name));
                }
            }
        }
    }
}