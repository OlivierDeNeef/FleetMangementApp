using System.Collections.Generic;
using System.Data.SqlClient;
using DomainLayer.Exceptions.Managers;
using DomainLayer.Interfaces.Repos;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;
using DataAccessLayer.Exceptions.Repos;
using System;

namespace DataAccessLayer.Repos
{
    public class RijbewijsTypeRepo : IRijbewijsTypeRepo
    {
        private string _connectionString;
        private readonly IConfiguration _configuration;
        /*private readonly SqlConnection _connection;

        public RijbewijsTypeRepo(SqlConnection connection)
        {
            _connection = connection;
        }*/

        public RijbewijsTypeRepo(IConfiguration config)
        {
            _configuration = config;
            _connectionString = config.GetConnectionString("defaultConnection");
        }

        private SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            return connection;
        }


        public void VoegRijbewijsToe(RijbewijsType rijbewijsType)
        {
           
            var connection = new SqlConnection(_connectionString);
            const string query = "INSERT INTO dbo.rijbewijstype (Type) VALUES (@Type);";
            try
            {
                using var command = connection.CreateCommand();
                command.CommandText = query;
                command.Parameters.AddWithValue("@Type",rijbewijsType.Type);
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {

                throw new RijbewijsTypeRepoException(
                    "VoegRijbewijsToe - Er ging iets fout tijdens het opladen van het rijbewijstype",exception);
            }
            finally
            {
                connection.Close();
            }

        }

        public void VerwijderRijbewijsType(RijbewijsType rijbewijsType)
        {
            var connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM dbo.rijbewijstype WHERE Id = @id";
            command.Parameters.AddWithValue("@id", rijbewijsType.Id);


            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

        }

        public IEnumerable<RijbewijsType> GeefAlleRijbewijsTypes()
        {
            var connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM dbo.rijbewijstype";

            connection.Open();

            List<RijbewijsType> rijbewijsTypeLijst = new List<RijbewijsType>();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var rijbewijsType = new RijbewijsType(reader.GetInt32(0), reader.GetString(1));
                rijbewijsTypeLijst.Add(rijbewijsType);
            }

            connection.Close();
            return rijbewijsTypeLijst;
        }

        public bool BestaatRijbewijsType(RijbewijsType rijbewijsType)
        {
            var connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM dbo.rijbewijstype WHERE (type = @type)";

            command.Parameters.AddWithValue("@type", rijbewijsType.Type);
         

            connection.Open();

            var reader = command.ExecuteReader();
            bool bestaatType = reader.HasRows;


            connection.Close();
            return bestaatType;
        }

        public void UpdateRijbewijsType(RijbewijsType rijbewijsType)
        {
            var connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE rijbewijstype SET type = @type where Id = @id";
            command.Parameters.AddWithValue("@type",rijbewijsType.Type);
            command.Parameters.AddWithValue("@id",rijbewijsType.Id);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}