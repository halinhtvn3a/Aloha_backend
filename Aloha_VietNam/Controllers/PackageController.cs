using AlohaVietnam.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aloha_VietNam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IPackageService _packageService;

        public PackageController(IPackageService packageService)
        {
            _packageService = packageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPackage()
        {
            try
            {
                var packages = await _packageService.GetPackage();
                return Ok(packages);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
