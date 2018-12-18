using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MarsRover.Models;
using MarsRover.Service.Repository;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace MarsRover.Service.Nasa
{
    public class NasaService : INasaService
    {
        private readonly INasaPhotoRepository _nasaPhotoRepository;
        private readonly IMemoryCache _cache;

        public NasaService(INasaPhotoRepository nasaPhotoRepository, IMemoryCache memoryCache)
        {
            _cache = memoryCache;
            _nasaPhotoRepository = nasaPhotoRepository;
        }
        public async Task<IEnumerable<Photo>> GetPhoto(string rover, DateTime earthDate)
        {
            var earthDateString = earthDate.ToString("yyyy-MM-dd");
            return await _nasaPhotoRepository.GetPhoto(rover, earthDateString);
        }

        public async Task<IEnumerable<Rover>> GetAllRovers()
        {
            var result = await _nasaPhotoRepository.GetRovers();
            
            return result.Where(c => c.Status == "active");
        }

        public async Task<IEnumerable<FilePhoto>> GetDefaultDate(string rover)
        {

            var cacheKey = $"{rover}Data";

            if (!_cache.TryGetValue(cacheKey, out List<FilePhoto> filePhotos))
            {

                filePhotos = new List<FilePhoto>();
                var fileDates = await GetDatesFromFile();

                foreach (var date in fileDates)
                {
                    var earthDate = date.Date.ToString("yyyy-MM-dd");
                    var photoResult = await _nasaPhotoRepository.GetPhoto(rover, earthDate);

                    filePhotos.Add(new FilePhoto()
                    {
                        DateString = date.DateStringName,
                        Photos = photoResult
                    });
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(15));

                if (EnumerableExtensions.Any(filePhotos))
                    _cache.Set(cacheKey, filePhotos, cacheEntryOptions);
            }

            return _cache.Get(cacheKey) as IEnumerable<FilePhoto>;
        }

        private async Task<IEnumerable<FileDate>> GetDatesFromFile()
        {
            // Load file from project
            var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("MarsRover.Data.defaultDates.json");

            // Get json string
            var streamReader = new StreamReader(stream);
            string json = await streamReader.ReadToEndAsync();

            // Convert to CLR list of string
            var fileDateStrings = JsonConvert.DeserializeObject<List<string>>(json);

            var fileDates = new List<FileDate>();

            // Use only strings that can be used.
            foreach (var date in fileDateStrings)
            {
                if (DateTime.TryParse(date, out var tempDate))
                {
                    fileDates.Add(new FileDate()
                    {
                        Date = tempDate,
                        DateStringName = date
                    });
                }
            }

            return fileDates;         
        }
    }
}
