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

            var repo = new TankkaartRepo(
                    "Data Source=DESKTOP-A2ORN8D;Initial Catalog=FleetManagement;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            var tankkaart = repo.GeefTankkaart(2);


        }
    }
}
