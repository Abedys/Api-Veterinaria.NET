using ApiVeterinaria.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Veterinaria.Domain.services.Interface;

namespace ApiVeterinaria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolController : ControllerBase
    {
        private readonly IRolServices _rolServices;

        public RolController (IRolServices rolServices)
        {
            _rolServices = rolServices;
        }

        [HttpGet(Name ="GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _rolServices.GetAll();
            return Ok(result);
        }

    }
}
