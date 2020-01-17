using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KYKAPP
{
    public partial class frmSec : Form
    {
        public frmSec()
        {
            InitializeComponent();
        }

        private void BtnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmSec_Load(object sender, EventArgs e)
        {
            cmbTablo.Items.Add("Talep ve Öneriler");
            cmbTablo.Items.Add("Proje Fikirleri");
        }

        private void BtnTamam_Click(object sender, EventArgs e)
        {
            if(cmbTablo.SelectedItem != null)
            {
                this.Hide();
                frmMenu menu = (frmMenu)Application.OpenForms["frmMenu"];
                if(cmbTablo.SelectedItem.ToString() == "Talep ve Öneriler")
                {
                    menu.TalepProjeAktar(cmbTablo.SelectedItem.ToString(),true);
                }
                else{
                    menu.TalepProjeAktar(cmbTablo.SelectedItem.ToString(), false);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Lütfen bir tablo seçin.","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
    }
}
