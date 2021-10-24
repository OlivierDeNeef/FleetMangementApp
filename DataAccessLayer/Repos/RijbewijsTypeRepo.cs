using System.Collections.Generic;
using System.Data.SqlClient;
using DomainLayer.Exceptions.Managers;
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
        public void VoegRijbewijsToe(RijbewijsType rijbewijsType)
        {
            RijbewijsType type = null;
            SqlCommand command = new SqlCommand();
           // command.Connection = connectionString; //  needs fix

            command.CommandText = "INSERT INTO dbo.RijbewijsType Type VALUES(@Type)";
            command.Parameters.AddWithValue("@Type", rijbewijsType.Type);
        
            //  _connection.Open();
            int rows = command.ExecuteNonQuery();
            if (rows == 1)
            {
                command.CommandText = "SELECT Id FROM dbo.RijbewijsType " + "WHERE (Type = @Type)";
                int key = (int)command.ExecuteScalar();
                type = new RijbewijsType(key, rijbewijsType.Type);
            }

            //_connection.Close();
            //return type;  ?? void type correct? of geven we het gemaakte nog eens terug?

            //SQL statement ExecuteNonQuery


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