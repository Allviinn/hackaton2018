using System.Collections.Generic;

namespace SP.BE
{
    public class EvenementParcelle
    {
        public int IdEvenementParcelle { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }

        public IEnumerable<Evenement> Evenements { get; set; }
    }
}
