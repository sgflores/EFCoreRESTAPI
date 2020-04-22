using Blazored.LocalStorage;
using FrontEnd.Services.Interfaces;
using InventoryService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Services
{
    public class AuthService : BaseService, IAuthService
    {
        public AuthService(TokenAuthenticationStateProvider tokenAuthenticationStateProvider)
            : base(tokenAuthenticationStateProvider)
        {
           
        }

        public async Task<LoginResult> Login(UserInfo t)
        {
            var json = JsonConvert.SerializeObject(t);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await PostDataAsync($"Token", content)
                .ConfigureAwait(false);

            if (!string.IsNullOrEmpty(response))
            {
                LoginResult loginResult = JsonConvert.DeserializeObject<LoginResult>(response);
                // call class without initialization
                await ((TokenAuthenticationStateProvider)_tokenAuthenticationStateProvider).SetTokenAsync(loginResult.Token, loginResult.Expiry);
                // ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);
                return loginResult;
            }
            // not found
            throw new NotImplementedException();
        }

        public async Task Logout()
        {
            await((TokenAuthenticationStateProvider)_tokenAuthenticationStateProvider).SetTokenAsync(null);
            // ApiClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
