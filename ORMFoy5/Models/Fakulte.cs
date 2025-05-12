using System.Collections.Generic;

namespace ORMFoy5.Models
{
    public class Fakulte
    {
        public int FakulteID { get; set; }
        public string FakulteAd { get; set; }
        public virtual ICollection<Bolum> Bolumler { get; set; }

        public Fakulte()
        {
            Bolumler = new List<Bolum>();
        }
    }
}