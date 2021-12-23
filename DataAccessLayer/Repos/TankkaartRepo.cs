using DataAccessLayer.Exceptions.Repos;
using DomainLayer.Interfaces.Repos;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccessLayer.Repos
{
    public class TankkaartRepo : ITankkaartRepo
    {
        //0	tankkaart id
        //1	Kaartnummer
        //2	Geldigheidsdatum
        //3	Pincode
        //4	Tankkaart gearchiveerd
        //5	geblokkeerd
        //8	Brandstof id voor tankkaart
        //9	Brandstof type voor tankkaart
        //10 bestuurderId
        //11 Naam
        //12 Voornaam
        //13 Geboortedatum
        //14 rijksregisternummer
        //15 Bestuurder gearchiveerd
        //17 VoertuigId
        //18 Straat
        //19 huisnummer
        //20 postcode
        //21 land
        //22 stad
        //23 rijbewijsTypeId
        //25 Type van rijbewijs
        //28 Merk
        //29 model
        //30 chassisnummer
        //31 nummerplaat
        //32 Voertuig gearchiveerd
        //33 kleur
        //34 aantaldeuren
        //35 Hybride
        //36 WagenTypeId
        //37 brandstof id voor voertuig
        //39 type wagen voor voertuig
        //41 type brandstof voor voertuig


        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public TankkaartRepo(IConfiguration config)
        {
            _configuration = config;
            _connectionString = config.GetConnectionString("defaultConnection");
        }

        public TankkaartRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool BestaatTankkaart(int tankkaartId)
        {
            var connection = new SqlConnection(_connectionString);
            const string query = "SELECT * FROM dbo.Tankkaarten WHERE (Id = @Id)";
            using var command = connection.CreateCommand();
            connection.Open();
            try
            {
                command.Parameters.AddWithValue("@Id", tankkaartId);
                command.CommandText = query;
                var reader = command.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {
                throw new TankkaartRepoException("BestaatTankkaart - Er ging iets mis", e);
            }
            finally
            {
                connection.Close();
            }
        }

        public Tankkaart GeefTankkaart(int id)
        {
            using SqlConnection con = new(_connectionString);
            try
            {

                SqlCommand commandTankkaart = new("Select * from dbo.Tankkaarten t " +
                                                  "left join dbo.Tankkaarten_BrandstofTypes tb on tb.TankkaartId = t.Id " +
                                                  "left join dbo.BrandstofTypes btb on tb.BrandstofTypeId = btb.Id " +
                                                  "left join dbo.Bestuurders b on t.Id = b.TankkaartId " +
                                                  "left JOIN dbo.RijbewijsTypes_Bestuurders rb on b.Id = rb.BestuurderId " +
                                                  "left JOIN dbo.RijbewijsTypes r on rb.RijbewijsTypeId = r.Id " +
                                                  "left join dbo.Voertuigen v on b.VoertuigId = v.Id " +
                                                  "left join dbo.WagenTypes w on v.WagenTypeId = w.Id " +
                                                  "left join dbo.BrandstofTypes bta on v.BrandstofId = bta.Id where t.Id =@Id ", con);

                commandTankkaart.Parameters.AddWithValue("@Id", id);
                var reader = commandTankkaart.ExecuteReader();
                if (!reader.HasRows) throw new BestuurderRepoException(nameof(GeefTankkaart) + " - Geen tankkaart gevonden");
                Tankkaart tankkaart = null;
                while (reader.Read())
                {
                    //tankkaart
                    if (tankkaart == null)
                    {
                        tankkaart = new Tankkaart((int)reader[0], (string)reader[1], (DateTime)reader[2],
                            (string)reader[3], (bool)reader[5], (bool)reader[4], new List<BrandstofType>());

                        if (reader[8] != DBNull.Value)
                        {
                            tankkaart.VoegBrandstofTypeToe(new BrandstofType((int)reader[8], (string)reader[9]));
                        }
                        //bestuurder
                        if (reader[10] != DBNull.Value)
                        {
                            var bestuurder = new Bestuurder((int)reader[10], (string)reader[11], (string)reader[12],
                                (DateTime)reader[13], (string)reader[14], new List<RijbewijsType>(), (bool)reader[15]);

                            if (reader[18] != DBNull.Value)
                            {
                                bestuurder.ZetAdres(new Adres((string)reader[18], (string)reader[19], (string)reader[22], (string)reader[20], (string)reader[21]));
                            }

                            if (reader[23] != DBNull.Value)
                            {
                                bestuurder.VoegRijbewijsTypeToe(new RijbewijsType((int)reader[23], (string)reader[25]));
                            }

                            if (reader[17] != DBNull.Value)
                            {
                                var voertuig = new Voertuig((int)reader[17], (string)reader[28], (string)reader[29],
                                    (string)reader[30],
                                    (string)reader[31], new BrandstofType((int)reader[37], (string)reader[41]),
                                    new WagenType((int)reader[36], (string)reader[39]));
                                bestuurder.ZetVoertuig(voertuig);
                            }
                            tankkaart.ZetBestuurder(bestuurder);
                        }
                    }
                    else
                    {

                        if (reader[8] != DBNull.Value && tankkaart.GeefBrandstofTypes().All(b => b.Id != (int)reader[8]))
                        {
                            tankkaart.VoegBrandstofTypeToe(new BrandstofType((int)reader[8], (string)reader[9]));
                        }

                        if (reader[10] == DBNull.Value) continue;
                        if (reader[23] != DBNull.Value && tankkaart.Bestuurder.GeefRijbewijsTypes().All(r => r.Id != (int)reader[23]))
                        {
                            tankkaart.Bestuurder.VoegRijbewijsTypeToe(new RijbewijsType((int)reader[23], (string)reader[25]));
                        }


                    }
                }
                return tankkaart;
            }
            catch (Exception e)
            {
                throw new TankkaartRepoException(nameof(GeefTankkaart) + " - Er ging iets mis.", e);
            }
            finally
            {
                con.Close();
            }
        }

        public void VoegTankkaartToe(Tankkaart tankkaart)
        {
            SqlTransaction transaction = null;
            using var connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                var command = new SqlCommand(
                    "INSERT INTO [dbo].[Tankkaarten]  ([Kaartnummer],[Geldigheidsdatum],[Pincode],[Gearchiveerd] ,[Geblokkeerd]) OUTPUT INSERTED.Id VALUES (@kaartnummer, @geldigheidsdatum, @pincode, @isGeblokkeerd, @isGearchiveerd)",
                    connection, transaction);

                command.Parameters.AddWithValue("@kaartnummer", tankkaart.Kaartnummer);
                command.Parameters.AddWithValue("@geldigheidsdatum", tankkaart.Geldigheidsdatum);
                command.Parameters.AddWithValue("@pincode", tankkaart.Pincode);
                command.Parameters.AddWithValue("@isGeblokkeerd", tankkaart.IsGeblokkeerd);
                command.Parameters.AddWithValue("@isGearchiveerd", tankkaart.IsGearchiveerd);
                var id = (int)command.ExecuteScalar();

                foreach (var brandstofType in tankkaart.GeefBrandstofTypes())
                {
                    var command2 = new SqlCommand("INSERT into dbo.Tankkaarten_Brandstoftypes (TankkaartId, BrandstofTypeId) VALUES (@tankkaartId, @brandstofTypeId)",
                            connection, transaction);
                    command2.Parameters.AddWithValue("@brandstofTypeId", brandstofType.Id);
                    command2.Parameters.AddWithValue("@tankkaartId", id);
                    command2.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            catch (Exception e)
            {
                try
                {
                    transaction?.Rollback();
                }
                catch (Exception exception)
                {
                    throw new TankkaartRepoException("VoegTankkaartToe - Er ging iets mis. Rollback uitgevoerd ", exception);
                }
                throw new TankkaartRepoException("VoegTankkaartToe - Er ging iets mis. Rollback uitgevoerd ", e);
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdateTankkaart(Tankkaart tankkaart)
        {
            SqlTransaction transaction = null;
            using var connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                var command = new SqlCommand("UPDATE dbo.Tankkaarten SET Kaartnummer=@Kaartnummer, Geldigheidsdatum = @Geldigheidsdatum, Pincode=@Pincode, Gearchiveerd =@Gearchiveerd, Geblokkeerd = @Geblokkeerd where Id=@Id",
                        connection, transaction);
                command.Parameters.AddWithValue("@Kaartnummer", tankkaart.Kaartnummer);
                command.Parameters.AddWithValue("@Geldigheidsdatum", tankkaart.Geldigheidsdatum);
                command.Parameters.AddWithValue("@Pincode", tankkaart.Pincode);
                command.Parameters.AddWithValue("@Geblokkeerd", tankkaart.IsGeblokkeerd);
                command.Parameters.AddWithValue("@Gearchiveerd", tankkaart.IsGearchiveerd);
                command.Parameters.AddWithValue("@Id", tankkaart.Id);
                command.ExecuteNonQuery();

                var command2 = new SqlCommand("DELETE FROM dbo.Tankkaarten_BrandstofTypes where TankkaartId=@Id", connection, transaction);
                command2.Parameters.AddWithValue("@Id", tankkaart.Id);
                command2.ExecuteNonQuery();

                foreach (var brandstofType in tankkaart.GeefBrandstofTypes())
                {
                    var command3 = new SqlCommand("INSERT into dbo.Tankkaarten_Brandstoftypes (TankkaartId, BrandstofTypeId) VALUES (@tankkaartId, @brandstofTypeId)",
                        connection, transaction);
                    command3.Parameters.AddWithValue("@brandstofTypeId", brandstofType.Id);
                    command3.Parameters.AddWithValue("@tankkaartId", tankkaart.Id);
                    command3.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            catch (Exception e)
            {
                try
                {
                    transaction?.Rollback();
                }
                catch (Exception exception)
                {
                    throw new TankkaartRepoException("Update Tankkaart - Er ging iets mis. Rollback niet uitgevoerd", exception);
                }
                throw new TankkaartRepoException("Update Tankkaart - Er ging iets mis. Rollback uitgevoerd", e);
            }
            finally
            {
                connection.Close();
            }
        }

        //ASK :TankkaartRepo --> GeefGefilterdeTankkaarten: Hoe filteren op een lijst met brandstoffen?
        public IReadOnlyList<Tankkaart> GeefGefilterdeTankkaarten(string kaartnummer, DateTime geldigheidsdatum, List<BrandstofType> lijstBrandstoftypes, bool geachiveerd)
        {
            using SqlConnection con = new(_connectionString);
            try
            {
                var cmd = new SqlCommand();
                var query = "Select * from dbo.Tankkaarten t " +
                            "left join dbo.Tankkaarten_BrandstofTypes tb on tb.TankkaartId = t.Id " +
                            "left join dbo.BrandstofTypes btb on tb.BrandstofTypeId = btb.Id " +
                            "left join dbo.Bestuurders b on t.Id = b.TankkaartId " +
                            "left JOIN dbo.RijbewijsTypes_Bestuurders rb on b.Id = rb.BestuurderId " +
                            "left JOIN dbo.RijbewijsTypes r on rb.RijbewijsTypeId = r.Id " +
                            "left join dbo.Voertuigen v on b.VoertuigId = v.Id " +
                            "left join dbo.WagenTypes w on v.WagenTypeId = w.Id " +
                            "left join dbo.BrandstofTypes bta on v.BrandstofId = bta.Id where t.Gearchiveerd =@Gearchiveerd  ";

                cmd.Parameters.AddWithValue("@Gearchiveerd", geachiveerd);
                bool next = false;
                if (!string.IsNullOrWhiteSpace(kaartnummer))
                {
                    query += ", t.Kaartnummer=@Kaartnummer";
                    cmd.Parameters.AddWithValue("@Kaartnummer", kaartnummer);
                    next = true;
                }
                if (geldigheidsdatum != DateTime.MinValue)
                {
                    if (next) query += ", ";
                    query += "t.Geldigheidsdatum = @Geldigheidsdatum";
                    cmd.Parameters.AddWithValue("@Geldigheidsdatum", geldigheidsdatum);
                    next = true;
                }

                cmd.Connection = con;
                cmd.CommandText = query;
                con.Open();
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows) throw new BestuurderRepoException(nameof(GeefTankkaart) + " - Geen tankkaart gevonden");
                Tankkaart tankkaart = null;
                var tankkaarten = new List<Tankkaart>();
                while (reader.Read())
                {
                    //tankkaart
                    if (tankkaarten.All(t => t.Id != (int)reader[0]))
                    {
                        tankkaart = new Tankkaart((int)reader[0], (string)reader[1], (DateTime)reader[2],
                            (string)reader[3], (bool)reader[5], (bool)reader[4], new List<BrandstofType>());
                        tankkaarten.Add(tankkaart);
                        if (reader[8] != DBNull.Value)
                        {
                            tankkaart.VoegBrandstofTypeToe(new BrandstofType((int)reader[8], (string)reader[9]));
                        }
                        //bestuurder
                        if (reader[10] != DBNull.Value)
                        {
                            var bestuurder = new Bestuurder((int)reader[10], (string)reader[11], (string)reader[12],
                                (DateTime)reader[13], (string)reader[14], new List<RijbewijsType>() { new((int)reader[23], (string)reader[25]) }, (bool)reader[15]);

                            if (reader[18] != DBNull.Value)
                            {
                                bestuurder.ZetAdres(new Adres((string)reader[18], (string)reader[19], (string)reader[22], (string)reader[20], (string)reader[21]));
                            }

                            if (reader[23] != DBNull.Value && bestuurder.GeefRijbewijsTypes().All(r => r.Id != (int)reader[23]))
                            {
                                bestuurder.VoegRijbewijsTypeToe(new RijbewijsType((int)reader[23], (string)reader[25]));
                            }

                            if (reader[17] != DBNull.Value)
                            {
                                var voertuig = new Voertuig((int)reader[17], (string)reader[28], (string)reader[29],
                                    (string)reader[30],
                                    (string)reader[31], new BrandstofType((int)reader[37], (string)reader[41]),
                                    new WagenType((int)reader[36], (string)reader[39]));
                                bestuurder.ZetVoertuig(voertuig);
                            }
                            tankkaart.ZetBestuurder(bestuurder);
                        }
                    }
                    else
                    {

                        if (reader[8] != DBNull.Value && tankkaart.GeefBrandstofTypes().All(b => b.Id != (int)reader[8]))
                        {
                            tankkaart.VoegBrandstofTypeToe(new BrandstofType((int)reader[8], (string)reader[9]));
                        }

                        if (reader[10] == DBNull.Value) continue;
                        if (reader[23] != DBNull.Value && tankkaart.Bestuurder.GeefRijbewijsTypes().All(r => r.Id != (int)reader[23]))
                        {
                            tankkaart.Bestuurder.VoegRijbewijsTypeToe(new RijbewijsType((int)reader[23], (string)reader[25]));
                        }


                    }
                }
                return tankkaarten;
            }
            catch (Exception e)
            {
                throw new TankkaartRepoException(nameof(GeefGefilterdeTankkaarten) + " - Er ging iets mis.", e);
            }
            finally
            {
                con.Close();
            }
        }



    }
}
