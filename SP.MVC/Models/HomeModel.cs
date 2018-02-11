using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SP.BE;

namespace SP.MVC.Models
{
    public class HomeModel
    {
        public HomeModel()
        {
            this.User = new User();
            this.Parcelle = new Parcelle();
            this.Parcelles = new List<Parcelle>();
            this.EvenementParcelle = new EvenementParcelle();
            this.EvenementParcelles = new List<EvenementParcelle>();
            this.Evenement = new Evenement();
            this.Evenements = new List<Evenement>();
        }

        public int IdParcelle { get; set; }
        public SelectList SelectListEvenementParcelle { get; set; }

        /// <summary>
        /// Permet le chargement des catégories dans la SelectList passée à la vue.
        /// </summary>
        /// <param name="liste">Enumération des catégories.</param>
        public void LoadEvenementParcelle(IEnumerable<EvenementParcelle> liste)
        {
            this.SelectListEvenementParcelle = new SelectList(
                liste.ToList(),
                nameof(this.EvenementParcelle.IdEvenementParcelle),
                nameof(this.EvenementParcelle.Description));
        }


        public User User { get; set; }
        public Parcelle Parcelle { get; set; }
        public List<Parcelle> Parcelles { get; set; }
        public EvenementParcelle EvenementParcelle { get; set; }
        public List<EvenementParcelle> EvenementParcelles { get; set; }
        public Evenement Evenement { get; set; }
        public List<Evenement> Evenements { get; set; }
    }
}