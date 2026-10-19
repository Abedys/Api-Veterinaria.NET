using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria.Common.Exceptions
{
    //eL HttpResponseException es una clase que hereda de la clase base Exception y se utiliza para representar excepciones
    //relacionadas con respuestas HTTP. Esta clase tiene dos propiedades: status, que representa el código de estado HTTP de la respuesta,
    //y value, que representa el valor de la respuesta HTTP. La propiedad value se inicializa con null! para indicar que no puede ser nula.
    public class HttpResponseException: Exception
    {

        public int Status { get; set; }

        public object Value { get; set; } = null!;
    }
}
