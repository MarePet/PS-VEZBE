using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseBroker;
using Domain;

namespace AppLogic
{
    public class Controller
    {
        private static Controller instance;
        private static Broker broker = new Broker();
        private Controller(){
        }
        public static Controller Instance {
            get {
                if (instance == null)
                { 
                    instance = new Controller(); 
                }
                return instance;
            }
        }

        public List<Projekat> GetProjektiRukovodilac(Korisnik korisnik)
        {
            List<Projekat> projekats = new List<Projekat>();
            try
            {
               broker.OpenConnection();
               projekats=broker.GetProjektiBazaRukovodilac(korisnik);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                broker.CloseConnection();
            }
            return projekats;
         }

        public List<Projekat> GetProjektiUcesnik(Korisnik korisnik)
        {
            List<Projekat> projekats = new List<Projekat>();
            try
            {
                broker.OpenConnection();
                projekats = broker.GetProjektiBazaUcesnik(korisnik);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                broker.CloseConnection();
            }
            return projekats;
        }

        public List<Korisnik> GetUcesnik(List<Projekat> projekatsRukovodilac, Korisnik k)
        {
            List<Korisnik> korisniks = new List<Korisnik>();
            try
            {
                broker.OpenConnection();
               korisniks=broker.GetUcesnikBaza(projekatsRukovodilac,k);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally {
                broker.CloseConnection();
            }
            return korisniks;
        }

        public void ObrisiUcesnika(Korisnik korisnikZaBrisanje, Projekat projekatZaBisanje)
        {
            try
            {
                broker.OpenConnection();
                broker.ObrisiUcesnikaBaza(korisnikZaBrisanje,projekatZaBisanje);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                broker.CloseConnection();
            }
        }

        public void AddUcesnik(Korisnik korisnik, Projekat projekat)
        {
            try
            {
                broker.OpenConnection();
                broker.AddUcesnikBaza(korisnik, projekat);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                broker.CloseConnection();
            }
        }

        public List<Korisnik> GetAllFreeUcesnici(Projekat selectedItem, Korisnik korisnik)
        {
            List<Korisnik> korisniks = new List<Korisnik>();
            try
            {
                broker.OpenConnection();
                korisniks = broker.GetAllFreeUcesniciBaza(selectedItem, korisnik);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                broker.CloseConnection();
            }
            return korisniks;
        }

        public List<Korisnik> GetUcesnik1(Projekat selectedItem, Korisnik korisnik)
        {
            List<Korisnik> korisniks = new List<Korisnik>();
            try
            {
                broker.OpenConnection();
                korisniks = broker.GetUcesnikBaza1(selectedItem, korisnik);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                broker.CloseConnection();
            }
            return korisniks;
        }

        public Korisnik Login(Korisnik korisnik) {

            try
            {
                broker.OpenConnection();
                List<Korisnik> korisniks = broker.GetUsers();
                foreach (Korisnik k in korisniks) {
                    if (k.Username == korisnik.Username &&
                            k.Password == korisnik.Password) {
                        return k;
                    }
                }return null;
            }
            finally
            {
                broker.CloseConnection();
            }
        }
    }
}
