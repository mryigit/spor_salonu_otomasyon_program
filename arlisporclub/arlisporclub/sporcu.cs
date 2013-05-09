using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Devart.Data.SQLite;

namespace arlisporclub
{
    public partial class sporcu : Form
    {
        public sporcu()
        {
            InitializeComponent();
        }
        public string yol;
        public int islem;
        public Int64 sonuc;
        private void duzen_Click(object sender, EventArgs e)
        {
            degistir(false);
        }

        private void kaydet_Click(object sender, EventArgs e)
        {
            degistir(true);

            DateTime dtarihi1 = Convert.ToDateTime(dtarihi.Text);
            String dotar = dtarihi1.ToString("yyyy-MM-dd");

            DateTime ktarihi1 = Convert.ToDateTime(kaytar.Text);
            String ktar = ktarihi1.ToString("yyyy-MM-dd");

            DateTime btarihi1 = Convert.ToDateTime(bittar.Text);
            String bitar = btarihi1.ToString("yyyy-MM-dd");

            if(uye_durum.Checked)
                islem = 1;
            else
                islem = 0;

            string mdfFilename = "fitline.sqlite";
            string outputFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string attachDbFilename = Path.Combine(outputFolder, mdfFilename);
            
            SQLiteConnection sqlConn = new SQLiteConnection();
            sqlConn.ConnectionString = string.Format(@"Data Source={0}", attachDbFilename);
            sqlConn.Open();
            SQLiteCommand cmd = new SQLiteCommand(string.Format("UPDATE uye_table SET adi='{0}',soyadi = '{1}', d_tarihi = '{2}', d_yeri = '{3}', meslek = '{4}', telefon = '{5}', k_tarihi = '{6}', b_tarihi = '{7}', d_islemi = '{8}', resim_yol = '{9}', ucret = '{11}' where tc_no = '{10}'",adi.Text,soyadi.Text,dotar,dyeri.Text,meslek.Text,telefon.Text,ktar,bitar,islem,yol,tc_no.Text,Convert.ToInt32(aylik.Text)), sqlConn);
            cmd.ExecuteNonQuery();
            sqlConn.Close();

            MessageBox.Show("Güncelleme Başarılı.");
            this.Close();

            Form3.ActiveForm.Close();
            Form3 f3 = new Form3();
            f3.Show();
        }
        private void degistir(bool x)
        {
            tc_no.ReadOnly = x;
            adi.ReadOnly = x;
            soyadi.ReadOnly = x;
            telefon.ReadOnly = x;
            meslek.ReadOnly = x;
            dyeri.Enabled = !x;
            dtarihi.Enabled = !x;
            kaytar.Enabled = !x;
            bittar.Enabled = !x;
            ressec_gizli.Enabled = !x;
            uye_durum.Enabled = !x;
            aylik.ReadOnly = x;  
        }
        private void ressec_gizli_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "C# Corner Open File Dialog";
            fdlg.InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            fdlg.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;

            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                yol = fdlg.FileName;
            }

            try
            {
                pictureBox2.Image = Image.FromFile(fdlg.FileName);
                pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            }
            catch (ArgumentException)
            { 
                MessageBox.Show("fotoğraf seçilemedi"); 
            }
        }
        private void sil_Click(object sender, EventArgs e)
        {
            DialogResult  karar;
            karar = MessageBox.Show( "Bu kullanıcıyı silmek istediğinize eminmisiniz?",
                "uyarı",
                MessageBoxButtons.YesNo);

            if (karar == DialogResult.Yes)
            {
                string mdfFilename = "fitline.sqlite";
                string outputFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string attachDbFilename = Path.Combine(outputFolder, mdfFilename);
                
                SQLiteConnection sqlConn = new SQLiteConnection();
                sqlConn.ConnectionString = string.Format(@"Data Source={0}", attachDbFilename);
                sqlConn.Open();
                SQLiteCommand cmd = new SQLiteCommand(string.Format("delete from uye_table where tc_no = '{0}'", tc_no.Text), sqlConn);
                cmd.ExecuteNonQuery();
                sqlConn.Close();

                MessageBox.Show("Silme işlemi Başarılı!");
                this.Close();

                Form3.ActiveForm.Close();
                Form3 f3 = new Form3();
                f3.Show();
            }
            else
                MessageBox.Show("işlem iptal edildi!");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime btarihi1 = Convert.ToDateTime(bittar.Text);
            String bitar = btarihi1.ToString("yyyy-MM-dd");
            string bugun = DateTime.Now.ToString("yyyy-MM-dd");
            Int64 gun,ay,yil;
            DialogResult  secim;
            String yenibitar;

            yil = Convert.ToInt64(bugun.Substring(0, 4))-Convert.ToInt64(bitar.Substring(0, 4));
            
            if (Convert.ToInt64(bugun.Substring(5, 2)) < Convert.ToInt64(bitar.Substring(5, 2)))
            {
                yil = yil - 1;
                ay = Convert.ToInt64(bugun.Substring(5, 2)) - Convert.ToInt64(bitar.Substring(5, 2)) + 12;
            }
            else
                ay = Convert.ToInt64(bugun.Substring(5, 2)) - Convert.ToInt64(bitar.Substring(5, 2));

            if (Convert.ToInt64(bugun.Substring(8, 2)) < Convert.ToInt64(bitar.Substring(8, 2)))
            {
                ay = ay - 1;
                gun = Convert.ToInt64(bugun.Substring(8, 2)) - Convert.ToInt64(bitar.Substring(8, 2)) + 30;
            }
            else
                gun = Convert.ToInt64(bugun.Substring(8, 2)) - Convert.ToInt64(bitar.Substring(8, 2));

            secim = MessageBox.Show(string.Format("bu kullanıcı {0} gun {1} ay {2} yıl boyunca ödeme yapmamış bu dikkate alınsınmı?",gun, ay, yil),
                "uyarı",
                MessageBoxButtons.YesNo);
            
            if (secim == DialogResult.Yes)
            {
                if (Convert.ToInt64(bitar.Substring(5, 2)) == 12)
                    yenibitar = (Convert.ToInt64(bitar.Substring(0, 4)) + 1).ToString() + "-" + "1" + "-" + bitar.Substring(8, 2);
                else
                    yenibitar = bitar.Substring(0, 4) + "-" + (Convert.ToInt64(bitar.Substring(5, 2)) + 1).ToString() + "-" + bitar.Substring(8, 2);
            }
            else
            {
                if (Convert.ToInt64(bugun.Substring(5, 2)) >= 12)
                    yenibitar = (Convert.ToInt64(bugun.Substring(0, 4)) + 1).ToString() + "-" + "1" + "-" + bugun.Substring(8, 2);
                else
                    yenibitar = bugun.Substring(0, 4) + "-" + (Convert.ToInt64(bugun.Substring(5, 2)) + 1).ToString() + "-" + bugun.Substring(8, 2);
            }
             
            string mdfFilename = "fitline.sqlite";
            string outputFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string attachDbFilename = Path.Combine(outputFolder, mdfFilename);
            
            SQLiteConnection sqlConn = new SQLiteConnection();
            sqlConn.ConnectionString = string.Format(@"Data Source={0}", attachDbFilename);
            sqlConn.Open();
            SQLiteCommand cmd = new SQLiteCommand(string.Format(@"UPDATE uye_table SET b_tarihi = '{0}' where tc_no = '{1}' ",yenibitar,tc_no.Text), sqlConn);
            cmd.ExecuteNonQuery();
            sqlConn.Close();

            MessageBox.Show("Ödeme işlemi başarılı!");
            this.Close();

            Form3.ActiveForm.Close();
            Form3 f3 = new Form3();
            f3.Show();   
        }
    }
}
