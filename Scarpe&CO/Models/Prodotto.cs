using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Scarpe_CO.Models
{
    public class Prodotto
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public int Qta { get; set; }
        public string Nome { get; set; }
        public string Prezzo { get; set; }
        public string Descrizione { get; set; }

        public string ImmagineCopertina { get; set; }
        public string ImmagineDue { get; set; }
        public string ImmagineTre { get; set; }
        public bool Disponibile { get; set; }

        public Prodotto() { }


        public Prodotto(string nome, string prezzo, string descrizione, string immagineCopertina, string immagineDue, string immagineTre, bool disponibile)
        {

            Nome = nome;
            Prezzo = prezzo;
            Descrizione = descrizione;
            ImmagineCopertina = immagineCopertina;
            ImmagineDue = immagineDue;
            ImmagineTre = immagineTre;
            Disponibile = disponibile;
        }

        public Prodotto(int id, string nome, string prezzo, string descrizione, string immagineCopertina, string immagineDue, string immagineTre, bool disponibile)
        {
            Id = id;
            Nome = nome;
            Prezzo = prezzo;
            Descrizione = descrizione;
            ImmagineCopertina = immagineCopertina;
            ImmagineDue = immagineDue;
            ImmagineTre = immagineTre;
            Disponibile = disponibile;
        }

        public Prodotto(int id, int qta , string nome, string prezzo, string descrizione, string immagineCopertina, string immagineDue, string immagineTre, bool disponibile)
        {
            Id = id;
            Qta = qta;
            Nome = nome;
            Prezzo = prezzo;
            Descrizione = descrizione;
            ImmagineCopertina = immagineCopertina;
            ImmagineDue = immagineDue;
            ImmagineTre = immagineTre;
            Disponibile = disponibile;
        }




        public static void EditProdotto(Prodotto P, string queryString, bool boolImg1, bool boolImg2, bool boolImg3)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //string query = "UPDATE Prodotti SET Nome = @Nome, Prezzo = @Prezzo, Descrizione = @Descrizione, ImmagineCopertina = @ImmagineCopertina, ImmagineDue = @ImmagineDue, ImmagineTre = @ImmagineTre, Disponibile = @Disponibile WHERE IdScarpa = @Id;";
                string query = queryString;
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("Nome", P.Nome);
                cmd.Parameters.AddWithValue("Prezzo", P.Prezzo);
                cmd.Parameters.AddWithValue("Descrizione", P.Descrizione);
                cmd.Parameters.AddWithValue("Disponibile", P.Disponibile);
                cmd.Parameters.AddWithValue("Id", P.Id);

                if (boolImg1)
                {
                    cmd.Parameters.AddWithValue("@ImmagineCopertina", P.ImmagineCopertina);
                }

                // Aggiungi gli altri parametri per le immagini solo se i file corrispondenti sono stati forniti
                if (boolImg2)
                {
                    cmd.Parameters.AddWithValue("@ImmagineDue", P.ImmagineDue);
                }

                if (boolImg3)
                {
                    cmd.Parameters.AddWithValue("@ImmagineTre", P.ImmagineTre);
                }



                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    
                }
                catch (Exception ex)
                {
                   
                    Console.WriteLine("Errore durante l'aggiornamento del prodotto: " + ex.Message); 
                }
                finally
                {
                    conn.Close();
                }
            }
        }


        public static void AddProdotto(Prodotto P)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Prodotti (Nome, Prezzo, Descrizione, ImmagineCopertina, ImmagineDue, ImmagineTre, Disponibile) VALUES (@Nome, @Prezzo, @Descrizione, @ImmagineCopertina, @ImmagineDue, @ImmagineTre, @Disponibile);";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("Nome", P.Nome);
                cmd.Parameters.AddWithValue("Prezzo", P.Prezzo);
                cmd.Parameters.AddWithValue("Descrizione", P.Descrizione);
                cmd.Parameters.AddWithValue("ImmagineCopertina", P.ImmagineCopertina);
                cmd.Parameters.AddWithValue("ImmagineDue", P.ImmagineDue);
                cmd.Parameters.AddWithValue("ImmagineTre", P.ImmagineTre);
                cmd.Parameters.AddWithValue("Disponibile", P.Disponibile);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    
                }
                catch (Exception ex)
                {
               
                    Console.WriteLine("Errore durante l'inserimento del prodotto: " + ex.Message);
                   
                }
                finally 
                {
                    conn.Close();
                }
            }
        }

       



    }
}