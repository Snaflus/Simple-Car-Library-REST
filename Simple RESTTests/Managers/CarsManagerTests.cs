using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple_REST.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Class_Library;

namespace Simple_REST.Managers.Tests
{
    [TestClass()]
    public class CarsManagerTests
    {
        private CarsManager _manager;
        [TestInitialize]
        public void SetUp()
        {
            _manager = new CarsManager();
        }

        [TestMethod()]
        public void GetAllCarsTest()
        {
            IEnumerable<Car> cars = null;
            cars = _manager.GetAllCars("Ford", null, "");
            Assert.IsNotNull(_manager.GetAllCars(null, null, null));
            if (!cars.Any())
            {
                Assert.Fail();
            }
            cars = _manager.GetAllCars("", 5000, "");
            if (!cars.Any())
            {
                Assert.Fail();
            }
            cars = _manager.GetAllCars("", null, "123ABC4");
            if (!cars.Any())
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void GetCarByIdTest()
        {
            Car car = null;
            car = _manager.GetCarById(1);
            Assert.IsNull(_manager.GetCarById(0));
            Assert.AreEqual("Ford", car.Model);
        }

        [TestMethod()]
        public void AddCarTest()
        {
            Car car = new Car(5, "Test5", 9000, "HIJKLMN");
            Assert.AreEqual(car, _manager.AddCar(car)); //tests that addcar returns the created car
            Car addedCar = null;
            addedCar = _manager.GetCarById(5);
            Assert.AreEqual(5, addedCar.Id);
            Assert.AreEqual("Test5",addedCar.Model);
            Assert.AreEqual(9000,addedCar.Price);
            Assert.AreEqual("HIJKLMN",addedCar.License);
        }
        [TestMethod()]
        public void DeleteCarTest()
        {
            Assert.AreEqual(_manager.GetCarById(4), _manager.DeleteCar(4)); //tests that deletecar returns the deleted car
            Car car = _manager.GetCarById(4);
            Assert.AreEqual(null,car);
        }
    }
}