namespace KYKAPP
{
    partial class frmGiris
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGiris));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblKucult = new System.Windows.Forms.Label();
            this.lblKapat = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKadi = new System.Windows.Forms.TextBox();
            this.txtParola = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGiris = new System.Windows.Forms.Button();
            this.pboxGirisYukleniyor = new System.Windows.Forms.PictureBox();
            this.lblGirisYapiliyor = new System.Windows.Forms.Label();
            this.backGWPanelGiris = new System.ComponentModel.BackgroundWorker();
            this.timerAnaSayfa = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxGirisYukleniyor)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Red;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblKucult);
            this.panel1.Controls.Add(this.lblKapat);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(463, 32);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(4, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "BUCAK KYK";
            // 
            // lblKucult
            // 
            this.lblKucult.AutoSize = true;
            this.lblKucult.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblKucult.ForeColor = System.Drawing.Color.White;
            this.lblKucult.Location = new System.Drawing.Point(419, 6);
            this.lblKucult.Name = "lblKucult";
            this.lblKucult.Size = new System.Drawing.Size(17, 24);
            this.lblKucult.TabIndex = 2;
            this.lblKucult.Text = "-";
            this.lblKucult.Click += new System.EventHandler(this.LblKucult_Click);
            // 
            // lblKapat
            // 
            this.lblKapat.AutoSize = true;
            this.lblKapat.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblKapat.ForeColor = System.Drawing.Color.White;
            this.lblKapat.Location = new System.Drawing.Point(442, 7);
            this.lblKapat.Name = "lblKapat";
            this.lblKapat.Size = new System.Drawing.Size(18, 18);
            this.lblKapat.TabIndex = 1;
            this.lblKapat.Text = "X";
            this.lblKapat.Click += new System.EventHandler(this.LblKapat_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(0, 31);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(230, 269);
            this.panel2.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(4, 32);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(220, 200);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(254, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Kullanıcı Adı";
            // 
            // txtKadi
            // 
            this.txtKadi.Location = new System.Drawing.Point(257, 110);
            this.txtKadi.Name = "txtKadi";
            this.txtKadi.Size = new System.Drawing.Size(178, 20);
            this.txtKadi.TabIndex = 4;
            // 
            // txtParola
            // 
            this.txtParola.Location = new System.Drawing.Point(258, 159);
            this.txtParola.Name = "txtParola";
            this.txtParola.Size = new System.Drawing.Size(177, 20);
            this.txtParola.TabIndex = 5;
            this.txtParola.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(255, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Parola";
            // 
            // btnGiris
            // 
            this.btnGiris.BackColor = System.Drawing.Color.Black;
            this.btnGiris.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGiris.ForeColor = System.Drawing.Color.White;
            this.btnGiris.Location = new System.Drawing.Point(291, 196);
            this.btnGiris.Name = "btnGiris";
            this.btnGiris.Size = new System.Drawing.Size(109, 38);
            this.btnGiris.TabIndex = 7;
            this.btnGiris.Text = "Giriş yap";
            this.btnGiris.UseVisualStyleBackColor = false;
            this.btnGiris.Click += new System.EventHandler(this.BtnGiris_Click);
            // 
            // pboxGirisYukleniyor
            // 
            this.pboxGirisYukleniyor.Image = ((System.Drawing.Image)(resources.GetObject("pboxGirisYukleniyor.Image")));
            this.pboxGirisYukleniyor.Location = new System.Drawing.Point(258, 256);
            this.pboxGirisYukleniyor.Name = "pboxGirisYukleniyor";
            this.pboxGirisYukleniyor.Size = new System.Drawing.Size(20, 20);
            this.pboxGirisYukleniyor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboxGirisYukleniyor.TabIndex = 8;
            this.pboxGirisYukleniyor.TabStop = false;
            // 
            // lblGirisYapiliyor
            // 
            this.lblGirisYapiliyor.AutoSize = true;
            this.lblGirisYapiliyor.Location = new System.Drawing.Point(278, 259);
            this.lblGirisYapiliyor.Name = "lblGirisYapiliyor";
            this.lblGirisYapiliyor.Size = new System.Drawing.Size(154, 13);
            this.lblGirisYapiliyor.TabIndex = 9;
            this.lblGirisYapiliyor.Text = "Giriş yapılıyor. Lütfen bekleyin...";
            // 
            // backGWPanelGiris
            // 
            this.backGWPanelGiris.WorkerReportsProgress = true;
            this.backGWPanelGiris.WorkerSupportsCancellation = true;
            this.backGWPanelGiris.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackGWPanelGiris_DoWork);
            // 
            // timerAnaSayfa
            // 
            this.timerAnaSayfa.Enabled = true;
            this.timerAnaSayfa.Tick += new System.EventHandler(this.TimerAnaSayfa_Tick);
            // 
            // frmGiris
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(462, 299);
            this.Controls.Add(this.lblGirisYapiliyor);
            this.Controls.Add(this.pboxGirisYukleniyor);
            this.Controls.Add(this.btnGiris);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtParola);
            this.Controls.Add(this.txtKadi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmGiris";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmGiris_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmGiris_MouseDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxGirisYukleniyor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblKucult;
        private System.Windows.Forms.Label lblKapat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtKadi;
        private System.Windows.Forms.TextBox txtParola;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnGiris;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pboxGirisYukleniyor;
        private System.Windows.Forms.Label lblGirisYapiliyor;
        private System.ComponentModel.BackgroundWorker backGWPanelGiris;
        private System.Windows.Forms.Timer timerAnaSayfa;
    }
}

