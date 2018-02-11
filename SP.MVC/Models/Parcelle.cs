using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SP.MVC.Models
{
    public class Parcelle
    {
        public int IdParcelle { get; set; }
        public string Nom { get; set; }
        public string Ville { get; set; }
        public string Adresse { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public DateTime CreationDate { get; set; }

        public IEnumerable<EvenementModel> Evenements { get; set; }
    }
}