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

        private SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
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
            SqlCommand command = new SqlCommand();
            command.Connection = _connection;
            command.CommandText = "DELETE FROM dbo.rijbewijstype WHERE Id = @id";
            command.Parameters.AddWithValue("@id", rijbewijsType.Id);


            _connection.Open();
            command.ExecuteNonQuery();
            _connection.Close();

        }

        public IEnumerable<RijbewijsType> GeefAlleRijbewijsTypes()
        {
            SqlCommand command = new SqlCommand();
            command.Connection = _connection;
            command.CommandText = "SELECT * FROM dbo.rijbewijstype";

            _connection.Open();

            List<RijbewijsType> rijbewijsTypeLijst = new List<RijbewijsType>();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var rijbewijsType = new RijbewijsType(reader.GetInt32(0), reader.GetString(1));
                rijbewijsTypeLijst.Add(rijbewijsType);
            }

            _connection.Close();
            return rijbewijsTypeLijst;
        }

        public bool BestaatRijbewijsType(RijbewijsType rijbewijsType)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = _connection;
            command.CommandText = "SELECT * FROM dbo.rijbewijstype WHERE (type = @type)";

            command.Parameters.AddWithValue("@type", rijbewijsType.Type);
         

            _connection.Open();

            var reader = command.ExecuteReader();
            bool bestaatType = reader.HasRows;


            _connection.Close();
            return bestaatType;
        }

        public void UpdateRijbewijsType(RijbewijsType rijbewijsType)
        {
            throw new System.NotImplementedException();
        }
    }
}