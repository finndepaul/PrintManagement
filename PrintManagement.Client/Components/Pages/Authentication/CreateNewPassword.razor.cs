using Microsoft.AspNetCore.Components;
using PrintManagement.Application.Payloads.RequestModels.UserRequests;
using PrintManagement.Client.Services.Interface;

namespace PrintManagement.Client.Components.Pages.Authentication
{
    public partial class CreateNewPassword
    {
        [Inject] IAuthService _ser { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }
        string confirmCode;
        string newPassword;
        string confirmPassword;
        CancellationToken cancellationToken;
        CreateNewPasswordRequest request = new CreateNewPasswordRequest();
        private async Task HandleCreatePassword()
        {
            request.ConfirmCode = confirmCode;
            request.NewPassword = newPassword;
            request.ConfirmPassword = confirmPassword;
            var result = await _ser.ConfirmCreateNewPassword(request,cancellationToken);
            if (result)
            {
                _navigationManager.NavigateTo("https://localhost:7295/");
            }
        }
    }
}
