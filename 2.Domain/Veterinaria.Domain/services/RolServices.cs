using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Core.UnitWork.interfacce;
using Veterinaria.Domain.services.Interface;
using Veterinaria.Entity.Models.Secure;

namespace Veterinaria.Domain.services
{
    public class RolServices: IRolServices
    {
        //inyetamos la uidad de trabajo
        private readonly IUnitOfWork _unitOfWork;
        public RolServices(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        
        }


        public async Task<List<RolEntity>> GetAll()
        {
           var rols = await _unitOfWork.RolRepository.GetAllAsync();
             
            return rols;

        }



    }
}
