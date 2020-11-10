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
using System.Threading;

namespace Orari_Profesori
{
    public partial class Login : Form
    {
        bool succesfulLogin = false;
        string numeUtilizator;
        int idUtilizator;
        int idProfesor;

        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            const string ConnString =
                @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=../../Orari_Profesori.mdb";
            const string ProviderName = "System.Data.OleDb";

            DbProviderFactory factory =
            DbProviderFactories.GetFactory(ProviderName);

            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = ConnString;
                connection.Open();

                DbCommand cmdCheck = connection.CreateCommand();
                cmdCheck.CommandText = "SELECT COUNT(Id_user) from Useri WHERE Username = @user AND Password = @pass";

                DbParameter paramUser = cmdCheck.CreateParameter();
                paramUser.DbType = System.Data.DbType.String;
                paramUser.Value = textBox1.Text;
                cmdCheck.Parameters.Add(paramUser);

                DbParameter paramPass = cmdCheck.CreateParameter();
                paramPass.DbType = System.Data.DbType.String;
                paramPass.Value = textBox2.Text;
                cmdCheck.Parameters.Add(paramPass);

                int ok = (int)cmdCheck.ExecuteScalar();
                cmdCheck.Parameters.Clear();

                if ( ok == 0)
                {
                    MessageBox.Show("Username sau password gresite!");
                }
                else if (ok == 1)
                {
                    
                    succesfulLogin = true;

                    cmdCheck.CommandText = "SELECT Nume from Useri WHERE Username = @user";
                    paramUser.DbType = System.Data.DbType.String;
                    paramUser.Value = textBox1.Text;
                    cmdCheck.Parameters.Add(paramUser);
                    numeUtilizator = (string)cmdCheck.ExecuteScalar();

                    cmdCheck.CommandText = "SELECT Id_user from Useri WHERE Username = @user";
                    idUtilizator = (int)cmdCheck.ExecuteScalar();
                    cmdCheck.Parameters.Clear();

                    cmdCheck.CommandText = "SELECT Id_profesor from Profesori WHERE Id_user = @id_user";
                    paramUser.DbType = System.Data.DbType.Int32;
                    paramUser.Value = idUtilizator;
                    cmdCheck.Parameters.Add(paramUser);
                    idProfesor = (int)cmdCheck.ExecuteScalar();
                    cmdCheck.Parameters.Clear();

                    connection.Close();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Eroare la conectare! Contactati administratorul aplicatiei!");
                }
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void openNewForm(object obj)
        {
            Application.Run(new MeniuPrincipal());
        }

        public bool getLoginStatus()
        {
            return succesfulLogin;
        }

        public string getNumeUtilizator()
        {
            return numeUtilizator;
        }

        public int getIdUtilizator()
        {
            return idUtilizator;
        }

        public int getIdProfesor()
        {
            return idProfesor;
        }
    }
}
