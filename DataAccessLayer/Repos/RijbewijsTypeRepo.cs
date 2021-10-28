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
        //private string connectionString;
        //private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;

        public RijbewijsTypeRepo(SqlConnection connection)
        {
            _connection = connection;
        }


        //dit is voor als we met config file werken
       /* public RijbewijsTypeRepo(IConfiguration config)
        {
            _configuration = config;
            connectionString = config.GetConnectionString("defaultConnection");
        }*/

        /*private SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }*/


        public void VoegRijbewijsToe(RijbewijsType rijbewijsType)
        {
            //RijbewijsType nieuwType = null;
            SqlCommand command = new SqlCommand();
            command.Connection = _connection;

            command.CommandText = "INSERT INTO dbo.rijbewijstype (type) VALUES(@type)";
            command.Parameters.AddWithValue("@type",rijbewijsType.Type);
            _connection.Open();
            int rows = command.ExecuteNonQuery();
            /*if (rows == 1)
            {
                command.CommandText = "SELECT id from dbo.rijbewijstype WHERE (type = @type)";
                int key = (int)command.ExecuteScalar();
                nieuwType = new RijbewijsType(key,rijbewijsType.Type);

            }*/
            _connection.Close();
            //return nieuwType;

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
            SqlCommand command = new SqlCommand();
            command.Connection = _connection;
            command.CommandText = "UPDATE rijbewijstype SET type = @type where Id = @id";
            command.Parameters.AddWithValue("@type",rijbewijsType.Type);
            command.Parameters.AddWithValue("@id",rijbewijsType.Id);
            _connection.Open();
            command.ExecuteNonQuery();
            _connection.Close();
        }
    }
}