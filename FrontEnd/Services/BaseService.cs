using FrontEnd.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Services
{
    public class BaseService : IBaseService
    {
        public HttpClient ApiClient { get; set; }
        public string ErrorMessage { get; set; }

        public BaseService()
        {
            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri("http://localhost:5001/api/");
            // ApiClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public async Task<string> GetDataAsync(string address, Dictionary<string, string> parameters)
        {
            ErrorMessage = string.Empty;
            string result = null;

            address = GeneratePathAndQueryString(address, parameters);

            try
            {
                using HttpResponseMessage response = await ApiClient.GetAsync(address);

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();

                }

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return result;
        }

        public async Task<string> PostDataAsync(string address, StringContent content)
        {
            ErrorMessage = string.Empty;
            string result = null;

            try
            {
                using HttpResponseMessage response = await ApiClient.PostAsync(address, content);

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();

                }

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return result;
        }


        public async Task<string> PutDataAsync(string address, StringContent content)
        {
            ErrorMessage = string.Empty;
            string result = null;

            try
            {
                using HttpResponseMessage response = await ApiClient.PutAsync(address, content);

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();

                }

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return result;
        }


        public async Task<string> DeleteDataAsync(string address, Dictionary<string, string> parameters)
        {
            ErrorMessage = string.Empty;
            string result = null;

            address = GeneratePathAndQueryString(address, parameters);

            try
            {
                using HttpResponseMessage response = await ApiClient.DeleteAsync(address);

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();

                }

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return result;
        }

        // validate correct url query string. should it start with ? or &
        // returns http://localhost?key1=123 or http://localhost?key1=123&key2=132
        public string GeneratePathAndQueryString(string path, Dictionary<string, string> queryParams, bool isSessionTokenRequired = false)
        {
            queryParams = queryParams ?? new Dictionary<string, string>();

            if (queryParams.Count() == 0)
            {
                return path;
            }

            if (path.IndexOf("?") == -1)
            {
                // url address is without ?
                string firstKey = queryParams.FirstOrDefault().Key;
                string pathAndQuery = $"{path}?{firstKey}={queryParams.FirstOrDefault().Value}";
                queryParams.Remove(firstKey);
                return $"{pathAndQuery}{GetQueryString(queryParams ?? new Dictionary<string, string>())}";
            }
            else
            {
                // url address does not start with ?
                return $"{path}{GetQueryString(queryParams ?? new Dictionary<string, string>())}";
            }
        }


        // format dictionary as query string appended to url
        // returns &key1=123&key2=123
        public string GetQueryString(Dictionary<string, string> parameters)
        {
            StringBuilder paramString = new StringBuilder();
            foreach (var parameter in parameters)
            {
                string keyValue = string.Format("&{0}={1}", parameter.Key, parameter.Value);
                paramString.Append(keyValue);
            }
            return paramString.ToString();
        }

    }
}
