using System.Collections.Generic;
using System.Data.SqlClient;
using DomainLayer.Interfaces.Repos;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Repos
{
    public class RijbewijsTypeRepo : IRijbewijsTypeRepo
    {
        private string connectionString;
        private readonly IConfiguration _configuration;

        public RijbewijsTypeRepo(IConfiguration config)
        {
            _configuration = config;
            connectionString = config.GetConnectionString("defaultConnection");
        }

        private SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
        public void VoegRijbewijsToe(RijbewijsType rijbewijsType)
        {
            throw new System.NotImplementedException();
        }

        public void VerwijderRijbewijsType(RijbewijsType rijbewijsType)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<RijbewijsType> GeefAlleRijbewijsTypes()
        {
            throw new System.NotImplementedException();
        }

        public bool BestaatRijbewijsType(RijbewijsType rijbewijsType)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateRijbewijsType(RijbewijsType rijbewijsType)
        {
            throw new System.NotImplementedException();
        }
    }
}