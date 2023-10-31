using SteamGuard.Worker.Util;

namespace Worker.Test
{
    [TestClass]
    public class FileUtilityTests
    {
        private const string SOURCE_DIR = "C:\\UnitTest\\SteamGuard\\Source";
        private const string TARGET_DIR = "C:\\UnitTest\\SteamGuard\\Target";

        [TestMethod]
        public void Test_CopyFiles_SourcePathEmpty_ThrowsArgumentNullException()
        {
            try
            {
                FileUtility.CopyFiles(string.Empty, string.Empty);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'sourcePath')");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void Test_CopyFiles_TargetPathEmpty_ThrowsArgumentNullException()
        {
            try
            {
                FileUtility.CopyFiles(SOURCE_DIR, string.Empty);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'targetPath')");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void Test_CopyFiles_SourceDirNotExists_ThrowsInvalidOperationException()
        {
            try
            {
                FileUtility.CopyFiles(SOURCE_DIR, TARGET_DIR);
            }
            catch (InvalidOperationException ex)
            {
                Assert.AreEqual(ex.Message, $"Source directory {SOURCE_DIR} does not exist");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void Test_CopyFiles_FilesCopiedToTargetSubDir_Success()
        {
            try
            {
                CreateDirectoryIfNotExists(SOURCE_DIR);
                CreateDirectoryIfNotExists(TARGET_DIR);

                File.Create($"{SOURCE_DIR}\\temp.txt").Close();

                FileUtility.CopyFiles(SOURCE_DIR, TARGET_DIR);

                var targetDir = new DirectoryInfo(TARGET_DIR);
                Assert.IsNotNull(targetDir);

                var targetSubDir = targetDir.GetDirectories().FirstOrDefault();
                Assert.IsNotNull(targetSubDir);

                var targetSubDirFiles = targetSubDir.GetFiles();
                Assert.IsTrue(targetSubDirFiles.Any());
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                DeleteDirectoryIfExists($"C:\\UnitTest");
            }
        }

        private static void CreateDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        private static void DeleteDirectoryIfExists(string path)
        {
            if (Directory.Exists(path))
                Directory.Delete(path, true);
        }
    }
}