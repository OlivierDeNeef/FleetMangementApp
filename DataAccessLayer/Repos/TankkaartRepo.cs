﻿using DomainLayer.Interfaces.Repos;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
            const string query = "SELECT dbo.Tankkaarten.Id, Kaartnummer, Geldigheidsdatum, Pincode,Gearchiveerd ,Geblokkeerd ,BrandstoftypeId ,[Type] FROM dbo.Tankkaarten INNER JOIN dbo.Tankkaarten_Brandstoftypes on Id=TankkaartId INNER JOIN dbo.BrandstofTypes on BrandstofTypeId=dbo.brandstoftypes.Id    WHERE dbo.Tankkaarten.Id=@id;";
            using var command = connection.CreateCommand();
            try
            {
                command.CommandText = query;
                command.Connection = connection;
                command.Parameters.AddWithValue("@id", id);
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

                return new Tankkaart(tankkaartid,kaartnummer,geldigheidsDatum,pincode,bestuurder,geblokkeerd,gearchiveerd, brandstofTypes);
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


        public IReadOnlyList<Tankkaart> GeefGefilterdeTankkaarten([Optional] int id, [Optional] string kaartnummer, [Optional] DateTime geldigheidsdatum, [Optional] List<BrandstofType> lijstBrandstoftypes, [Optional] bool geachiveerd)
        {
            throw new NotImplementedException();
        }


        public void UpdateTankkaart(Tankkaart t)
        {
            throw new NotImplementedException();
        }

        public void VoegTankkaartToe(Tankkaart t)
        {
            throw new NotImplementedException();
        }
    }
}
