﻿using IMPAR_API.Models;
using Microsoft.EntityFrameworkCore;
using IMPAR_API.Data;
using IMPAR_API.Repositories.Interfaces;

namespace IMPAR_API.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _dbContext;

        public CarRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Car>> GetAllAsync(bool includePhoto = false)
        {
            if (includePhoto)
            {
                return await _dbContext.Cars.Include(c => c.Photo).ToListAsync();
            }
            else
            {
                return await _dbContext.Cars.ToListAsync();
            }
        }

        public async Task<IEnumerable<Car>> GetPaginatedAsync(int skip, int top, bool includePhoto = false)
        {
            IQueryable<Car> query = _dbContext.Cars;

            if (includePhoto)
            {
                query = query.Include(c => c.Photo);
            }

            query = query.Skip(skip).Take(top);

            return await query.ToListAsync();
        }

        public async Task<Car> GetByIdAsync(int id, bool includePhoto = false)
        {
            if (includePhoto)
            {
                return await _dbContext.Cars.Include(c => c.Photo).FirstOrDefaultAsync(c => c.Id == id);
            }
            else
            {
                return await _dbContext.Cars.FindAsync(id);
            }
        }

        public async Task AddAsync(Car car)
        {
            _dbContext.Cars.Add(car);
            await _dbContext.SaveChangesAsync();

            if (car.Photo != null)
            {
                var photo = new Photo { Base64 = car.Photo.Base64 };
                _dbContext.Photos.Add(photo);
                await _dbContext.SaveChangesAsync();
                car.PhotoId = photo.Id;
                await _dbContext.SaveChangesAsync();
            }
        }


        public async Task UpdateAsync(Car car)
        {
            _dbContext.Cars.Attach(car);
            _dbContext.Entry(car).State = EntityState.Modified;

            if (car.Photo != null)
            {
                _dbContext.Photos.Attach(car.Photo);
                _dbContext.Entry(car.Photo).State = EntityState.Modified;
            }

            await _dbContext.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var car = await _dbContext.Cars.FindAsync(id) ?? throw new ArgumentException("Car not found.");
            _dbContext.Cars.Remove(car);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddPhotoAsync(Photo photo)
        {
            _dbContext.Photos.Add(photo);
            await _dbContext.SaveChangesAsync();
        }
    }
}
