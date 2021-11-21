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
    public class TankkaartRepo : ITankkaartRepo
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
               
                SqlCommand commandTankkaart = new("SELECT * dbo.Tankkaarten WHERE dbo.Tankkaarten.Id=@Id", con);
                commandTankkaart.Parameters.AddWithValue("@Id", id);
                var readerTankkaart = commandTankkaart.ExecuteReader();

                if (readerTankkaart.HasRows)
                {
                    Tankkaart tankkaart = new((int) readerTankkaart["Id"], (string) readerTankkaart["Kaartnummer"], (DateTime) readerTankkaart["GeldigheidsDatum"]);
                      
                    if(readerTankkaart["Pincode"] != DBNull.Value) tankkaart.ZetPincode((string)readerTankkaart["Pincode"]);
                    if(readerTankkaart["Gearchiveerd"] != DBNull.Value) tankkaart.ZetGearchiveerd((bool)readerTankkaart["Gearchiveerd"]);
                    if(readerTankkaart["Geblokkeerd"] != DBNull.Value &&(bool) readerTankkaart["Geblokkeerd"]) tankkaart.BlokkeerKaart();
                        

                    SqlCommand commandBrandstofTypes = new("SELECT * dbo.Tankkaarten_Brandstoftypes left join dbo.BrandstofTypes on BrandstofTypeId=dbo.brandstoftypes.Id WHERE TankkaartId=@Id", con);
                    commandBrandstofTypes.Parameters.AddWithValue("@Id", tankkaart.Id); 
                    var readerBrandStoffen = commandBrandstofTypes.ExecuteReader();
                    if (readerBrandStoffen.HasRows)
                    {
                        while (readerBrandStoffen.Read())
                        {
                            tankkaart.VoegBrandstofTypeToe(new BrandstofType((int)readerBrandStoffen["BrandstoftypeId"], (string)readerBrandStoffen["Type"]));
                        }
                    }

                    SqlCommand commandBestuurder = new("SELECT * dbo.Bestuurders WHERE TankkaartenId=@Id", con);
                    commandBestuurder.Parameters.AddWithValue("@Id", tankkaart.Id);
                    var readerBestuurder = commandBestuurder.ExecuteReader();
                    if (readerBestuurder.HasRows)
                    { 
                        var rijbewijzen =  new List<RijbewijsType>();
                        SqlCommand commandBestuurderRijbewijzen = new("SELECT * dbo.Bestuurders WHERE TankkaartenId=@Id", con);
                        commandBestuurderRijbewijzen.Parameters.AddWithValue("@Id", (int)readerBestuurder["Id"]);
                        var readerRijbewijzen = commandBestuurderRijbewijzen.ExecuteReader();
                            
                        if (readerRijbewijzen.HasRows)
                        {
                                    
                            while (readerRijbewijzen.Read())
                            {
                                rijbewijzen.Add(new RijbewijsType((int)readerRijbewijzen[0],(string)readerRijbewijzen[1]));
                            }
                        }

                        Bestuurder bestuurder = new((int) readerBestuurder[0], (string) readerBestuurder["Naam"], (string) readerBestuurder["Voornaam"], (DateTime) readerBestuurder["Geboortedatum"], (string) readerBestuurder["Rijksregisternummer"], rijbewijzen, (bool) readerBestuurder["Gearchiveerd"]);

                        if (readerBestuurder["Straat"] != DBNull.Value)
                        {
                            var adres = new Adres((string)readerBestuurder["Straat"], (string)readerBestuurder["Huisnummer"], (string)readerBestuurder["Stad"], (string)readerBestuurder["Postcode"], (string)readerBestuurder["Land"]);
                            bestuurder.ZetAdres(adres);
                        }

                        if (readerBestuurder["VoertuigId"] != DBNull.Value)
                        {
                            SqlCommand commandVoertuig = new("SELECT * dbo.Voertuig WHERE BestuurderId=@Id", con);
                            commandVoertuig.Parameters.AddWithValue("@Id", (int)readerBestuurder["VoertuigId"]);
                            var readerVoeruig = commandBestuurder.ExecuteReader();
                            if (readerVoeruig.HasRows)
                            {
                                //TODO
                            }
                        }
                    }

                    return tankkaart;
                }
                else
                {
                    throw new Exception();
                }
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
            var query = "SELECT dbo.Tankkaarten.Id, Kaartnummer, Geldigheidsdatum, Pincode,Gearchiveerd ,Geblokkeerd ,BrandstoftypeId ,[Type] FROM dbo.Tankkaarten INNER JOIN dbo.Tankkaarten_Brandstoftypes on Id=TankkaartId INNER JOIN dbo.BrandstofTypes on BrandstofTypeId=dbo.brandstoftypes.Id WHERE "
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

        }



    }
}
