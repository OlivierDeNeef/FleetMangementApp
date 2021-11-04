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
        /// <summary>
        /// Voeg branstoftype toe aan de tabel brandstoftype
        /// </summary>
        /// <param name="brandstofType"></param>
        public void VoegBrandstofTypeToe(BrandstofType brandstofType)
        {
            var connection = new SqlConnection(_connectionString);
            string query = "INSERT INTO dbo.BRANDSTOFFENTYPES (type) VALUES(@type)";
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
        }

        /// <summary>
        /// checkt of het brandstoftype al bestaat in de lijst
        /// </summary>
        /// <param name="brandstofType"></param>
        /// <returns></returns>
        public bool BestaatBrandstofType(BrandstofType brandstofType)
        {
            var connection = new SqlConnection(_connectionString);
            string query = "SELECT * FROM dbo.BRANDSTOFFENTYPES WHERE (type = @type)";
            bool bestaatType;
            using (SqlCommand command = connection.CreateCommand())
            {
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

            }
            return bestaatType;
        }

        /// <summary>
        /// update de info van het brandstoftype
        /// </summary>
        /// <param name="brandstofType"></param>
        public void UpdateBrandstofType(BrandstofType brandstofType)
        {
            var connection = new SqlConnection(_connectionString);
            string query = "UPDATE BRANDSTOFFENTYPES SET type = @type where Id = @id";
            using (SqlCommand command = connection.CreateCommand())
            {
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
        }
        /// <summary>
        /// geeft een lijst van alle brandstoftypen
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BrandstofType> GeefAlleBrandstofTypes()
        {
            var connection = new SqlConnection(_connectionString);

            string query = "SELECT * FROM dbo.BRANDSTOFFENTYPES";

            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Connection = connection;
                    command.CommandText = query;

                    List<BrandstofType> brandstoftypelijst = new List<BrandstofType>();
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
        }

        /// <summary>
        /// verwijderd een brandstoftype
        /// </summary>
        /// <param name="id"></param>
        public void VerwijderBrandstofType(int id) 
        {
            var connection = new SqlConnection(_connectionString);
            string query = "DELETE FROM dbo.BRANDSTOFFENTYPES WHERE Id = @id";
            using (SqlCommand command = connection.CreateCommand())
            {
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
                    throw new BrandstofTypeManagerException("VerwijderBrandstofTypes - Er liep iets mis", e);
                }
                finally
                {
                    connection.Close();

                }
            }
        }
    }
}
