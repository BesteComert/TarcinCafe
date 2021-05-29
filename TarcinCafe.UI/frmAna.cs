using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
            if( File.Exists("db.json"))
            {
                string json = File.ReadAllText("db.json");
                db = JsonSerializer.Deserialize<KafeVeri>(json);
            }
            //Urun u1 = new Urun
            //{
            //    UrunAd = "Çay",
            //    BirimFiyat = 2.50m,
            //};
            //Urun u2 = new Urun
            //{
            //    UrunAd = "Tost",
            //    BirimFiyat = 13.00m,
            //};
            //Urun u3 = new Urun
            //{
            //    UrunAd = "Yeşilçay",
            //    BirimFiyat = 3.00m,
            //};
            //db.Urunler.Add(u1);
            //db.Urunler.Add(u2);
            //db.Urunler.Add(u3);

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

        private void geçmişSiparişlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmGecmisSiparisler(db).ShowDialog();
        }

        private void lvMasalar_DoubleClick(object sender, EventArgs e)
        {
            if (db.AktifSiparisler.Any(x => x.MasaNo == (int)lvMasalar.SelectedItems[0].Tag))
            {
                foreach (Siparis item in db.AktifSiparisler)
                {
                    if (item.MasaNo == (int)lvMasalar.SelectedItems[0].Tag)
                        siparis = item;
                }
            }
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
            frmSiparis fr = new frmSiparis(db, siparis);
            fr.Tasindi += Fr_Tasindi;
            fr.ShowDialog();
            ListViewItem lvi= lvMasalar.SelectedItems[0];
            if (siparis.Durum != SiparisDurum.Aktif)
            {
                lvi.ImageKey = "bos";
            }

        }

        private void Fr_Tasindi(object sender, TasindiEventArgs e)
        {
            foreach (ListViewItem item in lvMasalar.Items)
            {
                if (e.YeniMasa == (int)item.Tag)
                    item.ImageKey = "dolu";
                else if (e.EskiMasa == (int)item.Tag)
                    item.ImageKey = "bos";
            }
        }

        private void urunlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmUrunler(db).ShowDialog();
        }

        private void frmAna_FormClosing(object sender, FormClosingEventArgs e)
        {
            string json = JsonSerializer.Serialize(db);
            File.WriteAllText("db.json", json);
        }
    }
}
