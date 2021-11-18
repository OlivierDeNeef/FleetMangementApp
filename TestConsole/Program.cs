using System;
using System.Collections.Generic;
using DataAccessLayer.Repos;
using DomainLayer.Models;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionstring =
                "Data Source=DESKTOP-A2ORN8D;Initial Catalog=FleetManagement;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            Console.WriteLine("Toevoegen klanten");
            var bestuurderRepo = new BestuurderRepo(connectionstring);
            var repo = new TankkaartRepo(connectionstring, bestuurderRepo);
            //var tankkaart = repo.GeefTankkaart(2);
            //var list = new List<BrandstofType>();
            //list.Add(new BrandstofType(1,"Benzine"));
            //list.Add(new BrandstofType(2,"Diesel"));
            //var tankkaart = new Tankkaart("123645", DateTime.Now, "1234", null, true, true, list);
            //repo.VoegTankkaartToe(tankkaart);


        }
    }
}
