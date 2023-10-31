namespace SteamGuard.Worker.Util
{
    public static class FileUtility
    {
        public static void CopyFiles(string sourcePath, string targetPath)
        {
            if (string.IsNullOrEmpty(sourcePath))
                throw new ArgumentNullException(nameof(sourcePath));

            if (string.IsNullOrEmpty(targetPath))
                throw new ArgumentNullException(nameof(targetPath));

            if (!Directory.Exists(sourcePath))
                throw new InvalidOperationException($"Source directory {sourcePath} does not exist");

            if (!Directory.Exists(targetPath))
                Directory.CreateDirectory(targetPath);

            var sourceDir = new DirectoryInfo(sourcePath);
            var targetDir = new DirectoryInfo(targetPath);
            var targetSubDir = targetDir.CreateSubdirectory($"Save_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}");

            foreach (var file in sourceDir.GetFiles())
            {
                File.Copy(file.FullName, Path.Combine(targetSubDir.FullName, file.Name));
            }
        }
    }
}