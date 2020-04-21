using InventoryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResult> Login(UserInfo loginModel);
        Task Logout();
        // Task<RegisterResult> Register(RegisterModel registerModel);
    }
}
