using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Veterinaria.Core.Data.Seeder
{
    public class DataBaseSeeder
    {
        //es una clase que actua como orquestador de Seeder
        // Iconfiguration es una interfaz que permite acceder a la configuracion de la aplicacion, como por ejemplo el appsettings.json
        // CancellationToken es una clase que permite cancelar una operacion asincrona, por ejemplo si el usuario cierra la aplicacion o si hay un error
        public static async Task SeedAsync (DataContext context, IConfiguration configuration, CancellationToken ct = default )
        {
            await RolesSeeder.SeedRolesAsync(context, ct );
        }
    }
}
