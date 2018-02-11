using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SP.BE;

namespace SP.MVC.Models
{
    public class HomeModel
    {
        public HomeModel()
        {
            this.User = new User();
            this.Parcelle = new Parcelle();
            this.EvenementParcelle = new EvenementParcelle();
            this.EvenementParcelles = new List<EvenementParcelle>();
        }

        public User User { get; set; }
        public Parcelle Parcelle { get; set; }
        public EvenementParcelle EvenementParcelle { get; set; }
        public List<EvenementParcelle> EvenementParcelles { get; set; }
    }
}