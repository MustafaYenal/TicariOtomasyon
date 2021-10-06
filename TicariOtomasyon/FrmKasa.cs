using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.Charts;

namespace TicariOtomasyon
{
    public partial class FrmKasa : Form
    {
        public FrmKasa()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        void musterihareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute MusteriHareketler", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void firmahareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute FirmaHareketler", bgl.baglanti());
            da.Fill(dt);
            gridControl3.DataSource = dt;
        }

        public string ad;
        private void FrmKasa_Load(object sender, EventArgs e)
        {
            LblAktifKullanıcı.Text = ad;

            musterihareket();
            firmahareket();

            //Toplam tutar
            SqlCommand komut1 = new SqlCommand("Select SUM(TUTAR) FROM TBL_FATURADETAY", bgl.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                LblToplamTutar.Text = dr1[0].ToString() + " TL";
            }
            bgl.baglanti().Close();

            //Son ayın toplam faturaları
            SqlCommand komut2 = new SqlCommand("Select (ELEKTIRIK+DOGALGAZ+SU+INTERNET+EKSTRA) FROM TBL_GIDERLER ORDER BY ID ASC ",bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                LblOdemeler.Text = dr2[0].ToString()+" TL";
            }
            bgl.baglanti().Close();

            //Son ayın personel maaşları
            SqlCommand komut3 = new SqlCommand("Select MAASLAR FROM TBL_GIDERLER ORDER BY ID ASC ", bgl.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                LblPersonelMaaslari.Text = dr3[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam Müşteri Sayısı
            SqlCommand komut4 = new SqlCommand("Select count(*) from TBL_MUSTERILER", bgl.baglanti());
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                LblMusteriSayısı.Text = dr4[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam Firma Sayısı
            SqlCommand komut5 = new SqlCommand("Select count(*) from TBL_FIRMALAR", bgl.baglanti());
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                LblFirmaSayisi.Text = dr5[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam Personel Sayısı
            SqlCommand komut6 = new SqlCommand("Select count(*) from TBL_PERSONELLER", bgl.baglanti());
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                LblPersonelSayısı.Text = dr6[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam Firmasehir Sayısı
            SqlCommand komut7 = new SqlCommand("Select count(Distinct(IL)) from TBL_FIRMALAR", bgl.baglanti());
            SqlDataReader dr7 = komut7.ExecuteReader();
            while (dr7.Read())
            {
                LblSehirSayisi1.Text = dr7[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam MUSTERİSEHİR Sayısı
            SqlCommand komut8 = new SqlCommand("Select count(Distinct(IL)) from TBL_MUSTERILER", bgl.baglanti());
            SqlDataReader dr8 = komut8.ExecuteReader();
            while (dr8.Read())
            {
                LblSehirSayisi2.Text = dr8[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam STOK Sayısı
            SqlCommand komut9 = new SqlCommand("Select SUM(ADET) from TBL_URUNLER", bgl.baglanti());
            SqlDataReader dr9 = komut9.ExecuteReader();
            while (dr9.Read())
            {
                LblStokSayısı.Text = dr9[0].ToString();
            }
            bgl.baglanti().Close();

            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_GIDERLER", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource = dt;
            

        }

        int sayac = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            if (sayac>0 && sayac<=5)
            {
                //son 4 ay elektirk faturası
                chartControl1.Series["AYLAR"].Points.Clear();
                groupControl10.Text = "ELEKTIRIK";
                SqlCommand komut10 = new SqlCommand("Select top 4 AY,ELEKTIRIK FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }
            else if (sayac>5 && sayac<=10)
            {
                //son 4 ay su faturası
                chartControl1.Series["AYLAR"].Points.Clear();
                groupControl10.Text = "SU";
                SqlCommand komut11 = new SqlCommand("Select top 4 AY,SU FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
            else if (sayac > 10 && sayac <= 15)
            {
                chartControl1.Series["AYLAR"].Points.Clear();
                groupControl10.Text = "DOĞALGAZ";
                SqlCommand komut12 = new SqlCommand("Select top 4 AY,DOGALGAZ FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr12 = komut12.ExecuteReader();
                while (dr12.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr12[0], dr12[1]));
                }
                bgl.baglanti().Close();
            }
            else if (sayac > 15 && sayac <= 20)
            {
                chartControl1.Series["AYLAR"].Points.Clear();
                groupControl10.Text = "İNTERNET";
                SqlCommand komut13 = new SqlCommand("Select top 4 AY,INTERNET FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr13 = komut13.ExecuteReader();
                while (dr13.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr13[0], dr13[1]));
                }
                bgl.baglanti().Close();
            }
            else if (sayac > 20 && sayac <= 25)
            {
                chartControl1.Series["AYLAR"].Points.Clear();
                groupControl10.Text = "EKSTRA";
                SqlCommand komut14 = new SqlCommand("Select top 4 AY,EKSTRA FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr14 = komut14.ExecuteReader();
                while (dr14.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr14[0], dr14[1]));
                }
                bgl.baglanti().Close();
            }
            else if (sayac==25)
            {
                sayac = 0;
            }

        }

        int sayac2=0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            sayac2++;
            if (sayac2 > 0 && sayac2 <= 5)
            {
                //son 4 ay elektirk faturası
                chartControl2.Series["AYLAR"].Points.Clear();
                groupControl11.Text = "ELEKTIRIK";
                SqlCommand komut10 = new SqlCommand("Select top 4 AY,ELEKTIRIK FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }
            else if (sayac2 > 5 && sayac2 <= 10)
            {
                //son 4 ay su faturası
                chartControl2.Series["AYLAR"].Points.Clear();
                groupControl11.Text = "SU";
                SqlCommand komut11 = new SqlCommand("Select top 4 AY,SU FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
            else if (sayac2 > 10 && sayac2 <= 15)
            {
                chartControl2.Series["AYLAR"].Points.Clear();
                groupControl11.Text = "DOĞALGAZ";
                SqlCommand komut12 = new SqlCommand("Select top 4 AY,DOGALGAZ FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr12 = komut12.ExecuteReader();
                while (dr12.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr12[0], dr12[1]));
                }
                bgl.baglanti().Close();
            }
            else if (sayac2 > 15 && sayac2 <= 20)
            {
                chartControl2.Series["AYLAR"].Points.Clear();
                groupControl11.Text = "İNTERNET";
                SqlCommand komut13 = new SqlCommand("Select top 4 AY,INTERNET FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr13 = komut13.ExecuteReader();
                while (dr13.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr13[0], dr13[1]));
                }
                bgl.baglanti().Close();
            }
            else if (sayac2 > 20 && sayac2 <= 25)
            {
                chartControl2.Series["AYLAR"].Points.Clear();
                groupControl11.Text = "EKSTRA";
                SqlCommand komut14 = new SqlCommand("Select top 4 AY,EKSTRA FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr14 = komut14.ExecuteReader();
                while (dr14.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr14[0], dr14[1]));
                }
                bgl.baglanti().Close();
            }
            else if (sayac2 == 25)
            {
                sayac2 = 0;
            }
        }
    }
}
