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

        //return type aangepast void => BrandstofType
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


        } //return type aangepast void => BrandstofType

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
            SqlCommand command = new SqlCommand();
            command.Connection = _connection;
            command.CommandText = "SELECT * FROM dbo.BRANDSTOFTYPE";

            _connection.Open();

            List<BrandstofType> brandstoftypelijst = new List<BrandstofType>();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var brandstofType = new BrandstofType(reader.GetInt32(0), reader.GetString(1));
                brandstoftypelijst.Add(brandstofType);
            }

            _connection.Close();
            return brandstoftypelijst;
        }
        public void VerwijderBrandstofType(int id) //parameter aangepast
        {
            SqlCommand command = new SqlCommand();
            command.Connection = _connection;
            command.CommandText = "DELETE FROM dbo.BRANDSTOFTYPE WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);


            _connection.Open();
            command.ExecuteNonQuery();
            _connection.Close();

        }



    }
}