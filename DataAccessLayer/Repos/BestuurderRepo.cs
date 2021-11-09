﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DomainLayer.Exceptions.Managers;
using DomainLayer.Interfaces.Repos;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;



namespace DataAccessLayer.Repos
{
    public class BestuurderRepo : IBestuurderRepo
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public BestuurderRepo(string connectionString, IConfiguration config)
        {
            _configuration = config;
            _connectionString = config.GetConnectionString("defaultConnection");
        }public BestuurderRepo(string connectionString)
        {
            _connectionString = connectionString;
        
        }

        public IReadOnlyList<Bestuurder> GeefGefilderdeBestuurders(string voornaam = null, string naam = null,
            DateTime geboortedatum = new DateTime(), List<RijbewijsType> lijstRijbewijstypes = null, string rijksregisternummer = null,
            bool gearchiveerd = false)
        {
            throw new NotImplementedException();
        }

        public void VoegBestuurderToe(Bestuurder bestuurder)
        {
            var connection = new SqlConnection(_connectionString);
            string query = "INSERT INTO dbo.BESTUURDERS (Id, Naam, Voornaam, _rijbewijsTypes, Geboortedatum, " +
                           "Rijksregisternummer, straat, busnummer, huisnummer, Stad, Postcode, Land, TankkaartenId, VoertuigenId, IsGearchiveerd) " +
                           "VALUES (@Id, @Naam, @Voornaam, @_rijbewijsTypes, @Geboortedatum, " +
                           "@Rijksregisternummer, @straat, @busnummer, @huisnummer, @Stad, @Postcode, @Land,  @TankkaartenId, @VoertuigId, @IsGearchiveerd";

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
                    command.Parameters.AddWithValue("@straat", bestuurder.Adres?.Straat);
                    command.Parameters.AddWithValue("@busnummer", bestuurder.Adres?.Busnummer);
                    command.Parameters.AddWithValue("@huisnummer", bestuurder.Adres?.Huisnummer);
                    command.Parameters.AddWithValue("@Stad", bestuurder.Adres?.Stad);
                    command.Parameters.AddWithValue("@Postcode", bestuurder.Adres?.Postcode);
                    command.Parameters.AddWithValue("@Land", bestuurder.Adres?.Land);
                    command.Parameters.AddWithValue("@TankkaartenId", bestuurder.Tankkaart?.Id);
                    command.Parameters.AddWithValue("@VoertuigId", bestuurder.Voertuig?.Id);
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
        public bool BestaatBestuurder(int bestuurderId)
        {
            bool bestaatBestuurder;   
            var connection = new SqlConnection(_connectionString);
            string query = "SELECT * FROM dbo.BESTUURDERS WHERE (Id = @Id)";

            using SqlCommand command = connection.CreateCommand();
            connection.Open();
            try
            {
                command.Parameters.AddWithValue("@Id", bestuurderId);
                command.CommandText = query;
                var reader = command.ExecuteReader();
                bestaatBestuurder = reader.HasRows;
            }
            catch (Exception e)
            {
                throw new BestuurderManagerException("Bestaat Bestuurder - Er ging iets mis", e);
            }
            finally
        {
                connection.Close();
            }

            return bestaatBestuurder;
        }

        public void VerwijderBestuurder(int id)
        {
            var connection = new SqlConnection(_connectionString);
            string query = "DELETE FROM dbo.BESTUURDERS WHERE Id = @id";
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
                    throw new BrandstofTypeManagerException("VerwijderBestuurder - Er liep iets mis", e);
        }
                finally
                {
                    connection.Close();

                }
            }
        }
        public void UpdateBestuurder(Bestuurder b)
        {
            throw new NotImplementedException();
        }
        public Bestuurder GeefBestuurder(int id)
        {
            Bestuurder b = null;
            var connection = new SqlConnection(_connectionString);
            string query = "SELECT * FROM dbo.BESTUURDERS WHERE id=@id";
            using (SqlCommand command = connection.CreateCommand())
            {
                try
                {
                    command.CommandText = query;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        RijbewijsType rijbewijs = new RijbewijsType(reader.GetInt32(0), reader.GetString(1));
                        b = new Bestuurder(id, reader.GetString(1), reader.GetString(2), reader.GetDateTime(3), reader.GetString(4), null); // hoe te fixen?
                    }

                    return b;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

        }

    }
}
