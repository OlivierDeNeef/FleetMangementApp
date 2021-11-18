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
            //var connectionString =
            //    "Data Source=DESKTOP-A2ORN8D;Initial Catalog=FleetManagement;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //Console.WriteLine("Toevoegen klanten");

            const string connectionString = @"Data Source=PC-VAN-LUCA\SQLEXPRESS;Initial Catalog=FleetManagement;Integrated Security=True;";
            //const string connectionString = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=FleetManagement;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


            var bestuurderrepo = new BestuurderRepo(connectionString);
            var rijbewijzen = new List<RijbewijsType>() { new(1, "A") };

            bestuurderrepo.VoegBestuurderToe(new Bestuurder("test", "abc", new DateTime(1999, 9, 2), "99090231323", rijbewijzen, false));


            Console.WriteLine("Toevoegen klanten");
            var bestuurderRepo = new BestuurderRepo(connectionString);
            var repo = new TankkaartRepo(connectionString); // issues
            var tankkaart = repo.GeefTankkaart(2);
            var list = new List<BrandstofType>();
            list.Add(new BrandstofType(1, "Benzine"));
            list.Add(new BrandstofType(2, "Diesel"));
            tankkaart = new Tankkaart("123645", DateTime.Now, "1234", null, true, true, list);
            repo.VoegTankkaartToe(tankkaart);



        }
    }
}
