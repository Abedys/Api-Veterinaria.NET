using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Entity.Models.Secure;

namespace Veterinaria.Entity.Models.Veterinaria
{
    //el esquema son para separar y identificar tablas y table es para indicar el nombre que tomara en la base de datos
    [Table("Pet", Schema = "Vet")]
    public class PetEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;


        [ForeignKey("SpeciesEntity")]
        public int SpeciesId { get; set; }
        public SpeciesEntity SpeciesEntity { get; set; } = null!;


        [ForeignKey("UserEntity")]
        public int UserId { get; set; }
        public UserEntity UserEntity { get; set; } = null!;



        [Required]
        public DateTime BirthDate { get; set; } 


        public decimal? Weight { get; set; }

        public string? PhotoPath { get; set; }

    }
}
