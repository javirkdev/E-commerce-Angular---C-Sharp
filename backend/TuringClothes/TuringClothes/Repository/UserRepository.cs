using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuringClothes.Database;
using TuringClothes.Dtos;

namespace TuringClothes.Repository
{
    public class UserRepository : Repository<User, long>
    {
        private readonly MyDatabase _myDatabase;
        public UserRepository(MyDatabase myDatabase) : base(myDatabase)
        {
            _myDatabase = myDatabase;
        }

        public async Task<User> GetUserById(long userId)
        {
            return await _myDatabase.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task AddAsync(User user)
        {
            await _myDatabase.AddAsync(user);
        }

        public async Task<UserDto> GetordersByUser(long userId)
        {
            var user = await _myDatabase.Users.Include(o=> o.Orders).ThenInclude(d=>d.OrderDetails).ThenInclude(p=>p.Product).FirstOrDefaultAsync(u=> u.Id == userId);
            var orders = user.Orders.ToList();

            var userDto = new UserDto
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Address = user.Address,
                Role = user.Role,
                orders = orders
            };

            return userDto;
        }

    }
}
