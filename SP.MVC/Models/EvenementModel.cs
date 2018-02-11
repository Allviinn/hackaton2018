using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SP.MVC.Models
{
    public class EvenementModel
    {
        public int IdEvenement { get; set; }
        public System.DateTime CreationDate { get; set; }
        public int ParcelleId { get; set; }
        public int EvenementParcelleId { get; set; }

        public EvenementParcelleModel EvenementParcelle { get; set; }
        public Parcelle Parcelle { get; set; }
    }
}