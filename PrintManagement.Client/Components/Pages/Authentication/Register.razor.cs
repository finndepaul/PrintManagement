using Microsoft.AspNetCore.Components;
using PrintManagement.Application.Payloads.RequestModels.UserRequests;
using PrintManagement.Client.Services.Interface;
using System.ComponentModel.DataAnnotations;

namespace PrintManagement.Client.Components.Pages.Authentication
{
    public partial class Register
    {
        [Inject] IAuthService _ser { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }
        private string username;
        private string password;
        private string fullName;
        private DateTime? dateOfBirth = DateTime.Today;
        private string email;
        private string phoneNumber;
        RegisterRequest request = new RegisterRequest();
        CancellationToken cancellationToken;
        private async Task HandleRegister()
        {
            request.UserName = username;
            request.Password = password;
            request.Email = email;
            request.PhoneNumber = phoneNumber;
            request.DateOfBirth = dateOfBirth;
            request.FullName = fullName;
            var result = await _ser.Register(request, cancellationToken);
            if (result.Data != null)
            {
                _navigationManager.NavigateTo("https://localhost:7295/confirm-register");
            }
        }
    }
}
