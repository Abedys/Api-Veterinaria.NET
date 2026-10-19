
using ApiVeterinaria.Handlers;
using Microsoft.EntityFrameworkCore;
using Veterinaria.Core.Data;
using Veterinaria.Core.Repository;
using Veterinaria.Core.Repository.Interface;
using Veterinaria.Core.UnitWork;
using Veterinaria.Core.UnitWork.interfacce;
using Veterinaria.Domain.services;
using Veterinaria.Domain.services.Interface;

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


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
