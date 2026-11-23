using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria.Entity.DTO.Tokens
{
    public class TokenDto
    {
        public string Token { get; set; } = null!;
        public double Expiration { get; set; }

        public DateTime DateExpiration { get; set; }
    }
}
