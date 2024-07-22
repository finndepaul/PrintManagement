using PrintManagement.Application.Payloads.RequestModels.UserRequests;
using PrintManagement.Application.Payloads.ResponseModels;
using PrintManagement.Application.Payloads.Responses;

namespace PrintManagement.Client.Services.Interface
{
    public interface IAuthService
    {
        Task<ResponseObject<DataResponseLogin>> Login(LoginRequest request, CancellationToken cancellationToken);
        Task<ResponseObject<DataResponseUser>> Register(RegisterRequest request, CancellationToken cancellationToken);
        Task<bool> ConfirmRegisterAccount(string confirmCode, CancellationToken cancellationToken);
        Task<bool> ForgetPassword(string email, CancellationToken cancellationToken);
        Task<bool> ConfirmCreateNewPassword(CreateNewPasswordRequest request, CancellationToken cancellationToken);
    }
}
