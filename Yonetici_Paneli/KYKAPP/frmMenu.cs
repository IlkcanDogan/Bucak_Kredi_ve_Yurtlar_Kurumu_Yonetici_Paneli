using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Resources;
using System.Data;
using System.IO;

namespace KYKAPP
{

    public partial class frmMenu : Form
    {
        API api = new API();
        public frmMenu()
        {
            InitializeComponent();
        }


        public string TOKEN = fields.TOKEN;
        public string Dizin;
        public string FotografDizinEtkinlik = null;
        public string FotografDizinSportif = null;
        public string FotografDizinGonulBagi = null;
        public string FotografDizinDuyuru = null;

        OpenFileDialog Defaultfotograf;

        #region DataGridViewSecilenIDDondur
        private string secIdDondur(DataGridView dgwObje, int IdIndex = 0)
        {
            string secID = "";

            foreach (DataGridViewRow row in dgwObje.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[row.Cells.Count - 1];
                if (chk.Value != null)
                {
                    if ((bool)chk.Value == true)
                    {
                        if (secID != "")
                            secID = secID + "+" + row.Cells[IdIndex].Value.ToString();
                        else
                            secID = row.Cells[IdIndex].Value.ToString();
                    }
                }

            }
            return secID;

        }
        #endregion

        #region ExcelTabloCikarmaFonksiyon
        void DisariAktar(DataGridView dgwObje, string KayitDizini, ToolStripProgressBar bar, BackgroundWorker worker)
        {

            try
            {
                foreach (DataGridViewRow row in dgwObje.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[row.Cells.Count - 1];
                    chk.Value = null;
                }

                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                worksheet = workbook.Sheets["Sayfa1"];
                worksheet = workbook.ActiveSheet;

                for (int i = 1; i < dgwObje.Columns.Count + 1; i++)
                {
                    try
                    {
                        if (dgwObje.Columns[i - 1].HeaderText != "ID")
                        {
                            if (dgwObje.Columns[i - 1].HeaderText != "SEÇ")
                            {
                                worksheet.Cells[1, i - 1] = dgwObje.Columns[i - 1].HeaderText;
                            }
                        }

                    }
                    catch (Exception) { }

                }

                bar.Maximum = dgwObje.Rows.Count + 1;

                for (int i = 0; i < dgwObje.Rows.Count + 1; i++)
                {
                    for (int j = 1; j < dgwObje.Columns.Count; j++)
                    {
                        try
                        {
                            worksheet.Cells[i + 2, j] = dgwObje.Rows[i].Cells[j].Value.ToString();
                        }
                        catch (Exception) { }

                    }

                    worker.ReportProgress(i);
                }

                try
                {
                    workbook.SaveAs(KayitDizini, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    app.Quit();

                    //MessageBox.Show("Öğrenci Listesi Dışarıya Aktarıldı.", "Dışarı Aktar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Dışarı Aktarma Başarısız. Bir Hata İle Karşılaşıldı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
            catch (Exception e)
            {
                MessageBox.Show("İşlem Gerçekleştirilemiyor. Lütfen Daha Sonra Tekrar Deneyin." + e.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        #endregion

        #region DataGridViewOtoVeriEkleme
        private bool dgwVeriEkle(string link, DataGridView dgwObje)
        {
            api.Istek(new { }, link, "Authorization", fields.TOKEN, dgwObje);
            return true;
        }
        #endregion

        #region SecKaldirDGW
        bool tiklama = false;
        private void secKaldir(DataGridView dwgObje)
        {
            foreach (DataGridViewRow row in dwgObje.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[row.Cells.Count - 1];

                if (!tiklama)
                    chk.Value = true;
                else
                    chk.Value = false;

            }
            if (!tiklama)
                tiklama = true;
            else
                tiklama = false;
        }
        #endregion

        #region YemekListeElemanSil
        private void YemekListeSil(CheckedListBox yemekListeObje)
        {
            try
            {
                int i = 0;
                while (i < yemekListeObje.Items.Count)
                {
                    if (yemekListeObje.CheckedItems[i].ToString() != "")
                    {
                        yemekListeObje.Items.Remove(yemekListeObje.CheckedItems[i]);
                    }

                    i++;
                }
            }
            catch (Exception)
            {
                //
            }
            
        }
        #endregion

        #region YemekListesiGetir
        private string YemekListesiEleman(CheckedListBox yemekListeObje)
        {
            string eleman = "";
            try
            {
                int i = 0;
                while (i < yemekListeObje.Items.Count)
                {
                    if (eleman != "")
                        eleman = eleman + "+" + yemekListeObje.Items[i].ToString();
                    else
                        eleman = yemekListeObje.Items[i].ToString();

                    i++;
                }
            }
            catch (Exception)
            {
                //
            }

            return eleman;
        }
        #endregion

        #region APIConfigAyarlari
        private void ConfigYaz()
        {
            try
            {
                string configDizin = Application.StartupPath + @"\kyk.config";
                FileStream stream = new FileStream(configDizin, FileMode.Create, FileAccess.Write);

                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine("API URL=" + txtApiUrl.Text);

                writer.Flush();
                writer.Close();
                stream.Close();
                MessageBox.Show("API kayıt edildi. Değişikliklerin geçerli olması için programı yeniden başlatın.", "API", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("API kayıt edilemedi.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion

        #region FotografYukle
        private bool FotografYukle(string fotoDizin, string link, ToolStripStatusLabel lblDurum)
        {
            bool durum = false;
            lblDurum.Text = "Fotoğraf Yükleniyor...";
            api.Istek(new { }, link, "Authorization", fields.TOKEN, null, fotoDizin);

            if (api.HATA != "1")
            {
                lblDurum.Text = "Fotoğraf Yüklendi.";
                durum = true;
            }
            else
            {
                lblDurum.Text = "Fotoğraf Yüklenemedi.";
            }
            return durum;

        }
        #endregion

        #region TarihHesaplari
        private bool TarihFarkHesapla(DateTimePicker tarih)
        {
            bool durum = false;
            TimeSpan span = DateTime.Now.Subtract(tarih.Value);

            if (span.Days <= 0)
            {
                durum = true;
            }
            return durum;
        }

        private bool IkiTarihArasiFark(DateTimePicker tarihBaslangic, DateTimePicker tarihBitis)
        {
            bool durum = false;
            TimeSpan span = tarihBaslangic.Value.Subtract(tarihBitis.Value);
            if (span.Days <= 0)
            {
                durum = true;
            }
            return durum;
        }
        #endregion

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;

            panelMenu.BackColor = Color.FromArgb(206, 14, 14);
            btnKarekodKaydet.BackColor = Color.FromArgb(6, 47, 119);
            btnYeniKarekod.BackColor = Color.FromArgb(6, 47, 119);
            btnGenelParolaKaydet.BackColor = Color.FromArgb(6, 47, 119);
            btnYoneticiParolaDegistir.BackColor = Color.FromArgb(6, 47, 119);
            btnListeAktar.BackColor = Color.FromArgb(6, 47, 119);
            btnDefaultFotoSec.BackColor = Color.FromArgb(6, 47, 119);
            btnDefaultFotoYukle.BackColor = Color.FromArgb(6, 47, 119);
            btnSecileniSil.BackColor = Color.FromArgb(6, 47, 119);
            pbDefaultFoto.ImageLocation = fields.API_ADRES + "/fotograf/0.jpg";


            btnOgrListeYenile.BackColor = Color.FromArgb(6, 47, 119);
            btnOgrHesapSil.BackColor = Color.FromArgb(6, 47, 119);
            btnOgrHepsiniSec.BackColor = Color.FromArgb(6, 47, 119);

            btnEtkinlikAktar.BackColor = Color.FromArgb(6, 47, 119);
            btnEtkinlikFotografEkle.BackColor = Color.FromArgb(6, 47, 119);
            btnEtkinlikKaydet.BackColor = Color.FromArgb(6, 47, 119);
            btnEtkinlikKayitSil.BackColor = Color.FromArgb(6, 47, 119);
            btnEtkinlikSecKaldir.BackColor = Color.FromArgb(6, 47, 119);
            btnEtkinlikTemizle.BackColor = Color.FromArgb(6, 47, 119);
            btnEtkinlikYenile.BackColor = Color.FromArgb(6, 47, 119);

            btnSportifAktar.BackColor = Color.FromArgb(6, 47, 119);
            btnSportifFotografEkle.BackColor = Color.FromArgb(6, 47, 119);
            btnSportifKaydet.BackColor = Color.FromArgb(6, 47, 119);
            btnSportifKayitSil.BackColor = Color.FromArgb(6, 47, 119);
            btnSportifSecKaldir.BackColor = Color.FromArgb(6, 47, 119);
            btnSportifTemizle.BackColor = Color.FromArgb(6, 47, 119);
            btnSportifYenile.BackColor = Color.FromArgb(6, 47, 119);

            btnGonulBagiAktar.BackColor = Color.FromArgb(6, 47, 119);
            btnGonulBagiFotografEkle.BackColor = Color.FromArgb(6, 47, 119);
            btnGonulBagiKaydet.BackColor = Color.FromArgb(6, 47, 119);
            btnGonulBagiKayitSil.BackColor = Color.FromArgb(6, 47, 119);
            btnGonulBagiSecKaldir.BackColor = Color.FromArgb(6, 47, 119);
            btnGonulBagiTemizle.BackColor = Color.FromArgb(6, 47, 119);
            btnGonulBagiYenile.BackColor = Color.FromArgb(6, 47, 119);

            btnTurnuvaAktar.BackColor = Color.FromArgb(6, 47, 119);
            btnTurnuvaKaydet.BackColor = Color.FromArgb(6, 47, 119);
            btnTurnuvaKayitSil.BackColor = Color.FromArgb(6, 47, 119);
            btnTurnuvaSecKaldir.BackColor = Color.FromArgb(6, 47, 119);
            btnTurnuvaTemizle.BackColor = Color.FromArgb(6, 47, 119);
            btnTurnuvaYenile.BackColor = Color.FromArgb(6, 47, 119);

            btnKursAktar.BackColor = Color.FromArgb(6, 47, 119);
            btnKursKaydet.BackColor = Color.FromArgb(6, 47, 119);
            btnKursKayitSil.BackColor = Color.FromArgb(6, 47, 119);
            btnKursSecKaldir.BackColor = Color.FromArgb(6, 47, 119);
            btnKursTemizle.BackColor = Color.FromArgb(6, 47, 119);
            btnKursYenile.BackColor = Color.FromArgb(6, 47, 119);

            btnDuyuruAktar.BackColor = Color.FromArgb(6, 47, 119);
            btnDuyuruFotografEkle.BackColor = Color.FromArgb(6, 47, 119);
            btnDuyuruKaydet.BackColor = Color.FromArgb(6, 47, 119);
            btnDuyuruKayitSil.BackColor = Color.FromArgb(6, 47, 119);
            btnDuyuruSecKaldir.BackColor = Color.FromArgb(6, 47, 119);
            btnDuyuruTemizle.BackColor = Color.FromArgb(6, 47, 119);
            btnDuyuruYenile.BackColor = Color.FromArgb(6, 47, 119);

            btnTalepProjeAktar.BackColor = Color.FromArgb(6, 47, 119);
            btnTalepProjeKayitSil.BackColor = Color.FromArgb(6, 47, 119);
            btnTalepProjeSecKaldir.BackColor = Color.FromArgb(6, 47, 119);
            btnTalepProjeYenile.BackColor = Color.FromArgb(6, 47, 119);

            btnYemekEkle.BackColor = Color.FromArgb(6, 47, 119);
            btnYemekListeKaydet.BackColor = Color.FromArgb(6, 47, 119);
            btnYemekListeSil.BackColor = Color.FromArgb(6, 47, 119);
            btnYemekSil.BackColor = Color.FromArgb(6, 47, 119);

            btnAPIURLKaydet.BackColor = Color.FromArgb(6, 47, 119);

            cbGizle.Checked = true;
            cbBasvuruAktif.Checked = false;

            //lblAd.Text = fields.AD;
            //lblSoyad.Text = fields.SOYAD;
            //lblKadi.Text = fields.KULLANICI_ADI;
            txtGenelParola.Text = fields.G_PAROLA;
            txtApiUrl.Text = fields.API_ADRES;

            dateTpDuyuruBitis.Enabled = false;

            
            /*dgwVeriEkle("/yonetici_ogr_liste.php", dgwOgrenciler);
            dgwVeriEkle("/yonetici_etkinlik_liste.php", dgwEtkinlik);
            dgwVeriEkle("/yonetici_sportif_liste.php", dgwSportif);
            dgwVeriEkle("/yonetici_sosyal_sorumluluk_liste.php", dgwGonulBagi);
            dgwVeriEkle("/yonetici_turnuva_liste.php", dgwTurnuva);
            dgwVeriEkle("/yonetici_kurs_liste.php", dgwKurs);

            dgwVeriEkle("/yonetici_talep_proje_liste.php?TP_TIP=2", dgwTalep);
            dgwVeriEkle("/yonetici_talep_proje_liste.php?TP_TIP=1", dgwProje);
            dgwVeriEkle("/yonetici_duyuru_liste.php", dgwDuyuru);*/

            frmGiris frmgiris = (frmGiris)Application.OpenForms["frmGiris"];
            frmgiris.GirisSayfaGizle();
            this.WindowState = FormWindowState.Normal;

            

            backGWYemekListesi.RunWorkerAsync();

        }
        
        private void LblKapat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LblKucult_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void CbGizle_CheckedChanged(object sender, EventArgs e)
        {
            if (cbGizle.Checked)
            {
                txtGenelParola.UseSystemPasswordChar = true;
            }
            else
            {
                txtGenelParola.UseSystemPasswordChar = false;
            }
        }

        private void BtnYoneticiParolaDegistir_Click(object sender, EventArgs e)
        {
            txtYeniParola.Enabled = false;
            txtYeniParolaTekrar.Enabled = false;
            btnYoneticiParolaDegistir.Enabled = false;

            if (txtYeniParola.TextLength >= 8 && txtYeniParolaTekrar.TextLength >= 8)
            {
                if (txtYeniParola.Text == txtYeniParolaTekrar.Text)
                {
                    

                    api.Istek(new
                    {
                        YENI_PAROLA = txtYeniParola.Text
                    }, "/yonetici_parola_degistir.php", "Authorization",fields.TOKEN);

                    if (api.HATA != "1")
                    {
                        MessageBox.Show("Parolanız Değiştirildi.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Eski Parolanız Yanlış. Lütfen Tekrar Deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Yeni Parolanız Eşleşmiyor.","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }

            }
            else
            {
                MessageBox.Show("Parola En Az 8 Haneli Olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            txtYeniParola.Enabled = true;
            txtYeniParola.Clear();
        
            txtYeniParolaTekrar.Enabled = true;
            txtYeniParolaTekrar.Clear();

            btnYoneticiParolaDegistir.Enabled = true;

        }

        private void BtnYeniKarekod_Click(object sender, EventArgs e)
        {
            pbKarekod.ImageLocation = api.apiLink + "/test_karekod.php";
            if(pbKarekod.Image == pbKarekod.ErrorImage)
            {
                MessageBox.Show("Karekod Oluşturulamadı. Lütfen İnternet Bağlantınızı Kontrol Edin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void BtnKarekodKaydet_Click(object sender, EventArgs e)
        {
            SaveFileDialog kareKodkKaydet = new SaveFileDialog
            {
                Filter = "PNG | *.png",
                OverwritePrompt = true
            };

            if (kareKodkKaydet.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pbKarekod.Image.Save(kareKodkKaydet.FileName, ImageFormat.Png);
                    MessageBox.Show("Karekod Kayıt Edildi.", "Karekod", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Karekod Kayıt Edilemedi. Bir Hata İle Karşılaşıldı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }

            
        }
        
        private void BtnListeAktar_Click(object sender, EventArgs e)
        {
            
            SaveFileDialog excelBelge = new SaveFileDialog
            {
                Filter = "Excel | *.xlsx",
                OverwritePrompt = true,
                FileName = "Öğrenci Listesi"
            };

            if (excelBelge.ShowDialog() == DialogResult.OK)
            {
                Dizin = excelBelge.FileName;

                tslblOgrListeDurum.Text = "Aktarma İşlemi Başladı.";
                backGWOgrenciler.RunWorkerAsync();
                pBarOgrenciler.Visible = true;
            }


        }

        private void BtnOgrListeYenile_Click(object sender, EventArgs e)
        {
            tslblOgrListeDurum.Text = "Liste Yenileniyor...";

            dgwVeriEkle("/yonetici_ogr_liste.php", dgwOgrenciler);
            tslblOgrListeDurum.Text = "Liste Yenilendi";
        }

        private void BackGWOgrenciler_DoWork(object sender, DoWorkEventArgs e)
        {
            DisariAktar(dgwOgrenciler,Dizin, pBarOgrenciler,backGWOgrenciler);
        }

        private void BackGWOgrenciler_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pBarOgrenciler.Value = e.ProgressPercentage;
        }

        private void BackGWOgrenciler_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tslblOgrListeDurum.Text = "Aktarma İşlemi Tamamlandı.";
            pBarOgrenciler.Visible = false;
            pBarOgrenciler.Value = 0;
        }

        private void BtnOgrHesapDondur_Click(object sender, EventArgs e)
        {
           string idler = secIdDondur(dgwOgrenciler, 0);

            if(idler != "")
            {
                api.Istek(new
                {
                    SEC = idler
                }, "/yonetici_ogr_hesap_sil.php", "Authorization", fields.TOKEN);

                if (api.HATA != "1")
                {
                    MessageBox.Show("İşlem Gerçekleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgwVeriEkle("/yonetici_ogr_liste.php", dgwOgrenciler);
                }
                else
                {
                    MessageBox.Show("Bir Hata İle Karşılaşıldı. Hata Kodu: 5", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seçili Hesap Yok.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
        
        private void BtnOgrHepsiniSec_Click(object sender, EventArgs e)
        {
            secKaldir(dgwOgrenciler);
        }

        private void DgwOgrenciler_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            lblOgrAdet.Text = dgwOgrenciler.Rows.Count.ToString();
        }

        private void BtnOgrAra_Click(object sender, EventArgs e)
        {
            if (txtOgrAraTelNo.TextLength == 11)
            {
                btnOgrAra.Enabled = false;
                txtOgrAraTelNo.Enabled = false;
                tslblOgrListeDurum.Text = "Aranıyor...";
                dgwOgrenciler.CurrentCell = dgwOgrenciler.Rows[0].Cells[3];

                bool bulundu = false;
                int satirAdet = 0;
                while (satirAdet < dgwOgrenciler.Rows.Count)
                {
                    if (dgwOgrenciler.Rows[satirAdet].Cells[3].Value != null)
                    {
                        if (dgwOgrenciler.Rows[satirAdet].Cells[3].Value.ToString() == txtOgrAraTelNo.Text)
                        {
                            dgwOgrenciler.CurrentCell = dgwOgrenciler.Rows[satirAdet].Cells[3];
                            bulundu = true;
                            break;

                        }
                    }
                    satirAdet++;
                }

                btnOgrAra.Enabled = true;
                txtOgrAraTelNo.Enabled = true;

                if (bulundu)
                    tslblOgrListeDurum.Text = "Öğrenci Bulundu";
                else
                {
                    tslblOgrListeDurum.Text = "Bulunamadı!";
                    MessageBox.Show("Öğrenci Bulunamadı.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                    
            }
            else
            {
                MessageBox.Show("Telefon Numarası 11 Hane Olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void BtnEtkinlikTemizle_Click(object sender, EventArgs e)
        {
            txtEtkinlikBaslik.Clear();
            txtEtkinlikIcerik.Clear();
        }

        private void DgwEtkinlik_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            lblEtkinlikAdet.Text = dgwEtkinlik.Rows.Count.ToString();
        }

        private void BtnEtkinlikYenile_Click(object sender, EventArgs e)
        {

            tslblEtkinlikListeDurum.Text = "Liste Yenileniyor...";

            dgwVeriEkle("/yonetici_etkinlik_liste.php", dgwEtkinlik);
            tslblEtkinlikListeDurum.Text = "Liste Yenilendi";
        }

        private void BtnEtkinlikAktar_Click(object sender, EventArgs e)
        {
            SaveFileDialog excelBelge = new SaveFileDialog
            {
                Filter = "Excel | *.xlsx",
                OverwritePrompt = true,
                FileName = "Etkinlik Listesi"
            };

            if (excelBelge.ShowDialog() == DialogResult.OK)
            {
                Dizin = excelBelge.FileName;

                tslblEtkinlikListeDurum.Text = "Aktarma İşlemi Başladı. Lütfen Bekleyin...";
                backGWEtkinlik.RunWorkerAsync();
                pBarEtkinlik.Visible = true;
            }
        }

        private void BackGWEtkinlik_DoWork(object sender, DoWorkEventArgs e)
        {
            DisariAktar(dgwEtkinlik, Dizin, pBarEtkinlik, backGWEtkinlik);
        }

        private void BackGWEtkinlik_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pBarEtkinlik.Value = e.ProgressPercentage;
        }

        private void BackGWEtkinlik_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tslblEtkinlikListeDurum.Text = "Aktarma İşlemi Tamamlandı.";
            pBarEtkinlik.Visible = false;
            pBarEtkinlik.Value = 0;
        }

        private void BtnEtkinlikSecKaldir_Click(object sender, EventArgs e)
        {
            secKaldir(dgwEtkinlik);
        }

        private void BtnEtkinlikKayitSil_Click(object sender, EventArgs e)
        {
            string idler = secIdDondur(dgwEtkinlik, 0);

            if (idler != "")
            {
                api.Istek(new
                {
                    SEC = idler
                }, "/yonetici_etkinlik_sil.php", "Authorization", fields.TOKEN);

                if (api.HATA != "1")
                {
                    MessageBox.Show("İşlem Gerçekleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgwVeriEkle("/yonetici_etkinlik_liste.php", dgwEtkinlik);
                    tslblEtkinlikListeDurum.Text = "Kayıt Silindi.";
                }
                else
                {
                    MessageBox.Show("Bir Hata İle Karşılaşıldı. Hata Kodu: 6", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seçili Etkinlik Yok.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnEtkinlikKaydet_Click(object sender, EventArgs e)
        {

            if(txtEtkinlikBaslik.Text != "" && txtEtkinlikIcerik.Text != "")
            {
                tslblEtkinlikListeDurum.Text = "Etkinlik Kayıt Ediliyor...";
                api.Istek(new
                {
                    BASLIK = txtEtkinlikBaslik.Text,
                    ICERIK = txtEtkinlikIcerik.Text
                }, "/yonetici_etkinlik_ekle.php", "Authorization", fields.TOKEN);

                if (api.HATA != "1")
                {
                    dgwVeriEkle("/yonetici_etkinlik_liste.php", dgwEtkinlik);
                    txtEtkinlikBaslik.Clear();
                    txtEtkinlikIcerik.Clear();

                    if(FotografDizinEtkinlik != null)
                    {
                        FotografYukle(FotografDizinEtkinlik, "/yonetici_fotograf_ekle.php?TABLO_ID=1&KAYIT_ID=" + api.HATA, tslblEtkinlikListeDurum);
                    }
                    

                    MessageBox.Show("İşlem Gerçekleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tslblEtkinlikListeDurum.Text = "Etkinlik Kayıt Edildi.";
                    FotografDizinEtkinlik = null;
                }
                else
                {
                    MessageBox.Show("Etkinlik Eklenemedi. Lütfen Tekrar Deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen Başlık ve İçerik Bilgisini Boş Bırakmayınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSportifTemizle_Click(object sender, EventArgs e)
        {
            txtSportifBaslik.Clear();
            txtSportifIcerik.Clear();
        }

        private void DgwSportif_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            lblSportifAdet.Text = dgwSportif.Rows.Count.ToString();
        }

        private void BtnSportifYenile_Click(object sender, EventArgs e)
        {

            tslblSportifListeDurum.Text = "Liste Yenileniyor...";

            dgwVeriEkle("/yonetici_sportif_liste.php", dgwSportif);
            tslblSportifListeDurum.Text = "Liste Yenilendi";
        }

        private void BtnSportifAktar_Click(object sender, EventArgs e)
        {
            SaveFileDialog excelBelge = new SaveFileDialog
            {
                Filter = "Excel | *.xlsx",
                OverwritePrompt = true,
                FileName = "Sportif Faaliyet Listesi"
            };

            if (excelBelge.ShowDialog() == DialogResult.OK)
            {
                Dizin = excelBelge.FileName;

                tslblSportifListeDurum.Text = "Aktarma İşlemi Başladı. Lütfen Bekleyin...";
                backGWSportif.RunWorkerAsync();
                pBarSportif.Visible = true;
            }
        }

        private void BackGWSportif_DoWork(object sender, DoWorkEventArgs e)
        {
            DisariAktar(dgwSportif, Dizin, pBarSportif, backGWSportif);
        }

        private void BackGWSportif_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pBarSportif.Value = e.ProgressPercentage;
        }

        private void BackGWSportif_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tslblSportifListeDurum.Text = "Aktarma İşlemi Tamamlandı.";
            pBarSportif.Visible = false;
            pBarSportif.Value = 0;
        }

        private void BtnSportifSecKaldir_Click(object sender, EventArgs e)
        {
            secKaldir(dgwSportif);
        }

        private void BtnSportifKayitSil_Click(object sender, EventArgs e)
        {
            string idler = secIdDondur(dgwSportif, 0);

            if (idler != "")
            {
                api.Istek(new
                {
                    SEC = idler
                }, "/yonetici_sportif_sil.php", "Authorization", fields.TOKEN);

                if (api.HATA != "1")
                {
                    MessageBox.Show("İşlem Gerçekleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgwVeriEkle("/yonetici_sportif_liste.php", dgwSportif);
                    tslblSportifListeDurum.Text = "Kayıt Silindi.";
                }
                else
                {
                    MessageBox.Show("Bir Hata İle Karşılaşıldı. Hata Kodu: 6", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seçili Kayıt Yok.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSportifKaydet_Click(object sender, EventArgs e)
        {
            if (txtSportifBaslik.Text != "" && txtSportifIcerik.Text != "")
            {
                tslblSportifListeDurum.Text = "Kayıt Ediliyor...";
                api.Istek(new
                {
                    BASLIK = txtSportifBaslik.Text,
                    ICERIK = txtSportifIcerik.Text
                }, "/yonetici_sportif_ekle.php", "Authorization", fields.TOKEN);

                if (api.HATA != "1")
                {
                    dgwVeriEkle("/yonetici_sportif_liste.php", dgwSportif);
                    txtSportifBaslik.Clear();
                    txtSportifIcerik.Clear();

                    if (FotografDizinSportif != null)
                    {
                        FotografYukle(FotografDizinSportif, "/yonetici_fotograf_ekle.php?TABLO_ID=2&KAYIT_ID=" + api.HATA, tslblSportifListeDurum);
                    }

                    MessageBox.Show("İşlem Gerçekleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tslblSportifListeDurum.Text = "Sportif Faaliyet Kayıt Edildi.";
                    FotografDizinSportif = null;

                }
                else
                {
                    MessageBox.Show("Sportif Faaliyet Eklenemedi. Lütfen Tekrar Deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen Başlık ve İçerik Bilgisini Boş Bırakmayınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGonulBagiTemizle_Click(object sender, EventArgs e)
        {
            txtGonulBagiBaslik.Clear();
            txtGonulBagiIcerik.Clear();
        }

        private void DgwGonulBagi_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            lblGonulBagiAdet.Text = dgwGonulBagi.Rows.Count.ToString();
        }

        private void BtnGonulBagiYenile_Click(object sender, EventArgs e)
        {
            tslblGonulBagiListeDurum.Text = "Liste Yenileniyor...";

            dgwVeriEkle("/yonetici_sosyal_sorumluluk_liste.php", dgwGonulBagi);
            tslblGonulBagiListeDurum.Text = "Liste Yenilendi";
        }

        private void BtnGonulBagiAktar_Click(object sender, EventArgs e)
        {
            SaveFileDialog excelBelge = new SaveFileDialog
            {
                Filter = "Excel | *.xlsx",
                OverwritePrompt = true,
                FileName = "Gönül Bağı Projeleri Listesi"
            };

            if (excelBelge.ShowDialog() == DialogResult.OK)
            {
                Dizin = excelBelge.FileName;

                tslblGonulBagiListeDurum.Text = "Aktarma İşlemi Başladı. Lütfen Bekleyin...";
                backGWGonulBagi.RunWorkerAsync();
                pBarGonulBagi.Visible = true;
            }
        }

        private void BackGWGonulBagi_DoWork(object sender, DoWorkEventArgs e)
        {
            DisariAktar(dgwGonulBagi, Dizin, pBarGonulBagi, backGWGonulBagi);
        }

        private void BackGWGonulBagi_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pBarGonulBagi.Value = e.ProgressPercentage;
        }

        private void BackGWGonulBagi_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tslblGonulBagiListeDurum.Text = "Aktarma İşlemi Tamamlandı.";
            pBarGonulBagi.Visible = false;
            pBarGonulBagi.Value = 0;
        }

        private void BtnGonulBagiSecKaldir_Click(object sender, EventArgs e)
        {
            secKaldir(dgwGonulBagi);
        }

        private void BtnGonulBagiKayitSil_Click(object sender, EventArgs e)
        {
            string idler = secIdDondur(dgwGonulBagi, 0);

            if (idler != "")
            {
                api.Istek(new
                {
                    SEC = idler
                }, "/yonetici_sosyal_sorumluluk_sil.php", "Authorization", fields.TOKEN);

                if (api.HATA != "1")
                {
                    MessageBox.Show("İşlem Gerçekleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgwVeriEkle("/yonetici_sosyal_sorumluluk_liste.php", dgwGonulBagi);
                    tslblGonulBagiListeDurum.Text = "Kayıt Silindi.";
                }
                else
                {
                    MessageBox.Show("Bir Hata İle Karşılaşıldı. Hata Kodu: 6", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seçili Kayıt Yok.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnGonulBagiKaydet_Click(object sender, EventArgs e)
        {
            if (txtGonulBagiBaslik.Text != "" && txtGonulBagiIcerik.Text != "")
            {
                tslblGonulBagiListeDurum.Text = "Kayıt Ediliyor...";
                api.Istek(new
                {
                    BASLIK = txtGonulBagiBaslik.Text,
                    ICERIK = txtGonulBagiIcerik.Text
                }, "/yonetici_sosyal_sorumluluk_ekle.php", "Authorization", fields.TOKEN);

                if (api.HATA != "1")
                {
                    dgwVeriEkle("/yonetici_sosyal_sorumluluk_liste.php", dgwGonulBagi);
                    txtGonulBagiBaslik.Clear();
                    txtGonulBagiIcerik.Clear();

                    if (FotografDizinGonulBagi != null)
                    {
                        FotografYukle(FotografDizinGonulBagi, "/yonetici_fotograf_ekle.php?TABLO_ID=3&KAYIT_ID=" + api.HATA, tslblGonulBagiListeDurum);
                    }

                    MessageBox.Show("İşlem Gerçekleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tslblGonulBagiListeDurum.Text = "Gönül Bağı Projesi Kayıt Edildi.";
                    FotografDizinGonulBagi = null;
                }
                else
                {
                    MessageBox.Show("Gönül Bağı Projesi Eklenemedi. Lütfen Tekrar Deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen Başlık ve İçerik Bilgisini Boş Bırakmayınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnTurnuvaTemizle_Click(object sender, EventArgs e)
        {
            txtTurnuvaAdi.Clear();
            txtTurnuvaIcerik.Clear();
        }

        private void DgwTurnuva_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            lblTurnuvaAdet.Text = dgwTurnuva.Rows.Count.ToString();
        }

        private void BtnTurnuvaYenile_Click(object sender, EventArgs e)
        {
            tslblTurnuvaListeDurum.Text = "Liste Yenileniyor...";

            dgwVeriEkle("/yonetici_turnuva_liste.php", dgwTurnuva);
            tslblTurnuvaListeDurum.Text = "Liste Yenilendi";
        }

        private void BtnTurnuvaAktar_Click(object sender, EventArgs e)
        {
            SaveFileDialog excelBelge = new SaveFileDialog
            {
                Filter = "Excel | *.xlsx",
                OverwritePrompt = true,
                FileName = "Turnuva Listesi"
            };

            if (excelBelge.ShowDialog() == DialogResult.OK)
            {
                Dizin = excelBelge.FileName;

                tslblTurnuvaListeDurum.Text = "Aktarma İşlemi Başladı. Lütfen Bekleyin...";
                backGWTurnuva.RunWorkerAsync();
                pBarTurnuva.Visible = true;
            }
        }

        private void BackGWTurnuva_DoWork(object sender, DoWorkEventArgs e)
        {
            DisariAktar(dgwTurnuva, Dizin, pBarTurnuva, backGWTurnuva);
        }

        private void BackGWTurnuva_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pBarTurnuva.Value = e.ProgressPercentage;
        }

        private void BackGWTurnuva_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tslblTurnuvaListeDurum.Text = "Aktarma İşlemi Tamamlandı.";
            pBarTurnuva.Visible = false;
            pBarTurnuva.Value = 0;
        }

        private void BtnTurnuvaSecKaldir_Click(object sender, EventArgs e)
        {
            secKaldir(dgwTurnuva);
        }

        private void BtnTurnuvaKaydet_Click(object sender, EventArgs e)
        {
            if (TarihFarkHesapla(dateTpTurnuvaBaslangic))
            {
                if(IkiTarihArasiFark(dateTpTurnuvaBaslangic, dateTpTurnuvaBitis))
                {
                    if (txtTurnuvaAdi.Text != "" && txtTurnuvaIcerik.Text != "")
                    {
                        tslblTurnuvaListeDurum.Text = "Kayıt Ediliyor...";
                        dateTpTurnuvaBaslangic.Enabled = false;
                        dateTpTurnuvaBitis.Enabled = false;

                        DateTime BaslangicTarih = dateTpTurnuvaBaslangic.Value;
                        DateTime BitisTarih = dateTpTurnuvaBitis.Value;

                        api.Istek(new
                        {
                            TURNUVA_ADI = txtTurnuvaAdi.Text,
                            ICERIK = txtTurnuvaIcerik.Text,
                            BASVURU_BASLANGIC_TARIH = BaslangicTarih.ToShortDateString(),
                            BASVURU_BITIS_TARIH = BitisTarih.ToShortDateString()

                        }, "/yonetici_turnuva_ekle.php", "Authorization", fields.TOKEN);

                        dateTpTurnuvaBaslangic.Enabled = true;
                        dateTpTurnuvaBitis.Enabled = true;

                        if (api.HATA != "1")
                        {
                            dgwVeriEkle("/yonetici_turnuva_liste.php", dgwTurnuva);
                            txtTurnuvaAdi.Clear();
                            txtTurnuvaIcerik.Clear();

                            MessageBox.Show("İşlem Gerçekleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tslblTurnuvaListeDurum.Text = "Turnuva Kayıt Edildi.";
                        }
                        else
                        {
                            MessageBox.Show("Turnuva Eklenemedi. Lütfen Tekrar Deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lütfen Turnuva Adını ve İçerik Bilgisini Boş Bırakmayınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Başvuru bitiş tarihi başvuru başlangıç tarihten küçük olamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Başvuru başlangıç tarihi şuanki tarihten küçük olamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }

        private void BtnTurnuvaKayitSil_Click(object sender, EventArgs e)
        {
            string idler = secIdDondur(dgwTurnuva, 0);

            if (idler != "")
            {
                api.Istek(new
                {
                    SEC = idler
                }, "/yonetici_turnuva_sil.php", "Authorization", fields.TOKEN);

                if (api.HATA != "1")
                {
                    MessageBox.Show("İşlem Gerçekleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgwVeriEkle("/yonetici_turnuva_liste.php", dgwTurnuva);
                    tslblTurnuvaListeDurum.Text = "Kayıt Silindi.";
                }
                else
                {
                    MessageBox.Show("Bir Hata İle Karşılaşıldı. Hata Kodu: 6", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seçili Kayıt Yok.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
 
        private void DgwTurnuva_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            tslblTurnuvaListeDurum.Text = "Lütfen Bekleyin...";

            fields.BASVURU_KAYIT_ID = dgwTurnuva.CurrentRow.Cells[0].Value.ToString();
            fields.BASVURU_KAYIT_AD = dgwTurnuva.CurrentRow.Cells[1].Value.ToString();
            fields.BASVURU_KAYIT_TIP = "3";

            frmBasvuruListesi frmBasvuru = new frmBasvuruListesi();
            frmBasvuru.Show();
            tslblTurnuvaListeDurum.Text = "Başvuru Listesi Görüntülendi.";

        }

        private void BtnKursTemizle_Click(object sender, EventArgs e)
        {
            txtKursAdi.Clear();
            txtKursIcerik.Clear();
        }

        private void DgwKurs_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            lblKursAdet.Text = dgwKurs.Rows.Count.ToString();
        }

        private void BtnKursYenile_Click(object sender, EventArgs e)
        {
            tslblKursListeDurum.Text = "Liste Yenileniyor...";

            dgwVeriEkle("/yonetici_kurs_liste.php", dgwKurs);
            tslblKursListeDurum.Text = "Liste Yenilendi";
        }

        private void BtnKursAktar_Click(object sender, EventArgs e)
        {
            SaveFileDialog excelBelge = new SaveFileDialog
            {
                Filter = "Excel | *.xlsx",
                OverwritePrompt = true,
                FileName = "Kurs Listesi"
            };

            if (excelBelge.ShowDialog() == DialogResult.OK)
            {
                Dizin = excelBelge.FileName;

                tslblKursListeDurum.Text = "Aktarma İşlemi Başladı. Lütfen Bekleyin...";
                backGWKurs.RunWorkerAsync();
                pBarKurs.Visible = true;
            }
        }

        private void BackGWKurs_DoWork(object sender, DoWorkEventArgs e)
        {
            DisariAktar(dgwKurs, Dizin, pBarKurs, backGWKurs);
        }

        private void BackGWKurs_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pBarKurs.Value = e.ProgressPercentage;
        }

        private void BackGWKurs_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tslblKursListeDurum.Text = "Aktarma İşlemi Tamamlandı.";
            pBarKurs.Visible = false;
            pBarKurs.Value = 0;
        }

        private void BtnKursSecKaldir_Click(object sender, EventArgs e)
        {
            secKaldir(dgwKurs);
        }

        private void BtnKursKaydet_Click(object sender, EventArgs e)
        {
            if (TarihFarkHesapla(dateTpKursBaslangic))
            {
                if(IkiTarihArasiFark(dateTpKursBaslangic, dateTpKursBitis))
                {
                    if (txtKursAdi.Text != "" && txtKursIcerik.Text != "")
                    {
                        tslblKursListeDurum.Text = "Kayıt Ediliyor...";
                        dateTpKursBaslangic.Enabled = false;
                        dateTpKursBitis.Enabled = false;

                        DateTime BaslangicTarih = dateTpKursBaslangic.Value;
                        DateTime BitisTarih = dateTpKursBitis.Value;

                        api.Istek(new
                        {
                            KURS_ADI = txtKursAdi.Text,
                            ICERIK = txtKursIcerik.Text,
                            BASVURU_BASLANGIC_TARIH = BaslangicTarih.ToShortDateString(),
                            BASVURU_BITIS_TARIH = BitisTarih.ToShortDateString()

                        }, "/yonetici_kurs_ekle.php", "Authorization", fields.TOKEN);

                        dateTpKursBaslangic.Enabled = true;
                        dateTpKursBitis.Enabled = true;

                        if (api.HATA != "1")
                        {
                            dgwVeriEkle("/yonetici_kurs_liste.php", dgwKurs);
                            txtKursAdi.Clear();
                            txtKursIcerik.Clear();

                            MessageBox.Show("İşlem Gerçekleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tslblKursListeDurum.Text = "Kurs Kayıt Edildi.";
                        }
                        else
                        {
                            MessageBox.Show("Kurs Eklenemedi. Lütfen Tekrar Deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lütfen Kurs Adını ve İçerik Bilgisini Boş Bırakmayınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Başvuru bitiş tarihi başvuru başlangıç tarihten küçük olamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Başvuru başlangıç tarihi şuanki tarihten küçük olamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void BtnKursKayitSil_Click(object sender, EventArgs e)
        {
            string idler = secIdDondur(dgwKurs, 0);

            if (idler != "")
            {
                api.Istek(new
                {
                    SEC = idler
                }, "/yonetici_kurs_sil.php", "Authorization", fields.TOKEN);

                if (api.HATA != "1")
                {
                    MessageBox.Show("İşlem Gerçekleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgwVeriEkle("/yonetici_kurs_liste.php", dgwKurs);
                    tslblKursListeDurum.Text = "Kayıt Silindi.";
                }
                else
                {
                    MessageBox.Show("Bir Hata İle Karşılaşıldı. Hata Kodu: 6", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seçili Kayıt Yok.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DgwKurs_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            tslblKursListeDurum.Text = "Lütfen Bekleyin...";

            fields.BASVURU_KAYIT_ID = dgwKurs.CurrentRow.Cells[0].Value.ToString();
            fields.BASVURU_KAYIT_AD = dgwKurs.CurrentRow.Cells[1].Value.ToString();
            fields.BASVURU_KAYIT_TIP = "2";

            frmBasvuruListesi frmBasvuru = new frmBasvuruListesi();
            frmBasvuru.Show();
            tslblKursListeDurum.Text = "Başvuru Listesi Görüntülendi.";
        }

        private void CbBasvuruAktif_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBasvuruAktif.Checked)
            {
                dateTpDuyuruBitis.Enabled = true;
            }
            else
            {
                dateTpDuyuruBitis.Enabled = false;
            }
        }

        private void BtnDuyuruTemizle_Click(object sender, EventArgs e)
        {
            txtDuyuruBaslik.Clear();
            txtDuyuruIcerik.Clear();
        }

        private void DgwDuyuru_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            lblDuyuruAdet.Text = dgwDuyuru.Rows.Count.ToString();
        }

        private void BtnDuyuruYenile_Click(object sender, EventArgs e)
        {
            tslblDuyuruListeDurum.Text = "Liste Yenileniyor...";

            dgwVeriEkle("/yonetici_duyuru_liste.php", dgwDuyuru);
            tslblDuyuruListeDurum.Text = "Liste Yenilendi";
        }

        private void BtnDuyuruAktar_Click(object sender, EventArgs e)
        {
            SaveFileDialog excelBelge = new SaveFileDialog
            {
                Filter = "Excel | *.xlsx",
                OverwritePrompt = true,
                FileName = "Duyuru Listesi"
            };

            if (excelBelge.ShowDialog() == DialogResult.OK)
            {
                Dizin = excelBelge.FileName;

                tslblDuyuruListeDurum.Text = "Aktarma İşlemi Başladı. Lütfen Bekleyin...";
                backGWDuyuru.RunWorkerAsync();
                pBarDuyuru.Visible = true;
            }
        }

        private void BtnDuyuruKayitSil_Click(object sender, EventArgs e)
        {
            string idler = secIdDondur(dgwDuyuru, 0);

            if (idler != "")
            {
                api.Istek(new
                {
                    SEC = idler
                }, "/yonetici_duyuru_sil.php", "Authorization", fields.TOKEN);

                if (api.HATA != "1")
                {
                    MessageBox.Show("İşlem Gerçekleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgwVeriEkle("/yonetici_duyuru_liste.php", dgwDuyuru);
                    tslblDuyuruListeDurum.Text = "Kayıt Silindi.";
                }
                else
                {
                    MessageBox.Show("Bir Hata İle Karşılaşıldı. Hata Kodu: 6", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seçili Kayıt Yok.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BackGWDuyuru_DoWork(object sender, DoWorkEventArgs e)
        {
            DisariAktar(dgwDuyuru, Dizin, pBarDuyuru, backGWDuyuru);
        }

        private void BackGWDuyuru_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pBarDuyuru.Value = e.ProgressPercentage;
        }

        private void BackGWDuyuru_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tslblDuyuruListeDurum.Text = "Aktarma İşlemi Tamamlandı.";
            pBarDuyuru.Visible = false;
            pBarDuyuru.Value = 0;
        }

        private void BtnDuyuruSecKaldir_Click(object sender, EventArgs e)
        {
            secKaldir(dgwDuyuru);
        } 

        private void BtnDuyuruKaydet_Click(object sender, EventArgs e)
        {
         
            if (TarihFarkHesapla(dateTpDuyuruBitis))
            {
                if (txtDuyuruBaslik.Text != "" && txtDuyuruIcerik.Text != "")
                {
                    tslblDuyuruListeDurum.Text = "Kayıt Ediliyor...";
                    dateTpDuyuruBitis.Enabled = false;

                    DateTime BitisTarih = dateTpDuyuruBitis.Value;

                    string basvuru = "1";
                    if (!cbBasvuruAktif.Checked)
                        basvuru = "0";

                    api.Istek(new
                    {
                        DUYURU_BASLIK = txtDuyuruBaslik.Text,
                        ICERIK = txtDuyuruIcerik.Text,
                        BASVURU = basvuru,
                        BASVURU_BITIS_TARIH = BitisTarih.ToShortDateString()

                    }, "/yonetici_duyuru_ekle.php", "Authorization", fields.TOKEN);

                    if (cbBasvuruAktif.Checked)
                        dateTpDuyuruBitis.Enabled = true;

                    if (api.HATA != "1")
                    {
                        dgwVeriEkle("/yonetici_duyuru_liste.php", dgwDuyuru);
                        txtDuyuruBaslik.Clear();
                        txtDuyuruIcerik.Clear();
                        if (FotografDizinDuyuru != null)
                        {
                            FotografYukle(FotografDizinDuyuru, "/yonetici_fotograf_ekle.php?TABLO_ID=4&KAYIT_ID=" + api.HATA, tslblDuyuruListeDurum);
                        }

                        MessageBox.Show("İşlem Gerçekleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tslblDuyuruListeDurum.Text = "Duyuru Kayıt Edildi.";
                        FotografDizinDuyuru = null;
                    }
                    else
                    {
                        MessageBox.Show("Duyuru Eklenemedi. Lütfen Tekrar Deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen Duyuru Başlığını ve İçerik Bilgisini Boş Bırakmayınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Başvuru bitiş tarihi şuanki tarihten küçük olamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void DgwDuyuru_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if(dgwDuyuru.CurrentRow.Cells[5].Value.ToString() == "Evet")
            {
                tslblDuyuruListeDurum.Text = "Lütfen Bekleyin...";

                fields.BASVURU_KAYIT_ID = dgwDuyuru.CurrentRow.Cells[0].Value.ToString();
                fields.BASVURU_KAYIT_AD = "Duyuru";
                fields.BASVURU_KAYIT_TIP = "1";

                frmBasvuruListesi frmBasvuru = new frmBasvuruListesi();
                frmBasvuru.Show();
                tslblDuyuruListeDurum.Text = "Başvuru Listesi Görüntülendi.";
            }
            else
            {
                MessageBox.Show("Duyurunun Başvuru Listesi Yok.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            
        }

        private void DgwTalep_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            lblTalepAdet.Text = dgwTalep.Rows.Count.ToString();
        }

        private void DgwProje_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            lblProjeAdet.Text = dgwProje.Rows.Count.ToString();
        }

        private void BtnTalepProjeYenile_Click(object sender, EventArgs e)
        {
            tslblKursListeDurum.Text = "Liste Yenileniyor...";

            dgwVeriEkle("/yonetici_talep_proje_liste.php?TP_TIP=1", dgwProje);
            dgwVeriEkle("/yonetici_talep_proje_liste.php?TP_TIP=2", dgwTalep);
            tslblTalepProjeListeDurum.Text = "Listeler Yenilendi";
        }

        private void BtnTalepProjeAktar_Click(object sender, EventArgs e)
        {
            frmSec secfrm = new frmSec();
            secfrm.Show();
        }

        public void TalepProjeAktar(string fileName, bool seciTalepProje)
        {
            SaveFileDialog excelBelge = new SaveFileDialog
            {
                Filter = "Excel | *.xlsx",
                OverwritePrompt = true,
                FileName = fileName
            };

            if (excelBelge.ShowDialog() == DialogResult.OK)
            {
                Dizin = excelBelge.FileName;

                tslblTalepProjeListeDurum.Text = "Aktarma İşlemi Başladı. Lütfen Bekleyin..."; //
                if (seciTalepProje)
                {
                    backGWTalep.RunWorkerAsync();
                }
                else
                {
                    backGWProje.RunWorkerAsync();
                }
                pBarTalepProje.Visible = true;
            }
        }

        private void BtnTalepProjeKayitSil_Click(object sender, EventArgs e)
        {
            string idlerTalep = secIdDondur(dgwTalep, 0);
            string idlerProje = secIdDondur(dgwProje, 0);

            if (idlerTalep != "" || idlerProje != "")
            {
                ////////////////////---TALEP---///////////////////////////
                if(idlerTalep != "")
                {
                    api.Istek(new
                    {
                        SEC = idlerTalep,
                        TP_TIP = 2
                    }, "/yonetici_talep_proje_sil.php", "Authorization", fields.TOKEN);

                    if (api.HATA != "1")
                    {
                        MessageBox.Show("İşlem Gerçekleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgwVeriEkle("/yonetici_talep_proje_liste.php?TP_TIP=2", dgwTalep);
                        richTalep.Clear();
                        tslblTalepProjeListeDurum.Text = "Kayıt Silindi.";
                    }
                    else
                    {
                        MessageBox.Show("Bir Hata İle Karşılaşıldı. Hata Kodu: 6", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                ////////////////////////--PROJE--/////////////////////////

               if(idlerProje != "")
               {
                    api.Istek(new
                    {
                        SEC = idlerProje,
                        TP_TIP = 1
                    }, "/yonetici_talep_proje_sil.php", "Authorization", fields.TOKEN);

                    if (api.HATA != "1")
                    {
                        MessageBox.Show("İşlem Gerçekleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgwVeriEkle("/yonetici_talep_proje_liste.php?TP_TIP=1", dgwProje);
                        richProje.Clear();
                        tslblTalepProjeListeDurum.Text = "Kayıt Silindi.";
                    }
                    else
                    {
                        MessageBox.Show("Bir Hata İle Karşılaşıldı. Hata Kodu: 6", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            else
            {
                MessageBox.Show("Seçili Kayıt Yok.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnTalepProjeSecKaldir_Click(object sender, EventArgs e)
        {
            secKaldir(dgwTalep);
            if (tiklama)
                tiklama = false;
            else
                tiklama = true;
            secKaldir(dgwProje);
        }

        private void DgwTalep_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                richTalep.Text = dgwTalep.CurrentRow.Cells[2].Value.ToString();
            }
            catch (Exception) { }
            
        }

        private void DgwProje_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                richProje.Text = dgwProje.CurrentRow.Cells[2].Value.ToString();
            }
            catch (Exception) { }

        }

        private void BtnGenelParolaKaydet_Click(object sender, EventArgs e)
        {
            txtGenelParola.Enabled = false;
            btnGenelParolaKaydet.Enabled = false;

            if (txtGenelParola.TextLength >= 8)
            {
                api.Istek(new
                {
                    GENEL_PAROLA = txtGenelParola.Text
                }, "/yonetici_genel_parola_degistir.php", "Authorization", fields.TOKEN);

                if (api.HATA != "1")
                {
                    MessageBox.Show("Genel Parola Değiştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Genel Parola Değiştirilemedi. Lütfen Tekrar Deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Parola En Az 8 Haneli Olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            txtGenelParola.Enabled = true;
            btnGenelParolaKaydet.Enabled = true;
        }

        private void BtnEtkinlikFotografEkle_Click(object sender, EventArgs e)
        {
            OpenFileDialog fotograf = new OpenFileDialog
            {
                Filter = "PNG | *.png|JPG | *.jpg|JPEG | *.jpeg"
            };

            if (fotograf.ShowDialog() == DialogResult.OK)
            {
                FotografDizinEtkinlik = fotograf.FileName;
                tslblEtkinlikListeDurum.Text = "Fotoğraf Seçildi.";
            }

        }
       
        private void BtnSportifFotografEkle_Click(object sender, EventArgs e)
        {
            OpenFileDialog fotograf = new OpenFileDialog
            {
                Filter = "PNG | *.png|JPG | *.jpg|JPEG | *.jpeg"
            };

            if (fotograf.ShowDialog() == DialogResult.OK)
            {
                FotografDizinSportif = fotograf.FileName;
                tslblSportifListeDurum.Text = "Fotoğraf Seçildi.";
            }
        }

        private void BtnGonulBagiFotografEkle_Click(object sender, EventArgs e)
        {
            OpenFileDialog fotograf = new OpenFileDialog
            {
                Filter = "PNG | *.png|JPG | *.jpg|JPEG | *.jpeg"
            };

            if (fotograf.ShowDialog() == DialogResult.OK)
            {
                FotografDizinGonulBagi = fotograf.FileName;
                tslblGonulBagiListeDurum.Text = "Fotoğraf Seçildi.";
            }
        }

        private void DgwOgrenciler_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            lblOgrAdet.Text = dgwOgrenciler.Rows.Count.ToString();
        }

        private void DgwTalep_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            lblTalepAdet.Text = dgwTalep.Rows.Count.ToString();
        }

        private void DgwProje_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            lblProjeAdet.Text = dgwProje.Rows.Count.ToString();
        }

        private void DgwEtkinlik_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            lblEtkinlikAdet.Text = dgwEtkinlik.Rows.Count.ToString();
        }

        private void DgwSportif_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            lblSportifAdet.Text = dgwSportif.Rows.Count.ToString();
        }

        private void DgwGonulBagi_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            lblGonulBagiAdet.Text = dgwGonulBagi.Rows.Count.ToString();
        }

        private void DgwTurnuva_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            lblTurnuvaAdet.Text = dgwTurnuva.Rows.Count.ToString();
        }

        private void DgwKurs_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            lblKursAdet.Text = dgwKurs.Rows.Count.ToString();
        }

        private void DgwDuyuru_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            lblDuyuruAdet.Text = dgwDuyuru.Rows.Count.ToString();
        }

        private void DgwDuyuru_Scroll(object sender, ScrollEventArgs e)
        {
            //MessageBox.Show("Scrool");

            int firstDisplayed = dgwDuyuru.FirstDisplayedScrollingRowIndex;
            int displayed = dgwDuyuru.DisplayedRowCount(true);
            int lastVisible = (firstDisplayed + displayed) -1;
            int lastIndex = dgwDuyuru.RowCount - 1;

            //dgwDuyuru.Rows.Add();  //Add your row

            if (lastVisible == lastIndex)
            {
                //MessageBox.Show("FFFF");
            }

        }

        private void BtnDuyuruFotografEkle_Click(object sender, EventArgs e)
        {
            OpenFileDialog fotograf = new OpenFileDialog
            {
                Filter = "PNG | *.png|JPG | *.jpg|JPEG | *.jpeg"
            };

            if (fotograf.ShowDialog() == DialogResult.OK)
            {
                FotografDizinDuyuru = fotograf.FileName;
                tslblDuyuruListeDurum.Text = "Fotoğraf Seçildi.";
            }
        }

        private void BackGWTalep_DoWork(object sender, DoWorkEventArgs e)
        {
            DisariAktar(dgwTalep, Dizin, pBarTalepProje, backGWTalep);
        }

        private void BackGWProje_DoWork(object sender, DoWorkEventArgs e)
        {
            DisariAktar(dgwProje, Dizin, pBarTalepProje, backGWProje);
        }

        private void BackGWTalep_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pBarTalepProje.Value = e.ProgressPercentage;
        }

        private void BackGWProje_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pBarTalepProje.Value = e.ProgressPercentage;
        }

        private void BackGWTalep_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tslblTalepProjeListeDurum.Text = "Aktarma İşlemi Tamamlandı.";
            pBarTalepProje.Visible = false;
            pBarTalepProje.Value = 0;
        }

        private void BackGWProje_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tslblTalepProjeListeDurum.Text = "Aktarma İşlemi Tamamlandı.";
            pBarTalepProje.Visible = false;
            pBarTalepProje.Value = 0;
        }

        private void BtnAPIURLKaydet_Click(object sender, EventArgs e)
        {
            ConfigYaz();
        }

        private void BtnDefaultFotoSec_Click(object sender, EventArgs e)
        {
            Defaultfotograf = new OpenFileDialog
            {
                Filter = "PNG | *.png|JPG | *.jpg|JPEG | *.jpeg"
            };

            if (Defaultfotograf.ShowDialog() == DialogResult.OK)
            {
                //FotografDizinEtkinlik = Defaultfotograf.FileName;
                pbDefaultFoto.ImageLocation = Defaultfotograf.FileName;
               
            }
        }

        private void BtnSecileniSil_Click(object sender, EventArgs e)
        {
            pbDefaultFoto.ImageLocation = fields.API_ADRES + "/fotograf/0.jpg";
        }

        private void BtnDefaultFotoYukle_Click(object sender, EventArgs e)
        {
            if (FotografYukle(Defaultfotograf.FileName, "/yonetici_fotograf_ekle.php?DEFAULT=default", tslblEtkinlikListeDurum))
            {
                MessageBox.Show("Fotoğraf Yüklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Ön tanımlı fotoğraf yüklenemedi.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void BtnYemekEkle_Click(object sender, EventArgs e)
        {
            if(cmbGun.SelectedIndex != -1)
            {
               if(txtYemekAdi.TextLength > 0)
                {
                    if(cmbGun.SelectedIndex == 0)
                    {
                        chkListPazartesi.Items.Add(txtYemekAdi.Text);
                    }
                    else if(cmbGun.SelectedIndex == 1)
                    {
                        chkListSali.Items.Add(txtYemekAdi.Text);
                    }
                    else if (cmbGun.SelectedIndex == 2)
                    {
                        chkListCarsamba.Items.Add(txtYemekAdi.Text);
                    }
                    else if(cmbGun.SelectedIndex == 3)
                    {
                        chkListPersembe.Items.Add(txtYemekAdi.Text);
                    }
                    else if (cmbGun.SelectedIndex == 4)
                    {
                        chkListCuma.Items.Add(txtYemekAdi.Text);
                    }
                    else if (cmbGun.SelectedIndex == 5)
                    {
                        chkListCumartesi.Items.Add(txtYemekAdi.Text);
                    }
                    else
                    {
                        chkListPazar.Items.Add(txtYemekAdi.Text);
                    }
                    txtYemekAdi.Clear();
                }
                else
                {
                    MessageBox.Show("Lütfen yemek adını yazınız.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen gün seçiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void BtnYemekSil_Click(object sender, EventArgs e)
        {
            YemekListeSil(chkListPazartesi);
            YemekListeSil(chkListSali);
            YemekListeSil(chkListCarsamba);
            YemekListeSil(chkListPersembe);
            YemekListeSil(chkListCuma);
            YemekListeSil(chkListCumartesi);
            YemekListeSil(chkListPazar);
        }

        private void BtnYemekListeKaydet_Click(object sender, EventArgs e)
        {
            btnYemekListeKaydet.Enabled = false;

            string pzt = YemekListesiEleman(chkListPazartesi);
            string sal = YemekListesiEleman(chkListSali);
            string car = YemekListesiEleman(chkListCarsamba);
            string per = YemekListesiEleman(chkListPersembe);
            string cum = YemekListesiEleman(chkListCuma);
            string cmt = YemekListesiEleman(chkListCumartesi);
            string paz = YemekListesiEleman(chkListPazar);

            if(pzt != "" && sal != "" && per != "" && cum != "" && cmt != "" && paz != ""){

                
                api.Istek(new
                {
                    PZT = pzt,
                    SAL = sal,
                    CAR = car,
                    PER = per,
                    CUM = cum,
                    CMT = cmt,
                    PAZ = paz

                }, "/yonetici_yemek_listesi_ekle.php", "Authorization", fields.TOKEN);

                if (api.HATA != "1")
                {
                    MessageBox.Show("Haftalık yemek listesi kayıt edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Bir hata oluştu. Lütfen sistem yöneticisine başvurunuz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Haftanın tüm günleri için bir liste oluşturunuz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            btnYemekListeKaydet.Enabled = true;
        }

        private void BackGWYemekListesi_DoWork(object sender, DoWorkEventArgs e)
        {
            api.Istek(new { }, "/yemek_listesi.php", "", "", dgwDuyuru, null, true, chkListPazartesi, chkListSali, chkListCarsamba, chkListPersembe, chkListCuma, chkListCumartesi, chkListPazar);
        }

        private void BtnYemekListeSil_Click(object sender, EventArgs e)
        {
            api.Istek(new { }, "/yonetici_yemek_listesi_sil.php", "Authorization", fields.TOKEN);

            if (api.HATA != "1")
            {
                chkListPazartesi.Items.Clear();
                chkListSali.Items.Clear();
                chkListCarsamba.Items.Clear();
                chkListPersembe.Items.Clear();
                chkListCuma.Items.Clear();
                chkListCumartesi.Items.Clear();
                chkListPazar.Items.Clear();

                MessageBox.Show("Haftalık yemek listesi silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Bir hata oluştu. Lütfen sistem yöneticisine başvurunuz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
