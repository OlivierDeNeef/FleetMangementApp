using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DataAccessLayer.Exceptions.Repos;
using DomainLayer.Interfaces.Repos;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Repos
{
    public class WagenTypeRepo : IWagenTypeRepo
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public WagenTypeRepo(IConfiguration config)
        {
            _configuration = config;
            _connectionString = config.GetConnectionString("defaultConnection");
        }

        public void VoegWagenTypeToe(WagenType wagenType)
        {
            var connection = new SqlConnection(_connectionString);
            const string query = "INSERT INTO dbo.WagenTypes (Type) VALUES (@Type);";
            try
            {
                using var command = connection.CreateCommand();
                command.CommandText = query;
                command.Parameters.AddWithValue("@Type", wagenType.Type);
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {

                throw new WagenTypeRepoException(
                    "VoegWagenTypeToe - Er ging iets fout tijdens het opladen van het wagentype", exception);
            }
            finally
            {
                connection.Close();
            }
           
        }

        public void VerwijderWagenType(WagenType brandstofType)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateWagenType(WagenType brandstofType)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<WagenType> GeefAlleWagenTypes()
        {
            throw new System.NotImplementedException();
        }

        public bool BestaatWagenType(WagenType brandstofType)
        {
            throw new System.NotImplementedException();
        }
    }
}