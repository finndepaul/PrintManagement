using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Org.BouncyCastle.Asn1.Ocsp;
using PrintManagement.Application.Payloads.RequestModels.UserRequests;
using PrintManagement.Application.Payloads.ResponseModels;
using PrintManagement.Application.Payloads.Responses;
using PrintManagement.Client.Extensions;
using PrintManagement.Client.Services.Interface;
using System.Net.Http.Headers;
using System.Text.Json;

namespace PrintManagement.Client.Services.Implement
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage; // nuget
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> ConfirmCreateNewPassword(CreateNewPasswordRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _httpClient.PutAsJsonAsync("/api/User/ConfirmCreateNewPassword", request, cancellationToken);
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ConfirmRegisterAccount(string confirmCode, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _httpClient.GetAsync($"/api/Auth/ConfirmRegisterAccount?confirmCode={confirmCode}", cancellationToken);
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ForgetPassword(string email, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _httpClient.GetAsync($"/api/User/ForgetPassword?email={email}", cancellationToken);
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                return  false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ResponseObject<DataResponseLogin>> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Auth/Login", request, cancellationToken);
            var content = await result.Content.ReadAsStringAsync(cancellationToken);
            var loginResponse = JsonSerializer.Deserialize<ResponseObject<DataResponseLogin>>(content,
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });
            if (!result.IsSuccessStatusCode)
            {
                return loginResponse;
            }
            await _localStorage.SetItemAsync("authToken", loginResponse.Data.AccessToken);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(request.UserName);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.Data.AccessToken);
            return loginResponse;
        }

        public async Task<ResponseObject<DataResponseUser>> Register(RegisterRequest request, CancellationToken cancellationToken)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Auth/Register", request, cancellationToken);
            var content = await result.Content.ReadAsStringAsync(cancellationToken);
            var loginResponse = JsonSerializer.Deserialize<ResponseObject<DataResponseUser>>(content,
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });
            return loginResponse;
        }
    }
}
