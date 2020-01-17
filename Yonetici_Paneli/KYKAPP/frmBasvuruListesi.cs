using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace KYKAPP
{
    public partial class frmBasvuruListesi : Form
    {
        API api = new API();
        public frmBasvuruListesi()
        {
            InitializeComponent();
        }

        public string TOKEN = fields.TOKEN;
        public string Dizin;

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
        private void dgwVeriEkle(object jsonVeri,string link, DataGridView dgwObje)
        {
            api.Istek(jsonVeri, link, "Authorization", fields.TOKEN, dgwObje);
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

        private void FrmBasvuruListesi_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            panelBasvuruListe.BackColor = Color.FromArgb(206, 14, 14);

            btnBasvuruListeAktar.BackColor = Color.FromArgb(6, 47, 119);
            btnBasvuruListeKayitSil.BackColor = Color.FromArgb(6, 47, 119);
            btnBasvuruListeSecKaldir.BackColor = Color.FromArgb(6, 47, 119);

            tslblBasvuruListeDurum.Text = "Lütfen Bekleyin...";

            dgwVeriEkle(new {
                KAYIT_TIP = fields.BASVURU_KAYIT_TIP,
                KAYIT_ID = fields.BASVURU_KAYIT_ID
            }, "/yonetici_basvuru_ogr_liste.php", dgwBasvuruListe);

            tslblBasvuruListeDurum.Text = "Kayıtlar Getirildi.";

        }

        private void BtnBasvuruListeAktar_Click(object sender, EventArgs e)
        {    
            if(dgwBasvuruListe.Rows.Count > 0)
            {
                SaveFileDialog excelBelge = new SaveFileDialog
                {
                    Filter = "Excel | *.xlsx",
                    OverwritePrompt = true,
                    FileName = fields.BASVURU_KAYIT_AD + " Başvuru Listesi"
                };

                if (excelBelge.ShowDialog() == DialogResult.OK)
                {
                    Dizin = excelBelge.FileName;

                    tslblBasvuruListeDurum.Text = "Aktarma İşlemi Başladı. Lütfen Bekleyin...";
                    backGWBasvuruListe.RunWorkerAsync();
                    pBarBasvuruListe.Visible = true;
                }
            }
            else
                MessageBox.Show("Başvuru Listesi Boş", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void BackGWBasvuruListe_DoWork(object sender, DoWorkEventArgs e)
        {
            DisariAktar(dgwBasvuruListe, Dizin, pBarBasvuruListe, backGWBasvuruListe);
        }

        private void BackGWBasvuruListe_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pBarBasvuruListe.Value = e.ProgressPercentage;
        }

        private void BackGWBasvuruListe_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tslblBasvuruListeDurum.Text = "Aktarma İşlemi Tamamlandı.";
            pBarBasvuruListe.Visible = false;
            pBarBasvuruListe.Value = 0;
        }

        private void LblKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DgwBasvuruListe_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            lblBasvuruListeAdet.Text = dgwBasvuruListe.Rows.Count.ToString();
        }

        private void BtnBasvuruListeSecKaldir_Click(object sender, EventArgs e)
        {
            if (dgwBasvuruListe.Rows.Count > 0)
                secKaldir(dgwBasvuruListe);
            else
                MessageBox.Show("Başvuru Listesi Boş","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        private void BtnBasvuruListeKayitSil_Click(object sender, EventArgs e)
        {
            string idler = secIdDondur(dgwBasvuruListe, 0);

            if (idler != "")
            {
                api.Istek(new
                {
                    SEC = idler,
                    KAYIT_TIP = fields.BASVURU_KAYIT_TIP
                }, "/yonetici_basvuru_ogr_sil.php", "Authorization", fields.TOKEN);

                if (api.HATA != "1")
                {
                    MessageBox.Show("İşlem Gerçekleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgwVeriEkle(new
                    {
                        KAYIT_TIP = fields.BASVURU_KAYIT_TIP,
                        KAYIT_ID = fields.BASVURU_KAYIT_ID
                    }, "/yonetici_basvuru_ogr_liste.php", dgwBasvuruListe);

                    tslblBasvuruListeDurum.Text = "Kayıt Silindi.";
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
    }
}
