using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TuringClothes.Database;
using TuringClothes.Dtos;
using TuringClothes.Model;

namespace TuringClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public UserController(UnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpGet("UserOrder")]
        public async Task<ActionResult<UserDto>> GetUserOrders()
        {
            var userId = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userId) || !long.TryParse(userId, out var userIdLong))
            {
                return Unauthorized("Invalid user ID.");
            }

            var userDto = await _unitOfWork.UserRepository.GetordersByUser(userIdLong);
            return Ok(userDto);
        }
    }
}


