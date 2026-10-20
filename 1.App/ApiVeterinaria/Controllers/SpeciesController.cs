using ApiVeterinaria.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Veterinaria.Common.Resources;
using Veterinaria.Domain.services.Interface;
using Veterinaria.Entity.DTO;
using Veterinaria.Entity.DTO.Species;

namespace ApiVeterinaria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(CustomExceptionHandler))]
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
            IActionResult action;
            bool result = await _speciesServices.AddSpecie(name);
            ResponseDto response = new ResponseDto
            {
                IsSuccess = result,
                Message = result ? GeneralMessages.ItemInserted : GeneralMessages.ItemNoInserted,
                Result = null
            };
            if (result)
            {
                //si el resultado es verdadero, se devuelve un código de estado 200 (OK) con la respuesta
                action = Ok(response);
            }
            else
            {
                //si el resultado es falso, se devuelve un código de estado 400 (Bad Request) con la respuesta
                action = BadRequest(response);
            }
            return action;

        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] SpeciesDto species)
        {
            //esta es la segunda forma de enviar un response, se crea un objeto ResponseDto y se devuelve con el código de estado correspondiente
            bool result = await _speciesServices.UpdateSpecie(species);
            ResponseDto response = new ResponseDto
            {
                IsSuccess = result,
                Message = result ? GeneralMessages.ItemUpdated : GeneralMessages.ItemNoUpdated,
                Result = null
            };

            return result ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete( int specieId)
        {
            bool result = await _speciesServices.DeleteSpecie(specieId);
            ResponseDto response = new ResponseDto
            {
                IsSuccess = result,
                Message = result ? GeneralMessages.ItemDeleted : GeneralMessages.ItemNoDeleted,
                Result = null
            };

            return result ? Ok(response) : BadRequest(response);
            
        }

    }
}
