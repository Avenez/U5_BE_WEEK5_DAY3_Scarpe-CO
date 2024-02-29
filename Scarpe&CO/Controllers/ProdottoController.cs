using Scarpe_CO.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
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

                        string Descrizione = reader["Descrizione"].ToString();
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



        public ActionResult BackOffice() 
        {
            if (Request.Cookies["LOGIN_COOKIE"]["admin"] == "True")
            {

                Session["isLogged"] = true;
                List<Prodotto> prodotti = new List<Prodotto>();

                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM Prodotti";
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

        [HttpGet]
        public ActionResult Edit(int id) 
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = $"SELECT * FROM Prodotti WHERE IdScarpa = {id}";

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

                    Prodotto prodottoEdit = new Prodotto(IdProdotto, Nome, Totale, Descrizione, ImmagineCopertina, ImmagineDue, ImmagineTre, Disponibile);
                    return View(prodottoEdit);
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

            return View();
        }

        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "ImmagineCopertina, ImmagineDue, ImmagineTre")] Prodotto P, HttpPostedFileBase ImmagineCopertinaFile, HttpPostedFileBase ImmagineDueFile, HttpPostedFileBase ImmagineTreFile, int id)
        {
            string queryString ="";
            if (ModelState.IsValid)
            {
                // salvataggio dell'immagine copertina
                if (ImmagineCopertinaFile != null && ImmagineCopertinaFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(ImmagineCopertinaFile.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/ImmaginiProdotto"), fileName);
                    ImmagineCopertinaFile.SaveAs(filePath);
                    P.ImmagineCopertina = "/ImmaginiProdotto/" + fileName; // Salva il percorso dell'immagine nel modello
                }
 

                // salvataggio dell'immagine due
                if (ImmagineDueFile != null && ImmagineDueFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(ImmagineDueFile.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/ImmaginiProdotto/"), fileName);
                    ImmagineDueFile.SaveAs(filePath);
                    P.ImmagineDue = "/ImmaginiProdotto/" + fileName; // Salva il percorso dell'immagine nel modello
                }


                // salvataggio dell'immagine tre
                if (ImmagineTreFile != null && ImmagineTreFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(ImmagineTreFile.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/ImmaginiProdotto"), fileName);
                    ImmagineTreFile.SaveAs(filePath);
                    P.ImmagineTre = "/ImmaginiProdotto/" + fileName; // Salva il percorso dell'immagine nel modello
                }


                try
                {
                    queryString = "UPDATE Prodotti SET " +
                                    "Nome = @Nome, " +
                                    "Prezzo = @Prezzo, " +
                                    "Descrizione = @Descrizione, " +
                                    (ImmagineCopertinaFile != null && ImmagineCopertinaFile.ContentLength > 0 ? "ImmagineCopertina = @ImmagineCopertina, " : "") +
                                    (ImmagineDueFile != null && ImmagineDueFile.ContentLength > 0 ? "ImmagineDue = @ImmagineDue, " : "") +
                                    (ImmagineTreFile != null && ImmagineTreFile.ContentLength > 0 ? "ImmagineTre = @ImmagineTre, " : "") +
                                    "Disponibile = @Disponibile " +
                                    "WHERE IdScarpa = @Id;";

                    bool boolImg1;
                    bool boolImg2;
                    bool boolImg3;

                    if (ImmagineCopertinaFile != null && ImmagineCopertinaFile.ContentLength > 0)
                    {
                         boolImg1 = true;
                    }
                    else {
                         boolImg1 = false;
                    }

                    if (ImmagineDueFile != null && ImmagineDueFile.ContentLength > 0)
                    {
                         boolImg2 = true;
                    }
                    else
                    {
                         boolImg2 = false;
                    }

                    if (ImmagineTreFile != null && ImmagineTreFile.ContentLength > 0)
                    {
                         boolImg3 = true;
                    }
                    else
                    {
                         boolImg3 = false;
                    }
                    Prodotto.EditProdotto(P, queryString, boolImg1, boolImg2, boolImg3);
                    return RedirectToAction("BackOffice", "Prodotto");
                    
                }
                catch 
                {
                    ModelState.AddModelError("","Si è verificato un Errore durante la modifica dell'articolo");
                }
            }

            return View();
        }


        

        public ActionResult ChangeVisibility(Prodotto P)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE Prodotti SET Disponibile = @Disponibile WHERE IdScarpa = @Id";
                    cmd.Parameters.AddWithValue("@Disponibile", P.Disponibile ? 0 : 1); 
                    cmd.Parameters.AddWithValue("@Id", P.Id);

                    cmd.ExecuteNonQuery();
                    return RedirectToAction("BackOffice", "Prodotto");
                }

                
                
            }
            catch (Exception ex)
            {
               
                Console.WriteLine("Errore durante l'aggiornamento del prodotto: " + ex.Message);
                
                return View("Error");
            }
        }
    }
}