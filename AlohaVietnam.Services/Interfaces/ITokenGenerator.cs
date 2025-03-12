using AlohaVietnam.Repositories.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlohaVietnam.Services.Interfaces
{
    public interface ITokenGenerator
    {
        Task<string> GenerateJwtToken(User user, string? name, string role);
        Task<string> GenerateRefreshToken();
    }
}
