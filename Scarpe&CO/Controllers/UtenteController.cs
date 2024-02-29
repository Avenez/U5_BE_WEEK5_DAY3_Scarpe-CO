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


    public class UtenteController : Controller
    {

        static string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ToString();
        SqlConnection conn = new SqlConnection(connectionString);
        // GET: Utente
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout() {
            Session.Clear();
            HttpCookie cookie = new HttpCookie("LOGIN_COOKIE");
            cookie.Expires = DateTime.Now.AddDays(-1);

            Response.Cookies.Add(cookie);
        
        
        return RedirectToAction("Login", "Utente");
        }


        [HttpGet]
        public ActionResult Login() {


        return View();
        }

        [HttpPost]
        public ActionResult Login(Utente U) {

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = $"SELECT * FROM Utenti WHERE Nome = @Nome AND Password = @Password ";

                    cmd.Parameters.AddWithValue("Nome", U.Nome);
                    cmd.Parameters.AddWithValue("Password", U.Password);

                SqlDataReader reader = cmd.ExecuteReader();


                if (reader.Read()) {
                    

                        HttpCookie cookie = new HttpCookie("LOGIN_COOKIE");
                        cookie.Values["nome"] = reader["Nome"].ToString();
                        cookie.Values["password"] = reader["Password"].ToString();
                        cookie.Values["admin"] = reader["Admin"].ToString();
                        cookie.Expires = DateTime.Now.AddDays(30);
                        Response.Cookies.Add(cookie);
                        Session["isLogged"] = true;


                        return RedirectToAction("Index", "Home");

                    
                    //reader.Close();
                    
                }

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

            ModelState.Clear();
            return View();

        }
    }
}