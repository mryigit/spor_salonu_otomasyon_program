using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Devart.Data.SQLite;

namespace arlisporclub
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            DateTime dt = DateTime.Now;
            int year = dt.Year;
            int month = dt.Month;
            int day = dt.Day;
            string day1, mont1;

            if (day.ToString().Length == 1)
                day1 = "0" + day.ToString();
            else
                day1 = day.ToString();
            if (month.ToString().Length == 1)
                mont1 = "0" + month.ToString();
            else
                mont1 = month.ToString();

            kayit_tar.Text = day1 + "." + mont1 + "." + year.ToString();
            dtarihi.Text = day1 + "." + mont1 + "." + year.ToString();
        }
        public string yol;       

        private void tc_no_TextChanged(object sender, EventArgs e)
        {
            long a;
            if (!long.TryParse(tc_no.Text, out a))
                tc_no.Clear();
        }
        private void yeni_kaydet_Click(object sender, EventArgs e)
        {
            if (tc_no.Text == "")
            {
                MessageBox.Show("Kart no alanı boş bırakılamaz!", "Hata1");
                return;
            }
            if (adi.Text == "")
            { 
                MessageBox.Show("İsim alanı boş bırakılamaz!", "Hata3");
                return; 
            }
            if (soyadi.Text == "")
            { 
                MessageBox.Show("Soyadi alanı boş bırakılamaz!", "Hata4");
                return; 
            }
            if (!Char.IsDigit(telefon.Text[telefon.Text.Length-1]))
            {
                MessageBox.Show("Telefon 10 haneli olmalıdır!", "Hata2");
                return;
            }

            string mdfFilename = "fitline.sqlite";
            string outputFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string attachDbFilename = Path.Combine(outputFolder, mdfFilename);
            
            SQLiteConnection sqlConn = new SQLiteConnection();
            sqlConn.ConnectionString = string.Format(@"Data Source={0}", attachDbFilename);
            sqlConn.Open();
            
            DateTime  dtarihi1 = Convert.ToDateTime(dtarihi.Text);
            String zaman1 = dtarihi1.ToString("yyyy-MM-dd");

            DateTime kayit_tar1 = Convert.ToDateTime(kayit_tar.Text);
            String zaman2 = kayit_tar1.ToString("yyyy-MM-dd");
            String zaman3 = "";

            if (Convert.ToInt32(zaman2.Substring(5, 2)) == 12)
                zaman3 = Convert.ToString(Convert.ToInt32(zaman2.Substring(0, 4)) +1) +"-"+ Convert.ToString(0 + Convert.ToInt32(comboBox1.Text)) + zaman2.Substring(7, 3);
            else
                zaman3 = zaman2.Substring(0, 5) + Convert.ToString(Convert.ToInt32(zaman2.Substring(5, 2)) + Convert.ToInt32(comboBox1.Text)) + zaman2.Substring(7, 3);

            string sql2 = string.Format(@"INSERT INTO uye_table (tc_no, adi, soyadi, d_tarihi, d_yeri, meslek, telefon, k_tarihi, b_tarihi, resim_yol, ucret) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}')", tc_no.Text, adi.Text, soyadi.Text, zaman1, dyeri.Text, meslek.Text, telefon.Text, zaman2, zaman3, yol, Convert.ToInt64(aylik.Text));
            
            SQLiteDataAdapter adapter = new SQLiteDataAdapter();
            SQLiteCommand command = new SQLiteCommand(sql2,sqlConn);
            try
            {
                command.ExecuteNonQuery();
                command.Connection = sqlConn;
                adapter.SelectCommand = command;
                sqlConn.Close();
            }
            catch (Devart.Data.SQLite.SQLiteException)
            {
                MessageBox.Show("Hata:Bu T.C.No kullanılmaktadır lütfen kontrol ederek tekrar deneyiniz!");
                return;
            }

            MessageBox.Show("kayıt başarılı!");

            tc_no.Clear(); adi.Clear(); soyadi.Clear(); telefon.Clear(); meslek.Clear();
            telefon.Text = "5";
            aylik.Text = "50";
            pictureBox1.Image = arlisporclub.Properties.Resources.rsz_1profil_image; 
        }
        private void resimsec_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "C# Corner Open File Dialog";
            fdlg.InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            fdlg.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                 yol  = fdlg.FileName;
            }
            try
            {
                pictureBox1.Image = Image.FromFile(fdlg.FileName);
                pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            }
            catch (ArgumentException) 
            { 
                MessageBox.Show("fotoğraf seçilemedi");
            }
        }
    }
}
