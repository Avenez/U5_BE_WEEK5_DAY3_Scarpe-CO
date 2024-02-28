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
    public class ProdottoController : Controller
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
        SqlConnection conn = new SqlConnection(connectionString);
        // GET: Prodotto
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Dettagli(int id) 
        {
            try
            {

                try
                {
                    conn.Open();


                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = $"SELECT * FROM Prodotti WHERE IdScarpa = {id} ";

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int IdProdotto = int.Parse(reader["IdScarpa"].ToString());
                        string Nome = reader["Nome"].ToString();

                        string Prezzo = reader["Prezzo"].ToString();
                        string[] Cifra;
                        Cifra = Prezzo.ToString().Split(',');
                        string Totale = Cifra[0] + "." + Cifra[1].Substring(0, 2);

                        string Descrizione = reader["Destrizione"].ToString();
                        string ImmagineCopertina = reader["ImmagineCopertina"].ToString();
                        string ImmagineDue = reader["ImmagineDue"].ToString();
                        string ImmagineTre = reader["ImmagineTre"].ToString();
                        bool Disponibile = reader["Disponibile"].ToString() == "True" ? true : false;


                        Prodotto ProdottoDettagli = new Prodotto(IdProdotto, Nome, Totale, Descrizione, ImmagineCopertina, ImmagineDue, ImmagineTre, Disponibile);
                        //ViewBag.PD = ProdottoDettagli;
                        return View(ProdottoDettagli);
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

                return RedirectToAction("Index", "Home");
            }
            catch {
                return View("Error", "Shared");
            }
        }
    }
}