namespace KYKAPP
{
    partial class frmBasvuruListesi
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgwBasvuruListe = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgwOGRAD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgwOGRSOYAD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgwOGRCEPTEL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panelBasvuruListe = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblKapat = new System.Windows.Forms.Label();
            this.btnBasvuruListeKayitSil = new System.Windows.Forms.Button();
            this.btnBasvuruListeSecKaldir = new System.Windows.Forms.Button();
            this.btnBasvuruListeAktar = new System.Windows.Forms.Button();
            this.sStripBasvuruListe = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblBasvuruListeDurum = new System.Windows.Forms.ToolStripStatusLabel();
            this.pBarBasvuruListe = new System.Windows.Forms.ToolStripProgressBar();
            this.lblBasvuruListeAdet = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.backGWBasvuruListe = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.dgwBasvuruListe)).BeginInit();
            this.panelBasvuruListe.SuspendLayout();
            this.sStripBasvuruListe.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgwBasvuruListe
            // 
            this.dgwBasvuruListe.AllowUserToAddRows = false;
            this.dgwBasvuruListe.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.ForestGreen;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.dgwBasvuruListe.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgwBasvuruListe.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgwBasvuruListe.BackgroundColor = System.Drawing.Color.White;
            this.dgwBasvuruListe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwBasvuruListe.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn12,
            this.dgwOGRAD,
            this.dgwOGRSOYAD,
            this.dgwOGRCEPTEL,
            this.dataGridViewCheckBoxColumn4});
            this.dgwBasvuruListe.GridColor = System.Drawing.Color.Silver;
            this.dgwBasvuruListe.Location = new System.Drawing.Point(7, 71);
            this.dgwBasvuruListe.Name = "dgwBasvuruListe";
            this.dgwBasvuruListe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwBasvuruListe.Size = new System.Drawing.Size(488, 372);
            this.dgwBasvuruListe.TabIndex = 43;
            this.dgwBasvuruListe.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.DgwBasvuruListe_RowsAdded);
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn12.HeaderText = "ID";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Visible = false;
            // 
            // dgwOGRAD
            // 
            this.dgwOGRAD.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgwOGRAD.HeaderText = "AD";
            this.dgwOGRAD.Name = "dgwOGRAD";
            // 
            // dgwOGRSOYAD
            // 
            this.dgwOGRSOYAD.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgwOGRSOYAD.HeaderText = "SOYAD";
            this.dgwOGRSOYAD.Name = "dgwOGRSOYAD";
            // 
            // dgwOGRCEPTEL
            // 
            this.dgwOGRCEPTEL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgwOGRCEPTEL.HeaderText = "TELEFON";
            this.dgwOGRCEPTEL.Name = "dgwOGRCEPTEL";
            // 
            // dataGridViewCheckBoxColumn4
            // 
            this.dataGridViewCheckBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewCheckBoxColumn4.HeaderText = "SEÇ";
            this.dataGridViewCheckBoxColumn4.Name = "dataGridViewCheckBoxColumn4";
            this.dataGridViewCheckBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewCheckBoxColumn4.Width = 53;
            // 
            // panelBasvuruListe
            // 
            this.panelBasvuruListe.BackColor = System.Drawing.Color.Red;
            this.panelBasvuruListe.Controls.Add(this.label1);
            this.panelBasvuruListe.Controls.Add(this.lblKapat);
            this.panelBasvuruListe.Location = new System.Drawing.Point(-1, 0);
            this.panelBasvuruListe.Name = "panelBasvuruListe";
            this.panelBasvuruListe.Size = new System.Drawing.Size(502, 32);
            this.panelBasvuruListe.TabIndex = 44;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(4, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Başvuru Listesi";
            // 
            // lblKapat
            // 
            this.lblKapat.AutoSize = true;
            this.lblKapat.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblKapat.ForeColor = System.Drawing.Color.White;
            this.lblKapat.Location = new System.Drawing.Point(478, 6);
            this.lblKapat.Name = "lblKapat";
            this.lblKapat.Size = new System.Drawing.Size(18, 18);
            this.lblKapat.TabIndex = 1;
            this.lblKapat.Text = "X";
            this.lblKapat.Click += new System.EventHandler(this.LblKapat_Click);
            // 
            // btnBasvuruListeKayitSil
            // 
            this.btnBasvuruListeKayitSil.BackColor = System.Drawing.Color.Black;
            this.btnBasvuruListeKayitSil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBasvuruListeKayitSil.ForeColor = System.Drawing.Color.White;
            this.btnBasvuruListeKayitSil.Location = new System.Drawing.Point(371, 38);
            this.btnBasvuruListeKayitSil.Name = "btnBasvuruListeKayitSil";
            this.btnBasvuruListeKayitSil.Size = new System.Drawing.Size(124, 27);
            this.btnBasvuruListeKayitSil.TabIndex = 55;
            this.btnBasvuruListeKayitSil.Text = "Seçilen Başvuruları Sil";
            this.btnBasvuruListeKayitSil.UseVisualStyleBackColor = false;
            this.btnBasvuruListeKayitSil.Click += new System.EventHandler(this.BtnBasvuruListeKayitSil_Click);
            // 
            // btnBasvuruListeSecKaldir
            // 
            this.btnBasvuruListeSecKaldir.BackColor = System.Drawing.Color.Black;
            this.btnBasvuruListeSecKaldir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBasvuruListeSecKaldir.ForeColor = System.Drawing.Color.White;
            this.btnBasvuruListeSecKaldir.Location = new System.Drawing.Point(190, 38);
            this.btnBasvuruListeSecKaldir.Name = "btnBasvuruListeSecKaldir";
            this.btnBasvuruListeSecKaldir.Size = new System.Drawing.Size(124, 27);
            this.btnBasvuruListeSecKaldir.TabIndex = 54;
            this.btnBasvuruListeSecKaldir.Text = "Tümünü Seç / Kaldır";
            this.btnBasvuruListeSecKaldir.UseVisualStyleBackColor = false;
            this.btnBasvuruListeSecKaldir.Click += new System.EventHandler(this.BtnBasvuruListeSecKaldir_Click);
            // 
            // btnBasvuruListeAktar
            // 
            this.btnBasvuruListeAktar.BackColor = System.Drawing.Color.Black;
            this.btnBasvuruListeAktar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBasvuruListeAktar.ForeColor = System.Drawing.Color.White;
            this.btnBasvuruListeAktar.Location = new System.Drawing.Point(6, 38);
            this.btnBasvuruListeAktar.Name = "btnBasvuruListeAktar";
            this.btnBasvuruListeAktar.Size = new System.Drawing.Size(125, 27);
            this.btnBasvuruListeAktar.TabIndex = 53;
            this.btnBasvuruListeAktar.Text = "Listeyi Dışarı Aktar";
            this.btnBasvuruListeAktar.UseVisualStyleBackColor = false;
            this.btnBasvuruListeAktar.Click += new System.EventHandler(this.BtnBasvuruListeAktar_Click);
            // 
            // sStripBasvuruListe
            // 
            this.sStripBasvuruListe.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel5,
            this.tslblBasvuruListeDurum,
            this.pBarBasvuruListe});
            this.sStripBasvuruListe.Location = new System.Drawing.Point(0, 448);
            this.sStripBasvuruListe.Name = "sStripBasvuruListe";
            this.sStripBasvuruListe.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.sStripBasvuruListe.Size = new System.Drawing.Size(501, 22);
            this.sStripBasvuruListe.SizingGrip = false;
            this.sStripBasvuruListe.TabIndex = 56;
            this.sStripBasvuruListe.Text = "statusStrip1";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(50, 17);
            this.toolStripStatusLabel5.Text = "Durum :";
            // 
            // tslblBasvuruListeDurum
            // 
            this.tslblBasvuruListeDurum.Name = "tslblBasvuruListeDurum";
            this.tslblBasvuruListeDurum.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tslblBasvuruListeDurum.Size = new System.Drawing.Size(90, 17);
            this.tslblBasvuruListeDurum.Text = "Kayıtlar Getirildi";
            // 
            // pBarBasvuruListe
            // 
            this.pBarBasvuruListe.Name = "pBarBasvuruListe";
            this.pBarBasvuruListe.Size = new System.Drawing.Size(100, 16);
            this.pBarBasvuruListe.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pBarBasvuruListe.Visible = false;
            // 
            // lblBasvuruListeAdet
            // 
            this.lblBasvuruListeAdet.AutoEllipsis = true;
            this.lblBasvuruListeAdet.AutoSize = true;
            this.lblBasvuruListeAdet.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblBasvuruListeAdet.Location = new System.Drawing.Point(468, 452);
            this.lblBasvuruListeAdet.Name = "lblBasvuruListeAdet";
            this.lblBasvuruListeAdet.Size = new System.Drawing.Size(13, 13);
            this.lblBasvuruListeAdet.TabIndex = 58;
            this.lblBasvuruListeAdet.Text = "0";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label21.Location = new System.Drawing.Point(398, 452);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(71, 13);
            this.label21.TabIndex = 57;
            this.label21.Text = "Toplam Kayıt:";
            // 
            // backGWBasvuruListe
            // 
            this.backGWBasvuruListe.WorkerReportsProgress = true;
            this.backGWBasvuruListe.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackGWBasvuruListe_DoWork);
            this.backGWBasvuruListe.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackGWBasvuruListe_ProgressChanged);
            this.backGWBasvuruListe.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackGWBasvuruListe_RunWorkerCompleted);
            // 
            // frmBasvuruListesi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(501, 470);
            this.Controls.Add(this.lblBasvuruListeAdet);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.sStripBasvuruListe);
            this.Controls.Add(this.btnBasvuruListeKayitSil);
            this.Controls.Add(this.btnBasvuruListeSecKaldir);
            this.Controls.Add(this.btnBasvuruListeAktar);
            this.Controls.Add(this.panelBasvuruListe);
            this.Controls.Add(this.dgwBasvuruListe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmBasvuruListesi";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmBasvuruListesi";
            this.Load += new System.EventHandler(this.FrmBasvuruListesi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwBasvuruListe)).EndInit();
            this.panelBasvuruListe.ResumeLayout(false);
            this.panelBasvuruListe.PerformLayout();
            this.sStripBasvuruListe.ResumeLayout(false);
            this.sStripBasvuruListe.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgwBasvuruListe;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgwOGRAD;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgwOGRSOYAD;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgwOGRCEPTEL;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn4;
        private System.Windows.Forms.Panel panelBasvuruListe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblKapat;
        private System.Windows.Forms.Button btnBasvuruListeKayitSil;
        private System.Windows.Forms.Button btnBasvuruListeSecKaldir;
        private System.Windows.Forms.Button btnBasvuruListeAktar;
        private System.Windows.Forms.StatusStrip sStripBasvuruListe;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel tslblBasvuruListeDurum;
        private System.Windows.Forms.ToolStripProgressBar pBarBasvuruListe;
        private System.Windows.Forms.Label lblBasvuruListeAdet;
        private System.Windows.Forms.Label label21;
        private System.ComponentModel.BackgroundWorker backGWBasvuruListe;
    }
}