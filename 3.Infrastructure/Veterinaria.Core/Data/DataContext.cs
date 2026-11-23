using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Entity.Models.Secure;
using Veterinaria.Entity.Models.Veterinaria;

namespace Veterinaria.Core.Data
{
    public class DataContext:DbContext
    {
        //Aqui le estamos diciendo que esta clase va ser la unica que me va manejar mi base de datos
        public DataContext(DbContextOptions<DataContext> dbContextOptions): base(dbContextOptions)
        {

        
        }


        #region Table Secure
        public DbSet<UserEntity> UserEntities { get; set; }

        public DbSet<RolUserEntity> RolUserEntities { get; set; }

        public DbSet<RolesPermissionEntity> RolesPermissionEntities { get; set; }

        public DbSet<RolEntity> RolEntities { get; set; }

        public DbSet<PermissionEntity> PermissionEntities { get; set; }
        #endregion

        #region Table Vet
        public DbSet<BreedEntity> BreedEntities { get; set; }

        public DbSet<PetEntity> PetEntities { get; set; }

        public DbSet<SpeciesEntity> SpeciesEntities { get; set; }




        #endregion

        #region Seed

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<RolUserEntity>()
            .HasIndex(ru => new { ru.IdUser, ru.IdRol })
            .IsUnique();

        }

        #endregion



        #region HasData

        //FORMA DE HACERLO CON HASDATA
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // ROLES PREDETERMINADOS
        //    modelBuilder.Entity<RolEntity>().HasData(
        //        new RolEntity
        //        {
        //            IdRol = 1,
        //            Rol = "Admin"
        //        },
        //        new RolEntity
        //        {
        //            IdRol = 2,
        //            Rol = "Usuario"
        //        }
        //    );

        //    // Evitar que se repita la misma relación usuario-rol
        //    modelBuilder.Entity<RolUserEntity>()
        //        .HasIndex(ru => new { ru.IdUser, ru.IdRol })
        //        .IsUnique();
        //}


        //FORMA DE HACERLO CON EL METODO SEED
        #endregion



    }
}
