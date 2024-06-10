using IMPAR_API.Models;

namespace IMPAR_API.Repositories.Interfaces
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetAllAsync();
        Task<Photo> GetByIdAsync(int id);
        Task AddAsync(Photo photo);
        Task UpdateAsync(Photo photo);
        Task DeleteAsync(int id);
    }
}
