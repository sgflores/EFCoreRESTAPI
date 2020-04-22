using FrontEnd.Services.Interfaces;
using FrontEnd.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Services
{
    public class UserInfoService : BaseService, IUserInfoService
    {
        public UserInfoService(TokenAuthenticationStateProvider tokenAuthenticationStateProvider) 
            : base(tokenAuthenticationStateProvider)
        {

        }

        public Task<bool> Add(UserInfoViewModel t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserInfoViewModel>> GetListAsync()
        {
            Dictionary<string, string> param = new Dictionary<string, string>();

            var response = await GetDataAsync("UserInfoes", param)
                .ConfigureAwait(false);

            List<UserInfoViewModel> users = new List<UserInfoViewModel>();


            if (!string.IsNullOrEmpty(response))
            {
                users = JsonConvert.DeserializeObject<List<UserInfoViewModel>>(response);
            }

            return users;
        }

        public Task<List<UserInfoViewModel>> GetListAsync(Dictionary<string, string> parameters)
        {
            throw new NotImplementedException();
        }

        public Task<UserInfoViewModel> GetSingleAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public UserInfoViewModel SanitizeViewModel(UserInfoViewModel t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(int Id, UserInfoViewModel t)
        {
            throw new NotImplementedException();
        }
    }
}
