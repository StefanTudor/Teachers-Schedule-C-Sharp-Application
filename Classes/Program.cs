using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Orari_Profesori
{
    static class Program
    {
        public static string numeUtilizator;
        public static int idUtilizator;
        public static int idProfesor;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Login logare = new Login();
            logare.ShowDialog();
            if(logare.getLoginStatus() == true)
            {
                numeUtilizator = logare.getNumeUtilizator();
                idUtilizator = logare.getIdUtilizator();
                idProfesor = logare.getIdProfesor();
                MeniuPrincipal mp = new MeniuPrincipal();
                
                mp.setLabel1("Bine ai venit, " + numeUtilizator + "!");
                mp.ShowDialog();
            }
        }
    }
}
