using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Demo.PL.Helper
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile file , string folderName)
        {
            // 1. Get Located Folder Path
            // var folderPath = @"D:\\01course\\alearn\\Routezone\\Backendzone\\MVCzone\\Session 02\\Demos\\fromvideo_06\\G03 Solution\\Demo.PL\\wwwroot\\files\\Imgs\\";
            // var folderPath = Directory.GetCurrentDirectory() + "/wwwroot/files/" + folderName;
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", folderName);

            // 2. Get File Name and Make its Name UNIQUE [Use Guid]
            var fileName = $"{Guid.NewGuid()}{Path.GetFileName(file.FileName)}";

            // 3. Get File Path
            var filePath = Path.Combine(folderPath, fileName);

            // 4. Save File as Streams [Stream: Data Per Time]
            using var fs = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fs);
            return fileName;
        }

        public static void DeleteFile(string fileName, string folderName) 
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", folderName, fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
