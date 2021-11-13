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

            const string connectionString = @"Data Source=PC-VAN-LUCA\SQLEXPRESS;Initial Catalog=FleetManagement;Integrated Security=True;";
          //const string connectionString = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=FleetManagement;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            
            
            var bestuurderrepo = new BestuurderRepo(connectionString);
            var rijbewijzen = new List<RijbewijsType>(){new(1,"A")};
            
            bestuurderrepo.VoegBestuurderToe(new Bestuurder("test", "abc", new DateTime(1999, 9,2 ) , "99090231323", rijbewijzen, false));

            


        }
    }
}
