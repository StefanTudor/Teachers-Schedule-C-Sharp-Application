using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Orari_Profesori
{
    public partial class PieChart : UserControl
    {
        float[] valori;
        Brush[] brushes;
        public int diametru;
        

        public PieChart()
        {
            InitializeComponent();
        }

        int getTotal()
        {
            int total = 0;
            foreach (int i in valori)
            {
                total += i;
            }
            return total;
        }

        public void setValori(float[] v)
        {
            valori = new float[v.Length];
            for(int i=0; i<valori.Length; i++)
            {
                valori[i] = v[i];
            }
        }

        public void setBrushes(Brush[] b)
        {
            brushes = new Brush[valori.Length];
            for (int i = 0; i < valori.Length; i++)
            {
                brushes[i] = b[i];
            }
        }

        public void setDiametru(int d)
        {
            diametru = d;
        }

        public int getImageBoxSize()
        {
            return pictureBox1.Size.Height;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            float[] degree = new float[valori.Length];
            int total = getTotal();
            for (int i = 0; i < valori.Length; i++)
            {
                degree[i] = (valori[i] / total) * 360;
            }
            Graphics graphics = pictureBox1.CreateGraphics();

            Rectangle rect = new Rectangle(0, 0, diametru, diametru);

            float sumDegree = 0;
            graphics.FillPie(brushes[0], rect, 0, degree[0]);
            for (int i=1; i<valori.Length; i++)
            {
                sumDegree += degree[i-1];
                graphics.FillPie(brushes[i], rect, sumDegree, degree[i]);
            }
        }

        
    }
}
