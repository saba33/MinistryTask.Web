using MinistryTask.Serivices.Models.ResposeModels.UserRequestModels;
using MinistryTask.Serivices.Models.ResposeModels.UserResponses;

namespace MinistryTask.Serivices.Abstraction
{
    public interface IUserService
    {
        Task<RegisterResponse> RegisterUserAsync(UserDto user);
        Task<LoginResponse> LoginUser(LoginRequestDto request);
        Task<Dictionary<byte[], byte[]>> GetHashandSalt(string mail);
    }
}
