using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace HEMS.NetzwerkBibliothek
{
    [Serializable()]
    public class BinärSerialisierer : Serializable
    {
        public KomponentenListe deserialisieren(string pfad)
        {
            FileStream stream = new FileStream(pfad, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            KomponentenListe erg = (KomponentenListe)formatter.Deserialize(stream);
            stream.Close();
            return erg;
        }

        public void serialisieren(string pfad, KomponentenListe kl)
        {
            FileStream stream = new FileStream(pfad, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, kl);
            stream.Close();
        }
    }
}
