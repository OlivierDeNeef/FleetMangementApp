using System.Collections.Generic;
using System.Data.SqlClient;
using DomainLayer.Interfaces.Repos;
using DomainLayer.Models;

namespace DataAccessLayer.Repos
{
    public class BrandstofTypeRepo : IBrandstofTypeRepo
    {

        private readonly SqlConnection _connection;

        public BrandstofTypeRepo(SqlConnection connection)
        {
            _connection = connection;
        }

        public BrandstofType VoegBrandstofTypeToe(BrandstofType brandstofType)
        {
            BrandstofType nieuwType = null;
            SqlCommand command = new SqlCommand();
            command.Connection = _connection;

            command.CommandText = "INSERT INTO dbo.BRANSTOFTYPE (type) VALUES(@type)";
            command.Parameters.AddWithValue("@type", brandstofType.Type);
            _connection.Open();
            int rows = command.ExecuteNonQuery();
            if (rows == 1)
            {
                command.CommandText = "SELECT id from dbo.BRANSTOFTYPE WHERE (type = @type)";
                int key = (int) command.ExecuteScalar();
                nieuwType = new BrandstofType(key, brandstofType.Type);

            }
            _connection.Close();
            return nieuwType;


        } //moet getest worden, link naar databank? SQL script?

        public bool BestaatBrandstofType(BrandstofType brandstofType)
        {
      
            SqlCommand command = new SqlCommand();
            command.Connection = _connection;
            command.CommandText = "SELECT * FROM dbo.BRANSTOFTYPE WHERE (type = @type)";

            command.Parameters.AddWithValue("@type", brandstofType.Type);
         

            _connection.Open();

            var reader = command.ExecuteReader();
            bool bestaatType = reader.HasRows;


            _connection.Close();
            return bestaatType;
        } // dit moet getest worden
        public void VerwijderBrandstofType(BrandstofType brandstofType)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateBrandstofType(BrandstofType brandstofType)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = _connection;
            command.CommandText = "UPDATE BRANDSTOFTYPE SET type = @type where Id = @id";
            command.Parameters.AddWithValue("@type", brandstofType.Type);
            command.Parameters.AddWithValue("@id", brandstofType.Id);
            _connection.Open();
            command.ExecuteNonQuery();
            _connection.Close();
        }

        public IEnumerable<BrandstofType> GeefAlleBrandstofTypes()
        {
            throw new System.NotImplementedException();
        }

    }
}