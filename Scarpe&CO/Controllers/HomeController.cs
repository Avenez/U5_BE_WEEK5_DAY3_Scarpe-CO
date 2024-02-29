using Microsoft.Ajax.Utilities;
using Scarpe_CO.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Scarpe_CO.Controllers
{
    public class HomeController : Controller
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
        SqlConnection conn = new SqlConnection(connectionString);
        List<Prodotto> prodotti = new List<Prodotto>();
        public ActionResult Index()
        {

            if (Request.Cookies["LOGIN_COOKIE"] != null) {

                Session["isLogged"] = true;
                try
                {
                    conn.Open();


                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM Prodotti WHERE Disponibile = 1";

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int IdProdotto = int.Parse(reader["IdScarpa"].ToString());
                        string Nome = reader["Nome"].ToString();

                        string Prezzo = reader["Prezzo"].ToString();
                        string[] Cifra;
                        Cifra = Prezzo.ToString().Split(',');
                        string Totale = Cifra[0] + "." + Cifra[1].Substring(0, 2);

                        string Descrizione = reader["Descrizione"].ToString();
                        string ImmagineCopertina = reader["ImmagineCopertina"].ToString();
                        string ImmagineDue = reader["ImmagineDue"].ToString();
                        string ImmagineTre = reader["ImmagineTre"].ToString();
                        bool Disponibile = reader["Disponibile"].ToString() == "True" ? true : false;

                        prodotti.Add(new Prodotto(IdProdotto, Nome, Totale, Descrizione, ImmagineCopertina, ImmagineDue, ImmagineTre, Disponibile));

                    }
                    reader.Close();


                }
                catch (Exception ex)
                {
                    Response.Write("Errore");
                    Response.Write(ex);
                }
                finally
                {
                    conn.Close();
                }


                return View(prodotti);
            } 
            else 
            {
                return RedirectToAction("Login", "Utente"); ;
            }

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}