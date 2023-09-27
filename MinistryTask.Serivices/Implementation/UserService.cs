using Microsoft.AspNetCore.Http;
using MinistryTask.Domain.Abstractions;
using MinistryTask.Domain.Models;
using MinistryTask.Serivices.Abstraction;
using MinistryTask.Serivices.Models.ResposeModels.UserRequestModels;
using MinistryTask.Serivices.Models.ResposeModels.UserResponses;

namespace MinistryTask.Serivices.Implementation
{
    public class UserService : IUserService
    {
        private readonly IPasswordHasher _hasher;
        private readonly IUnitOfWork _unit;
        private readonly IJwtService _jwtService;
        public UserService(IPasswordHasher hasher, IUnitOfWork unit, IJwtService jwtService)
        {
            _hasher = hasher;
            _unit = unit;
            _jwtService = jwtService;
        }

        public async Task<Dictionary<byte[], byte[]>> GetHashandSalt(string mail)
        {
            byte[] passHash;
            byte[] passSalt;
            var result = new Dictionary<byte[], byte[]>();
            var user = (await _unit.Users.FindAsync(p => p.Equals(mail))).FirstOrDefault();

            if (user != null)
            {
                passHash = user.PasswordHash;
                passSalt = user.PasswordSalt;
                result.Add(passHash, passSalt);
                return result;
            }

            return result;
        }

        public async Task<LoginResponse> LoginUser(LoginRequestDto request)
        {
            var user = (await _unit.Users.FindAsync(u => u.Email == request.Mail))
                .FirstOrDefault();

            if (user == null)
            {
                return new LoginResponse { StatusCode = StatusCodes.Status400BadRequest, Token = null, Message = "მეილი ან პაროლი არასწორია გთხოვთ გადაამოწმოთ" };
            }

            if (!_hasher.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new LoginResponse { StatusCode = StatusCodes.Status400BadRequest, Token = null, Message = "მეილი ან პაროლი არასწორია გთხოვთ გადაამოწმოთ" };
            }

            var token = _jwtService.GenerateToken(user.Id.ToString(), user.Role);

            return new LoginResponse
            {
                Token = token,
                Message = "ტოკენი წარმატებით დაგენერირდა",
                StatusCode = StatusCodes.Status200OK
            };
        }

        public async Task<RegisterResponse> RegisterUserAsync(UserDto request)
        {
            _hasher.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new User()
            {
                Name = request.Name,
                Email = request.Email,
                DateOfBirth = request.DateOfBirth,
                IdNumber = request.IdNumber,
                LastName = request.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _unit.Users.Add(user);
            _unit.Save();
            return new RegisterResponse
            {
                Message = "Registration was Sucessfull",
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
}
