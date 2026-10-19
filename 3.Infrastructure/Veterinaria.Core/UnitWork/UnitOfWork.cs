using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Core.Data;
using Veterinaria.Core.Repository;
using Veterinaria.Core.Repository.Interface;
using Veterinaria.Core.UnitWork.interfacce;
using Veterinaria.Entity.Models.Secure;
using Veterinaria.Entity.Models.Veterinaria;

namespace Veterinaria.Core.UnitWork
{
    //El IDisposable es un recolector de bassura lo que hace esque cuando terminemos una peticion me limpie la memeoria o mate la instancia por que ya no se esta usando
    public class UnitOfWork: IUnitOfWork, IDisposable
    {
        //aqui estamos inyectando el datacontext
        private readonly DataContext _context;

        private bool disposed = false;

        //el constructor
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }


        #region Properties

        private IRepository<UserEntity> userRepository;
        private IRepository<RolEntity> rolRepository;
        private IRepository<PermissionEntity> permissionRepository;
        private IRepository<RolesPermissionEntity> rolesPermissionRepository;
        private IRepository<RolUserEntity> rolesUserRepository;
        private IRepository<SpeciesEntity> speciesRepository;
        private IRepository<PetEntity> petRepository;
        private IRepository<BreedEntity> breedRepository;
        #endregion

        #region Members

        //userRepository
        public IRepository<UserEntity> UserRepository
        {
            get
            {
                //aqui le estamos diciendo que si la instancia de userRepository no existe que me cree una nueva
                if (userRepository == null)
                    userRepository = new Repository<UserEntity>(_context);

                return userRepository;
            }
        }

        //rolRepository
        public IRepository<RolEntity> RolRepository
        {
            get
            {
                //aqui le estamos diciendo que si la instancia de userRepository no existe que me cree una nueva
                if (rolRepository == null)
                    rolRepository = new Repository<RolEntity>(_context);

                return rolRepository;
            }
        }

        //permissionRepository
        public IRepository<PermissionEntity> PermissionRepository
        {
            get
            {
                //aqui le estamos diciendo que si la instancia de userRepository no existe que me cree una nueva
                if (permissionRepository == null)
                    permissionRepository = new Repository<PermissionEntity>(_context);

                return permissionRepository;
            }
        }

        //rolesPermissionRepository
        public IRepository<RolesPermissionEntity> RolesPermissionRepository
        {
            get
            {
                //aqui le estamos diciendo que si la instancia de userRepository no existe que me cree una nueva
                if (rolesPermissionRepository == null)
                    rolesPermissionRepository = new Repository<RolesPermissionEntity>(_context);

                return rolesPermissionRepository;
            }
        }

        //rolesUserRepository
        public IRepository<RolUserEntity> RolesUserRepository
        {
            get
            {
                //aqui le estamos diciendo que si la instancia de userRepository no existe que me cree una nueva
                if (rolesUserRepository == null)
                    rolesUserRepository = new Repository<RolUserEntity>(_context);

                return rolesUserRepository;
            }
        }

        //rolesUserRepository
        public IRepository<BreedEntity> BreedRepository
        {
            get
            {
                //aqui le estamos diciendo que si la instancia de userRepository no existe que me cree una nueva
                if (breedRepository == null)
                    breedRepository = new Repository<BreedEntity>(_context);

                return breedRepository;
            }
        }

        //rolesUserRepository
        public IRepository<PetEntity> PetRepository
        {
            get
            {
                //aqui le estamos diciendo que si la instancia de userRepository no existe que me cree una nueva
                if (petRepository == null)
                    petRepository = new Repository<PetEntity>(_context);

                return petRepository;
            }
        }

        //rolesUserRepository
        public IRepository<SpeciesEntity> SpeciesRepository
        {
            get
            {
                //aqui le estamos diciendo que si la instancia de userRepository no existe que me cree una nueva
                if (speciesRepository == null)
                    speciesRepository = new Repository<SpeciesEntity>(_context);

                return speciesRepository;
            }
        }






        #endregion






        //Esto para inciar una transacion
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync(); 
        }

        //esto  es para el recolector de basura
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

    }
}
