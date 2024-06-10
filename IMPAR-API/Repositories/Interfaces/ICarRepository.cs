using IMPAR_API.Models;

namespace IMPAR_API.Repositories.Interfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAllAsync(bool includePhoto = false);
        Task<IEnumerable<Car>> GetPaginatedAsync(int skip, int top, bool includePhoto = false);
        Task<Car> GetByIdAsync(int id, bool includePhoto = false);
        Task AddAsync(Car car);
        Task UpdateAsync(Car car);
        Task DeleteAsync(int id);
        Task AddPhotoAsync(Photo photo);
    }
}
