
using IMPAR_API.Models;

namespace IMPAR_API.Services.Interfaces
{
    public interface IPhotoService
    {
        Task<IEnumerable<Photo>> GetAllAsync();
        Task<Photo> GetByIdAsync(int id);
        Task AddAsync(Photo photo);
        Task UpdateAsync(Photo photo);
        Task DeleteAsync(int id);
    }
}
