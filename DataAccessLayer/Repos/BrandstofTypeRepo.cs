using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DomainLayer.Exceptions.Managers;
using DomainLayer.Exceptions.Models;
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

        public void VoegBrandstofTypeToe(BrandstofType brandstofType)
        {
            var connection = new SqlConnection(_connectionString);
            var query = "INSERT INTO dbo.BRANSTOFTYPES (type) VALUES(@type)";
            using var command = connection.CreateCommand();
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

        public bool BestaatBrandstofType(BrandstofType brandstofType)
        {
            var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM dbo.BRANSTOFTYPES WHERE (type = @type)";
            bool bestaatType;
            using var command = connection.CreateCommand();
            connection.Open();
            try
            {
                command.Parameters.AddWithValue("@type", brandstofType.Type);
                command.CommandText = query;
                var reader = command.ExecuteReader();
                bestaatType = reader.HasRows;
            }
            catch (Exception e)
            {
                throw new BrandstofTypeManagerException("BestaatBranstoftype - Er ging iets mis", e);
            }
            finally
            {
                connection.Close();
            }

            return bestaatType;
        }
        public void UpdateBrandstofType(BrandstofType brandstofType)
        {
            var connection = new SqlConnection(_connectionString);
            var query = "UPDATE BRANDSTOFTYPES SET type = @type where Id = @id";
            using var command = connection.CreateCommand();
            connection.Open();
            try
            {
                command.CommandText = query;
                command.Parameters.AddWithValue("@type", brandstofType.Type);
                command.Parameters.AddWithValue("@id", brandstofType.Id);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new BrandstofTypeManagerException("Update Brandstoftype - Er ging iets mis ", e);
            }
            finally
            {
                connection.Close();
            }
        }
        public IEnumerable<BrandstofType> GeefAlleBrandstofTypes()
        {
            var connection = new SqlConnection(_connectionString);
            const string query = "SELECT * FROM dbo.BRANDSTOFTYPES";
            using var command = connection.CreateCommand();
            connection.Open();
            try
            {
                command.Connection = connection;
                command.CommandText = query;

                var brandstoftypelijst = new List<BrandstofType>();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var brandstofType = new BrandstofType(reader.GetInt32(0), reader.GetString(1));
                    brandstoftypelijst.Add(brandstofType);
                }
                return brandstoftypelijst;
            }
            catch (Exception e)
            {
                throw new BrandstofTypeManagerException("Geef AlleBrandStofTypes - er ging iets mis", e);
            }
            finally
            {
                connection.Close();
            }
        }
        public void VerwijderBrandstofType(int id) 
        {
            var connection = new SqlConnection(_connectionString);
            var query = "DELETE FROM dbo.BRANDSTOFTYPES WHERE Id = @id";
            using var command = connection.CreateCommand();
            try
            {
                connection.Open();
                command.CommandText = query;
                command.Connection = connection;
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new BrandstofTypeManagerException("GeefAlleBrandstofTypes - Er liep iets mis", e);
            }
            finally
            {
                connection.Close();

            }
        }
    }
}
