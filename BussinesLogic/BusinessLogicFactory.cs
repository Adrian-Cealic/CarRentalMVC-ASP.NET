using BusinessLogic.Interfaces;
using Data_Access_Layer;

namespace BusinessLogic
{
    public class BusinessLogicFactory
    {
        private readonly AppDbContext _context;

        public BusinessLogicFactory(AppDbContext context)
        {
            _context = context;
        }

        public IUserService GetUserService()
        {
            return new UserService(_context);
        }

        public ICarService GetCarService()
        {
            return new CarService(_context);
        }

        public IRentalService GetRentalService()
        {
            return new RentalService(_context);
        }

        public IAuthService GetAuthService()
        {
            return new AuthService(_context);
        }
    }
}