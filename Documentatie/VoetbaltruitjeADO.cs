using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BuisinessLayer;
using BuisinessLayer.Interfaces;
using BuisinessLayer.Model;

namespace ADOLayer
{
    //ploegen zitten maar in één competitie (op één moment)
    // PloegCompetitie is enkel voor het vullen van de dropdown.
    // In truitje vind je de details van dat truitje bij welke ploeg en competitie ze horen. er is geen rechtstreekse verwijzing.
    // seizoen in PloegCompetiite hoeft niet.
    public class VoetbaltruitjeADO : IVoetbaltruitjeRepository
    {
        private string _connection;

        public VoetbaltruitjeADO(string connectionString)
        {
            _connection = connectionString;
        }
        private SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection(_connection);
            return connection;

        }

        public Voetbaltruitje GeefVoetbaltruitje(int id)
        {
            Voetbaltruitje v = null;
            SqlCommand comm = new SqlCommand();
            SqlConnection conn = getConnection();
            string query = "SELECT * FROM dbo.Voetbaltruitje WHERE id = @id";
            comm.Parameters.AddWithValue("@id", id);
            conn.Open();
            comm.CommandText = query;
            var reader = comm.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                Club club = new Club(reader.GetString(0), reader.GetString(1));
                ClubSet clubset = new ClubSet(reader.GetBoolean(0), reader.GetInt32(1));
                Kledingmaat kledingmaat = (Kledingmaat)reader.GetInt32(4);
                v = new Voetbaltruitje(id, club, reader.GetString(2), reader.GetDouble(3), kledingmaat,
                    clubset);
            }
            conn.Close();
            return v;
        }

        public IReadOnlyList<Voetbaltruitje> GeefVoetbaltruitjes(string competitie, string club, string seizoen, string kledingmaat,
            int? versie, bool? thuis, double? prijs, bool strikt = true)
        {

            SqlConnection connection = getConnection();
            List<Voetbaltruitje> truitjes = new List<Voetbaltruitje>();
            string query = "SELECT * FROM dbo.Truitjes WHERE ";

            bool AND = false;
            if (!string.IsNullOrEmpty(competitie))
            {
                AND = true;
                if (strikt)
                {
                    query += "competie=@competitie";

                }
                else
                {
                    query += "UPPER(competitie)=UPPER(@competitie)";
                }
            }

            if (!string.IsNullOrEmpty(club))
            {
                if (AND) query += " AND "; else AND = true;
                if (strikt)
                    query += " club = @club ";
                else
                {
                    query += " UPPER(club) = UPPER(@club) ";
                }

            }

            if (!string.IsNullOrEmpty(seizoen))
            {
                if (AND) query += " AND ";
                else AND = true;
                if (strikt)
                    query += " seizoen = @seizoen) ";
                else
                    query += " UPPER(seizoen) = UPPER(@seizoen ";
            }

            if (!string.IsNullOrEmpty(kledingmaat))
            {
                if (AND) query += " AND ";
                else AND = true;
                if (strikt)
                    query += " kledingmaat = @kledingmaat ";
                else
                    query += " UPPER(kleidingmaat) = UPPER(@kledingmaat) ";
            }

            if (versie != null)
            {
                if (AND) query += " AND ";
                else AND = true;
                query += " versie = @versie";

            }

            if (thuis != null)
            {
                if (AND)
                    query += " AND ";
                else
                    AND = true;

                query += " thuis = @thuis";
            }

            if (prijs != null)
            {
                if (AND)
                    query += " AND ";


                query += " prijs = @prijs";
            }

            using (SqlCommand command = connection.CreateCommand())
            {

                command.CommandText = query;
                try
                {
                    connection.Open();
                    if (!string.IsNullOrWhiteSpace(competitie))
                        command.Parameters.AddWithValue("@competitie", competitie);
                    if (!string.IsNullOrWhiteSpace(club))
                        command.Parameters.AddWithValue("@club", club);
                    if (!string.IsNullOrWhiteSpace(seizoen))
                        command.Parameters.AddWithValue("@seizoen", seizoen);
                    if (!string.IsNullOrWhiteSpace(kledingmaat))
                        command.Parameters.AddWithValue("@kledingmaat", kledingmaat);
                    if (versie != null)
                        command.Parameters.AddWithValue("@versie", versie);
                    if (thuis != null)
                        command.Parameters.AddWithValue("@thuis", thuis);
                    if (prijs != null)
                        command.Parameters.AddWithValue("@prijs", prijs);

                    command.CommandText = query;
                    SqlDataReader datareader = command.ExecuteReader();

                    while (datareader.Read())
                    {

                        int voetbaltruitjeId = (int)datareader["voetbaltruitjeId"];
                        string competitieDb = (string)datareader["competitie"];
                        Club clubdb = (Club)datareader["club"];
                        string seizoendb = (string)datareader["seizoen"];
                        Kledingmaat kledingmaatdb = (Kledingmaat)datareader["kledingmaat"];
                        int versiedb = (int)datareader["versie"];
                        //vul aan met kolom
                        double prijsdb = (double)datareader["prijs"];
                        string uitthuisdb = (string)datareader["uitthuis"];

                        bool thuisdb = uitthuisdb == "Thuis";
                        ClubSet clubsetdb = new ClubSet(thuisdb, versiedb);
                        Club c = new Club(competitieDb, club);


                        Voetbaltruitje voetbaltruitje =
                            new Voetbaltruitje(voetbaltruitjeId, c, seizoendb, prijsdb, kledingmaatdb, clubsetdb); //clubset is object van (int) versie en (bool) thuis
                        truitjes.Add(voetbaltruitje);

                    }
                }
                catch (Exception e)
                {
                    throw new VoetbaltruitjeBeheerderException("GeefVoetbalTruitjes - er ging iets mis");
                }
            }
            return truitjes;
        }

        public bool BestaatVoetbaltruitje(int id)
        {
            SqlConnection connection = getConnection();
            string query = "SELECT Count(*) from dbo.Voetbaltruitje WHERE Id = @id";
            SqlCommand command = new SqlCommand();
            try
            {
                connection.Open();
                command.CommandText = query;
                command.Parameters.AddWithValue("@id", id);
                int count = (int)command.ExecuteScalar();
                if (count >= 1) return true;
                else return false;
            }
            catch (Exception e)
            {
                throw new VoetbaltruitjeException("truitje bestaat al: ", e);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool BestaatVoetbaltruitje(Voetbaltruitje truitje)
        {
            SqlConnection connection = getConnection();
            string query = "SELECT Count(*) from dbo.Voetbaltruitje WHERE Id = @id AND Prijs=@Prijs AND Maat=@Maat AND Seizoen=@Seizoen AND" +
                           " Versie=@Verise AND UitThuis=@UitThuis AND Ploeg=@Ploeg AND Competitie=@Competitie"; // moeten alle onderdelene hierbij komen?
            SqlCommand command = new SqlCommand();
            try
            {
                connection.Open();
                command.CommandText = query;
                command.Parameters.AddWithValue("@id", truitje.Id);
                command.Parameters.AddWithValue("@Prijs", truitje.Prijs);
                command.Parameters.AddWithValue("@Maat", truitje.Kledingmaat);
                command.Parameters.AddWithValue("@Seizoen", truitje.Seizoen);
                command.Parameters.AddWithValue("@Versie", truitje.ClubSet.Versie);
                command.Parameters.AddWithValue("@UitThuis", truitje.ClubSet.Thuis);
                command.Parameters.AddWithValue("@Ploeg", truitje.Club.Ploeg);
                command.Parameters.AddWithValue("@Competitie", truitje.Club.Competitie);
                int count = (int)command.ExecuteScalar();
                if (count >= 1) return true;
                else return false;
            }
            catch (Exception e)
            {
                throw new VoetbaltruitjeException("truitje bestaat al: ", e);
            }
            finally
            {
                connection.Close();
            }
        }

        public int VoegVoetbaltruitjeToe(Voetbaltruitje truitje)
        {
            string query =
                "INSERT INTO dbo.Truitje (Prijs, Maat, Seizoen, Versie, UitThuis, Ploeg, Competitie) output INSERTED.Id VALUES(@Prijs, @Maat, @Seizoen, @Versie, @UitThuis, @Ploeg, @Competitie)";

            SqlConnection conn = getConnection();
            conn.Open();
            using SqlCommand command = new SqlCommand(query, conn);
            try
            {
                command.Parameters.AddWithValue("@Prijs", truitje.Prijs);
                command.Parameters.AddWithValue("@Maat", truitje.Kledingmaat);
                command.Parameters.AddWithValue("@Seizoen", truitje.Seizoen);
                command.Parameters.AddWithValue("@Versie", truitje.ClubSet.Versie);
                command.Parameters.AddWithValue("@UitThuis", truitje.ClubSet.Thuis);
                command.Parameters.AddWithValue("@Ploeg", truitje.Club.Ploeg);
                command.Parameters.AddWithValue("@Competitie", truitje.Club.Competitie);
                int newID = (int)command.ExecuteScalar();
                return newID;

            }
            catch (Exception e)
            {
                throw new KlantBeheerderException("VoegTruitjeToe - Er ging iets mis", e);
            }
            finally
            {
                conn.Close();
            }
        }

        public void VerwijderVoetbaltruitje(int id)
        {
            var connection = getConnection();
            const string query = "DELETE FROM dbo.Truitje WHERE Id = @id";
            using var command = connection.CreateCommand();
            try
            {
                connection.Open();
                command.CommandText = query;
                command.Connection = getConnection();
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new KlantBeheerderException("VerwijderTruitje - Er liep iets mis", e);
            }
            finally
            {
                connection.Close();
            }
        }

        public void WijzigVoetbaltruitje(Voetbaltruitje v)
        {
            var connection = getConnection();

            const string query = "UPDATE TRUITJE SET Prijs=@Prijs, Maat=@Maat, Seizoen=@Seizoen, Versie=@Versie, UitThuis=@UitThuis, Ploeg=@Ploeg" +
                                 ", Competitie=@Competitie";

            try
            {
                using var command = connection.CreateCommand();

                connection.Open();
                command.Connection = getConnection();
                command.Parameters.AddWithValue("@Prijs", v.Prijs); // ok
                command.Parameters.AddWithValue("@Maat", v.Kledingmaat); // ok
                command.Parameters.AddWithValue("@Seizoen", v.Seizoen); // ok
                command.Parameters.AddWithValue("@Versie", v.ClubSet.Versie); // ok
                command.Parameters.AddWithValue("@UitThuis", v.ClubSet.Thuis); // ok
                command.Parameters.AddWithValue("@Ploeg", v.Club.Ploeg); // ok
                command.Parameters.AddWithValue("@Competitie", v.Club.Competitie); // ok

                command.CommandText = query;
                command.ExecuteNonQuery();


            }
            catch (Exception e)
            {
                throw new VoetbaltruitjeBeheerderException("Update Truitje - Er ging iets mis", e);
            }
            finally
            {
                connection.Close();
            };
        }
    }
}