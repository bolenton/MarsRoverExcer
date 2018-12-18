using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MarsRover.Service.Image
{
    public interface IImageService
    {
        Task SaveJpegImageAsync(Uri requestUri, string filename);
        FileStream RetrieveJpgImage(string filename);
        IEnumerable<string> RetrieveAllJpgImage();
    }
}