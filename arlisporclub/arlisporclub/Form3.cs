using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Devart.Data.SQLite;






namespace arlisporclub
{
    public partial class Form3 : Form
    {
        
        public Form3()
        {
            InitializeComponent();   
        }
        List<ListViewItem> items = null;
        
        private void Form3_Load(object sender, EventArgs e)
        {
            List<List<string>> kirmizi = new List<List<string>>();
            List<List<string>> yesil = new List<List<string>>();
            List<List<string>> gri = new List<List<string>>();
            Color renk = Color.Gray;
            int aktif = 0, borclu = 0, donmus = 0;
            Int64 islem;

            string mdfFilename = "fitline.sqlite";
            string outputFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string attachDbFilename = Path.Combine(outputFolder, mdfFilename);
            
            SQLiteConnection sqlConn = new SQLiteConnection();
            sqlConn.ConnectionString = string.Format(@"Data Source={0}",attachDbFilename);
            SQLiteDataReader dr;
            sqlConn.Open();
            SQLiteCommand cmd = new SQLiteCommand("select * from uye_table", sqlConn);
            dr = cmd.ExecuteReader();
            
            listView1.Columns.Add("Kart No", 100);
            listView1.Columns.Add("Adı", 100);
            listView1.Columns.Add("Soyadı", 100);
            listView1.Columns.Add("D.Tarihi", 100);
            listView1.Columns.Add("K.Tarihi", 100);
            listView1.Columns.Add("K.Bitiş.Tarihi", 100);
            listView1.Columns.Add("Aylık Ücret", 100);

            string bg_tarihi = DateTime.Now.ToShortDateString();
            while (dr.Read())
            {
                List<string> gecici = new List<string>();
                string bt_tarihi = Convert.ToString(dr.GetDateTime(8));
                islem = (Convert.ToInt64(bt_tarihi.Substring(0, 2)) + Convert.ToInt64(bt_tarihi.Substring(3, 2)) * 30 + Convert.ToInt64(bt_tarihi.Substring(6, 4)) * 365) - (Convert.ToInt64(bg_tarihi.Substring(0, 2)) + Convert.ToInt64(bg_tarihi.Substring(3, 2)) * 30 + Convert.ToInt64(bg_tarihi.Substring(6, 4)) * 365);
                
                if (dr.GetInt32(9) == 0 && islem < 0)
                {
                    borclu += 1;
                    
                    gecici.Add(dr.GetDecimal(0).ToString());
                    gecici.Add(dr.GetString(1));
                    gecici.Add(dr.GetString(2));
                    gecici.Add(dr.GetDateTime(3).ToString().Substring(0, 10));
                    gecici.Add(dr.GetDateTime(7).ToString().Substring(0, 10));
                    gecici.Add(dr.GetDateTime(8).ToString().Substring(0, 10));
                    gecici.Add(dr.GetInt32(11).ToString() + " T.L.");
                    kirmizi.Add(gecici);  
                }
                else if (dr.GetInt32(9) == 1)
                {
                    donmus += 1;
                    
                    gecici.Add(dr.GetDecimal(0).ToString());
                    gecici.Add(dr.GetString(1));
                    gecici.Add(dr.GetString(2));
                    gecici.Add(dr.GetDateTime(3).ToString().Substring(0, 10));
                    gecici.Add(dr.GetDateTime(7).ToString().Substring(0, 10));
                    gecici.Add(dr.GetDateTime(8).ToString().Substring(0, 10));
                    gecici.Add(dr.GetInt32(11).ToString() + " T.L.");
                    gri.Add(gecici); 
                }
                else
                {
                    aktif += 1;
                    
                    gecici.Add(dr.GetDecimal(0).ToString());
                    gecici.Add(dr.GetString(1));
                    gecici.Add(dr.GetString(2));
                    gecici.Add(dr.GetDateTime(3).ToString().Substring(0, 10));
                    gecici.Add(dr.GetDateTime(7).ToString().Substring(0, 10));
                    gecici.Add(dr.GetDateTime(8).ToString().Substring(0, 10));
                    gecici.Add(dr.GetInt32(11).ToString() + " T.L.");
                    yesil.Add(gecici);  
                }  
            }
            foreach(List<string> i in kirmizi)
            {
                ListViewItem Kullanici = new ListViewItem(i[0]);
                Kullanici.BackColor = Color.Red;
                Kullanici.SubItems.Add(i[1]);
                Kullanici.SubItems.Add(i[2]);
                Kullanici.SubItems.Add(i[3].Substring(0, 10));
                Kullanici.SubItems.Add(i[4].Substring(0, 10));
                Kullanici.SubItems.Add(i[5].Substring(0, 10));
                Kullanici.SubItems.Add(i[6] );
                listView1.Items.Add(Kullanici);
            }
            foreach (List<string> i in gri)
            {
                ListViewItem Kullanici = new ListViewItem(i[0]);
                Kullanici.BackColor = Color.Gray;
                Kullanici.SubItems.Add(i[1]);
                Kullanici.SubItems.Add(i[2]);
                Kullanici.SubItems.Add(i[3].Substring(0, 10));
                Kullanici.SubItems.Add(i[4].Substring(0, 10));
                Kullanici.SubItems.Add(i[5].Substring(0, 10));
                Kullanici.SubItems.Add(i[6]);
                listView1.Items.Add(Kullanici);
            }
            foreach (List<string> i in yesil)
            {
                ListViewItem Kullanici = new ListViewItem(i[0]);
                Kullanici.BackColor = Color.Green;
                Kullanici.SubItems.Add(i[1]);
                Kullanici.SubItems.Add(i[2]);
                Kullanici.SubItems.Add(i[3].Substring(0, 10));
                Kullanici.SubItems.Add(i[4].Substring(0, 10));
                Kullanici.SubItems.Add(i[5].Substring(0, 10));
                Kullanici.SubItems.Add(i[6]);
                listView1.Items.Add(Kullanici);
            }
            borc.Text = borclu.ToString();
            don.Text = donmus.ToString();
            ak.Text = aktif.ToString();
            toplam.Text = (borclu + donmus + aktif).ToString();

            items = listView1.Items.OfType<ListViewItem>().ToList();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            List<ListViewItem> insList = items.Where(x => x.SubItems[1].Text.StartsWith(textBox1.Text, StringComparison.CurrentCultureIgnoreCase)).ToList();
            listView1.Items.Clear();
            insList.ForEach(x => listView1.Items.Add(x));
        }
        private void bilgi_Click(object sender, EventArgs e)
        {
            string mdfFilename = "fitline.sqlite";
            string outputFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string attachDbFilename = Path.Combine(outputFolder, mdfFilename);

            SQLiteConnection sqlConn = new SQLiteConnection();
            sqlConn.ConnectionString = string.Format(@"Data Source={0}", attachDbFilename);
            SQLiteDataReader dr;
            sqlConn.Open();

            try
            {

                SQLiteCommand cmd = new SQLiteCommand(string.Format("select * from uye_table where tc_no = {0}", listView1.SelectedItems[0].Text), sqlConn);
                dr = cmd.ExecuteReader();
                sporcu spor = new sporcu();
                while (dr.Read())
                {
                    spor.tc_no.Text = dr.GetValue(0).ToString();
                    spor.adi.Text = dr.GetString(1);
                    spor.soyadi.Text = dr.GetString(2);
                    spor.telefon.Text = dr.GetString(6);
                    spor.aylik.Text = dr.GetInt32(11).ToString();

                    if (dr.GetString(5) == "")
                        spor.meslek.Text = "YOK";
                    else
                        spor.meslek.Text = dr.GetString(5);

                    spor.dyeri.Text = dr.GetString(4);
                    spor.dtarihi.Text = dr.GetValue(3).ToString().Substring(0, 2) + "." + dr.GetValue(3).ToString().Substring(3, 2) + "." + dr.GetValue(3).ToString().Substring(6, 4);
                    spor.kaytar.Text = dr.GetValue(7).ToString().Substring(0, 2) + "." + dr.GetValue(7).ToString().Substring(3, 2) + "." + dr.GetValue(7).ToString().Substring(6, 4);
                    spor.bittar.Text = dr.GetValue(8).ToString().Substring(0, 2) + "." + dr.GetValue(8).ToString().Substring(3, 2) + "." + dr.GetValue(8).ToString().Substring(6, 4);
                    
                    if (dr.GetString(10) == "")
                        spor.pictureBox2.Image = arlisporclub.Properties.Resources.rsz_1profil_image;
                    else
                        spor.pictureBox2.Image = Image.FromFile(dr.GetString(10));
                    if (Convert.ToInt64(dr.GetValue(9)) == 1)
                        spor.uye_durum.Checked = true;
                    else
                        spor.uye_durum.Checked = false;
                }
                spor.Show();
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Hata: Lütfen T.C. no yu mause yardımıyla seçtikden sonra 'Bilgileri Görüntüle' butonuna tıklayınız!");
            }
        }
    }
}
