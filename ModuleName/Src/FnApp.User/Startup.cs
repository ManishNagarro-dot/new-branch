using AutoMapper;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GOI.Seeker.Master.Shared.DTOs;
using GOI.Seeker.Master.Shared.DatabaseContext;
using GOI.Seeker.Master.Shared.Repositories;
using GOI.Seeker.Master.Shared.Services;
using System;
using GOI.Services.Common.UnitOfWork;

[assembly: FunctionsStartup(typeof(GOI.Seeker.Master.FnApp.User.Startup))]
namespace GOI.Seeker.Master.FnApp.User
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup : FunctionsStartup
    {
        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //Adding automapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GOI.Seeker.Master.Entities.User, UserDTO>().ReverseMap();
            });
            IMapper iMapper = config.CreateMapper();

            builder.Services.AddSingleton<IMapper>(iMapper);

            //Get connection string for db from azure functions config when deployed
            string connectionString = Environment.GetEnvironmentVariable("AajevikaSetuDBConn");

            builder.Services.AddDbContext<IDatabaseUnitOfWork, MasterDbContext>(options =>
                options.UseNpgsql(connectionString), ServiceLifetime.Scoped);

            //Adding Services
            RegisterServices(builder.Services);
        }

        /// <summary>
        /// Used for registering the custom user defined services
        /// </summary>
        /// <param name="services">IServiceCollection is used to inject any custom service</param>
        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
