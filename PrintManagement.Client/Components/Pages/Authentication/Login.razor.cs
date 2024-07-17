using Microsoft.AspNetCore.Components;
using PrintManagement.Application.Payloads.RequestModels.UserRequests;
using PrintManagement.Client.Services.Interface;

namespace PrintManagement.Client.Components.Pages.Authentication
{
    public partial class Login
    {
        [Inject] IAuthService _ser { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }

        private string username;
        private string password;
        private bool ShowErrors;
        private string Error = "";
        LoginRequest request = new LoginRequest();
        CancellationToken CancellationToken;
        private async Task HandleLogin()
        {
            ShowErrors = false;
            request.Password = password;
            request.UserName = username;
            var result = await _ser.Login(request, CancellationToken);
            if (result.Data == null)
            {
                ShowErrors = true;
                Error = result.Message;
            }
            else
            {
                _navigationManager.NavigateTo("https://localhost:7295/Home");
            }
        }
    }
}
