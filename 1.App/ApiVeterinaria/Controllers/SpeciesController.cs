using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Veterinaria.Domain.services.Interface;
using Veterinaria.Entity.DTO.Species;

namespace ApiVeterinaria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeciesController : ControllerBase
    {

        private readonly ISpeciesServices _speciesServices;
        public SpeciesController(ISpeciesServices speciesServices)
        {
            _speciesServices = speciesServices;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            List<SpeciesDto> result = await _speciesServices.GetAllSpeciesAsync();
            return Ok(result);
        }


        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] string name)
        {
            bool result = await _speciesServices.AddSpecie(name);
            return Ok(result);

        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] SpeciesDto species)
        {
            bool result = await _speciesServices.UpdateSpecie(species);
            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromRoute] int specieId)
        {
            bool result = await _speciesServices.DeleteSpecie(specieId);
            return Ok(result);
        }

    }
}
