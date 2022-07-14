using BackendTraineesTask1.Models.Dto;
using BackendTraineesTask1.Response;

namespace BackendTraineesTask1.Services.UserService
{
    public interface IUserService
    {
         Task<ServiceResponse<string>> SendNotification(SendRequestDto sendRequestDto);
    }
}