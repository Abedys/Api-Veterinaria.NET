using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Veterinaria.Entity.Models.Secure;

namespace Veterinaria.Core.Data.Seeder
{
    public class RolesSeeder
    {

        //metodo para agregar roles predeterminados a la base de datos
        //el context es el contexto de la base de datos y ct es un token de cancelacion
        // CancellationToken funciona para cancelar la operacion si es necesario, por ejemplo si el usuario cierra la aplicacion o si hay un error
        public static async Task SeedRolesAsync (DataContext context, CancellationToken ct )
        {
            //lista de roles predeterminados
            string[] defaultRoles =  { "Admin", "Usuario" };

            //obtenemos los roles que ya existen en la base de datos
            var existentes = await context.RolEntities
                .Select(r => r.Rol)
                .ToListAsync(ct); //listamos los roles existentes en la base de datos y los guardamos en una lista

            //obtenemos los roles que no existen en la base de datos
            var nuevos = defaultRoles
                .Except(existentes)
                .Select(nombre => new RolEntity { Rol = nombre })
                .ToList();

            //si no hay roles nuevos, no hacemos nada
            if (nuevos.Count == 0) return;

            //agregamos los roles nuevos a la base de datos
            context.RolEntities.AddRange(nuevos);
            //guardamos los cambios en la base de datos
            await context.SaveChangesAsync(ct);

           
        }
    }
}
