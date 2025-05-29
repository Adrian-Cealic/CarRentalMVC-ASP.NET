using Data_Access_Layer;
using Domain;
using System.Collections.Generic;
using BusinessLogic.Interfaces;
using BusinessLogic.Api;

namespace BusinessLogic
{
    public class CarService : CarApi, ICarService
    {
        public CarService(AppDbContext context) : base(context)
        {
        }

        public List<Car> GetAllCars()
        {
            return GetAllCarsAction();
        }

        public Car GetCarById(int carId)
        {
            return GetCarByIdAction(carId);
        }

        public bool AddCar(Car car)
        {
            return AddCarAction(car);
        }

        public bool UpdateCar(Car car)
        {
            return UpdateCarAction(car);
        }

        public bool DeleteCar(int carId)
        {
            return DeleteCarAction(carId);
        }
    }
}