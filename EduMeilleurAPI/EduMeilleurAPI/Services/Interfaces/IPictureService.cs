using EduMeilleurAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EduMeilleurAPI.Services.Interfaces
{
    public interface IPictureService
    {
        Task<Picture?> CreatePicture(Picture picture);

        Task<Picture?> Delete(Picture picture);

        Task<Picture?> GetAsync(User? user = null, int? id = null);
    }
}
