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
        private const string ImageDirectory = "Images";

        public ImageService(IHttpClientFactory httpClientFactory, IConfiguration configuration) 
            : base(httpClientFactory, configuration)
        {

        }

        public async Task SaveJpegImageAsync(Uri requestUri, string filename)
        {
            var filePath = $"{ImageDirectory}\\{filename}";

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
            return File.OpenRead($"{ImageDirectory}\\{filename}");
        }

        public IEnumerable<string> RetrieveAllJpgImage()
        {
            var imgList = new List<string>();
            var results = Directory.EnumerateFiles(ImageDirectory);

            results?.ToList().ForEach(c => imgList.Add(c));

            return imgList;
        }

        private void CleanUpOldImages()
        {
            var images = RetrieveAllJpgImage();

            if (images.Count() > 25)
            {
                FileSystemInfo fileInfo = new DirectoryInfo(ImageDirectory).GetFileSystemInfos()
                    .OrderBy(fi => fi.CreationTime).First();
                File.Delete($"{ImageDirectory}\\{fileInfo.Name}");
            }
        }
    }
}

