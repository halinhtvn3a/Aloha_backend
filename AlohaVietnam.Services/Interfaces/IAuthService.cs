using AlohaVietnam.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlohaVietnam.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse> Register(RegisterModel registerModel);
        Task<ApiResponse> Login(LoginModel loginModel);
    }
}
