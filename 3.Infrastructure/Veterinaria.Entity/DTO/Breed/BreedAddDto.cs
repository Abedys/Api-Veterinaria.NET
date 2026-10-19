using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria.Entity.DTO.Breed
{
    public class BreedAddDto
    {
        public string Name { get; set; } = null!;

        public int SpeciesId { get; set; }
    }
}
