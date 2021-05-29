using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TarcinCafe.Data;

namespace TarcinCafe.UI
{
    public partial class frmAna : Form
    {
        KafeVeri db = new KafeVeri();
        Siparis siparis;
        public frmAna()
        {
            db = new KafeVeri();
            Urun u1 = new Urun
            {
                UrunAd = "Çay",
                BirimFiyat = 2.50m,
            };
            Urun u2 = new Urun
            {
                UrunAd = "Tost",
                BirimFiyat = 13.00m,
            };
            Urun u3 = new Urun
            {
                UrunAd = "Yeşilçay",
                BirimFiyat = 3.00m,
            };
            db.Urunler.Add(u1);
            db.Urunler.Add(u2);
            db.Urunler.Add(u3);

            InitializeComponent();
            imgList.Images.Add("bos", Resource.bos);
            imgList.Images.Add("dolu", Resource.dolu);

            // resimListesi.Images.Add(Resources)
            MasalariDoldur();
        }

        private void MasalariDoldur()
        {
            ListViewItem lvi;
            for (int i = 1; i <= db.MasaAdet; i++)
            {
                lvi = new ListViewItem();
                lvi.ImageKey = (!(db.AktifSiparisler.Any(x => x.MasaNo == i)) ? lvi.ImageKey = "bos" : lvi.ImageKey = "dolu");
                lvi.Text = "Masa " + i;
                lvi.Tag = i;
                lvMasalar.Items.Add(lvi);
            }
        }

        private void ürünlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmUrunler().ShowDialog();
        }

        private void geçmişSiparişlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmGecmisSiparisler().ShowDialog();
        }

        private void lvMasalar_DoubleClick(object sender, EventArgs e)
        {
            if (db.AktifSiparisler.Any(x => x.MasaNo == (int)lvMasalar.SelectedItems[0].Tag))
                siparis = (Siparis)db.AktifSiparisler.Where(x => x.MasaNo == (int)lvMasalar.SelectedItems[0].Tag);
            else
            {
                siparis = new Siparis()
                {
                    MasaNo = (int)lvMasalar.SelectedItems[0].Tag,
                    Durum = SiparisDurum.Aktif,
                };
                db.AktifSiparisler.Add(siparis);
            }
            lvMasalar.SelectedItems[0].ImageKey = "dolu";
            new frmSiparis(db, siparis).ShowDialog();
        }
    }
}
