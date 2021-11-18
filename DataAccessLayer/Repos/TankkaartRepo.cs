using DomainLayer.Interfaces.Repos;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Exceptions.Repos;
using DomainLayer.Exceptions.Managers;

namespace DataAccessLayer.Repos
{
    public class TankkaartRepo: ITankkaartRepo
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        private readonly IBestuurderRepo _bestuurderRepo;

        public TankkaartRepo(IConfiguration config ,IBestuurderRepo bestuurderRepo)
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
            var brandstofTypes = new List<BrandstofType>();
            var connection = new SqlConnection(_connectionString);
            const string query = "SELECT dbo.Tankkaarten.Id, Kaartnummer, Geldigheidsdatum, Pincode,Gearchiveerd ,Geblokkeerd ,BrandstoftypeId ,[Type] FROM dbo.Tankkaarten INNER JOIN dbo.Tankkaarten_Brandstoftypes on Id=TankkaartId INNER JOIN dbo.BrandstofTypes on BrandstofTypeId=dbo.brandstoftypes.Id    WHERE dbo.Tankkaarten.Id=@brandstofTypeId;";
            using var command = connection.CreateCommand();
            try
            {
                command.CommandText = query;
                command.Connection = connection;
                command.Parameters.AddWithValue("@brandstofTypeId", id);
                connection.Open();
                var reader = command.ExecuteReader();
                var eersteIteratie = true;
                if(!reader.HasRows) throw new TankkaartRepoException("TankkaartId bestaat niet");
                var tankkaartid = 0;
                string kaartnummer = null;
                var geblokkeerd = false;
                DateTime geldigheidsDatum = default;
                string pincode = null;
                var gearchiveerd = false;
                while (reader.Read())
                {
                    if (eersteIteratie)
                    {
                        tankkaartid = (int) reader["Id"];
                        kaartnummer = (string) reader["Kaartnummer"];
                        geldigheidsDatum = (DateTime) reader["GeldigheidsDatum"];
                        pincode = (string) reader["Pincode"];
                        gearchiveerd = (bool) reader["Gearchiveerd"];
                        geblokkeerd = (bool) reader["Geblokkeerd"];
                        eersteIteratie = false;
                    }
                    brandstofTypes.Add(new BrandstofType((int)reader["BrandstoftypeId"],(string)reader["Type"]));
                }

                var bestuurder = _bestuurderRepo.GeefBestuurderMetTankkaart(tankkaartid); //TODO

                return new Tankkaart(tankkaartid,kaartnummer,geldigheidsDatum,pincode,bestuurder,geblokkeerd,gearchiveerd,brandstofTypes);
            }
            catch (Exception e)
            {
                throw new TankkaartRepoException("GeefBestuurder - Er ging iets mis", e);
            }
            finally
            {
                connection.Close();
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
                if (first)query += ", ";
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
