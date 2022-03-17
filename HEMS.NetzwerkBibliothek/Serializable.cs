using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HEMS.NetzwerkBibliothek
{
    public interface Serializable
    {
        void serialisieren(String pfad, KomponentenListe kl);

        KomponentenListe deserialisieren(String pfad);


    }
}
