using Car_Class_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#pragma warning disable CS1591

namespace Simple_REST.Managers
{
    public class CarsManager
    {
        private static int _nextId = 1;
        private static List<Car> _data = new List<Car>()
        {
            new Car(_nextId++,"Ford",5000,"123ABC4"),
            new Car(_nextId++,"Mercedes",15000,"ABC123D"),
            new Car(_nextId++,"Nissan",10000, "DCB321A"),
            new Car(_nextId++,"Hotwheels",20,"SPEEDY")
        };
        public List<Car> GetAllCars(string filterModel, int? filterPrice, string filterLicense)
        {
            List<Car> result = _data;
            if (!string.IsNullOrWhiteSpace(filterModel))
            {
                result = result.FindAll(c => c.Model.Contains(filterModel, StringComparison.OrdinalIgnoreCase));
            }
            if (filterPrice != null)
            {
                result = result.FindAll(c => (c.Price <= filterPrice));
            }
            if (!string.IsNullOrWhiteSpace(filterLicense))
            {
                result = result.FindAll(c => c.License.Contains(filterLicense, StringComparison.OrdinalIgnoreCase));
            }
            return result;
        }
        public Car GetCarById(int Id)
        {
            return _data.Find(car => car.Id == Id);
        }
        public Car AddCar(Car newCar)
        {
            newCar.Id = _nextId++;
            _data.Add(newCar);
            return newCar;
        }

        public Car DeleteCar(int id)
        {
            Car car = _data.Find(car => car.Id == id);
            if (car == null) return null;
            _data.Remove(car);
            return car;
        }
    }
}
