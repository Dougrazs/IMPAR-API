using IMPAR_API.Services.Interfaces;
using IMPAR_API.Repositories.Interfaces;
using IMPAR_API.Models;

namespace IMPAR_API.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoRepository _photoRepository;

        public PhotoService(IPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        public async Task<IEnumerable<Photo>> GetAllAsync()
        {
            return await _photoRepository.GetAllAsync();
        }

        public async Task<Photo> GetByIdAsync(int id)
        {
            return await _photoRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Photo photo)
        {
            await _photoRepository.AddAsync(photo);
        }

        public async Task UpdateAsync(Photo photo)
        {
            await _photoRepository.UpdateAsync(photo);
        }

        public async Task DeleteAsync(int id)
        {
            await _photoRepository.DeleteAsync(id);
        }
    }
}
