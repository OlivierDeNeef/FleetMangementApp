using System;
using System.Data.SqlClient;
using DomainLayer.Exceptions.Managers;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Repos
{
    public class BestuurderRepo
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public BestuurderRepo(string connectionString, IConfiguration config)
        {
            _configuration = config;
            _connectionString = config.GetConnectionString("defaultConnection");
        }

        public void VoegBestuurderToe(Bestuurder bestuurder)
        {
            var connection = new SqlConnection(_connectionString);
            string query = "INSERT INTO dbo.BESTUURDER (Id, Naam, Voornaam, _rijbewijsTypes, Geboortedatum, " +
                           "Rijksregisternummer, Adres, Tankkaart, Voertuig, IsGearchiveerd) " +
                           "VALUES (@Id, @Naam, @Voornaam, @_rijbewijsTypes, @Geboortedatum, " +
                           "@Rijksregisternummer, @Adres, @Tankkaart, @Voertuig, @IsGearchiveerd";

            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();

                try
                {
                    command.Parameters.AddWithValue("@Id", bestuurder.Id);
                    command.Parameters.AddWithValue("@Naam", bestuurder.Naam);
                    command.Parameters.AddWithValue("@Voornaam", bestuurder.Voornaam);
                    command.Parameters.AddWithValue("@_rijbewijstypes", bestuurder.GeefRijbewijsTypes());
                    command.Parameters.AddWithValue("@Geboortedatum", bestuurder.Geboortedatum);
                    command.Parameters.AddWithValue("@Rijksregisternummer", bestuurder.Rijksregisternummer);
                    command.Parameters.AddWithValue("@Adres", bestuurder.Adres);
                    command.Parameters.AddWithValue("@Tankkaart", bestuurder.Tankkaart);
                    command.Parameters.AddWithValue("@Voertuig", bestuurder.Voertuig);
                    command.Parameters.AddWithValue("@IsGearchiveerd", bestuurder.IsGearchiveerd);
                    command.CommandText = query;
                    command.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    throw new BestuurderManagerException("VoegBestuurderToe - Er ging iets mis ", e);
                }
                finally
                {
                    connection.Close();
                }
            }
       }
    }
}