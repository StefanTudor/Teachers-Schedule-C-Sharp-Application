using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Orari_Profesori
{
    public partial class Adauga : Form
    {
        Activitate a = new Activitate();
        int id_profesor;
        public Adauga()
        {
            InitializeComponent();

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
                command.CommandText = "SELECT Id_profesor FROM Profesori WHERE Id_user = @param";

                command.Parameters.Clear();
                DbParameter p = command.CreateParameter();
                p.DbType = System.Data.DbType.Int32;
                p.Value = Program.idUtilizator;
                command.Parameters.Add(p);

                id_profesor = (int)command.ExecuteScalar();

            }
        }
        public Activitate GetActivitate()
        {
            return a;
        }

        public int getIdProfesor()
        {
            return id_profesor;
        }

        bool ok = false;
        public bool getStatus()
        {
            return ok;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
            bool error = false;
            if (comboTip.Text == "")
            {
                errorProvider1.SetError(comboTip, "Alege tipul de activitate!");
                error = true; }

            if (comboZiua.Text == "")
            {
                errorProvider1.SetError(comboZiua, "Alege ziua!");
                error = true;
            }

            if (textBoxMaterie.Text == "")
            {
                errorProvider1.SetError(textBoxMaterie, "Completeaza campul materie!");
                error = true;
            }

            string[] text = textBoxOra.Text.Split(':');

            if(text.Length != 2)
            {
                errorProvider1.SetError(textBoxOra, "Ora trebuie sa fie in formatul 'HH:MM'!");
                error = true;
            }

            if (textBoxOra.Text == "")
            {
                errorProvider1.SetError(textBoxOra, "Completeaza campul ora!");
                error = true;
            }

            if (textBoxSala.Text == "")
            {
                errorProvider1.SetError(textBoxSala, "Completeaza campul!");
                error = true;
            }

            if (textBoxGrupa.Text == "")
            {
                errorProvider1.SetError(textBoxGrupa, "Completeaza campul!");
                error = true;
            }

            if(!error)
            {
                errorProvider1.Dispose();
                if (MessageBox.Show("Esti sigur ca ai introdus corect datele?", "Confirmare", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    a.setTip(comboTip.Text);
                    a.setMaterie(textBoxMaterie.Text);
                    a.setOra(textBoxOra.Text);
                    a.setGrupa(textBoxGrupa.Text);
                    a.setZiua(comboZiua.Text);
                    a.setSala(textBoxSala.Text);
                    ok = true;
                    this.Close();
                }
            }
        }

        private void textBoxOra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar==(char)':')
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBoxGrupa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}
