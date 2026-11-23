using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Entity.DTO.Tokens;

namespace Veterinaria.Domain.services.Interface
{
   public interface IAuthServices
    {
        Task<TokenDto> Login(LoginDto login);
    }
}
