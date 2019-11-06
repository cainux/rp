using Microsoft.AspNetCore.Mvc;
using RP.Core.Entities;
using RP.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RP.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlbumsController : ControllerBase
    {
        private IIntegratorService integratorService;

        public AlbumsController(IIntegratorService integratorService)
        {
            this.integratorService = integratorService;
        }

        [HttpGet]
        public async Task<IEnumerable<Album>> Get([FromQuery]int? userId)
        {
            if (userId.HasValue)
            {
                return await integratorService.GetByUserIdAsync(userId.Value);
            }
            else
            {
                return await integratorService.GetAllAsync();
            }
        }
    }
}
