using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace KYKAPP
{
    public partial class frmGiris : Form
    {
        

        public frmGiris()
        {
            InitializeComponent();
        }

        private void LblKapat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LblKucult_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void FrmGiris_Load(object sender, EventArgs e)
        {
            ConfigOku();
            timerAnaSayfa.Enabled = false;
            CheckForIllegalCrossThreadCalls = false;

            panel1.BackColor = Color.FromArgb(206, 14, 14);
            panel2.BackColor = Color.FromArgb(17, 63, 119);

            btnGiris.BackColor = Color.FromArgb(6, 47, 119);

            pboxGirisYukleniyor.Visible = false;
            lblGirisYapiliyor.Visible = false;
        }

        private void BtnGiris_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtKadi.Text != "" && txtParola.Text != "")
                {
                    timerAnaSayfa.Enabled = true;
                    backGWPanelGiris.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("Parolanızı ve Kullanıcı Adınızı Boş Bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {

            }
            

        }

        bool durum = false;

        private void BackGWPanelGiris_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            

            txtKadi.Enabled = false;
            txtParola.Enabled = false;
            btnGiris.Enabled = false;

            pboxGirisYukleniyor.Visible = true;
            lblGirisYapiliyor.Text = "Giriş yapılıyor. Lütfen bekleyin...";
            lblGirisYapiliyor.Visible = true;

            API api = new API();

            api.Istek(new
            {
                KULLANICI_ADI = txtKadi.Text,
                PAROLA = txtParola.Text
            }, "/yonetici_giris.php");

            if (api.HATA != "1")
            {
                fields.AD = api.AD;
                fields.SOYAD = api.SOYAD;
                fields.KULLANICI_ADI = api.KULLANICI_ADI;
                fields.TOKEN = api.TOKEN;
                fields.G_PAROLA = api.G_PAROLA;

                if(api.TOKEN != null)
                {
                    lblGirisYapiliyor.Text = "Sistem verileri yükleniyor...";
                    durum = true;
                }
                else
                {  
                    timerAnaSayfa.Enabled = false;

                    pboxGirisYukleniyor.Visible = false;
                    lblGirisYapiliyor.Visible = false;

                    txtKadi.Enabled = true;
                    txtParola.Enabled = true;
                    btnGiris.Enabled = true;
                    MessageBox.Show("API Erişim Hatası. Lütfen Sistem Yöneticisine Başvurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                timerAnaSayfa.Enabled = false;

                pboxGirisYukleniyor.Visible = false;
                lblGirisYapiliyor.Visible = false;

                txtKadi.Enabled = true;
                txtParola.Enabled = true;
                btnGiris.Enabled = true;
                MessageBox.Show("Parolanız veya Kullanıcı Adınız Yanlış.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        private void AnaSayfaAc()
        {
            frmMenu frmmenu = new frmMenu();
            frmmenu.WindowState = FormWindowState.Minimized;
            frmmenu.Show();
        }

        public void GirisSayfaGizle()
        {
            this.Hide();
            pboxGirisYukleniyor.Visible = false;
            lblGirisYapiliyor.Visible = false;
        }

        private void TimerAnaSayfa_Tick(object sender, EventArgs e)
        {
            if (durum)
            {
                AnaSayfaAc();
                timerAnaSayfa.Enabled = false;
            }
        }

        private void ConfigOku()
        {
            try
            {
                string configDizin = Application.StartupPath + @"\kyk.config";
                FileStream stream = new FileStream(configDizin, FileMode.OpenOrCreate, FileAccess.Read);

                StreamReader reader = new StreamReader(stream);
                fields.API_ADRES = reader.ReadLine().Split('=')[1].ToString();

                reader.Close();
                stream.Close();
            }
            catch (Exception) { }
        }

        private void FrmGiris_MouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}
