using financial_management_service.Core.Constant;

namespace financial_management_service.Utils
{
    public class FileUtils
    {
        protected FileUtils(){}

        public static string GetPathFile(string fileName)
        {
            var saveDirectory = System.IO.Directory.GetCurrentDirectory() + "/wwwrot/";

            if (!Directory.Exists(saveDirectory))
                Directory.CreateDirectory(saveDirectory);

            return Path.Combine(saveDirectory, fileName);
        }

        public static void Delete(string filePath)
        {
            if (!Directory.Exists(filePath))
                File.Delete(filePath);
        }

        public static string? RemoveExtensionInFileName(string nameFile)
        {
            var fileName = nameFile;
            if (fileName?.IndexOf(".") != -1)
                fileName = fileName?[..fileName.IndexOf(".")];

            return fileName;
        }

        public static void SaveFileToLocal(string fName, byte[] obj)
        {
            using var fileStream = new FileStream(fName, FileMode.Create);

            for (int i = 0; i < obj.Length; i++)
                fileStream.WriteByte(obj[i]);

            fileStream.Seek(0, SeekOrigin.Begin);
        }

        public static void CleanUpFile(string fName)
        {
            Task.Run(() =>
            {
                try
                {
                    Thread.Sleep(1000);
                    File.Delete(fName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            });
        }

        public static byte[] ConvertFileToByte(IWebHostEnvironment environment, string relativePath) => System.IO.File.ReadAllBytes(GetFilePath(environment, relativePath));

        public static string GetFilePath(IWebHostEnvironment environment, string relativePath)
        {
            if (string.IsNullOrWhiteSpace(environment.WebRootPath))
                environment.WebRootPath = Directory.GetCurrentDirectory();

            return Path.Combine(environment.WebRootPath, relativePath);
        }
    }
}
