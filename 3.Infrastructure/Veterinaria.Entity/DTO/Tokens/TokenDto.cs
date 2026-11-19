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
        public DateTime Expiration { get; set; }

        public DateTime DataExpiration { get; set; }
    }
}
