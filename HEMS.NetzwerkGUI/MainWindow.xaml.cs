using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HEMS.NetzwerkBibliothek;
using Microsoft.Win32;

namespace HEMS.NetzwerkGUI
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
  
    
    public partial class MainWindow : Window
    {

        private KomponentenListe kListe = new KomponentenListe();
        private HashSet<string> prodSetGeb = new HashSet<string>();
        private HashSet<string> prodSetRaum = new HashSet<string>();
        

        public MainWindow()
        {
            InitializeComponent();
            textBoxHersteller.Focus();
            prodSetGeb.Add("Liste");
            prodSetRaum.Add("Liste");
            listViewKomponente.ItemsSource = this.kListe;
            comboboxGebaeude.ItemsSource = this.prodSetGeb;
            comboboxGebaeude.SelectedIndex = 0;
            comboboxGebaeude.Items.Refresh();
            comboboxRaumnummer.ItemsSource = this.prodSetRaum;
            comboboxRaumnummer.SelectedIndex = 0;
            comboboxRaumnummer.Items.Refresh();
        }

        private void Hinzufügen_Click(object sender, RoutedEventArgs e)
        {
            //temporäres Komponenten Objekt erstellen
            Komponente k = new Komponente();
            k.Hersteller = textBoxHersteller.Text;
            k.Beschreibung = textBoxBeschreibung.Text;
            DateTime? x = textBoxInbetriebnahme.SelectedDate;
            if (x != null) {
                k.Inbetriebnahme = (DateTime)x;
                //k.setInbetriebnahme((DateTime)x); veraltet
            }
            k.Gebäudename = textBoxGebäude.Text;
            k.Raumnummer = Convert.ToInt32(textBoxRaum.Text);

            kListe.Add(k);
            listViewKomponente.Items.Refresh();
            refreshComboBoxGebaeude();
            refreshComboBoxRaumnummer();

            textBoxHersteller.Text = string.Empty;
            textBoxBeschreibung.Text = string.Empty;
            textBoxInbetriebnahme.Text = string.Empty;
            textBoxGebäude.Text = string.Empty;
            textBoxRaum.Text = string.Empty;

            textBoxHersteller.Focus();
        }

        private void Speichern_Click(object sender, RoutedEventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                String pfadname = saveFileDialog.FileName;
                FileInfo finfo = new FileInfo(pfadname);
                String extension = finfo.Extension;

                if (extension == ".bin")
                {
                    BinärSerialisierer bs = new BinärSerialisierer();
                    kListe.connect(bs);
                    kListe.serialisieren(pfadname);
                }
                else if (extension == ".xml")
                {
                    XMLSerialisierer xs = new XMLSerialisierer();
                    kListe.connect(xs);
                    kListe.serialisieren(pfadname);
                }
                
                MessageBox.Show("Gespeichert!");  
            }
        }

        private void Aufrufen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Handy File (*.bin)|*.bin|Handy File (*.xml)|*.xml|Alle Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                String pfadname = openFileDialog.FileName;
                FileInfo finfo = new FileInfo(pfadname);
                String extension = finfo.Extension;
                if (extension == ".bin")
                {
                    BinärSerialisierer bs = new BinärSerialisierer();
                    kListe.connect(bs);
                    kListe.deserialisieren(pfadname);
                }
                else if (extension == ".xml")
                {
                    XMLSerialisierer xs = new XMLSerialisierer();
                    kListe.connect(xs);
                    kListe.deserialisieren(pfadname);
                }

                refreshComboBoxGebaeude();
                refreshComboBoxRaumnummer();
                listViewKomponente.Items.Refresh();
            }

        }
        private void comboboxGebaeude_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            string select = comboboxGebaeude.SelectedItem.ToString();
            if (select == "Liste")
            {
                listViewKomponente.ItemsSource = this.kListe;
            }
            else
            {
                listViewKomponente.ItemsSource = kListe.SearchGebaeude(select);

            }
            listViewKomponente.Items.Refresh();
        }

        private void comboboxRaumnummer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            string select = comboboxRaumnummer.SelectedItem.ToString();
            if (select == "Liste")
            {
                listViewKomponente.ItemsSource = this.kListe;
            }
            else
            {
                listViewKomponente.ItemsSource = kListe.SearchRaum(Convert.ToInt32(select));

            }
            listViewKomponente.Items.Refresh();
        }


        private void refreshComboBoxGebaeude()
        {
              
            foreach (var item in kListe)
            {
                prodSetGeb.Add(item.Gebäudename);
            }

            comboboxGebaeude.Items.Refresh();;
        }

        private void refreshComboBoxRaumnummer()
        {

            foreach (var item in kListe)
            {
                prodSetRaum.Add(item.Raumnummer.ToString());
            }

            comboboxRaumnummer.Items.Refresh(); ;
        }

        private static readonly Regex nurZahlen = new Regex("[^0-9]+");
        private void textBoxRaum_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(nurZahlen.IsMatch(e.Text))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }

        }
    }
    
}
