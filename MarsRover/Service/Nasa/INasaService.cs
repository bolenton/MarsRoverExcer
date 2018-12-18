using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarsRover.Models;

namespace MarsRover.Service.Nasa
{
    public interface INasaService
    {
        Task<IEnumerable<Photo>> GetPhoto(string rover, DateTime earthDate);
        Task<IEnumerable<Rover>> GetAllRovers();
        Task<IEnumerable<FilePhoto>> GetDefaultDate(string rover);
    }
}