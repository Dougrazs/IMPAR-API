using IMPAR_API.Models;

namespace IMPAR_API.Services.Interfaces
{
    public interface ICarService
    {
        Task<IEnumerable<Car>> GetAllAsync(bool includePhoto = false);
        Task<Car> GetByIdAsync(int id, bool includePhoto = false);
        Task<Car> AddAsync(Car car);
        Task UpdateAsync(int id, Car car);
        Task<IEnumerable<Car>> GetPaginatedAsync(int skip, int top, bool includePhoto = false);
        Task DeleteAsync(int id);
        Task AddPhotoAsync(int carId, string base64Image);
    }
}