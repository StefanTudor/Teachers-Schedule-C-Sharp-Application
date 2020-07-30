using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orari_Profesori
{
    
    public class Activitate
    {
        string tip;
        string ora;
        string materie;
        string grupa;
        string ziua;
        string sala;

        public Activitate()
        {
            tip = null;
            ora = null;
            materie = null;
            grupa = null;
            ziua = null;
            sala = null;
        }

        public Activitate(string t, string o, string m, string g, string z, string s)
        {
            tip = t;
            ora = o;
            materie = m;
            grupa = g;
            ziua = z;
            sala = s;
        }

        public void setTip(string tip)
        {
            this.tip = tip;
        }

        public string getTip()
        {
            return tip;
        }

        public void setOra(string ora)
        {
            this.ora = ora;
        }

        public string getOra()
        {
            return ora;
        }

        public void setMaterie(string materie)
        {
            this.materie = materie;
        }

        public string getMaterie()
        {
            return materie;
        }

        public void setGrupa(string grupa)
        {
            this.grupa = grupa;
        }

        public string getGrupa()
        {
            return grupa;
        }

        public void setZiua(string ziua)
        {
            this.ziua = ziua;
        }

        public string getZiua()
        {
            return ziua;
        }

        public void setSala(string sala)
        {
            this.sala = sala;
        }

        public string getSala()
        {
            return sala;
        }

        public override string ToString()
        {
            return tip + "(" + materie + ") | Ora: "+ ora + " | Grupa:" + grupa + " | Sala: " + sala;

        }
    }
}
