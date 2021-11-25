using DataAccessLayer.Exceptions.Repos;
using DomainLayer.Exceptions.Managers;
using DomainLayer.Exceptions.Models;
using DomainLayer.Interfaces.Repos;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccessLayer.Repos
{
    public class WagenTypeRepo
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

        public void VerwijderWagenType(int id)
        {
            var connection = new SqlConnection(_connectionString);
            const string query = "DELETE FROM dbo.Wagentypes WHERE Id = @id";
            try
            {
                using var command = connection.CreateCommand();
                connection.Open();
                command.CommandText = query;
                command.Connection = connection;
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new WagenTypeException("VerwijderWagenType - Er liep iets mis", e);
            }
            finally
            {
                connection.Close();

            }
        }


        public void UpdateWagenType(WagenType wagenType)
        {
            var connection = new SqlConnection(_connectionString);
            const string query = "UPDATE dbo.Wagentypes SET Type = @type where Id = @id";
            try
            {
                using var command = connection.CreateCommand();
                connection.Open();
                command.CommandText = query;
                command.Parameters.AddWithValue("@type", wagenType.Type);
                command.Parameters.AddWithValue("@id", wagenType.Id);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new WagenTypeException("UpdateWagenType - Er ging iets mis ", e);
            }
            finally
            {
                connection.Close();
            }
        }

        public IEnumerable<WagenType> GeefAlleWagenTypes()
        {
            var connection = new SqlConnection(_connectionString);
            const string query = "SELECT * FROM dbo.WagenTypes";
            try
            {
                using var command = connection.CreateCommand();
                connection.Open();
                command.Connection = connection;
                command.CommandText = query;
                var alleWagenTypes = new List<WagenType>();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var wagenType = new WagenType(reader.GetInt32(0), reader.GetString(1));
                    alleWagenTypes.Add(wagenType);
                }
                return alleWagenTypes;
            }
            catch (Exception e)
            {
                throw new BrandstofTypeManagerException("GeefAlleWagenTypes - er ging iets mis", e);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool BestaatWagenType(WagenType brandstofType)
        {
            var connection = new SqlConnection(_connectionString);
            const string query = "SELECT * FROM dbo.WagenTypes WHERE (type = @type)";
            try
            {
                using var command = connection.CreateCommand();
                connection.Open();
                command.Parameters.AddWithValue("@type", brandstofType.Type);
                command.CommandText = query;
                var reader = command.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {
                throw new BrandstofTypeManagerException("BestaatWagenType - Er ging iets mis", e);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}