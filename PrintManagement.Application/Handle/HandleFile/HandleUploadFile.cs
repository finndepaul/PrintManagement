using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.Handle.HandleFile
{
    public class HandleUploadFile
    {
        public static async Task<string> WriteFile(IFormFile file)
        {
            string fileName = "";
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = "DuyDong_" + DateTime.Now.Ticks + extension;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload", "Files");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                var exactPath = Path.Combine(filePath, fileName);
                using (var stream = new FileStream(exactPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch
            {
                throw;
            }
            return fileName;
        }
    }
}
