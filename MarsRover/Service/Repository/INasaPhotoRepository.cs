using System.Collections.Generic;
using System.Threading.Tasks;
using MarsRover.Models;

namespace MarsRover.Service.Repository
{
    public interface INasaPhotoRepository
    {
        Task<IEnumerable<Photo>> GetPhoto(string rover, string earthDate);
        Task<IEnumerable<Rover>> GetRovers();
    }
}
