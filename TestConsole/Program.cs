using System;
using System.Collections.Generic;
using DataAccessLayer.Repos;
using DomainLayer;
using DomainLayer.Models;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Toevoegen klanten");
            string connectionString = @"Data Source=PC-VAN-LUCA\SQLEXPRESS;Initial Catalog=FleetManagement;Integrated Security=True;";
            var bestuurderrepo = new BestuurderRepo(connectionString);
            List<RijbewijsType> rijbewijzen = new List<RijbewijsType>();
            if (!bestuurderrepo.BestaatBestuurder(1))
            {
                var rijbewijs = new RijbewijsType(1,"A");
                rijbewijzen.Add(rijbewijs);
                bestuurderrepo.VoegBestuurderToe(new Bestuurder("test", "abc", new DateTime(1999, 9,2 ) , "99090231323", rijbewijzen, false));
            }

            Bestuurder verwacht = new Bestuurder(1, "test", "abc", DateTime.Now, "99090223313", new List<RijbewijsType>{new RijbewijsType(1, "A")}, false);
            bool bestaat = bestuurderrepo.BestaatBestuurder(verwacht.Id);
            Console.WriteLine($"Bestuurder bestaat: {bestaat}");
        }
    }
}
