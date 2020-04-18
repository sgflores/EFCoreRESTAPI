using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontEnd.Services.Interfaces
{
    public interface IBaseService
    {
        public Task<string> GetDataAsync(string address, Dictionary<string, string> parameters);
        public Task<string> PostDataAsync(string address, StringContent content);
        public Task<string> PutDataAsync(string address, StringContent content);
        public Task<string> DeleteDataAsync(string address, Dictionary<string, string> parameters);


    }
}
