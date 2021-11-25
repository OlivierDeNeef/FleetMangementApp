using DataAccessLayer.Exceptions.Repos;
using DomainLayer.Interfaces.Repos;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;

namespace DataAccessLayer.Repos
{
    public class TankkaartRepo
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        private readonly IBestuurderRepo _bestuurderRepo;

        public TankkaartRepo(IConfiguration config, IBestuurderRepo bestuurderRepo)
        {
            _configuration = config;
            _bestuurderRepo = bestuurderRepo;
            _connectionString = config.GetConnectionString("defaultConnection");
        }

        public TankkaartRepo(string connectionString, IBestuurderRepo bestuurderRepo)
        {
            _connectionString = connectionString;
            _bestuurderRepo = bestuurderRepo;
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
                throw;
            }
            finally
            {
                con.Close();
            }
        }


        public void VoegTankkaartToe(Tankkaart tankkaart)
        {
            var connection = new SqlConnection(_connectionString);
            const string query =
                "INSERT INTO [dbo].[Tankkaarten]  ([Kaartnummer],[Geldigheidsdatum],[Pincode],[Gearchiveerd] ,[Geblokkeerd]) OUTPUT INSERTED.Id VALUES (@kaartnummer, @geldigheidsdatum, @pincode, @isGeblokkeerd, @isGearchiveerd)";

            try
            {
                using var command = connection.CreateCommand();
                connection.Open();
                command.Parameters.AddWithValue("@kaartnummer", tankkaart.Kaartnummer);
                command.Parameters.AddWithValue("@geldigheidsdatum", tankkaart.Geldigheidsdatum);
                command.Parameters.AddWithValue("@pincode", tankkaart.Pincode);
                command.Parameters.AddWithValue("@isGeblokkeerd", tankkaart.IsGeblokkeerd);
                command.Parameters.AddWithValue("@isGearchiveerd", tankkaart.IsGearchiveerd);
                command.CommandText = query;
                var id = (int)command.ExecuteScalar();
                VoegBrandstofTypesToeAanTankkaart(id, tankkaart.GeefBrandstofTypes());
            }
            catch (Exception e)
            {
                throw new TankkaartRepoException("VoegTankkaartToe - Er ging iets mis ", e);
            }
            finally
            {
                connection.Close();
            }
        }

        private void VoegBrandstofTypesToeAanTankkaart(int id, IEnumerable<BrandstofType> brandstofTypes)
        {
            var connection = new SqlConnection(_connectionString);
            const string query = "INSERT into dbo.Tankkaarten_Brandstoftypes (TankkaartId, BrandstofTypeId) VALUES (@tankkaartId, @brandstofTypeId)";
            foreach (var brandstofType in brandstofTypes)
            {
                try
                {
                    using var command = connection.CreateCommand();
                    connection.Open();
                    command.Parameters.AddWithValue("@brandstofTypeId", brandstofType.Id);
                    command.Parameters.AddWithValue("@tankkaartId", id);
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new TankkaartRepoException("VoegRijbewijstypeToeAanBestuurder - er ging iets mis", e);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void UpdateTankkaart(Tankkaart tankkaart)
        {
            var connection = new SqlConnection(_connectionString);
            const string query = "UPDATE dbo.Tankkaarten SET ";
            try
            {
                using var command = connection.CreateCommand();
                connection.Open();
                command.Parameters.AddWithValue("@kaartnummer", tankkaart.Kaartnummer);
                command.Parameters.AddWithValue("@geldigheidsdatum", tankkaart.Geldigheidsdatum);
                command.Parameters.AddWithValue("@pincode", tankkaart.Pincode);
                command.Parameters.AddWithValue("@isGeblokkeerd", tankkaart.IsGeblokkeerd);
                command.Parameters.AddWithValue("@isGearchiveerd", tankkaart.IsGearchiveerd);
                command.CommandText = query;
                command.ExecuteNonQuery();
                UpdateBrandstofTypesVanTankkaart(tankkaart.Id, tankkaart.GeefBrandstofTypes());
            }
            catch (Exception e)
            {
                throw new TankkaartRepoException("Update Tankkaart - Er ging iets mis", e);
            }
            finally
            {
                connection.Close();
            }
        }

        private void UpdateBrandstofTypesVanTankkaart(int tankkaartId, IReadOnlyList<BrandstofType> brandstofTypes)
        {
            var dbBranstofTypes = GeefAlleBrandstofTypesVanTankkaart(tankkaartId);
            foreach (var dbBrandstofType in dbBranstofTypes)
            {
                if (brandstofTypes.All(x => x.Id != dbBrandstofType.Id))
                {
                    VerwijderBrandstofTypeVanTankkaart(tankkaartId, dbBrandstofType.Id);
                }
            }

            foreach (var brandstofType in brandstofTypes)
            {
                if (dbBranstofTypes.All(x => x.Id != brandstofType.Id))
                {
                    VoegBrandstofTypesToeAanTankkaart(tankkaartId, new List<BrandstofType>() { brandstofType });
                }
            }
        }

        public IReadOnlyList<BrandstofType> GeefAlleBrandstofTypesVanTankkaart(int tankkaartId)
        {
            var connection = new SqlConnection(_connectionString);
            const string querySelect = "SELECT * FROM dbo.Tankkaarten_BrandstofTypes where TankkaartId=@tankkaartId";
            var dbBranstofTypes = new List<BrandstofType>();
            try
            {
                using var command = connection.CreateCommand();
                connection.Open();
                command.Parameters.AddWithValue("@tankkaartId", tankkaartId);
                command.CommandText = querySelect;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var brandstofTypes = new BrandstofType(reader.GetInt32(0), reader.GetString(1));
                    dbBranstofTypes.Add(brandstofTypes);
                }
            }
            catch (Exception e)
            {
                throw new TankkaartRepoException("", e);
            }
            finally
            {
                connection.Close();
            }
            return dbBranstofTypes;
        }

        private void VerwijderBrandstofTypeVanTankkaart(int tankkaartId, int brandstofTypeId)
        {
            var connection = new SqlConnection(_connectionString);
            const string query = "DELETE FROM dbo.Tankkaarten_BrandstofTypes where TankkaartId=@tankkaartId AND BrandstofTypeId=@brandstofTypeId";
            try
            {
                using var command = connection.CreateCommand();
                connection.Open();
                command.Parameters.AddWithValue("@brandstofTypeId", brandstofTypeId);
                command.Parameters.AddWithValue("@TankkaartId", tankkaartId);
                command.CommandText = query;
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new TankkaartRepoException("VerwijderBrandstofTypeVanTankkaat - er ging iets mis", e);
            }
            finally
            {
                connection.Close();
            }
        }


        public IReadOnlyList<Tankkaart> GeefGefilterdeTankkaarten([Optional] string kaartnummer, [Optional] DateTime geldigheidsdatum, [Optional] List<BrandstofType> lijstBrandstoftypes, [Optional] bool geachiveerd)
        {
            var query = "SELECT dbo.Tankkaarten.Id, Kaartnummer, Geldigheidsdatum, Pincode,Gearchiveerd ,Geblokkeerd ,BrandstoftypeId ,[Type] FROM dbo.Tankkaarten INNER JOIN dbo.Tankkaarten_Brandstoftypes on Id=TankkaartId INNER JOIN dbo.BrandstofTypes on BrandstofTypeId=dbo.brandstoftypes.Id WHERE ";
            var first = true;
            if (!string.IsNullOrWhiteSpace(kaartnummer))
            {
                query += "Kaartnummer=@kaartnummer";
                first = false;
            }

            if (geldigheidsdatum != DateTime.MinValue)
            {
                if (first) query += ", ";
                query += "Geldigheidsdatum=@geldigheidsdatum";
                first = false;
            }

            if (lijstBrandstoftypes != null)
            {
                if (first) query += ", ";
                query += "";
            }
            //Ask
            return null;
        }



    }
}
