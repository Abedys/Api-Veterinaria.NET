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
    [Table("Rol", Schema = "security")]
    public class RolEntity
    {
        [Key] //key es para decirle que en base de datos va a ser clave primaria
        public int IdRol { get; set; }

        [MaxLength(50)] //aqui indicamos la cantidad de caracteres que va recibir ese campo 
        [Required] //esto indica que el campo es requerido
        public string Rol { get; set; } = null!;

        public virtual List<RolUserEntity> RolUserEntities { get; set; } = new List<RolUserEntity>(); //referencia de Rol - Usuario

        public virtual List<RolesPermissionEntity> RolesPermissionEntities { get; set; } = new List<RolesPermissionEntity>(); //referencia de Rol - permiso


    }
}
