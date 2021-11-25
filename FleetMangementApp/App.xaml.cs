using System.IO;
using System.Windows;
using DataAccessLayer.Repos;
using DomainLayer.Interfaces.Repos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace FleetMangementApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public App()
        {
            
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _configuration = builder.Build();
        }


        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<IBestuurderRepo,BestuurderRepo>();
            services.AddSingleton<IVoertuigRepo,VoertuigRepo>();
            services.AddSingleton<ITankkaartRepo,TankkaartRepo>();
            services.AddSingleton<IBrandstofTypeRepo,BrandstofTypeRepo>();
            services.AddSingleton<IRijbewijsTypeRepo, RijbewijsTypeRepo>();
            services.AddSingleton<IWagenTypeRepo,WagenTypeRepo>();
            services.AddTransient<MainWindow>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
