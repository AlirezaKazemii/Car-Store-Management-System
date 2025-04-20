using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ImageStorateService : IImageStorageService
    {
        public async Task<List<string>> SaveImageAsync(List<IFormFile> images)
        {
            var imagePaths = new List<string>();

            // مسیر کامل به دایرکتوری images در wwwroot
            var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

            // اگر دایرکتوری images وجود نداشته باشد، آن را بساز
            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            foreach (var image in images)
            {
                // نام فایل جدید و بدون فضای اضافی
                var fileName = Path.GetFileNameWithoutExtension(image.FileName) + "_" + Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                var filePath = Path.Combine(uploadDirectory, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                // مسیر فایل ذخیره‌شده را به لیست اضافه کن
                imagePaths.Add(Path.Combine("images", fileName)); // مسیر نسبی برای استفاده در پاسخ API
            }

            return imagePaths;
        }
    }
}
