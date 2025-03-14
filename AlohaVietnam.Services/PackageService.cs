using AlohaVietnam.Repositories.Interfaces;
using AlohaVietnam.Repositories.Models;
using AlohaVietnam.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlohaVietnam.Services
{
    public class PackageService : IPackageService
    {
        private readonly IRepository<Package, int> _packageRepository;
        public PackageService(IRepository<Package, int> packageRepository) {
            _packageRepository = packageRepository;
        }

        public async Task<IEnumerable<Package>> GetPackage()
        {
            try
            {
                return await _packageRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the news: " + ex.Message);
            }
        }
    }
}
