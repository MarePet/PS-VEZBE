using Domen;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrokerBaze
{
    public class Broker
    {
        private SqlConnection connection;

        public Broker()
        {
            connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Kolokvijum;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        public void OpenConnection()
        {
            connection.Open();
        }

        public void CloseConnection()
        {
            connection.Close();
        }

        public List<Predmet> VratiSvePredmete()
        {
            List<Predmet> predmeti = new List<Predmet>();
            SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select * from predmet";
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Predmet p = new Predmet { 
                    PredmetId = reader.GetInt32(0),
                    Naziv = reader.GetString(1),
                    ESPB = reader.GetInt32(2)
                };
                predmeti.Add(p);
            }
            return predmeti;
        }

        public List<Prijava> VratiSvePrijave()
        {
            List<Prijava> prijave = new List<Prijava>();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "select * from prijava pr join Predmet p on(pr.PredmetId=p.PredmetId)";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Prijava p = new Prijava
                {
                    PrijavaId = (int)reader[0],
                    Ime = (string)reader[1],
                    Prezime = (string)reader[2],
                    Ocena = (int)reader[3],
                    Predmet = new Predmet
                    {
                        PredmetId = (int)reader[4],
                        Naziv = (string)reader[6],
                        ESPB = (int) reader[7]
                    }
                };
                prijave.Add(p);
            }
            return prijave;
        }

        public void SacuvajPrijavu(Prijava p)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "insert into prijava values (@ime, @prezime, @ocena, @predmet)";
            command.Parameters.AddWithValue("@ime", p.Ime);
            command.Parameters.AddWithValue("@prezime", p.Prezime);
            command.Parameters.AddWithValue("@ocena", p.Ocena);
            command.Parameters.AddWithValue("@predmet", p.Predmet.PredmetId);
            command.ExecuteNonQuery();
        }
    }
}
