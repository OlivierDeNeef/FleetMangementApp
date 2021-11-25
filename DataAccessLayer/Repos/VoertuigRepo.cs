using DataAccessLayer.Exceptions.Repos;
using DomainLayer.Interfaces.Repos;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repos
{
    public class VoertuigRepo
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public VoertuigRepo(IConfiguration config)
        {
            _configuration = config;
            _connectionString = config.GetConnectionString("defaultConnection");
        }

        public bool BestaatVoertuig(Voertuig voertuig)
        {
            var connection = new SqlConnection(_connectionString);
            const string query = "SELECT * FROM dbo.voertuigen WHERE (id = @id)";
            try
            {
                using var command = connection.CreateCommand();
                connection.Open();
                command.Parameters.AddWithValue("@id", voertuig.Id);
                command.CommandText = query;
                var reader = command.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {
                throw new VoertuigRepoException("BestaatVoertuig - Er ging iets mis", e);
            }
            finally
            {
                connection.Close();
            }
        }

        public IReadOnlyList<Voertuig> GeefGefilterdeVoertuigen([Optional] int id, [Optional] string merk, [Optional] string model, [Optional] int aantalDeuren, [Optional] string nummerplaat, [Optional] string chassisnummer, [Optional] string kleur, [Optional] WagenType wagenType, [Optional] BrandstofType brandstofType, [Optional] bool gearchiveerd, [Optional] RijbewijsType type, [Optional] bool isHybride)
        {
            throw new NotImplementedException();
        }

        public Voertuig GeefVoertuig(int id)
        {
            var connection = new SqlConnection(_connectionString);
            const string query = "SELECT * FROM ((dbo.voertuigen INNER JOIN dbo.WagenTypes ON dbo.voertuigen.WagenTypeId = dbo.wagentypes.Id) INNER JOIN dbo.BrandstofTypes ON dbo.voertuigen.BrandstofId = dbo.BrandstofTypes.Id) WHERE id=@id";
            using var command = connection.CreateCommand();
            try
            {
                command.CommandText = query;
                command.Connection = connection;
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                var reader = command.ExecuteReader();
                Voertuig voertuig;
                BrandstofType brandstof;
                WagenType wagenType;
                if (reader.HasRows)
                {
                    reader.Read();

                    brandstof = new BrandstofType((int)reader[11], (string)reader[12]);
                    wagenType = new WagenType((int)reader[13], (string)reader[14]);
                    voertuig = new Voertuig((int)reader[0], (string)reader[1], (string)reader[2], (string)reader[3], (string)reader[4], brandstof, wagenType );
                    voertuig.ZetGearchiveerd((bool)reader[5]);
                    voertuig.ZetKleur((string)reader[6]);
                    voertuig.ZetAantalDeuren((int)reader[7]);
                    voertuig.ZetHybride((bool)reader[8]);

                }
                else
                {
                    throw new VoertuigRepoException("VoertuigId bestaat niet");
                }

                return voertuig;
            }
            catch (Exception e)
            {
                throw new VoertuigRepoException("GeefVoertuig - Er ging iets mis", e);
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdateVoertuig(Voertuig voertuig)
        {
            var connection = new SqlConnection(_connectionString);

            const string query = "UPDATE BESTUURDERS SET Merk=@Merk, Model=@Model, Chassisnummer=@Chassisnummer, Nummerplaat=@Nummerplaat, Gearchiveerd=@Gearchiveerd, Kleur=@Kleur, AantalDeuren=@AantalDeuren, Hybride=@Hybride, WagenTypeId=@WagenTypeId, BrandstofId=@BrandstofId WHERE Id=@Id";

            try
            {
                using var command = connection.CreateCommand();
                connection.Open();
                command.Parameters.AddWithValue("@Id", voertuig.Id);
                command.Parameters.AddWithValue("@Merk", voertuig.Merk); 
                command.Parameters.AddWithValue("@Model", voertuig.Model); 
                command.Parameters.AddWithValue("@Chassisnummer", voertuig.Chassisnummer); 
                command.Parameters.AddWithValue("@Nummerplaat", voertuig.Nummerplaat); 
                command.Parameters.AddWithValue("@Gearchiveerd", (object)voertuig.IsGearchiveerd ?? DBNull.Value); 
                command.Parameters.AddWithValue("@Kleur", (object)voertuig.Kleur ?? DBNull.Value);
                command.Parameters.AddWithValue("@AantalDeuren", (object)voertuig.AantalDeuren ?? DBNull.Value);
                command.Parameters.AddWithValue("@Hybride", (object)voertuig.IsHybride ?? DBNull.Value); 
                command.Parameters.AddWithValue("@WagenTypeId", (object)voertuig.WagenType?.Id ?? DBNull.Value); 
                command.Parameters.AddWithValue("@BrandstofId", (object)voertuig.BrandstofType?.Id ?? DBNull.Value); 
                command.CommandText = query;
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new VoertuigRepoException("UpdateVoertuig - Er ging iets mis", e);
            }
            finally
            {
                connection.Close();
            }
        }

        public void VoegVoertuigToe(Voertuig voertuig)
        {
            var connection = new SqlConnection(_connectionString);
            const string query =
                "INSERT INTO dbo.voertuigen (Merk, Model, Chassisnummer, Nummerplaat, Gearchiveerd, Kleur, AantalDeuren, Hybride, WagenTypeId, BrandstofId) " +
                "VALUES (@Merk, @Model, @Chassisnummer, @Nummerplaat, @Gearchiveerd, @Kleur, @AantalDeuren, @Hybride, @WagenTypeId, @BrandstofId) " +
                "OUTPUT INSERTED.Id";

            try
            {
                using var command = connection.CreateCommand();
                connection.Open();
                command.Parameters.AddWithValue("@Id", voertuig.Id);
                command.Parameters.AddWithValue("@Merk", voertuig.Merk);
                command.Parameters.AddWithValue("@Model", voertuig.Model);
                command.Parameters.AddWithValue("@Chassisnummer", voertuig.Chassisnummer);
                command.Parameters.AddWithValue("@Nummerplaat", voertuig.Nummerplaat);
                command.Parameters.AddWithValue("@Gearchiveerd", (object)voertuig.IsGearchiveerd ?? DBNull.Value);
                command.Parameters.AddWithValue("@Kleur", (object)voertuig.Kleur ?? DBNull.Value);
                command.Parameters.AddWithValue("@AantalDeuren", (object)voertuig.AantalDeuren ?? DBNull.Value);
                command.Parameters.AddWithValue("@Hybride", (object)voertuig.IsHybride ?? DBNull.Value);
                command.Parameters.AddWithValue("@WagenTypeId", (object)voertuig.WagenType?.Id ?? DBNull.Value);
                command.Parameters.AddWithValue("@BrandstofId", (object)voertuig.BrandstofType?.Id ?? DBNull.Value);
                command.CommandText = query;
                command.ExecuteScalar();
                
            }
            catch (Exception e)
            {
                throw new VoertuigRepoException("VoegVoertuigToe - Er ging iets mis ", e);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
