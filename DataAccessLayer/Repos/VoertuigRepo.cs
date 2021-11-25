﻿using DataAccessLayer.Exceptions.Repos;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;

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
            using var connection = new SqlConnection(_connectionString);
            try
            {
                var command = new SqlCommand("select * from dbo.Voertuigen v " +
                                             "left join dbo.WagenTypes w on v.WagenTypeId= w.Id " +
                                             "left join  dbo.BrandstofTypes bt ON v.BrandstofId = bt.Id " +
                                             "left join dbo.Bestuurders b on v.Id = b.VoertuigId " +
                                             "left JOIN dbo.RijbewijsTypes_Bestuurders rb on b.Id = rb.BestuurderId " +
                                             "left JOIN dbo.RijbewijsTypes r on rb.RijbewijsTypeId = r.Id " +
                                             "left join dbo.Tankkaarten t on b.TankkaartId = t.Id " +
                                             "left join dbo.Tankkaarten_BrandstofTypes tb on tb.TankkaartId = t.Id " +
                                             "left join dbo.BrandstofTypes btb on tb.BrandstofTypeId = btb.Id where v.Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                var reader = command.ExecuteReader();
                if (!reader.HasRows) throw new VoertuigRepoException(nameof(GeefVoertuig) + " - Geen voertuig gevonden");
                Voertuig voertuig = null;
                var voertuigen = new List<Voertuig>();
                while (reader.Read())
                {
                    if (voertuigen.All(v=> v.Id != (int)reader[0]))
                    {
                        voertuig = new Voertuig((int)reader[0], (string)reader[1], (string)reader[2],
                           (string)reader[3],
                           (string)reader[4], new BrandstofType((int)reader[10], (string)reader[14]),
                           new WagenType((int)reader[9], (string)reader[12]));
                        voertuigen.Add(voertuig);
                        if (reader[15] != DBNull.Value)
                        {
                            var bestuurder = new Bestuurder((int)reader[15], (string)reader[16], (string)reader[18],
                                (DateTime)reader[18], (string)reader[19], new List<RijbewijsType>(), (bool)reader[20]);

                            if (reader[23] != DBNull.Value)
                            {
                                bestuurder.ZetAdres(new Adres((string)reader[23], (string)reader[24], (string)reader[27], (string)reader[25], (string)reader[26]));
                            }

                            if (reader[28] != DBNull.Value)
                            {
                                bestuurder.VoegRijbewijsTypeToe(new RijbewijsType((int)reader[28], (string)reader[30]));
                            }

                            if (reader[21] != DBNull.Value)
                            {
                                bestuurder.ZetTankkaart(new Tankkaart((int)reader[21], (string)reader[33], (DateTime)reader[34], (string)reader[35], (bool)reader[37], (bool)reader[36], new List<BrandstofType>()));

                                if (reader[40] != DBNull.Value)
                                {
                                    bestuurder.Tankkaart.VoegBrandstofTypeToe(new BrandstofType((int)reader[40], (string)reader[41]));
                                }
                            }

                            voertuig.ZetBestuurder(bestuurder);
                        }
                    }
                    else
                    {
                        if (reader[15] == DBNull.Value) continue;
                        if (reader[40] != DBNull.Value && voertuig.Bestuurder.Tankkaart.GeefBrandstofTypes().All(b => b.Id != (int)reader[40]))
                        {
                            voertuig.Bestuurder.Tankkaart.VoegBrandstofTypeToe(new BrandstofType((int)reader[40], (string)reader[41]));
                        }

                        if (reader[28] != DBNull.Value && voertuig.Bestuurder.GeefRijbewijsTypes().All(r => r.Id != (int)reader[28]))
                        {
                            voertuig.Bestuurder.VoegRijbewijsTypeToe(new RijbewijsType((int)reader[28], (string)reader[30]));
                        }
                    }
                }

                return voertuigen;
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

        public Voertuig GeefVoertuig(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                var command = new SqlCommand("select * from dbo.Voertuigen v " +
                                             "left join dbo.WagenTypes w on v.WagenTypeId= w.Id " +
                                             "left join  dbo.BrandstofTypes bt ON v.BrandstofId = bt.Id " +
                                             "left join dbo.Bestuurders b on v.Id = b.VoertuigId " +
                                             "left JOIN dbo.RijbewijsTypes_Bestuurders rb on b.Id = rb.BestuurderId " +
                                             "left JOIN dbo.RijbewijsTypes r on rb.RijbewijsTypeId = r.Id " +
                                             "left join dbo.Tankkaarten t on b.TankkaartId = t.Id " +
                                             "left join dbo.Tankkaarten_BrandstofTypes tb on tb.TankkaartId = t.Id " +
                                             "left join dbo.BrandstofTypes btb on tb.BrandstofTypeId = btb.Id where v.Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                var reader = command.ExecuteReader();
                if (!reader.HasRows) throw new VoertuigRepoException(nameof(GeefVoertuig) + " - Geen voertuig gevonden");
                Voertuig voertuig = null;
                while (reader.Read())
                {
                    if (voertuig == null)
                    {
                        voertuig = new Voertuig((int)reader[0], (string)reader[1], (string)reader[2],
                           (string)reader[3],
                           (string)reader[4], new BrandstofType((int)reader[10], (string)reader[14]),
                           new WagenType((int)reader[9], (string)reader[12]));
                        if (reader[15] != DBNull.Value)
                        {
                            var bestuurder = new Bestuurder((int)reader[15], (string)reader[16], (string)reader[18],
                                (DateTime)reader[18], (string)reader[19], new List<RijbewijsType>(), (bool)reader[20]);

                            if (reader[23] != DBNull.Value)
                            {
                                bestuurder.ZetAdres(new Adres((string)reader[23], (string)reader[24], (string)reader[27], (string)reader[25], (string)reader[26]));
                            }

                            if (reader[28] != DBNull.Value)
                            {
                                bestuurder.VoegRijbewijsTypeToe(new RijbewijsType((int)reader[28], (string)reader[30]));
                            }

                            if (reader[21] != DBNull.Value)
                            {
                                bestuurder.ZetTankkaart(new Tankkaart((int)reader[21], (string)reader[33], (DateTime)reader[34], (string)reader[35], (bool)reader[37], (bool)reader[36], new List<BrandstofType>()));

                                if (reader[40] != DBNull.Value)
                                {
                                    bestuurder.Tankkaart.VoegBrandstofTypeToe(new BrandstofType((int)reader[40], (string)reader[41]));
                                }
                            }

                            voertuig.ZetBestuurder(bestuurder);
                        }
                    }
                    else
                    {
                        if (reader[15] == DBNull.Value) continue;
                        if (reader[40] != DBNull.Value &&  voertuig.Bestuurder.Tankkaart.GeefBrandstofTypes().All(b => b.Id != (int)reader[40]))
                        {
                            voertuig.Bestuurder.Tankkaart.VoegBrandstofTypeToe(new BrandstofType((int)reader[40], (string)reader[41]));
                        }

                        if (reader[28] != DBNull.Value && voertuig.Bestuurder.GeefRijbewijsTypes().All(r => r.Id != (int)reader[28]))
                        {
                            voertuig.Bestuurder.VoegRijbewijsTypeToe(new RijbewijsType((int)reader[28], (string)reader[30]));
                        }
                    }
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
