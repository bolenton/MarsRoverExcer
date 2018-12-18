using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MarsRover.Models;
using Microsoft.Extensions.Configuration;

namespace MarsRover.Service.Image
{
    public class ImageService : BaseHttpService, IImageService
    {
        private readonly string _imageDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}images";

        public ImageService(IHttpClientFactory httpClientFactory, IConfiguration configuration) 
            : base(httpClientFactory, configuration)
        {

        }

        public async Task SaveJpegImageAsync(Uri requestUri, string filename)
        {
            EnsureImageDirectoryExist();
            var filePath = $"{_imageDirectory}\\{filename}";

            using (var request = new HttpRequestMessage(HttpMethod.Get, requestUri))
            using (
                Stream contentStream = await (await _apiClient.SendAsync(request)).Content.ReadAsStreamAsync(),
                stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 3145728, true))
            {
                await contentStream.CopyToAsync(stream);
            }

            CleanUpOldImages();
        }

        public FileStream RetrieveJpgImage(string filename)
        {
            return File.OpenRead($"{_imageDirectory}\\{filename}");
        }

        public IEnumerable<string> RetrieveAllJpgImage()
        {
            EnsureImageDirectoryExist();

            var imgList = new List<string>();
            var results = Directory.EnumerateFiles(_imageDirectory);
            results?.ToList().ForEach(c => imgList.Add(c.Substring(c.IndexOf("\\images\\") + 9)));

            return imgList;
        }

        private void CleanUpOldImages()
        {
            var images = RetrieveAllJpgImage();

            if (images.Count() > 25)
            {
                FileSystemInfo fileInfo = new DirectoryInfo(_imageDirectory).GetFileSystemInfos()
                    .OrderBy(fi => fi.CreationTime).First();
                File.Delete($"{_imageDirectory}\\{fileInfo.Name}");
            }
        }

        private void EnsureImageDirectoryExist()
        {
            if (!Directory.Exists(_imageDirectory))
                Directory.CreateDirectory(_imageDirectory);
        }
    }
}

