using Data_Access_Layer;
using Domain;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Api
{
    public class CarApi
    {
        protected readonly AppDbContext _context;

        public CarApi(AppDbContext context)
        {
            _context = context;
        }

        internal List<Car> GetAllCarsAction()
        {
            return _context.Cars.ToList();
        }

        internal Car GetCarByIdAction(int carId)
        {
            return _context.Cars.FirstOrDefault(c => c.Id == carId);
        }

        internal bool AddCarAction(Car car)
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

        internal bool UpdateCarAction(Car car)
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

        internal bool DeleteCarAction(int carId)
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