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
    public partial class frmUrunler : Form
    {
        Urun urun = new Urun();
        KafeVeri _db;
        BindingList<Urun> _blUrunler;
        public frmUrunler(KafeVeri db)
        {
            _db = db;
            _blUrunler = new BindingList<Urun>(_db.Urunler);
            InitializeComponent();
            dgvUrunler.DataSource = _blUrunler;
        }

        //private void dgvUrunler_DoubleClick(object sender,EventArgs e)
        //{
        //    urun = (Urun)dgvUrunler.SelectedRows[0].DataBoundItem;
        //    txtUrunAd.Text = urun.UrunAd;
        //    nudFiyat.Value = urun.BirimFiyat;
        //    btnEkle.Visible = false;
        //    btnDuzenle.Visible = true;
        //    btnVazgec.Visible = true;
        //    txtUrunAd.Enabled = false;
        //}

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            if (txtUrunAd.Text == "")
                return;
            urun.UrunAd = txtUrunAd.Text;
            urun.BirimFiyat = nudFiyat.Value;
            btnEkle.Visible = true;
            btnDuzenle.Visible = false;
            btnVazgec.Visible = false;
            btnEkle.Text = "EKLE";
            txtUrunAd.Text = "";
            nudFiyat.Value = 1m;
        }

        private void btnVazgec_Click(object sender, EventArgs e)
        {
            btnEkle.Visible = true;
            btnDuzenle.Visible = false;
            btnVazgec.Visible = false;
            btnEkle.Text = "EKLE";
            txtUrunAd.Text = "";
            nudFiyat.Value = 1m;
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (btnEkle.Text == "EKLE")
            {
                Urun u = new Urun
                {
                    UrunAd = txtUrunAd.Text,
                    BirimFiyat = nudFiyat.Value
                };
                _blUrunler.Add(u);
                txtUrunAd.Text = "";
                nudFiyat.Value = 1m;
            }
            else
            {
                _blUrunler.Remove(urun);
                _blUrunler.ResetBindings();
            }

        }

        private void dgvUrunler_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            int secilen = e.RowIndex;
            urun = _blUrunler[secilen];
            // urun = (Urun)dgvUrunler.SelectedRows[0].DataBoundItem;
            txtUrunAd.Text = urun.UrunAd;
            nudFiyat.Value = urun.BirimFiyat;
            btnEkle.Text = "SİL";
            btnDuzenle.Visible = true;
            btnVazgec.Visible = true;
        }

        private void dgvUrunler_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                urun = (Urun)dgvUrunler.SelectedRows[0].DataBoundItem;
                _blUrunler.Remove(urun);
            }
        }
    }
}
