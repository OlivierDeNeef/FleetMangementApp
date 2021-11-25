using System.Collections.Generic;
using System.Data.SqlClient;
using DomainLayer.Exceptions.Managers;
using DomainLayer.Interfaces.Repos;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;
using DataAccessLayer.Exceptions.Repos;
using System;
using DomainLayer.Exceptions.Models;

namespace DataAccessLayer.Repos
{
    public class RijbewijsTypeRepo :IRijbewijsTypeRepo
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public RijbewijsTypeRepo(IConfiguration config)
        {
            _configuration = config;
            _connectionString = config.GetConnectionString("defaultConnection");
        }

        public void VoegRijbewijsToe(RijbewijsType rijbewijsType)
        {
            var connection = new SqlConnection(_connectionString);
            const string query = "INSERT INTO dbo.rijbewijstypes (Type) VALUES (@Type);";
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
            const string query = "DELETE FROM dbo.rijbewijstypes WHERE Id = @id";
            try
            {
                using var command = connection.CreateCommand();
                command.CommandText = query;
                command.Parameters.AddWithValue("@id",rijbewijsType.Id);

                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                throw new RijbewijsTypeRepoException(
                    "VerwijderRijbewijs - Er ging iets fout tijdens het verwijderen van het rijbewijstype",exception);
            }
            finally
            {
                connection.Close();
            }

        }

        public IEnumerable<RijbewijsType> GeefAlleRijbewijsTypes()
        {
            var connection = new SqlConnection(_connectionString);
            var rijbewijsTypeLijst = new List<RijbewijsType>();
            try
            {
                using var command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM dbo.rijbewijstypes";
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var rijbewijsType = new RijbewijsType(reader.GetInt32(0), reader.GetString(1));
                    rijbewijsTypeLijst.Add(rijbewijsType);
                }
                return rijbewijsTypeLijst;
            }

            catch (Exception e)
            {
                throw new RijbewijsTypeException("GeefAlleRijbewijsTypes - Er ging iets mis", e);
            }
            finally
            {
                 connection.Close();
            
            }
        }

        public bool BestaatRijbewijsType(RijbewijsType rijbewijsType)
        {
            var connection = new SqlConnection(_connectionString);
            try
            {
                using var command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM dbo.rijbewijstype WHERE (type = @type)";

                command.Parameters.AddWithValue("@type", rijbewijsType.Type);


                connection.Open();

                var reader = command.ExecuteReader();
                bool bestaatType = reader.HasRows;
                return bestaatType;

            }
            catch (Exception e)
            {
                throw new RijbewijsTypeException("BestaatRijbewijsType - Er ging iets mis", e);
            }
            finally
            {
                connection.Close();
                
            }
            
        }

        public void UpdateRijbewijsType(RijbewijsType rijbewijsType)
        {
            var connection = new SqlConnection(_connectionString);
            try
            {
                using var command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = "UPDATE rijbewijstype SET type = @type where Id = @id";
                command.Parameters.AddWithValue("@type", rijbewijsType.Type);
                command.Parameters.AddWithValue("@id", rijbewijsType.Id);
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new RijbewijsTypeException("UpdateRijbewijsType - Er ging iets mis", e);
            }
            finally
            {
                connection.Close();
            }
            
            
        }
    }
}