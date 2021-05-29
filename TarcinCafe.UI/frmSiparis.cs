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
    public partial class frmSiparis : Form
    {
        KafeVeri _db = new KafeVeri();
        Siparis _siparis = new Siparis();
        BindingList<SiparisDetay> _blSiparisDetay ;
        public frmSiparis(KafeVeri db,Siparis siparis)
        {
            _db = db;
            _siparis = siparis;
            _blSiparisDetay = new BindingList<SiparisDetay>(_siparis.SiparisDetay);
            InitializeComponent();
            dgvSiparisDetaylar.DataSource = _blSiparisDetay;
            SayfayiYenile();
            _blSiparisDetay.ListChanged += _blSiparisDetay_ListChanged;
        }

        private void _blSiparisDetay_ListChanged(object sender, ListChangedEventArgs e)
        {
            SayfayiYenile();
        }

        private void SayfayiYenile()
        {
            cbUrun.DataSource = _db.Urunler;
            cbMasaNo.DataSource = Enumerable.Range(1, 20).Where(x => !_db.AktifSiparisler.Any(y => y.MasaNo == x)).ToList() ;
            cbMasaNo.SelectedIndex = -1;
            cbUrun.SelectedIndex = -1;
            nudAdet.Value = 1m;
            lblMasaNo.Text = _siparis.MasaNo.ToString("00");
            this.Text ="Masa "+ _siparis.MasaNo.ToString();
            lblTutar.Text = _siparis.ToplamTutarTL.ToString();
        }

        private void frmSiparis_Load(object sender, EventArgs e)
        {

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (cbUrun.SelectedIndex < 0)
                return;
            SiparisDetay sp = new SiparisDetay()
            {
                UrunAd = _db.Urunler[cbUrun.SelectedIndex].UrunAd,
                BirimFiyat = _db.Urunler[cbUrun.SelectedIndex].BirimFiyat,
                Adet = Convert.ToInt32(nudAdet.Value)
            };
            _blSiparisDetay.Add(sp);
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            _siparis.Durum = SiparisDurum.Iptal;
            SiparisiKapa(0);
        }

        private void SiparisiKapa(decimal odenen)
        {
            _siparis.KapanisZamani = DateTime.Now;
            _siparis.OdenenTutar = odenen;
            _db.GecmisSiparisler.Add(_siparis);
            _db.AktifSiparisler.Remove(_siparis);
        }

        private void btnOdemeAl_Click(object sender, EventArgs e)
        {
            _siparis.Durum = SiparisDurum.Odendi;
            SiparisiKapa(Convert.ToDecimal(_siparis.ToplamTutarTL));
        }

        private void btnAnasayfa_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
