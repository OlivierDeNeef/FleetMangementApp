using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DomainLayer.Interfaces;
using DomainLayer.Interfaces.Repos;
using Microsoft.Extensions.DependencyInjection;

namespace FleetMangementApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;

        public App()
        {
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
            services.AddSingleton<IRijbewijsTypeRepo>();
            services.AddSingleton<IWagenTypeRepo>();



        }
    }
}
