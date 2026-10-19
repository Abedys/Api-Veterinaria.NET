using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Core.Repository.Interface;
using Veterinaria.Entity.Models.Secure;
using Veterinaria.Entity.Models.Veterinaria;

namespace Veterinaria.Core.UnitWork.interfacce
{
    public interface IUnitOfWork
    {
        Task<IDbContextTransaction> BeginTransactionAsync();

        Task<int> SaveAsync();

        IRepository<UserEntity> UserRepository { get; }

        IRepository<RolEntity> RolRepository { get; }

        IRepository<PermissionEntity> PermissionRepository { get; }

        IRepository<RolesPermissionEntity> RolesPermissionRepository { get; }

        IRepository<RolUserEntity> RolesUserRepository { get; }


        IRepository<BreedEntity> BreedRepository { get; }
        IRepository<PetEntity> PetRepository { get; }
        IRepository<SpeciesEntity> SpeciesRepository { get; }



    }
}
