using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Orari_Profesori
{
    public partial class Grafic : Form
    {
        public Grafic(float z1, float z2, float z3, float z4, float z5)
        {
            InitializeComponent();
            pieChart1.setValori(new float[] { z1, z2, z3, z4, z5 });
            pieChart1.setBrushes(new Brush[] { new SolidBrush(Color.MediumPurple), new SolidBrush(Color.CornflowerBlue), new SolidBrush(Color.DodgerBlue), new SolidBrush(Color.DeepSkyBlue), new SolidBrush(Color.Turquoise) });
            pieChart1.setDiametru(pieChart1.getImageBoxSize());
        }
    }
}
