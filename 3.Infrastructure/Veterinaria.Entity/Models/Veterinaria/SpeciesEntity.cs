using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria.Entity.Models.Veterinaria
{
    //el esquema son para separar y identificar tablas y table es para indicar el nombre que tomara en la base de datos
    [Table("Species", Schema = "Vet")]
    public class SpeciesEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;


        public List<BreedEntity> BreedEntities { get; set; } = new List<BreedEntity>();

        public List<PetEntity> PetEntities { get; set; } = new List<PetEntity>();
    }
}
