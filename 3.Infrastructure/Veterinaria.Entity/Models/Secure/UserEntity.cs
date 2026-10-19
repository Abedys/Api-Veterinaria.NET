using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Entity.Models.Veterinaria;

namespace Veterinaria.Entity.Models.Secure
{

    //el esquema son para separar y identificar tablas y table es para indicar el nombre que tomara en la base de datos
    [Table("User", Schema = "security")]
    public class UserEntity
    {

        [Key] //key es para decirle que en base de datos va a ser clave primaria
        public int IdUser { get; set; }

        [MaxLength(50)] //aqui indicamos la cantidad de caracteres que va recibir ese campo 
        [Required] //esto indica que el campo es requerido
        public string Name { get; set; } = null!;

        [MaxLength(50)]
        public string? LastName { get; set; } //el ? indica que el campo recibe nulo

        [MaxLength(150)]
        [Required]
        public string Email { get; set; } =  null!;

        [MaxLength(1000)]
        [Required] 
        public string Password { get; set; } = null!; //esto indica que no va ser nulo el campo

        //De esta manera estamos diciendo que esta referenciada con otra tabla
        public virtual List<RolUserEntity> RolUserEntities { get; set; } = new List<RolUserEntity>(); //referencia de User - Rol

        public virtual List<PetEntity> PetEntities { get; set; } = new List<PetEntity>();





    }
}
