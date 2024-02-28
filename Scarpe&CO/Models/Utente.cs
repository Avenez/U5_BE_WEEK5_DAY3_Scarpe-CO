using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Scarpe_CO.Models
{
    public class Utente
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        public string Nome { get; set; }

        public string Password { get; set; }

        [ScaffoldColumn(false)]
        public bool Admin { get; set; }

        public Utente() { }

        public Utente( string nome, string password, bool admin)
        {
            Nome = nome;
            Password = password;
            Admin = admin;
        }
        public Utente(int id, string nome, string password, bool admin)
        {
            ID = id;
            Nome = nome;
            Password = password;
            Admin = admin;
        }
    

}
}