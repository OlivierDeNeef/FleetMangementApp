using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DomainLayer.Exceptions.Managers;
using DomainLayer.Interfaces.Repos;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Repos
{
    public class BrandstofTypeRepo : IBrandstofTypeRepo
    {

        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public BrandstofTypeRepo(IConfiguration config)
        {
            _configuration = config;
            _connectionString = config.GetConnectionString("defaultConnection");
        }

        //return type aangepast void => BrandstofType
        public void VoegBrandstofTypeToe(BrandstofType brandstofType)
        {
            BrandstofType nieuwType = null;
            var connection = new SqlConnection(_connectionString);
            string query = "INSERT INTO dbo.BRANSTOFTYPE (type) VALUES(@type)";
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.AddWithValue("@type", brandstofType.Type);
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                  

                }
                catch (Exception e)
                {
                    
                    throw new BrandstofTypeManagerException("VoegBrandstofTypeToe - Er ging iets mis ", e);
                }
                finally
                {
                    connection.Close();
                }
                
            }
           
            


        } //return type aangepast void => BrandstofType

        public bool BestaatBrandstofType(BrandstofType brandstofType)
        {
      
            SqlCommand command = new SqlCommand();
            var connection = new SqlConnection(_connectionString);
            command.Connection = connection;
            command.CommandText = "SELECT * FROM dbo.BRANSTOFTYPE WHERE (type = @type)";

            command.Parameters.AddWithValue("@type", brandstofType.Type);
         

            connection.Open();

            var reader = command.ExecuteReader();
            bool bestaatType = reader.HasRows;


            connection.Close();
            return bestaatType;
        } 
        public void UpdateBrandstofType(BrandstofType brandstofType)
        {
            SqlCommand command = new SqlCommand();
            var connection = new SqlConnection(_connectionString);
            command.Connection = connection;
            command.CommandText = "UPDATE BRANDSTOFTYPE SET type = @type where Id = @id";
            command.Parameters.AddWithValue("@type", brandstofType.Type);
            command.Parameters.AddWithValue("@id", brandstofType.Id);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        } 
        public IEnumerable<BrandstofType> GeefAlleBrandstofTypes()
        {
            SqlCommand command = new SqlCommand();
            var connection = new SqlConnection(_connectionString);
            command.Connection = connection;
            command.CommandText = "SELECT * FROM dbo.BRANDSTOFTYPE";

            connection.Open();

            List<BrandstofType> brandstoftypelijst = new List<BrandstofType>();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var brandstofType = new BrandstofType(reader.GetInt32(0), reader.GetString(1));
                brandstoftypelijst.Add(brandstofType);
            }

            connection.Close();
            return brandstoftypelijst;
        }
        public void VerwijderBrandstofType(int id) //parameter aangepast
        {
            SqlCommand command = new SqlCommand();
            var connection = new SqlConnection(_connectionString);
            command.Connection = connection;
            command.CommandText = "DELETE FROM dbo.BRANDSTOFTYPE WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);


            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

        }



    }
}
