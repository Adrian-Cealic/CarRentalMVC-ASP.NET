using Domain;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces
{
    public interface IRentalService
    {
        List<Rental> GetAllRentals();
        List<Rental> GetUserRentals(int userId);
        Rental GetRentalById(int rentalId);
        bool CreateRental(Rental rental);
        bool UpdateRentalStatus(int rentalId, RentalStatus status);
        bool CancelRental(int rentalId);
    }
}