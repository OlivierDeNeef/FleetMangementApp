using System;
using DataAccessLayer.Repos;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionstring= "Data Source=DESKTOP-A2ORN8D;Initial Catalog=FleetManagement;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
            Console.WriteLine("Toevoegen klanten");
            var bestuurderRepo = new BestuurderRepo(connectionstring);
            var repo = new TankkaartRepo(connectionstring,bestuurderRepo);
            var tankkaart = repo.GeefTankkaart(2);


        }
    }
}
