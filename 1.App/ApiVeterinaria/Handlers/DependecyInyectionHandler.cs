


using Veterinaria.Core.Repository;
using Veterinaria.Core.Repository.Interface;
using Veterinaria.Core.UnitWork;
using Veterinaria.Core.UnitWork.interfacce;
using Veterinaria.Domain.services;
using Veterinaria.Domain.services.Interface;

namespace ApiVeterinaria.Handlers
{
    public class DependecyInyectionHandler
    {
        //eL metodo DependecyInyectionConfig se encarga de registrar los servicios y repositorios en el contenedor de inyección de dependencias.
        //IServicesCollection es una interfaz que representa una colección de servicios desde el program que se pueden inyectar en la aplicación.
        public static void DependecyInyectionConfig(IServiceCollection services)
        {

            
            //infraestrucuture
            services.AddScoped<IUnitOfWork, UnitOfWork>(); //inyeccion de la unidad de trabajo
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); //inyeccion del patron Repository

            //Domain
            services.AddScoped<IRolServices, RolServices>();
            services.AddScoped<ISpeciesServices, SpeciesServices>();
        }


    }
}
