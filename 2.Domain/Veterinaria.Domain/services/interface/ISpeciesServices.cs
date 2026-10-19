using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Entity.DTO.Species;
using Veterinaria.Entity.Models.Veterinaria;

namespace Veterinaria.Domain.services.Interface
{
    public interface ISpeciesServices
    {
        Task<List<SpeciesDto>> GetAllSpeciesAsync();


        Task<bool> AddSpecie(string name);

        Task<bool> UpdateSpecie(SpeciesDto species);

        Task<bool> DeleteSpecie(int specieId);

    }
}
