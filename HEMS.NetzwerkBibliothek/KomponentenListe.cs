using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HEMS.NetzwerkBibliothek
{
    [Serializable()]
    public class KomponentenListe : List<Komponente>
    {
        private Serializable lnk = null;

        public void connect(Serializable s)
        {
            this.lnk = s;
        }

        public void serialisieren(String pfad)
        {
            lnk.serialisieren(pfad, this);
        }

        public void deserialisieren(String pfad)
        {
            KomponentenListe kl = lnk.deserialisieren(pfad);
            this.Clear();
            this.AddRange(kl);

        }


        public KomponentenListe SearchGebaeude(string gebaeude)
        {
            KomponentenListe neueListe = new KomponentenListe();

            foreach (var item in this)
            {
                if (item.Gebäudename.Equals(gebaeude))
                {
                    neueListe.Add(item);
                }
            }
            return neueListe;
        }

        public KomponentenListe SearchRaum(int raum)
        {
            KomponentenListe neueListe = new KomponentenListe();
            foreach (var item in this)
            {
                if (item.Raumnummer.Equals(raum))
                {
                    neueListe.Add(item);
                }
            }
            return neueListe;
        }
    }
}
