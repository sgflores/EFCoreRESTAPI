using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace FrontEnd.Services
{
    // https://www.mikesdotnetting.com/article/342/managing-authentication-token-expiry-in-webassembly-based-blazor
    // https://github.com/chrissainty/AuthenticationWithClientSideBlazor/blob/master/src/AuthenticationWithClientSideBlazor.Client/Services
    public class TokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        ILocalStorageService _localStorage;
        HttpClient _httpClient;

        public TokenAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorage = localStorageService;
        }


        // if one is provided.It also stores the token's expiry time. 
        // If no token is provided, the method removes both the storage keys related to the token and its expiry time, 
        // effectively logging the user out
        public async Task SetTokenAsync(string token, DateTime expiry = default)
        {
            if (token == null)
            {
                await _localStorage.RemoveItemAsync("authToken");
                await _localStorage.RemoveItemAsync("authTokenExpiry");
            }
            else
            {
                await _localStorage.SetItemAsync("authToken", token);
                await _localStorage.SetItemAsync("authTokenExpiry", expiry);
            }

            // raises the AuthenticationStateChanged event that the CascadingAuthenticationState component subscribes to, 
            // updating the CascadingAuthenticationState component about the current authentication status of the user.
            
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        // checks the expiry time of the token. If the expiry time has expired, the SetToken method is called without a token being provided, logging the user out. 
        // Otherwise a valid token is returned, if one exists.
        public async Task<string> GetTokenAsync()
        {
            // var expiry = await _jsRuntime.InvokeAsync<object>("localStorage.getItem", "authTokenExpiry");
            var expiry = await _localStorage.GetItemAsync<string>("authExpiry");
            if (expiry != null)
            {
                if (DateTime.Parse(expiry.ToString()) > DateTime.Now)
                {
                    return await _localStorage.GetItemAsync<string>("authToken");
                }
                else
                {
                    await SetTokenAsync(null);
                }
            }
            return null;
        }

        // This method parses the JSON Web Token and creates a ClaimsPrincipal(representing the current user) with either the identity information(ClaimsIdentity) 
        // obtained from the token, or an empty ClaimsIdentity if no token exists.
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await GetTokenAsync();
            var identity = string.IsNullOrEmpty(token)
                ? new ClaimsIdentity()
                : new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        /*
          The method for parsing the JWT is taken from the Mission Control demo. 
          JWTs contain three parts: a header, a payload (the source of the ClaimsIdentity information) 
          and a signature. Each part is Base64 Url encoded and then the parts are joined using dots. 
          The final output e.g. header.payload.signature forms the token. 
          When using Base64 Url encoding, output padding is optional, 
          and in fact is not included in the generation of JWTs. 
          The System.Convert.FromBase64String method expects the input string to have output padding 
          where necessary, and will raise a FormatException if it is missing. 
          Therefore the additional private method at the end of the class is used to put 
          padding characters (=) on to the end of the payload if they are needed 
          before the string is decoded.
         */

        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
