using DataAccessLayer.Exceptions.Repos;
using DomainLayer.Exceptions.Managers;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;



namespace DataAccessLayer.Repos
{
    public class BestuurderRepo
    {
        //0	bestuurderId
        //1	Naam
        //2	Voornaam
        //3	Geboortedatum
        //4	rijksregisternummer
        //5	Bestuurder gearchiveerd
        //6	TankkaartId
        //7	VoertuigId
        //8	Straat
        //9	huisnummer
        //10 postcode
        //11 land
        //12 stad
        //13 rijbewijsTypeId
        //15 Type van rijbewijs
        //18 Merk
        //19 model
        //20 chassisnummer
        //21 nummerplaat
        //22 Voertuig gearchiveerd
        //23 kleur
        //24 aantaldeuren
        //25 Hybride
        //26 WagenTypeId
        //27 brandstof id voor voertuig
        //29 type wagen voor voertuig
        //31 type brandstof voor voertuig
        //33 Kaartnummer
        //34 Geldigheidsdatum
        //35 pincode
        //36 Tankkaart gearchiveerd
        //37 Geblokkeerd
        //39 Brandstof id voor tankkaart
        //40 type brandstof voor tankkaart


        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        public BestuurderRepo(IConfiguration config)
        {
            _configuration = config;
            _connectionString = config.GetConnectionString("defaultConnection");
        }
        public BestuurderRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        //ask: filter op een list
        public IReadOnlyList<Bestuurder> GeefGefilderdeBestuurders(string voornaam, string naam, DateTime geboortedatum, List<RijbewijsType> lijstRijbewijstypes, string rijksregisternummer, bool gearchiveerd)
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                var cmd = new SqlCommand();
                var query = "SELECT * FROM dbo.Bestuurders b " +
                            "left JOIN dbo.RijbewijsTypes_Bestuurders rb on b.Id = rb.BestuurderId " +
                            "left JOIN dbo.RijbewijsTypes r on rb.RijbewijsTypeId = r.Id " +
                            "left join dbo.Voertuigen v on b.VoertuigId=v.Id " +
                            "left join dbo.WagenTypes w on v.WagenTypeId = w.Id " +
                            "left join dbo.BrandstofTypes bta on v.BrandstofId = bta.Id " +
                            "left join dbo.Tankkaarten t on b.TankkaartId = t.Id " +
                            "left join dbo.Tankkaarten_BrandstofTypes tb on tb.TankkaartId = t.Id " +
                            "left join dbo.BrandstofTypes btb on tb.BrandstofTypeId = btb.Id where b.Gearchiveerd =@Gearchiveerd ";

                cmd.Parameters.AddWithValue("@Gearchiveerd", gearchiveerd);

                bool next = false;
                if (string.IsNullOrWhiteSpace(voornaam))
                {
                    query += ", b.voornaam = @Voornaam";
                    cmd.Parameters.AddWithValue("@Voornaam", voornaam);
                    next = true;
                }

                if (string.IsNullOrWhiteSpace(naam))
                {
                    if (next) query += ", ";
                    query += "b.Naam = @Voornaam";
                    cmd.Parameters.AddWithValue("@Voornaam", voornaam);
                    next = true;
                }

                if (geboortedatum == DateTime.MinValue)
                {
                    if (next) query += ", ";
                    query += "b.Geboortedatum = @Geboortedatum";
                    cmd.Parameters.AddWithValue("@Geboortedatum", geboortedatum);
                    next = true;
                }
                if (string.IsNullOrWhiteSpace(rijksregisternummer))
                {
                    if (next) query += ", ";
                    query += "b.Naam = @Voornaam";
                    cmd.Parameters.AddWithValue("@Voornaam", voornaam);
                }

                cmd.Connection = connection;
                cmd.CommandText = query;
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows) throw new BestuurderRepoException(nameof(GeefBestuurder) + " - Geen bestuurder gevonden");
                var bestuurders = new List<Bestuurder>();
                Bestuurder bestuurder = null;
                while (reader.Read())
                {
                    if (bestuurders.All(b => b.Id != (int)reader[0]))
                    {
                        bestuurder = new Bestuurder((int)reader[0], (string)reader[1], (string)reader[2],
                            (DateTime)reader[3], (string)reader[4], new List<RijbewijsType>(), (bool)reader[5]);

                        bestuurders.Add(bestuurder);
                        if (reader[8] != DBNull.Value)
                        {
                            bestuurder.ZetAdres(new Adres((string)reader[8], (string)reader[9], (string)reader[12], (string)reader[10], (string)reader[11]));
                        }

                        if (reader[13] != DBNull.Value)
                        {
                            bestuurder.VoegRijbewijsTypeToe(new RijbewijsType((int)reader[13], (string)reader[15]));
                        }

                        if (reader[7] != DBNull.Value)
                        {
                            var voertuig = new Voertuig((int)reader[7], (string)reader[18], (string)reader[19],
                                (string)reader[20],
                                (string)reader[21], new BrandstofType((int)reader[27], (string)reader[31]),
                                new WagenType((int)reader[26], (string)reader[29]));
                            bestuurder.ZetVoertuig(voertuig);
                        }

                        if (reader[6] != DBNull.Value)
                        {
                            bestuurder.ZetTankkaart(new Tankkaart((int)reader[6], (string)reader[33], (DateTime)reader[34], (string)reader[35], (bool)reader[37], (bool)reader[36], new List<BrandstofType>()));

                            if (reader[39] != DBNull.Value)
                            {
                                bestuurder.Tankkaart.VoegBrandstofTypeToe(new BrandstofType((int)reader[39], (string)reader[40]));
                            }
                        }
                    }
                    else
                    {
                        if (reader[13] != DBNull.Value && bestuurder.GeefRijbewijsTypes().All(r => r.Id != (int)reader[13]))
                        {
                            bestuurder.VoegRijbewijsTypeToe(new RijbewijsType((int)reader[13], (string)reader[15]));
                        }

                        if (reader[6] == DBNull.Value) continue;
                        if (reader[39] != DBNull.Value && bestuurder.Tankkaart.GeefBrandstofTypes().All(b => b.Id != (int)reader[20]))
                        {
                            bestuurder.Tankkaart.VoegBrandstofTypeToe(new BrandstofType((int)reader[39], (string)reader[40]));
                        }
                    }
                }
                return bestuurders;
            }
            catch (Exception e)
            {
                throw new BestuurderManagerException("GeefBestuurder - Er ging iets mis", e);
            }
        }
        public void VoegBestuurderToe(Bestuurder bestuurder)
        {
            SqlTransaction transaction = null;
            using var connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();

                var command = new SqlCommand(
                    "INSERT INTO dbo.BESTUURDERS (Naam, Voornaam,  Geboortedatum, Rijksregisternummer, Straat, Busnummer, Huisnummer, Stad, Postcode, Land, TankkaartId, VoertuigId, Gearchiveerd) " +
                    "OUTPUT INSERTED.Id VALUES (@Naam, @Voornaam, @Geboortedatum, @Rijksregisternummer, @straat, @busnummer, @huisnummer, @Stad, @Postcode, @Land, @TankkaartenId, @VoertuigId, @IsGearchiveerd)", connection, transaction);
                command.Parameters.AddWithValue("@Naam", bestuurder.Naam);
                command.Parameters.AddWithValue("@Voornaam", bestuurder.Voornaam);
                command.Parameters.AddWithValue("@Geboortedatum", bestuurder.Geboortedatum);
                command.Parameters.AddWithValue("@Rijksregisternummer", bestuurder.Rijksregisternummer);
                command.Parameters.AddWithValue("@straat", (object)bestuurder.Adres?.Straat ?? DBNull.Value);
                command.Parameters.AddWithValue("@huisnummer", (object)bestuurder.Adres?.Huisnummer ?? DBNull.Value);
                command.Parameters.AddWithValue("@Stad", (object)bestuurder.Adres?.Stad ?? DBNull.Value);
                command.Parameters.AddWithValue("@Postcode", (object)bestuurder.Adres?.Postcode ?? DBNull.Value);
                command.Parameters.AddWithValue("@Land", (object)bestuurder.Adres?.Land ?? DBNull.Value);
                command.Parameters.AddWithValue("@TankkaartenId", (object)bestuurder.Tankkaart?.Id ?? DBNull.Value);
                command.Parameters.AddWithValue("@VoertuigId", (object)bestuurder.Voertuig?.Id ?? DBNull.Value);
                command.Parameters.AddWithValue("@IsGearchiveerd", bestuurder.IsGearchiveerd);
                var id = (int)command.ExecuteScalar();

                foreach (var rijbewijsType in bestuurder.GeefRijbewijsTypes())
                {
                    var command3 = new SqlCommand("insert into dbo.RijbewijsTypes_Bestuurders (BestuurderId,RijbewijsTypeId) values (@Id,@RijbeswijsTypeId)", connection, transaction);
                    command3.Parameters.AddWithValue("@Id", id);
                    command3.Parameters.AddWithValue("@RijbewijsTypeId", rijbewijsType.Id);
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
                    throw new BestuurderManagerException("VoegBestuurderToe - Er ging iets mis. Rollback uitgevoerd ", exception);
                }
                throw new BestuurderManagerException("VoegBestuurderToe - Er ging iets mis. Rollback uitgevoerd ", e);
            }
            finally
            {
                connection.Close();
            }
        }
        public bool BestaatBestuurder(int bestuurderId)
        {
            var connection = new SqlConnection(_connectionString);
            const string query = "SELECT * FROM dbo.BESTUURDERS WHERE Id = @Id";
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
            SqlTransaction transaction = null;
            using var connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();

                var command1 = new SqlCommand("delete dbo.RijbewijsTypes_Bestuurders where BestuurderId=@Id", connection, transaction);
                command1.Parameters.AddWithValue("@Id", id);
                command1.ExecuteNonQuery();

                var command2 = new SqlCommand("DELETE FROM dbo.BESTUURDERS WHERE Id = @id", connection, transaction);
                command2.Connection = connection;
                command2.Parameters.AddWithValue("@id", id);
                command2.ExecuteNonQuery();

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
                    Console.WriteLine(exception);
                    throw new BrandstofTypeManagerException("VerwijderBestuurder - Er liep iets mis. Rollback niet Uitgevoerd", exception);
                }
                throw new BrandstofTypeManagerException("VerwijderBestuurder - Er liep iets mis. Rollback Uitgevoerd", e);
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdateBestuurder(Bestuurder bestuurder)
        {
            SqlTransaction transaction = null;
            using var connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                var command1 = new SqlCommand(
                    "UPDATE BESTUURDERS SET Naam=@Naam, Voornaam=@Voornaam, Geboortedatum=@Geboordedatum, Rijksregisternummer=@Rijksregisternummer, Straat=@Straat, " +
                    "Busnummer=@Busnummer, Gearchiveerd=@Gearchiveerd, TankkaartId=@TankkaartId, VoertuigId=@VoertuigId,  " +
                    "Huisnummer=@Huisnummer, Stad=@Stad, Postcode=@Postcode, Land=@Land", connection, transaction);
                command1.Parameters.AddWithValue("@Naam", bestuurder.Naam); // ok
                command1.Parameters.AddWithValue("@Voornaam", bestuurder.Voornaam); // ok
                command1.Parameters.AddWithValue("@Geboortedatum", bestuurder.Geboortedatum); //ok 
                command1.Parameters.AddWithValue("@Rijksregisternummer", bestuurder.Rijksregisternummer); //ok
                command1.Parameters.AddWithValue("@Straat", (object)bestuurder.Adres?.Straat ?? DBNull.Value); //ok
                command1.Parameters.AddWithValue("@Huisnummer", (object)bestuurder.Adres?.Huisnummer ?? DBNull.Value); //ok
                command1.Parameters.AddWithValue("@Stad", (object)bestuurder.Adres?.Stad ?? DBNull.Value); //ok
                command1.Parameters.AddWithValue("@Postcode", (object)bestuurder.Adres?.Postcode ?? DBNull.Value); //ok
                command1.Parameters.AddWithValue("@Land", (object)bestuurder.Adres?.Land ?? DBNull.Value); //ok
                command1.Parameters.AddWithValue("@TankkaartenId", (object)bestuurder.Tankkaart?.Id ?? DBNull.Value); //k
                command1.Parameters.AddWithValue("@VoertuigId", (object)bestuurder.Voertuig?.Id ?? DBNull.Value); //ok
                command1.Parameters.AddWithValue("@IsGearchiveerd", bestuurder.IsGearchiveerd);
                command1.ExecuteNonQuery();

                var command2 = new SqlCommand("delete dbo.RijbewijsTypes_Bestuurders where BestuurderId=@Id", connection, transaction);
                command2.Parameters.AddWithValue("@Id", bestuurder.Id);
                command2.ExecuteNonQuery();

                foreach (var rijbewijsType in bestuurder.GeefRijbewijsTypes())
                {
                    var command3 = new SqlCommand("insert into dbo.RijbewijsTypes_Bestuurders (BestuurderId,RijbewijsTypeId) values (@Id,@RijbeswijsTypeId)", connection, transaction);
                    command3.Parameters.AddWithValue("@Id", bestuurder.Id);
                    command3.Parameters.AddWithValue("@RijbewijsTypeId", rijbewijsType.Id);
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
                    throw new BestuurderManagerException("Update Bestuurder - Er ging iets mis. Rolback niet uitgevoerd", exception);
                }
                throw new BestuurderManagerException("Update Bestuurder - Er ging iets mis. Rolback uitgevoerd", e);
            }
            finally
            {
                connection.Close();
            }
        }





        public Bestuurder GeefBestuurder(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                var cmd = new SqlCommand("SELECT * FROM dbo.Bestuurders b " +
                                         "left JOIN dbo.RijbewijsTypes_Bestuurders rb on b.Id = rb.BestuurderId " +
                                         "left JOIN dbo.RijbewijsTypes r on rb.RijbewijsTypeId = r.Id " +
                                         "left join dbo.Voertuigen v on b.VoertuigId=v.Id " +
                                         "left join dbo.WagenTypes w on v.WagenTypeId = w.Id " +
                                         "left join dbo.BrandstofTypes bta on v.BrandstofId = bta.Id " +
                                         "left join dbo.Tankkaarten t on b.TankkaartId = t.Id " +
                                         "left join dbo.Tankkaarten_BrandstofTypes tb on tb.TankkaartId = t.Id " +
                                         "left join dbo.BrandstofTypes btb on tb.BrandstofTypeId = btb.Id where b.id = @id", connection);

                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                    throw new BestuurderRepoException(nameof(GeefBestuurder) + " - Geen bestuurder gevonden");
                Bestuurder bestuurder = null;
                while (reader.Read())
                {
                    if (bestuurder == null)
                    {
                        bestuurder = new Bestuurder((int)reader[0], (string)reader[1], (string)reader[2],
                            (DateTime)reader[3], (string)reader[4], new List<RijbewijsType>(), (bool)reader[5]);

                        if (reader[8] != DBNull.Value)
                        {
                            bestuurder.ZetAdres(new Adres((string)reader[8], (string)reader[9], (string)reader[12], (string)reader[10], (string)reader[11]));
                        }

                        if (reader[13] != DBNull.Value)
                        {
                            bestuurder.VoegRijbewijsTypeToe(new RijbewijsType((int)reader[13], (string)reader[15]));
                        }

                        if (reader[7] != DBNull.Value)
                        {
                            var voertuig = new Voertuig((int)reader[7], (string)reader[18], (string)reader[19],
                                (string)reader[20],
                                (string)reader[21], new BrandstofType((int)reader[27], (string)reader[31]),
                                new WagenType((int)reader[26], (string)reader[29]));
                            bestuurder.ZetVoertuig(voertuig);
                        }

                        if (reader[6] != DBNull.Value)
                        {
                            bestuurder.ZetTankkaart(new Tankkaart((int)reader[6], (string)reader[33], (DateTime)reader[34], (string)reader[35],  (bool)reader[37], (bool)reader[36], new List<BrandstofType>()));

                            if (reader[39] != DBNull.Value)
                            {
                                bestuurder.Tankkaart.VoegBrandstofTypeToe(new BrandstofType((int)reader[39], (string)reader[40]));
                            }
                        }
                    }
                    else
                    {
                        if (reader[13] != DBNull.Value && bestuurder.GeefRijbewijsTypes().All(r => r.Id != (int)reader[13]))
                        {
                            bestuurder.VoegRijbewijsTypeToe(new RijbewijsType((int)reader[13], (string)reader[15]));
                        }

                        if (reader[6] == DBNull.Value) continue;
                        if (reader[39] != DBNull.Value && bestuurder.Tankkaart.GeefBrandstofTypes().All(b => b.Id != (int)reader[20]))
                        {
                            bestuurder.Tankkaart.VoegBrandstofTypeToe(new BrandstofType((int)reader[39], (string)reader[40]));
                        }
                    }
                }
                return bestuurder;
            }
            catch (Exception e)
            {
                throw new BestuurderManagerException("GeefBestuurder - Er ging iets mis", e);
            }
        }


    }
}
