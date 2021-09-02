using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance;
using Persistance.Implementations;
using Persistance.Interfaces;
using RtelEncryptionLibrary;

namespace API.Extensions
{
    public static class PesistanceServices
    {
        // TODO: LEFTERI FTIAXE CACHE
        public static void AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            // TODO: MIXALI KSEREIS
            string enc = RtelConfiguration.GetApplicationConnectionString(20000);
            var x = new RtelEncryption();
            x.DecryptString(ref enc);
            ConnectionProperties connectionProperties = new()
            {
                MisterApplicationsConnectionString = enc,
                // TODO: initial Connection TimeOut
                MisterApplicationsConnectionTimeOut = 15
            };

            services.AddSingleton(connectionProperties);
            // TODO: Remove Dummy class
            services.AddTransient<IApplicationMenuRepository, ApplicationRepositoryDummy>();
            services.AddTransient<IApplicationUserRepository, ApplicationUserRepositoryDummy>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<ICostCenterRepository, CostCenterRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IDestinationRepository, DestinationRepository>();
            services.AddTransient<IFloorRepository, FloorRepository>();
            services.AddTransient<IIOTypeRepository, IOTypeRepository>();
            services.AddTransient<IOfficeRepository, OfficeRepository>();
            services.AddTransient<ISiteRepository, SiteRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
