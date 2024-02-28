using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Scarpe_CO.Models
{
    public class Prodotto
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        public string Nome { get; set; }
        public string Prezzo { get; set; }
        public string Descrizione { get; set; }

        public string ImmagineCopertina { get; set; }
        public string ImmagineDue { get; set; }
        public string ImmagineTre { get; set; }
        public bool Diponibile { get; set; }

        public Prodotto() { }


        public Prodotto(string nome, string prezzo, string descrizione, string immagineCopertina, string immagineDue, string immagineTre, bool disponibile)
        {
            
            Nome = nome;
            Prezzo = prezzo;
            Descrizione = descrizione;
            ImmagineCopertina = immagineCopertina;
            ImmagineDue = immagineDue;
            ImmagineTre = immagineTre;
            Diponibile = disponibile;
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
            Diponibile = disponibile;
        }



    }
}