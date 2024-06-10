using IMPAR_API.Models;
using IMPAR_API.Services.Interfaces;
using IMPAR_API.Repositories.Interfaces;

namespace IMPAR_API.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IEnumerable<Car>> GetAllAsync(bool includePhoto = false)
        {
            return await _carRepository.GetAllAsync(includePhoto);
        }
        public async Task<IEnumerable<Car>> GetPaginatedAsync(int skip, int top, bool includePhoto = false)
        {
            return await _carRepository.GetPaginatedAsync(skip, top, includePhoto);
        }
        public async Task<Car> GetByIdAsync(int id, bool includePhoto = false)
        {
            return await _carRepository.GetByIdAsync(id, includePhoto);
        }

        public async Task<Car> AddAsync(Car car)
        {
            await _carRepository.AddAsync(car);
            return car;
        }

        public async Task AddPhotoAsync(int carId, string img64)
        {
            var car = await _carRepository.GetByIdAsync(carId);
            if (car == null)
            {
                throw new ArgumentException($"Car with id {carId} not found.");
            }

            // Create a new photo object with the provided Base64 string
            var photo = new Photo { Base64 = img64 };

            // Associate the photo with the car
            car.Photo = photo;

            // Add the photo to the database
            await _carRepository.AddPhotoAsync(photo);
        }

        public async Task UpdateAsync(int id, Car updatedCar)
        {
            var existingCar = await _carRepository.GetByIdAsync(id, true);

            if (existingCar == null)
            {
                throw new ArgumentException("Car not found.");
            }

            // Update only the provided fields
            if (!string.IsNullOrEmpty(updatedCar.Name))
            {
                existingCar.Name = updatedCar.Name;
            }

            if (!string.IsNullOrEmpty(updatedCar.Status))
            {
                existingCar.Status = updatedCar.Status;
            }

            if (updatedCar.Photo != null && !string.IsNullOrEmpty(updatedCar.Photo.Base64))
            {
                if (existingCar.Photo == null)
                {
                    existingCar.Photo = new Photo { Base64 = updatedCar.Photo.Base64 };
                }
                else
                {
                    existingCar.Photo.Base64 = updatedCar.Photo.Base64;
                }
            }

            await _carRepository.UpdateAsync(existingCar);
        }

        public async Task DeleteAsync(int id)
        {

            await _carRepository.DeleteAsync(id);
        }
    }
}
