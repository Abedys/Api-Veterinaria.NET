using ApiVeterinaria.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiVeterinaria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(CustomExceptionHandler))]
    public class BreedController : ControllerBase
    {
    }
}
