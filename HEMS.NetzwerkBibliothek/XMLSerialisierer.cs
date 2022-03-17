using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HEMS.NetzwerkBibliothek
{
    [Serializable()]
    public class XMLSerialisierer : Serializable
    {
        public KomponentenListe deserialisieren(string pfad)
        {
            FileStream stream = new FileStream(pfad, FileMode.Open);
            XmlSerializer deserialisierer = new XmlSerializer(typeof(KomponentenListe));
            KomponentenListe erg = (KomponentenListe)deserialisierer.Deserialize(stream);
            stream.Close();
            return erg;
        }

        public void serialisieren(string pfad, KomponentenListe kl)
        {
            FileStream stream = new FileStream(pfad, FileMode.Create);
            XmlSerializer serialisierer = new XmlSerializer(typeof(KomponentenListe));
            serialisierer.Serialize(stream, kl);
            stream.Close();
        }
    }
}
