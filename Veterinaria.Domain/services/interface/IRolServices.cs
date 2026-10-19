using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Entity.Models.Secure;

namespace Veterinaria.Domain.services.Interface
{
    public interface IRolServices
    {
        Task<List<RolEntity>> GetAll();
    }
}
