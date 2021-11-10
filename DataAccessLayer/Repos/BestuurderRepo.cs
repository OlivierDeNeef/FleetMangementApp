using System;
using System.Collections.Generic;
using System.Data;
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
        }
        public BestuurderRepo(string connectionString)
        {
            _connectionString = connectionString;

        }

        public IReadOnlyList<Bestuurder> GeefGefilderdeBestuurders(string voornaam = null, string naam = null,
            DateTime geboortedatum = new DateTime(), List<RijbewijsType> lijstRijbewijstypes = null, string rijksregisternummer = null,
            bool gearchiveerd = false)
        {
            throw new NotImplementedException();
        }



        private void VoegRijbewijstypeToeAanBestuurder(int bestuurderId, IEnumerable<RijbewijsType> rijbewijzenList)
        {
            var connection = new SqlConnection(_connectionString);
            string query =
                "INSERT into dbo.RijbewijzenTypes_Bestuurders (RijbewijzenTypesId, BestuurdersId) VALUES (@RijsbewijzentypesId, @bestuurderId)";


            foreach (var rijbewijs in rijbewijzenList)
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@RijsbewijzentypesId", rijbewijs.Id);
                        command.Parameters.AddWithValue("@bestuurdersId", bestuurderId);

                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw new BestuurderManagerException("VoegRijbewijstypeToeAanBestuurder - er ging iets mis", e);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        public void VoegBestuurderToe(Bestuurder bestuurder)
        { 
            var connection = new SqlConnection(_connectionString);
            string query = "INSERT INTO dbo.BESTUURDERS (Naam, Voornaam,  Geboortedatum, " +
                           "Rijksregisternummer, Straat, Busnummer, Huisnummer, Postcode, Land, TankkaartId, VoertuigId, Gearchiveerd) " +
                           "VALUES (@Naam, @Voornaam, @Geboortedatum, @Rijksregisternummer, @straat, @busnummer, @huisnummer, @Postcode, @Land, @TankkaartenId, @VoertuigId, @IsGearchiveerd)";

            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.AddWithValue("@Naam", bestuurder.Naam);
                    command.Parameters.AddWithValue("@Voornaam", bestuurder.Voornaam);
                    command.Parameters.AddWithValue("@Geboortedatum", bestuurder.Geboortedatum);
                    command.Parameters.AddWithValue("@Rijksregisternummer", bestuurder.Rijksregisternummer);
                    command.Parameters.AddWithValue("@straat", (object) bestuurder.Adres?.Straat ?? DBNull.Value);
                    command.Parameters.AddWithValue("@busnummer", (object) bestuurder.Adres?.Busnummer ?? DBNull.Value);
                    command.Parameters.AddWithValue("@huisnummer", (object) bestuurder.Adres?.Huisnummer ?? DBNull.Value);
                    //command.Parameters.AddWithValue("@Stad", (object) bestuurder.Adres?.Stad ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Postcode", (object) bestuurder.Adres?.Postcode ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Land", (object) bestuurder.Adres?.Land ?? DBNull.Value);
                    command.Parameters.AddWithValue("@TankkaartenId", (object) bestuurder.Tankkaart?.Id ?? DBNull.Value);
                    command.Parameters.AddWithValue("@VoertuigId", (object) bestuurder.Voertuig?.Id ?? DBNull.Value);
                    command.Parameters.AddWithValue("@IsGearchiveerd", bestuurder.IsGearchiveerd);
                    command.CommandText = query;
                    command.ExecuteNonQuery();


                    VoegRijbewijstypeToeAanBestuurder(bestuurder.Id, bestuurder.GeefRijbewijsTypes());
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
            List<RijbewijsType> rijbewijzen = new List<RijbewijsType>();
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
                        rijbewijzen.Add(rijbewijs);
                        b = new Bestuurder(id, reader.GetString(1), reader.GetString(2), reader.GetDateTime(3), reader.GetString(4), rijbewijzen, false); // hoe te fixen?
                    }
                    else
                    {
                        throw new BestuurderManagerException("BestuurderId bestaat niet");
                    }

                    return b;
                }
                catch (Exception e)
                {
                    throw new BestuurderManagerException("GeefBestuurder - Er ging iets mis");
                }
            }

        }

    }
}
