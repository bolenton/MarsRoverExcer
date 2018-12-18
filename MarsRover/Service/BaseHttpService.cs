using System;
using System.Net.Http;
using MarsRover.Models;
using Microsoft.Extensions.Configuration;

namespace MarsRover.Service
{
    public abstract class BaseHttpService
    {
        public readonly HttpClient _apiClient;

        protected BaseHttpService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _apiClient = httpClientFactory.CreateClient();
            _apiClient.BaseAddress = new
                Uri(configuration.GetSection(nameof(AppConst.ApiBaseUrl))[nameof(AppConst.Nasa)]);
        }
    }
}
