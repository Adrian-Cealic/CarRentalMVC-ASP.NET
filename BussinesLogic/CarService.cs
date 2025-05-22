using Data_Access_Layer;
using Domain;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity; // Required for Include if you use it later

namespace BusinessLogic
{
    public class CarService
    {
        private readonly AppDbContext _context;

        public CarService(AppDbContext context)
        {
            _context = context;
        }

        public List<Car> GetAllCars()
        {
            return _context.Cars.ToList();
        }

        public Car GetCarById(int carId)
        {
            return _context.Cars.FirstOrDefault(c => c.Id == carId);
        }

        public bool AddCar(Car car)
        {
            if (car == null)
            {
                return false;
            }
            // You can add more validation here if needed
            _context.Cars.Add(car);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateCar(Car car)
        {
            var existingCar = _context.Cars.Find(car.Id);
            if (existingCar == null)
            {
                return false;
            }

            _context.Entry(existingCar).CurrentValues.SetValues(car);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteCar(int carId)
        {
            var car = _context.Cars.Find(carId);
            if (car == null)
            {
                return false;
            }

            _context.Cars.Remove(car);
            _context.SaveChanges();
            return true;
        }
    }
} 