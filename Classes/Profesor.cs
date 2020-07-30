using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Orari_Profesori
{
    public class Profesor
    {
        public string nume;
        public string prenume;
        int id_user;
        int id_profesor;
        List<Profesor> lista = new List<Profesor>();

        public Profesor()
        {
            this.nume = "";
            this.prenume = "";
            this.id_user = 0;
            this.id_profesor = 0;
        }

        public Profesor(string nume, string prenume, int id_user, int id_profesor)
        {
            this.nume = nume;
            this.prenume = prenume;
            this.id_user = id_user;
            this.id_profesor = id_profesor;
        }

        public int getIdUser()
        {
            return id_user;
        }

        public int getIdProfesor()
        {
            return id_profesor;
        }

        public override string ToString()
        {
            return nume + " " + prenume + " " + id_user + " " + id_profesor ;
        }

    }
}
