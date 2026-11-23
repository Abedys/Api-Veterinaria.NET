
using ApiVeterinaria.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Veterinaria.Core.Data;
using Veterinaria.Core.Repository;
using Veterinaria.Core.Repository.Interface;
using Veterinaria.Core.UnitWork;
using Veterinaria.Core.UnitWork.interfacce;
using Veterinaria.Domain.services;
using Veterinaria.Domain.services.Interface;
using Veterinaria.Entity.DTO;

namespace ApiVeterinaria
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region SQLSERVER
            //Aqui Instanciamos la conexion a la base de Datos
            builder.Services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStringSQLserver"));

            });
            #endregion


            //Esta region es para inyectar las dependencias de los servicios, repositorios y unit of work
            #region Inyection
            DependecyInyectionHandler.DependecyInyectionConfig(builder.Services);

            #endregion


            #region JWT
            // Tokens:Key, Issuer, Audience y ExpireMinutes. En Docker los pisan Tokens__Key, Tokens__Issuer,
            // Tokens__Audience y Tokens__ExpireMinutes, con el mismo valor en todas las réplicas.
            var tokenAppSetting = builder.Configuration.GetSection("Tokens");
            JwtConfigurationHandler.ConfigureJwtAuthentication(builder.Services, tokenAppSetting);
            #endregion


            // Add services to the container.



            builder.Services.AddControllers(options =>
            {
                // Aplica a todos los controladores. Ya no hace falta [TypeFilter(typeof(CustomExceptionHandler))].
                options.Filters.Add<CustomExceptionHandler>();
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                // Los [Required] y [MaxLength] fallan antes de entrar a la acción.
                // Se responden con ResponseDto en lugar del ProblemDetails por defecto.
                options.InvalidModelStateResponseFactory = context =>
                {
                    Dictionary<string, string[]> errors = context.ModelState
                        .Where(entry => entry.Value?.Errors.Count > 0)
                        .ToDictionary(
                            entry => entry.Key,
                            entry => entry.Value!.Errors.Select(error => error.ErrorMessage).ToArray());

                    return new BadRequestObjectResult(new ResponseDto
                    {
                        IsSuccess = false,
                        Message = string.Join(" ", errors.SelectMany(error => error.Value)),
                        Result = errors
                    });
                };
            });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Introduce tu token JWT."
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                            });

           

            var app = builder.Build();



            // Solo el servicio migrator trae estas variables. Aplica las migraciones y sale sin abrir el puerto.
            // Las réplicas no las tienen, así que no compiten por migrar.
            if (app.Configuration.GetValue<bool>("APPLY_MIGRATIONS"))
            {
                using var scope = app.Services.CreateScope();
                DataContext db = scope.ServiceProvider.GetRequiredService<DataContext>();
                db.Database.Migrate();
                if (app.Configuration.GetValue<bool>("MIGRATE_ONLY"))
                    return;
            }


            // En el contenedor ASPNETCORE_URLS es http://+:8080. Sin "https" en la URL no hay redirección,
            // porque dentro de Docker solo hay HTTP y el redirect rompería las llamadas del proxy.
            string urls = app.Configuration["ASPNETCORE_URLS"] ?? "";
            if (app.Environment.IsDevelopment() && urls.Contains("https", StringComparison.OrdinalIgnoreCase))
                app.UseHttpsRedirection();

            app.UseForwardedHeaders();

            // INSTANCE_PREFIX viene del compose. Junto con el número del hostname identifica la réplica.
            string instanceName = InstanceName(app.Configuration["INSTANCE_PREFIX"]);

            // Header en todas las respuestas, para ver qué réplica atendió sin llamar a /instance.
            app.Use(async (context, next) =>
            {
                context.Response.Headers["X-Instance"] = instanceName;
                await next();
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.UseAuthentication();
            app.UseAuthorization();
            

          

            app.MapControllers();

            app.Run();



            // Docker nombra el hostname appveterinaria-1, appveterinaria-2, ...
            // Con INSTANCE_PREFIX=AppVeterinaria la respuesta queda AppVeterinaria-1.
            // Sin ese prefijo (Visual Studio) se devuelve el nombre del PC.
            static string InstanceName(string? prefix)
            {
                string host = Environment.MachineName;
                if (string.IsNullOrWhiteSpace(prefix))
                    return host;

                int dash = host.LastIndexOf('-');
                if (dash > 0 && int.TryParse(host.AsSpan(dash + 1), out _))
                    return prefix + host[dash..];

                return host;
            }

        }
    }
}
