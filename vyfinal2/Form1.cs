using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vyfinal2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public class BSTdugum
        {
            public int veri;
            public BSTdugum sag;
            public BSTdugum sol;
        }

        public BSTdugum kok;
        private void Ekle(int sayi)
        {
           
            BSTdugum yeni = new BSTdugum();
            yeni.veri = sayi;
            if (kok == null)
            {
                kok = yeni;
                label1.Text = Convert.ToString(sayi + "-düğüm eklendi");
            }
            else
            {
                BSTdugum gecici = kok;
                BSTdugum üst;
                while (true)
                {
                    üst = gecici;
                    if (sayi < gecici.veri)
                    {
                        gecici = gecici.sol;
                        if (gecici == null)
                        {
                            üst.sol = yeni;
                            label1.Text = Convert.ToString(sayi + "-düğüm eklendi");
                            return;
                        }

                    }
                    else
                    {
                        gecici = gecici.sag;
                        if (gecici == null)
                        {
                            üst.sag = yeni;
                            label1.Text = Convert.ToString(sayi + "-düğüm eklendi");
                            return;
                        }

                    }
                }
            }
            
            
        }
       
        public static BSTdugum Max(BSTdugum kok)
        {
            if (kok == null)
            {
                return null;
            }
            BSTdugum p = kok;
            while (p.sag != null)
            {
                p = p.sag;
            }
            return p;
        }
        public int dugumSayi(BSTdugum dugum)
        {
            int boyut = 0;
            if (dugum != null)
            {
                boyut = 1;
                boyut += dugumSayi(dugum.sag);
                boyut += dugumSayi(dugum.sol);
            }
            return boyut;
        }
        private void preOrder(BSTdugum isaretci)
        {
            if (isaretci == null)
            {
                return;
            }
            textBox5.Text += isaretci.veri + " ";
            preOrder(isaretci.sol);
            preOrder(isaretci.sag);
        }
        private void postOrder(BSTdugum isaretci)
        {
            if (isaretci == null)
            {
                return;
            }
            postOrder(isaretci.sol);
            postOrder(isaretci.sag);
            textBox7.Text += isaretci.veri + " ";

        }
        private void ınOrder(BSTdugum isaretci)
        {
            if (isaretci == null)
            {
                return;
            }
            ınOrder(isaretci.sol);
            textBox6.Text += isaretci.veri + " ";
            ınOrder(isaretci.sag);

        }
       
        private void button1_Click(object sender, EventArgs e)
        {

            int sayi = Convert.ToInt32(textBox1.Text);
            Ekle(sayi);

        }
       
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox5.Text = " ";
            textBox6.Text = " ";
            textBox7.Text = " ";
            textBox8.Text = " ";
            textBox9.Text = " ";
            textBox10.Text = " ";
            ınOrder(kok);
            postOrder(kok);
            preOrder(kok);
            textBox8.Text = Convert.ToString(dugumSayi(kok));
            textBox10.Text = (yukseklikBul(kok) - 1).ToString();
        }

        private int yukseklikBul(BSTdugum kok)
        {
            if (kok == null)
            {
                return 0;
            }
            return Math.Max(yukseklikBul(kok.sol), yukseklikBul(kok.sag)) + 1;
        }

        public void goster(BSTdugum dugum)
        {

            if (kok != null)
            {
                textBox4.Text = "--kök" + Convert.ToString(dugum.veri) + Environment.NewLine;
            }

            if (dugum.sol != null)
            {
                textBox4.Text += "sol--" + Convert.ToString(dugum.sol.veri) + Environment.NewLine;
                gosterAlt(dugum.sol, "sol--");
            }

            if (dugum.sag != null)
            {
                textBox4.Text += "sag--" + Convert.ToString(dugum.sag.veri) + Environment.NewLine;
                gosterAlt(dugum.sag, "sag--");
            }
        }

        public void gosterAlt(BSTdugum dugum, string isaretci)
        {
            if (dugum.sol != null)
            {
                textBox4.Text += isaretci + "sol--" + Convert.ToString(dugum.sol.veri) + Environment.NewLine;
                isaretci += "sol--";
                gosterAlt(dugum.sol, isaretci);
            }
            if (dugum.sag != null)
            {
                textBox4.Text += isaretci + "sag--" + Convert.ToString(dugum.sag.veri) + Environment.NewLine;
                isaretci += "sag--";
                gosterAlt(dugum.sag, isaretci);
            }
        }
       private void button4_Click(object sender, EventArgs e)
        {
            textBox4.Text = " ";
            goster(kok);
            
        }
         public BSTdugum sil(int sayi, BSTdugum kok)
        {
           
            if (kok == null)
            {
            return kok;
            }

            if (sayi < kok.veri)
            {
              kok.sol = sil(sayi, kok.sol);
            }

            else if (sayi > kok.veri)
            {
             kok.sag = sil(sayi, kok.sag);
            }

            else
            if (kok.sol != null && kok.sag != null)
            {
                kok.veri = Max(kok.sol).veri;
                kok.sol = sil(kok.veri, kok.sol);
            }
            else if (kok.sol != null)
            {
                
                kok = kok.sol;
            }
            else
            {
                
                kok = kok.sag;
               
            }
            return kok;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int silinecek = Convert.ToInt32(textBox2.Text);
            
            sil(silinecek, kok);
            label2.Text = Convert.ToString(silinecek + "-düğüm silindi");
         }
        public BSTdugum aramaYap(int sayi)
        {
         
            return Bul(kok, sayi);
        }
        public BSTdugum Bul(BSTdugum kok, int sayi)
        {
            
            if (kok == null)
            {
                MessageBox.Show("bulunamadı");
                return null;
            }
            if (sayi == kok.veri)
            {
                MessageBox.Show("bulundu");
                return kok;
            }
            else if (sayi < kok.veri)
            {
                return Bul(kok.sol, sayi);
            }
            else
            {
                return Bul(kok.sag, sayi);
            }
        }
      
        private void button3_Click(object sender, EventArgs e)
        {
            int sayi = Convert.ToInt32(textBox3.Text);
              aramaYap(sayi);
       }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }
    }
}






