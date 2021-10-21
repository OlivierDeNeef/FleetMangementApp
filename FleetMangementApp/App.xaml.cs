using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DataAccessLayer.Repos;
using DomainLayer.Interfaces;
using DomainLayer.Interfaces.Repos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;


namespace FleetMangementApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;
        private readonly IConfiguration configuration;

        public App()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional : false, reloadOnChange : true);
            configuration = builder.Build();
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }


        private void ConfigureServices(ServiceCollection services)
        {
            
            services.AddSingleton<IBestuurderRepo>();
            services.AddSingleton<IVoertuigRepo>();
            services.AddSingleton<ITankkaartRepo>();
            services.AddSingleton<IBrandstofTypeRepo>();
            services.AddSingleton<IRijbewijsTypeRepo, RijbewijsTypeRepo>();
            services.AddSingleton<IWagenTypeRepo>();



        }
    }
}
