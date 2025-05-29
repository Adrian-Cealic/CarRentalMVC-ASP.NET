using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Data_Access_Layer;
using Domain;
using BusinessLogic.Interfaces;
using BusinessLogic.Api;

namespace BusinessLogic
{
    public class RentalService : IRentalService
    {
        private readonly AppDbContext _context;

        public RentalService(AppDbContext context)
        {
            _context = context;
        }

        public List<Rental> GetAllRentals()
        {
            return _context.Rentals
                .Include(r => r.User)
                .Include(r => r.Car)
                .OrderByDescending(r => r.StartDate)
                .ToList();
        }

        public List<Rental> GetUserRentals(int userId)
        {
            return _context.Rentals
                .Include(r => r.Car)
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.StartDate)
                .ToList();
        }

        public Rental GetRentalById(int rentalId)
        {
            return _context.Rentals
                .Include(r => r.User)
                .Include(r => r.Car)
                .FirstOrDefault(r => r.Id == rentalId);
        }

        public bool CreateRental(Rental rental)
        {
            if (rental == null)
                return false;

            // Calculate total price based on days and car price
            var days = (rental.EndDate - rental.StartDate).Days + 1;
            var car = _context.Cars.Find(rental.CarId);
            
            if (car == null || !car.IsAvailable)
                return false;

            rental.TotalPrice = car.PricePerDay * days;
            
            // Set car as unavailable during the rental period
            car.IsAvailable = false;
            
            _context.Rentals.Add(rental);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateRentalStatus(int rentalId, RentalStatus status)
        {
            var rental = _context.Rentals.Find(rentalId);
            if (rental == null)
                return false;

            rental.Status = status;
            
            // If rental is completed or cancelled, make the car available again
            if (status == RentalStatus.Completed || status == RentalStatus.Cancelled)
            {
                var car = _context.Cars.Find(rental.CarId);
                if (car != null)
                {
                    car.IsAvailable = true;
                }
            }
            
            _context.SaveChanges();
            return true;
        }

        public bool CancelRental(int rentalId)
        {
            var rental = _context.Rentals
                .Include(r => r.Car)
                .FirstOrDefault(r => r.Id == rentalId);

            if (rental == null || rental.Status == RentalStatus.Completed)
                return false;

            rental.Status = RentalStatus.Cancelled;
            
            // Make the car available again
            if (rental.Car != null)
            {
                rental.Car.IsAvailable = true;
            }
            
            _context.SaveChanges();
            return true;
        }
    }
}