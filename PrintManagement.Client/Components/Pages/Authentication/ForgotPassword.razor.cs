using Microsoft.AspNetCore.Components;
using PrintManagement.Client.Services.Interface;

namespace PrintManagement.Client.Components.Pages.Authentication
{
    public partial class ForgotPassword
    {
        [Inject] IAuthService _ser { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }
        string email;
        CancellationToken cancellation;
        private async Task HandleForgot()
        {
            var result = await _ser.ForgetPassword(email, cancellation);
            if (result)
            {
                _navigationManager.NavigateTo("https://localhost:7295/create-new-password");
            }
        }
    }
}
