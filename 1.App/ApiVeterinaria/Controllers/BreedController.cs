using ApiVeterinaria.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Veterinaria.Domain.services.Interface;

namespace ApiVeterinaria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(CustomExceptionHandler))]
    public class BreedController : ControllerBase
    {
        private readonly IBreedServices _breedServices;
        public BreedController(IBreedServices breedServices)
        {
            _breedServices = breedServices;
        }
    }
}
