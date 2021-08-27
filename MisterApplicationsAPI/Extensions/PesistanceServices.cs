using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance;
using Persistance.ConnectionProperties;
using Persistance.Implementations;
using Persistance.Interfaces;
using RtelEncryptionLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            MSSQLConnectionProperties mSSQLConnectionProperties = new MSSQLConnectionProperties
            {
                ConnectionString = enc
            };

            services.AddSingleton(mSSQLConnectionProperties);
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
