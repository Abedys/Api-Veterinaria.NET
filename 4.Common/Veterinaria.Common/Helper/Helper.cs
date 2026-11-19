using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Veterinaria.Common.Helper
{

    //Un Helper es una clase que contiene métodos estáticos que proporcionan
    //funcionalidades auxiliares o de utilidad para otras partes del código.
    //Estos métodos suelen ser independientes del estado de la clase y se pueden
    //llamar directamente sin necesidad de crear una instancia de la clase Helper.
    public static class Helper
    {

        //un Regex (expresión regular) es una secuencia de caracteres que define un
        //patrón de búsqueda.
        private static readonly Regex EmailRegex = new(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?)+$",
                                                       RegexOptions.Compiled | RegexOptions.CultureInvariant);

        //El método IsEmail toma una cadena de texto (email) como parámetro y verifica si
        //cumple con el patrón de un correo electrónico válido utilizando la expresión
        //regular definida en EmailRegex.
        public static bool IsEmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email) && EmailRegex.IsMatch(email.Trim());
        }
    }

}
