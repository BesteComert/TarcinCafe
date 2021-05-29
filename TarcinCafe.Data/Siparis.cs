using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarcinCafe.Data
{
    public class Siparis
    {

        public int MasaNo { get; set; }
        public SiparisDurum Durum { get; set; }
        public decimal OdenenTutar { get; set; }
        public DateTime? AcilisZamani { get; set; } = DateTime.Now;
        public DateTime? KapanisZamani { get; set; }
        public List<SiparisDetay> SiparisDetay { get; set; } = new List<SiparisDetay>();
        public string ToplamTutarTL { get { return ToplamTutar().ToString("0.00₺"); } }
        public decimal ToplamTutar()
        {
            return SiparisDetay.Sum(x => x.BirimFiyat * x.Adet);
        }

    }
}
