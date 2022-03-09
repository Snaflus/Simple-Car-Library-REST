using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Car_Class_Library;
using Microsoft.AspNetCore.Http;
using Simple_REST.Managers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Simple_REST.Controllers
{
    /// <summary>
    /// Controls cars in database
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarsManager _manager = new CarsManager();

        /// <summary>
        /// Input search criteria for model or license. Price is given as a maximum value
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="filterPrice"></param>
        /// <param name="filterLicense"></param>
        /// <returns>List of cars that contain all given criteria</returns> 
        /// <response code="200">Car(s) retrieved successfully</response>
        /// <response code="404">No car found</response>
        // GET: api/<CarManager>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<IEnumerable<Car>> Get([FromQuery] string filterModel, [FromQuery] int? filterPrice, [FromQuery] string filterLicense)
        {
            IEnumerable<Car> cars = null;
            cars = _manager.GetAllCars(filterModel, filterPrice, filterLicense);

            if (!cars.Any()) return NotFound("No car found with criteria");
            return Ok(cars);
        }

        /// <summary>
        /// Input ID for a car
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The car object with given ID</returns> 
        /// <response code="200">Car retrieved successfully</response>
        /// <response code="404">Car not found</response>
        // GET api/<CarManager>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<Car> Get(int id)
        {
            Car car = _manager.GetCarById(id);
            if (car == null) return NotFound("No such car, id " + id);
            return Ok(car);
        }

        /// <summary>
        /// Add car object to database
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Car that was added</returns>
        /// <response code="201">Car created successfully</response>
        /// <response code="400">Variable(s) out of range or null</response>
        // POST api/<ItemsController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<Car> Post([FromBody] Car value)
        {
            if (value.Model == null || value.Price <= 0 || value.License == null) return BadRequest(value);

            Car car = new Car();
            car = _manager.AddCar(value);

            return Created("api/items/" + car.Id, car);
        }
        /// <summary>
        /// Deletes car object from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Car that was deleted</returns>
        /// <response code="200">Car deleted successfully</response>
        /// <response code="404">Car not found</response>
        // DELETE api/<CarManager>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult<Car> Delete(int id)
        {
            Car car = _manager.GetCarById(id);
            if (car == null) return NotFound("No such car, id: " + id);
            return Ok(_manager.DeleteCar(id));
        }
    }
}
