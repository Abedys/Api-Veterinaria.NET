using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria.Entity.DTO
{
    public class ResponseDto
    {

        public bool IsSuccess { get; set; }

        public string Message { get; set; } = null!; //aqui enviamos un mensaje de error o de exito

        public object? Result { get; set; } //esto es por si quiero responder un objecto

    }
}
