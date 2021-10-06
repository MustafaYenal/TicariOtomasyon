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

namespace TicariOtomasyon
{
    public partial class FrmFaturalar : Form
    {
        public FrmFaturalar()
        {
            InitializeComponent();
        }


        sqlbaglantisi bgl = new sqlbaglantisi();
        void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_FATURABILGI", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void temizle()
        {
            Txtid.Text = "";
            TxtSeri.Text = "";
            TxtSıraNo.Text = "";
            MskTarih.Text = "";
            MskSaat.Text = "";
            TxtVergiDaire.Text = "";
            TxtAlıcı.Text = "";
            TxtTeslimAlan.Text = "";
            TxtTeslimEden.Text = "";
        }
        private void FrmFaturalar_Load(object sender, EventArgs e)
        {
            Listele();
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (TxtFaturaid.Text == "")
            {
                SqlCommand komut = new SqlCommand("insert into TBL_FATURABILGI (SERI,SIRANO,TARIH,SAAT,VERGIDAIRE,ALICI,TESLIMEDEN,TESLIMALAN) VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtSeri.Text);
                komut.Parameters.AddWithValue("@p2", TxtSıraNo.Text);
                komut.Parameters.AddWithValue("@p3", MskTarih.Text);
                komut.Parameters.AddWithValue("@p4", MskSaat.Text);
                komut.Parameters.AddWithValue("@p5", TxtVergiDaire.Text);
                komut.Parameters.AddWithValue("@p6", TxtAlıcı.Text);
                komut.Parameters.AddWithValue("@p7", TxtTeslimEden.Text);
                komut.Parameters.AddWithValue("@p8", TxtTeslimAlan.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Fatura Bilgisi Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                temizle();
                Listele();
            }
            else if (TxtFaturaid.Text != "")
            {
               // SqlCommand komut2 = new SqlCommand("insert into TBL_FATURADETAY (URUNAD,MIKTAR,FIYAT,TUTAR,FATURAID) VALUES (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
               // komut2.Parameters.AddWithValue("@p1", TxtUrunAd.Text);
               // komut2.Parameters.AddWithValue("@p2", TxtMiktar.Text);
               // komut2.Parameters.AddWithValue("@p3", decimal.Parse(TxtFiyat.Text));
               //komut2.Parameters.AddWithValue("@p4", decimal.Parse(TxtTutar.Text));
               // komut2.Parameters.AddWithValue("@p5", TxtFaturaid.Text);
               // komut2.ExecuteNonQuery();
               // bgl.baglanti().Close();

                //Hareket Tablosuna veri girişi
                SqlCommand komut3 = new SqlCommand("insert into tbl_fırmahareketler (Urunıd,adet,personel,fırma,fıyat,toplam,faturaıd,tarıh) values (@r1,@p2,@r3,@r4,@pr,@r,@r7,@r8)", bgl.baglanti());
                komut3.Parameters.AddWithValue("@r1", TxtUrunid.Text);
                komut3.Parameters.AddWithValue("@r2", TxtMiktar.Text);
                komut3.Parameters.AddWithValue("@r3", TxtPersonel.Text);
                komut3.Parameters.AddWithValue("@r4", TxtFirma.Text);
                komut3.Parameters.AddWithValue("@r5", decimal.Parse(TxtFiyat.Text));
                komut3.Parameters.AddWithValue("@r6", decimal.Parse(TxtTutar.Text));
                komut3.Parameters.AddWithValue("@r7", TxtFaturaid.Text);
                komut3.Parameters.AddWithValue("@r8", MskTarih.Text);

                MessageBox.Show("Faturaya Ait Ürün Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                temizle();
                Listele();
            }

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                Txtid.Text = dr["FATURABILGIID"].ToString();
                TxtSeri.Text = dr["SERI"].ToString();
                TxtSıraNo.Text = dr["SIRANO"].ToString();
                MskTarih.Text = dr["TARIH"].ToString();
                MskSaat.Text = dr["SAAT"].ToString();
                TxtVergiDaire.Text = dr["VERGIDAIRE"].ToString();
                TxtAlıcı.Text = dr["ALICI"].ToString();
                TxtTeslimEden.Text = dr["TESLIMEDEN"].ToString();
                TxtTeslimAlan.Text = dr["TESLIMALAN"].ToString();

            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void TxtFiyat_EditValueChanged(object sender, EventArgs e)
        {
            //double miktar, fiyat, tutar;
            //miktar = Convert.ToDouble(TxtMiktar.Text);
            //fiyat = Convert.ToDouble(TxtFiyat.Text);
            //tutar = miktar * fiyat;
            //TxtTutar.Text = tutar.ToString();
            ////if (TxtTutar.Text=="")
            ////{
            ////    TxtTutar.Text = "";
            ////}
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete from TBL_FATURABILGI where FATURABILGIID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Fatura Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Question);
            temizle();
            Listele();
        }

        private void BtnTemizle_Click_1(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_FATURABILGI SET SERI=@P1,SIRANO=@P2,TARIH=@P3,SAAT=@P4,VERGIDAIRE=@P5,ALICI=@P6,TESLIMEDEN=@P7,TESLIMALAN=@P8 WHERE FATURABILGIID=@P9", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtSeri.Text);
            komut.Parameters.AddWithValue("@P2", TxtSıraNo.Text);
            komut.Parameters.AddWithValue("@P3", MskTarih.Text);
            komut.Parameters.AddWithValue("@P4", MskSaat.Text);
            komut.Parameters.AddWithValue("@P5", TxtVergiDaire.Text);
            komut.Parameters.AddWithValue("@P6", TxtAlıcı.Text);
            komut.Parameters.AddWithValue("@P7", TxtTeslimEden.Text);
            komut.Parameters.AddWithValue("@P8", TxtTeslimAlan.Text);
            komut.Parameters.AddWithValue("@P9", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Fatura Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            temizle();
            Listele();

        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaDetay fr = new FrmFaturaDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                fr.id = dr["FATURABILGIID"].ToString();
            }
            fr.Show();
        }



        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select URUNAD,SATISFIYAT FROM TBL_URUNLER WHERE ID=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtUrunid.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                TxtUrunAd.Text = dr[0].ToString();
                TxtFiyat.Text = dr[1].ToString();
            }
            bgl.baglanti().Close();

        }

        private void TxtMiktar_EditValueChanged(object sender, EventArgs e)
        {
            double miktar, fiyat, tutar;
            miktar = Convert.ToDouble(TxtMiktar.Text);
            fiyat = Convert.ToDouble(TxtFiyat.Text);
            tutar = miktar * fiyat;
            TxtTutar.Text = tutar.ToString();
        }
    }
}
