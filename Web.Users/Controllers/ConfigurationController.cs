using Microsoft.AspNetCore.Mvc;
using Web.Users.Services;

namespace Web.Users.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationService configurationService;

        public ConfigurationController(IConfigurationService configurationService)
        {
            this.configurationService = configurationService;
        }

        [HttpGet("purposes")]
        public IActionResult GetPurposes()
        {
            var purposes = configurationService.GetPurposes();

            return Ok(purposes);
        }
    }
}