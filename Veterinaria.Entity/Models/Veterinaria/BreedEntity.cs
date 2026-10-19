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
    [Table("Breed", Schema = "Vet")]
    public class BreedEntity
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;


        [ForeignKey("SpeciesEntity")]
        public int SpeciesId { get; set; } 

        public SpeciesEntity SpeciesEntity { get; set; } = null!;



    }
}
