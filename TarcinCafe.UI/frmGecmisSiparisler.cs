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
    public partial class frmGecmisSiparisler : Form
    {
        KafeVeri _db;
        public frmGecmisSiparisler(KafeVeri db)
        {
            _db = db;
            InitializeComponent();
            dgvSiparisler.DataSource = _db.GecmisSiparisler;
        }

        private void dgvSiparisDetaylar_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSiparisler.SelectedRows.Count >= 0)
            {
                Siparis siparis = (Siparis)dgvSiparisler.SelectedRows[0].DataBoundItem;
                dgvSiparisDetaylar.DataSource = siparis.SiparisDetay;
            }
            else
            {
                dgvSiparisDetaylar.DataSource = null;
            }
        }
    }
}
