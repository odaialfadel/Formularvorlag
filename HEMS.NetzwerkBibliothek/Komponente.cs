using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;

namespace HEMS.NetzwerkBibliothek
{
    [Serializable()]

    public class Komponente
    {
        public Guid Guid { get; set; }
        public String Hersteller { get; set; }
        public String Beschreibung { get; set; } //Switch, Router, Wireless Access Point
        public DateTime Inbetriebnahme { get; set; }
        public String Gebäudename { get; set; }
        public int Raumnummer { get; set; }
        
        public Komponente()
        {
            this.Guid = Guid.NewGuid();
        }

        /*
        public void setInbetriebnahme(DateTime datum)
        {
            this.Inbetriebnahme = datum;

        }
        */
    }
}
