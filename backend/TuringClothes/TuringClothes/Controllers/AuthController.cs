using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TuringClothes.Database;
using TuringClothes.Dtos;
using TuringClothes.Model;

namespace TuringClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly TokenValidationParameters _tokenParameters;
        private readonly UnitOfWork _unitOfWork;

        public AuthController(UnitOfWork unitOfWork, IOptionsMonitor<JwtBearerOptions> jwtOptions)
        {
            _unitOfWork = unitOfWork;
            _tokenParameters = jwtOptions.Get(JwtBearerDefaults.AuthenticationScheme).TokenValidationParameters;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDto loginData)
        {
            var user = await _unitOfWork.AuthRepository.GetByEmail(loginData.Email);

            if (user == null)
            {
                return NotFound("Usuario no encontrado");
            }
            if (loginData.Email == user.Email && BCrypt.Net.BCrypt.Verify(loginData.Password, user.Password))
            {
                string stringToken = GenerateToken(user);

                return Ok(new AuthResultDto { AccessToken = stringToken });
            }
            return Unauthorized("Email o contraseña incorrecto");
        }


        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto registerData)
        {
            var existingUser = await _unitOfWork.AuthRepository.GetByEmail(registerData.Email);
            if (existingUser != null)
            {
                return Conflict("El correo electrónico ya está registrado.");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerData.Password);
            var newUser = new User
            {
                Name = registerData.Name,
                Surname = registerData.Surname,
                Email = registerData.Email,
                Password = hashedPassword,
                Address = registerData.Address,
                Orders = new List<Order>(),
                Role = "user"
            };
            await _unitOfWork.UserRepository.AddAsync(newUser);
            await _unitOfWork.SaveChangesAsync();
            string stringToken = GenerateToken(newUser);

            return Ok(new AuthResultDto { AccessToken = stringToken });
        }

        [HttpGet("user by email")]
        public async Task<User?> GetUserByEmail(string mail)
        {
            var user = await _unitOfWork.AuthRepository.GetByEmail(mail);
            return user;
        }

        private string GenerateToken(User user)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Claims = new Dictionary<string, object>
                    {
                        { "id", user.Id },
                        { ClaimTypes.Name, user.Name},
                        { ClaimTypes.Email, user.Email },
                        { ClaimTypes.Role, user.Role }

                    },
                Expires = DateTime.UtcNow.AddYears(1),
                SigningCredentials = new SigningCredentials(
                       _tokenParameters.IssuerSigningKey,
                       SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }

        [Authorize]
        [HttpPut("UpdatePass")]
        public async Task<ActionResult> UpdatePassword([FromBody] PassDto passDto)
        {

            var userId = User.FindFirst("id").Value;

            if (string.IsNullOrEmpty(userId) || !long.TryParse(userId, out var userIdLong))
            {
                return Unauthorized("El usuario no está autenticado.");
            }

            var user = await _unitOfWork.AuthRepository.GetByIdAsync(userIdLong);


            if (!BCrypt.Net.BCrypt.Verify(passDto.OldPassword, user.Password))
            {
                return BadRequest("La contraseña introducida es incorrecta.");

            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(passDto.Password);
            user.Password = hashedPassword;

            await _unitOfWork.AuthRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return Ok("contraseña actualizada");
        }

        [Authorize]
        [HttpGet("GetEditUser")]
        public async Task<ActionResult> GetEditUser()
        {
            var userId = User.FindFirst("id").Value;

            if (string.IsNullOrEmpty(userId) || !long.TryParse(userId, out var userIdLong))
            {
                return Unauthorized("El usuario no está autenticado.");
            }

            var user = await _unitOfWork.AuthRepository.GetByIdAsync(userIdLong);

            var editDto = new EditDto
            {
                Name = user.Name,
                Surname = user.Surname,
                Address = user.Address,
                Email = user.Email,
            };
            return Ok(editDto);
        }


        [Authorize]
        [HttpPut("UserUpdate")]
        public async Task<ActionResult> EditUser([FromBody] EditDto editDto)
        {
            var userId = User.FindFirst("id").Value;

            if (string.IsNullOrEmpty(userId) || !long.TryParse(userId, out var userIdLong))
            {
                return Unauthorized("El usuario no está autenticado.");
            }

            var user = await _unitOfWork.AuthRepository.GetByIdAsync(userIdLong);

            user.Name = editDto.Name;
            user.Surname = editDto.Surname;
            user.Address = editDto.Address;
            user.Email = editDto.Email;



            await _unitOfWork.AuthRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
            string stringToken = GenerateToken(user);

            return Ok(new AuthResultDto { AccessToken = stringToken });

        }
    }



}

