namespace SP.BE
{
    public class Evenement
    {
        public int IdEvenement { get; set; }
        public System.DateTime CreationDate { get; set; }
        public int ParcelleId { get; set; }
        public int EvenementParcelleId { get; set; }

        public EvenementParcelle EvenementParcelle { get; set; }
        public Parcelle Parcelle { get; set; }
    }
}
