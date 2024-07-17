using Microsoft.AspNetCore.Components;
using PrintManagement.Client.Services.Interface;

namespace PrintManagement.Client.Components.Pages.Authentication
{
    public partial class ConfirmRegister
    {
        [Inject] IAuthService _ser { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }
        CancellationToken cancellation;
        string code;
        private async Task ConfirmReigster()
        {
            var result = await _ser.ConfirmRegisterAccount(code, cancellation);
            if (result)
            {
                _navigationManager.NavigateTo("https://localhost:7295/");
            }
        }
    }
}
