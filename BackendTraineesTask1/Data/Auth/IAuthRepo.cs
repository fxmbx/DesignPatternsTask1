using BackendTraineesTask1.Models.Dto;
using BackendTraineesTask1.Response;

namespace BackendTraineesTask1.Data.Auth
{
    public interface IAuthRepo
    {
        Task<ServiceResponse<AuthenticationResponse>> Register(UserRegisterDto req);
        Task<ServiceResponse<AuthenticationResponse>> Login(UserLoginDto req);

    }
}