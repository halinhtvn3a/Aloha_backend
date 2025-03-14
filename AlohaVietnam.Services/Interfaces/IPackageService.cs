using AlohaVietnam.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlohaVietnam.Services.Interfaces
{
    public interface IPackageService
    {
        Task<IEnumerable<Package>> GetPackage();
    }
}
