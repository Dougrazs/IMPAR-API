using Microsoft.AspNetCore.Mvc;
using IMPAR_API.Models;
using IMPAR_API.Services.Interfaces;
using Microsoft.AspNetCore.OData.Query;

namespace IMPAR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        // GET: api/Cars
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Car>>> Get(int skip = 0, int top = 10)
        {
            var cars = await _carService.GetPaginatedAsync(skip, top, includePhoto: true); // Specify to include photo data
            return Ok(cars);
        }

        // GET: api/Cars/ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> Get(int id)
        {
            var car = await _carService.GetByIdAsync(id, includePhoto: true); // Specify to include photo data
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }
        // POST: api/Cars
        [HttpPost]
        public async Task<ActionResult<Car>> Post(Car car)
        {
            try
            {
                if (car.Photo != null)
                {
                    // If the car has a photo, add it
                    var newCar = await _carService.AddAsync(car);
                    await _carService.AddPhotoAsync(newCar.Id, car.Photo.Base64);
                    return CreatedAtAction(nameof(Get), new { id = newCar.Id }, newCar);
                }
                else
                {
                    // If the car has no photo, just add it without adding a photo
                    var newCar = await _carService.AddAsync(car);
                    return CreatedAtAction(nameof(Get), new { id = newCar.Id }, newCar);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != car.Id)
            {
                return BadRequest("Car ID mismatch.");
            }

            try
            {
                await _carService.UpdateAsync(id, car);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // DELETE: api/Cars/ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _carService.DeleteAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
