using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
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

        public BestuurderRepo( IConfiguration config)
        {
            _configuration = config;
            _connectionString = config.GetConnectionString("defaultConnection");
        }

        public BestuurderRepo(string connectionString)
        {
            _connectionString = connectionString;

        }


        public IReadOnlyList<Bestuurder> GeefGefilderdeBestuurders(string voornaam, string naam, DateTime geboortedatum, List<RijbewijsType> lijstRijbewijstypes, string rijksregisternummer, bool gearchiveerd)
        {
            throw new NotImplementedException();
        }

        public void VoegBestuurderToe(Bestuurder bestuurder)
        { 
            var connection = new SqlConnection(_connectionString);
            const string query = 
                "INSERT INTO dbo.BESTUURDERS (Naam, Voornaam,  Geboortedatum, Rijksregisternummer, Straat, Busnummer, Huisnummer, Stad, Postcode, Land, TankkaartId, VoertuigId, Gearchiveerd) " +
                "OUTPUT INSERTED.Id VALUES (@Naam, @Voornaam, @Geboortedatum, @Rijksregisternummer, @straat, @busnummer, @huisnummer, @Stad, @Postcode, @Land, @TankkaartenId, @VoertuigId, @IsGearchiveerd)";

            try
            {
                using var command = connection.CreateCommand();
                connection.Open();
                command.Parameters.AddWithValue("@Naam", bestuurder.Naam);
                command.Parameters.AddWithValue("@Voornaam", bestuurder.Voornaam);
                command.Parameters.AddWithValue("@Geboortedatum", bestuurder.Geboortedatum);
                command.Parameters.AddWithValue("@Rijksregisternummer", bestuurder.Rijksregisternummer);
                command.Parameters.AddWithValue("@straat", (object) bestuurder.Adres?.Straat ?? DBNull.Value);
                command.Parameters.AddWithValue("@busnummer", (object) bestuurder.Adres?.Busnummer ?? DBNull.Value);
                command.Parameters.AddWithValue("@huisnummer", (object) bestuurder.Adres?.Huisnummer ?? DBNull.Value);
                command.Parameters.AddWithValue("@Stad", (object) bestuurder.Adres?.Stad ?? DBNull.Value);
                command.Parameters.AddWithValue("@Postcode", (object) bestuurder.Adres?.Postcode ?? DBNull.Value);
                command.Parameters.AddWithValue("@Land", (object) bestuurder.Adres?.Land ?? DBNull.Value);
                command.Parameters.AddWithValue("@TankkaartenId", (object) bestuurder.Tankkaart?.Id ?? DBNull.Value);
                command.Parameters.AddWithValue("@VoertuigId", (object) bestuurder.Voertuig?.Id ?? DBNull.Value);
                command.Parameters.AddWithValue("@IsGearchiveerd", bestuurder.IsGearchiveerd);
                command.CommandText = query;
                var id = (int)command.ExecuteScalar();
                VoegRijbewijstypeToeAanBestuurder(id, bestuurder.GeefRijbewijsTypes());
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

        public bool BestaatBestuurder(int bestuurderId)
        {
            var connection = new SqlConnection(_connectionString);
            const string query = "SELECT * FROM dbo.BESTUURDERS WHERE (Id = @Id)";
            using var command = connection.CreateCommand();
            connection.Open();
            try
            {
                command.Parameters.AddWithValue("@Id", bestuurderId);
                command.CommandText = query;
                var reader = command.ExecuteReader();
                return reader.HasRows;

            }
            catch (Exception e)
            {
                throw new BestuurderManagerException("Bestaat Bestuurder - Er ging iets mis", e);
            }
            finally
            {
                connection.Close();
            }
        }

        public void VerwijderBestuurder(int id)
        {
            var connection = new SqlConnection(_connectionString);
            const string query = "DELETE FROM dbo.BESTUURDERS WHERE Id = @id";
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
                throw new BrandstofTypeManagerException("VerwijderBestuurder - Er liep iets mis", e);
            }
            finally
            {
                connection.Close();
            }
        }
        private void VoegRijbewijstypeToeAanBestuurder(int bestuurderId, IEnumerable<RijbewijsType> rijbewijzenList)
        {
            var connection = new SqlConnection(_connectionString);
            const string query = 
                "INSERT into dbo.RijbewijsTypes_Bestuurders (RijbewijsTypeId, BestuurderId) "+
                "VALUES (@RijsbewijzentypesId, @bestuurderId)";
            foreach (var rijbewijs in rijbewijzenList)
            {
                try
                {
                    using var command = connection.CreateCommand();
                    connection.Open();
                    command.Parameters.AddWithValue("@RijsbewijzentypesId", rijbewijs.Id);
                    command.Parameters.AddWithValue("@bestuurderId", bestuurderId);
                    command.CommandText = query;
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



        private void VerwijderRijbewijsVanBestuurder(int bestuurderId, int rijbewijstypeId)
        {
            var connection = new SqlConnection(_connectionString);
            const string query =
                "DELETE FROM dbo.RijbewijsTypes_Bestuurders where bestuurderId=@bestuurderId AND RijbewijsTypeId=@rijbewijstypeId";
            
                try
                {
                    using var command = connection.CreateCommand();
                    connection.Open();
                    command.Parameters.AddWithValue("@rijbewijstypeId", rijbewijstypeId);
                    command.Parameters.AddWithValue("@bestuurderId", bestuurderId);
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new BestuurderManagerException("VerwijderRijbewijsVanBestuurder - er ging iets mis", e);
                }
                finally
                {
                    connection.Close();
                }
        }
        private void UpdateRijbewijzenVanBestuurder(int bestuurderId, IEnumerable<RijbewijsType> rijbewijzenList)
        {
            var connection = new SqlConnection(_connectionString);
            //voor elke rijbewijstype, bestaat het nog in de bestuurder_rijbewijs.
            var querySelect = "SELECT * FROM RijbewijsTypes_Bestuurders where BestuurderId=@BestuurderId";
            //.. executeReader
            var dbLijstrijbewijzen = new List<RijbewijsType>();
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = querySelect;
                var reader = command.ExecuteReader();
                do
                {
                    var rijbewijs = new RijbewijsType(reader.GetInt32(0), reader.GetString(1));
                    dbLijstrijbewijzen.Add(rijbewijs);
                } while (reader.HasRows);
                connection.Close();
            }
            foreach (var rijbewijs in dbLijstrijbewijzen) // voor elk rijbewijs in de databank, bestaat het in de UI? Als er meer zij in de UI, verwijder in de DB die
            {
                if (!rijbewijzenList.Any(x => x.Id == rijbewijs.Id))
                {
                    VerwijderRijbewijsVanBestuurder(bestuurderId, rijbewijs.Id);
                }
            }

            foreach (var rijbewijs in rijbewijzenList) // voor elk rijbewijs in de UI, bestaat die in de DB, is die er niet? => voeg die toe
            {
                if (!dbLijstrijbewijzen.Any(x => x.Id == rijbewijs.Id))
                {
                    VoegRijbewijstypeToeAanBestuurder(bestuurderId, new List<RijbewijsType>(){rijbewijs});
                }
            }
        }
        public void UpdateBestuurder(Bestuurder bestuurder)
        {
            var connection = new SqlConnection(_connectionString);
            
            const string query = "UPDATE BESTUURDERS SET Naam=@Naam, Voornaam=@Voornaam, Geboortedatum=@Geboordedatum, Rijksregisternummer=@Rijksregisternummer, Straat=@Straat, " +
                                 "Busnummer=@Busnummer, Gearchiveerd=@Gearchiveerd, TankkaartId=@TankkaartId, VoertuigId=@VoertuigId,  " +
                                 "Huisnummer=@Huisnummer, Stad=@Stad, Postcode=@Postcode, Land=@Land";

            try
            {
                using var command = connection.CreateCommand();
                connection.Open();
                command.Parameters.AddWithValue("@Naam", bestuurder.Naam); // ok
                command.Parameters.AddWithValue("@Voornaam", bestuurder.Voornaam); // ok
                command.Parameters.AddWithValue("@Geboortedatum", bestuurder.Geboortedatum); //ok 
                command.Parameters.AddWithValue("@Rijksregisternummer", bestuurder.Rijksregisternummer); //ok
                command.Parameters.AddWithValue("@Straat", (object)bestuurder.Adres?.Straat ?? DBNull.Value); //ok
                command.Parameters.AddWithValue("@Busnummer", (object)bestuurder.Adres?.Busnummer ?? DBNull.Value); //ok
                command.Parameters.AddWithValue("@Huisnummer", (object)bestuurder.Adres?.Huisnummer ?? DBNull.Value); //ok
                command.Parameters.AddWithValue("@Stad", (object)bestuurder.Adres?.Stad ?? DBNull.Value); //ok
                command.Parameters.AddWithValue("@Postcode", (object)bestuurder.Adres?.Postcode ?? DBNull.Value); //ok
                command.Parameters.AddWithValue("@Land", (object)bestuurder.Adres?.Land ?? DBNull.Value); //ok
                command.Parameters.AddWithValue("@TankkaartenId", (object)bestuurder.Tankkaart?.Id ?? DBNull.Value); //k
                command.Parameters.AddWithValue("@VoertuigId", (object)bestuurder.Voertuig?.Id ?? DBNull.Value); //ok
                command.Parameters.AddWithValue("@IsGearchiveerd", bestuurder.IsGearchiveerd);
                command.CommandText = query;
                command.ExecuteNonQuery();

                UpdateRijbewijzenVanBestuurder(bestuurder.Id, bestuurder.GeefRijbewijsTypes());
            }   
            catch (Exception e)
            {
                throw new BestuurderManagerException("Update Bestuurder - Er ging iets mis", e);
            }
            finally
            {
                connection.Close();
            }
            
        }

        public Bestuurder GeefBestuurder(int id)
        {
            var rijbewijzen = new List<RijbewijsType>();
            var connection = new SqlConnection(_connectionString);
            const string query = "SELECT * FROM dbo.BESTUURDERS WHERE id=@id";
            using var command = connection.CreateCommand();
            try
            {
                command.CommandText = query;
                command.Connection = connection;
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                var reader = command.ExecuteReader();
                Bestuurder bestuurder;
                if (reader.HasRows)
                {

                    reader.Read();
                    var rijbewijs = new RijbewijsType(reader.GetInt32(0), reader.GetString(1));
                    rijbewijzen.Add(rijbewijs);
                    bestuurder = new Bestuurder(id, reader.GetString(1), reader.GetString(2), reader.GetDateTime(3),
                        reader.GetString(4), rijbewijzen, false); // hoe te fixen?
                }
                else
                {
                    throw new BestuurderManagerException("BestuurderId bestaat niet");
                }

                return bestuurder;
            }
            catch (Exception e)
            {
                throw new BestuurderManagerException("GeefBestuurder - Er ging iets mis", e);
            }
            finally
            {
                connection.Close();
            }
        }

       
    }
}
