using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Entity.DTO.Breed;
using Veterinaria.Entity.Models.Veterinaria;

namespace Veterinaria.Domain.services.Interface
{
    public interface IBreedServices
    {

        Task<List<BreedDto>> GetAllBySpeciesId(int speciesId);

        Task<bool> AddBreed(BreedAddDto breed);

        Task<bool> DeleteBreed(int breedId);

        Task<bool> UpdateBreed(BreedUpdateDto breed);

    }
}
