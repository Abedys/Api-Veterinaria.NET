using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria.Entity.Models.Secure
{
    //el esquema son para separar y identificar tablas y table es para indicar el nombre que tomara en la base de datos
    [Table("RolUser", Schema = "security")]
    public class RolUserEntity
    {
        [Key] //key es para decirle que en base de datos va a ser clave primaria
        public int Id { get; set; }

        [Required]
        [ForeignKey("RolEntity")]
        public int IdRol { get; set; }
        public RolEntity RolEntity { get; set; } = null!; //aqui estoy diciendo que esta propiedad es de


        [Required]
        [ForeignKey("UserEntity")]
        public int IdUser { get; set; }
        public UserEntity UserEntity { get; set; } = null!;

    }
}
