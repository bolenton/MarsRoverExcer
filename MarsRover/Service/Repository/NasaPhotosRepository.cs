using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MarsRover.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MarsRover.Service.Repository
{
    public class NasaPhotosRepository : BaseHttpService, INasaPhotoRepository
    {
        public NasaPhotosRepository(IHttpClientFactory httpClientFactory, IConfiguration configuration) :
            base(httpClientFactory, configuration)
        {

        }

        public async Task<IEnumerable<Photo>> GetPhoto(string rover, string earthDate)
        {
            var response = await _apiClient.GetAsync($"{rover}/photos?earth_date={earthDate}&{AppConst.ApiKey}");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PhotoContainer>(resultString);

            return result.Photos;
        }

        public async Task<IEnumerable<Rover>> GetRovers()
        {
            var response = await _apiClient.GetAsync($"?{AppConst.ApiKey}");
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RoverContainer>(resultString);

            return result.Rovers;
        }
    }
}
