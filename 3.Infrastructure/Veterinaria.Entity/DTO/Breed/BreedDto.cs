using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria.Entity.DTO.Breed
{
    public class BreedDto: BreedUpdateDto
    {

        public string Species { get; set; } = null!;    
    }
}
