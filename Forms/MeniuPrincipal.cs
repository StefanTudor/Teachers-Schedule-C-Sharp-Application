using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;
using System.IO;

namespace Orari_Profesori
{
    public partial class MeniuPrincipal : Form
    {
        
        List<Activitate> activitati = new List<Activitate>();
        TreeNode node;
        TreeView tree;
        int id_profesor_curent;
        const string ConnString =
               @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\tudor\Desktop\Orari_Profesori\Orari_Profesori\Orari_Profesori.mdb";
        const string ProviderName = "System.Data.OleDb";

        DbProviderFactory factory =
        DbProviderFactories.GetFactory(ProviderName);

        public MeniuPrincipal()
        {
            InitializeComponent();
            comboBox1.Visible = false;

            //creaza lista cu activitati din DB
            setActivitati(Program.idProfesor);

            //adauga nodurile principale
            addNoduriZile();
           

            //citeste din lista si adauga in noduri
            foreach (Activitate a in activitati)
            {
                insereazaInTree(a);
            }
           
        }

        void addNoduriZile()
        {
            treeViewActivitati.Nodes.Add("Luni");
            treeViewActivitati.Nodes.Add("Marti");
            treeViewActivitati.Nodes.Add("Miercuri");
            treeViewActivitati.Nodes.Add("Joi");
            treeViewActivitati.Nodes.Add("Vineri");
        }

        void insereazaInTree(Activitate a)
        {
            if (a.getZiua() == "Luni")
            {
                treeViewActivitati.Nodes[0].Nodes.Add(a.ToString());
            }
            else if (a.getZiua() == "Marti")
            {
                treeViewActivitati.Nodes[1].Nodes.Add(a.ToString());
            }
            else if (a.getZiua() == "Miercuri")
            {
                treeViewActivitati.Nodes[2].Nodes.Add(a.ToString());
            }
            else if (a.getZiua() == "Joi")
            {
                treeViewActivitati.Nodes[3].Nodes.Add(a.ToString());
            }
            else if (a.getZiua() == "Vineri")
            {
                treeViewActivitati.Nodes[4].Nodes.Add(a.ToString());
            }
        }

        public void setLabel1(string text)
        {
            label1.Text = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Adauga add = new Adauga();
            add.ShowDialog();

            Activitate a = add.GetActivitate();
            activitati.Add(a);

            if(add.getStatus())
            {
                using (DbConnection connection = factory.CreateConnection())
                {
                    connection.ConnectionString = ConnString;
                    connection.Open();

                    DbCommand command = connection.CreateCommand();
                    command.CommandText = "SELECT MAX(Nr_activitate) FROM Activitati";
                    int nrAct = (int)command.ExecuteScalar();

                    command.CommandText = "INSERT INTO Activitati VALUES(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)";

                    command.Parameters.Clear();
                    DbParameter p1 = command.CreateParameter();
                    p1.DbType = DbType.Int32;
                    p1.Value = nrAct + 1;
                    command.Parameters.Add(p1);

                    DbParameter p2 = command.CreateParameter();
                    p2.DbType = DbType.String;
                    p2.Value = a.getTip();
                    command.Parameters.Add(p2);

                    DbParameter p3 = command.CreateParameter();
                    p3.DbType = DbType.String;
                    p3.Value = a.getMaterie();
                    command.Parameters.Add(p3);

                    DbParameter p4 = command.CreateParameter();
                    p4.DbType = DbType.String;
                    p4.Value = a.getOra();
                    command.Parameters.Add(p4);

                    DbParameter p5 = command.CreateParameter();
                    p5.DbType = DbType.String;
                    p5.Value = a.getGrupa();
                    command.Parameters.Add(p5);

                    DbParameter p6 = command.CreateParameter();
                    p6.DbType = DbType.String;
                    p6.Value = a.getZiua();
                    command.Parameters.Add(p6);

                    DbParameter p7 = command.CreateParameter();
                    p7.DbType = DbType.String;
                    p7.Value = a.getSala();
                    command.Parameters.Add(p7);

                    DbParameter p8 = command.CreateParameter();
                    p8.DbType = DbType.Int32;
                    p8.Value = add.getIdProfesor();
                    command.Parameters.Add(p8);

                    command.ExecuteNonQuery();

                    MessageBox.Show("S-a adaugat: " + a.ToString());
                }
            }
           
        }

        void setActivitati(int id_profesor)
        {
            if(activitati.Count != 0)
            {
                activitati.Clear();
            }

            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = ConnString;
                connection.Open();

                DbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Activitati WHERE id_profesor = @id_profesor";

                command.Parameters.Clear();
                DbParameter parameterId = command.CreateParameter();
                parameterId.DbType = System.Data.DbType.Int32;
                parameterId.Value = id_profesor;
                command.Parameters.Add(parameterId);

                DbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Activitate a = new Activitate(reader.GetValue(1).ToString(), reader.GetValue(2).ToString(), reader.GetValue(3).ToString(), reader.GetValue(4).ToString(), reader.GetValue(5).ToString(), reader.GetValue(6).ToString());
                    activitati.Add(a);
                }
                connection.Close();
            }
        }

        private void treeViewActivitati_MouseDown(object sender, MouseEventArgs e)
        {
            tree = (TreeView)sender;
            node = tree.GetNodeAt(e.X, e.Y);
            tree.SelectedNode = node;

            if (node != null)
            {
                treeViewActivitati.DoDragDrop(node, DragDropEffects.Move);
            }
        }
        
        private void panel2_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
            
        }

        private void panel2_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode item = (TreeNode)e.Data.GetData(typeof(TreeNode));
            if (MessageBox.Show("Ești sigur că vrei să ștergi activtatea?", "Confirmare", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                item.Remove();

                string[] s = item.ToString().Replace(" ",string.Empty).Split(':','|','(',')');
                MessageBox.Show("Șters!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                const string ConnString =
               @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\tudor\Desktop\Orari_Profesori\Orari_Profesori\Orari_Profesori.mdb";
                const string ProviderName = "System.Data.OleDb";

                DbProviderFactory factory =
                DbProviderFactories.GetFactory(ProviderName);

                using (DbConnection connection = factory.CreateConnection())
                {
                    connection.ConnectionString = ConnString;
                    connection.Open();
                    
                    DbCommand command = connection.CreateCommand();
                    command.Parameters.Clear();

                    command.CommandText = "DELETE FROM Activitati where Tip_activitate = @p1 AND Materie = @p2 AND Ora = @p3 AND Grupa = @p5 AND Sala = @p6";

                    DbParameter p1 = command.CreateParameter();
                    p1.DbType = System.Data.DbType.String;
                    p1.Value = s[1];
                    command.Parameters.Add(p1);
                    DbParameter p2 = command.CreateParameter();
                    p2.DbType = System.Data.DbType.String;
                    p2.Value = s[2];
                    command.Parameters.Add(p2);
                    DbParameter p3 = command.CreateParameter();
                    p3.DbType = System.Data.DbType.String;
                    p3.Value = s[5] + ":" + s[6];
                    command.Parameters.Add(p3);
                    DbParameter p5 = command.CreateParameter();
                    p5.DbType = System.Data.DbType.String;
                    p5.Value = s[8];
                    command.Parameters.Add(p5);
                    DbParameter p6 = command.CreateParameter();
                    p6.DbType = System.Data.DbType.String;
                    p6.Value = s[10];
                    command.Parameters.Add(p6);

                    command.ExecuteNonQuery();
                    connection.Close();
                }
                foreach (Activitate act in activitati)
                {
                    if (act.ToString() == item.Text)
                    {
                        activitati.Remove(act);
                    }
                }
            }
        }

        private void graficActivitatiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float z1 = treeViewActivitati.Nodes[0].Nodes.Count;
            float z2 = treeViewActivitati.Nodes[1].Nodes.Count;
            float z3 = treeViewActivitati.Nodes[2].Nodes.Count;
            float z4 = treeViewActivitati.Nodes[3].Nodes.Count;
            float z5 = treeViewActivitati.Nodes[4].Nodes.Count;

            Grafic grafic = new Grafic(z1,z2,z3,z4,z5);
            grafic.ShowDialog();
        }

        private string getTreeText()
        {
            string[] user = label1.Text.Split(',','!');
            string text = "Orar prof. " + user[1] +"\n\n\n\n\n\n";
            for(int i=0; i<5; i++)
            {
                if (treeViewActivitati.Nodes[i].Nodes.Count == 0)
                {
                    text += "\n" + treeViewActivitati.Nodes[i].Text + "\n";
                    text += "Nu aveti activitati in aceasta zi!" +"\n";
                }
                else
                {
                    text += "\n" + treeViewActivitati.Nodes[i].Text + "\n";
                    for (int j = 0; j < treeViewActivitati.Nodes[i].Nodes.Count; j++)
                    {
                        text += treeViewActivitati.Nodes[i].Nodes[j].Text + "\n";
                    }
                }
            }

            return text;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(getTreeText(), new Font("Times new roman", 16, FontStyle.Bold), Brushes.Black, new Point(100, 100));
        }

        private void inchideAplicatiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void salveazaPeDesktopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            File.WriteAllText(@"C:\Users\tudor\Desktop\orar.txt", getTreeText());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            treeViewActivitati.Nodes.Clear();
            addNoduriZile();
            setActivitati(newIdProf);
            foreach(Activitate a in activitati)
            {
                insereazaInTree(a);
            }
        }

        int newIdProf = Program.idProfesor;

        private void orarulAltorProfesoriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboBox1.Visible = true;
            comboBox1.Items.Clear();
            List<Profesor> profesori = new List<Profesor>();

            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = ConnString;
                connection.Open();

                DbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Profesori";

                DbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Profesor p = new Profesor(reader.GetValue(1).ToString(), reader.GetValue(2).ToString(), (int)reader.GetValue(3), (int)reader.GetValue(0));
                    profesori.Add(p);
                }
                connection.Close();

                foreach (Profesor prof in profesori)
                {
                    comboBox1.Items.Add("("+ prof.getIdProfesor() + ") "+ prof.nume + " " + prof.prenume);
                }
            }
        }

        private void orarulMeuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboBox1.Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] text = comboBox1.SelectedItem.ToString().Split('(',')');
            string id = text[1];
            newIdProf = Int32.Parse(id);

        }

        private void printeazaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }
    }
}
