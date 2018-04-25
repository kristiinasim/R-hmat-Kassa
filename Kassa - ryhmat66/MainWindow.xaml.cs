using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
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

namespace Kassa___ryhmat66
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string failOstukorv;
        public string failLaoseis;
        public int Algmaksumus = 0;
        List<LisaSeeToode> items = new List<LisaSeeToode>();

        private string appTitle = "Kassa";
        private string AppTooteNimi = "Toote nimi";
        private string AppTooteHind = "Hind";
        private string AppTooteKogus = "Kogus";

        public MainWindow()
        {
            InitializeComponent();

            TooteHind.Text = AppTooteHind;
            TooteKogus.Text = AppTooteKogus;
            TooteNimi.Text = AppTooteNimi;
            appWindow.Title = appTitle;

            failLaoseis = @"..\..\..\laoseis.txt";
            failOstukorv = @"..\..\..\ostukorv.txt";

            try
            {
                items = DeSerializeObject<List<LisaSeeToode>>(failLaoseis);
                if (items == null) { items = new List<LisaSeeToode>(); }
                TootedListBox.ItemsSource = items;
                TootedListBox.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message, appTitle);
                // logi veateade, kui on vaja
            }


            File.Delete(failOstukorv);
            string tekst = "Ostukorvis on: ";
            File.AppendAllText(failOstukorv, tekst);
            File.Create(failOstukorv).Close();

        }



        /*
         * Lisa toote nupu vajutus.
         * 
         * Lisab tekstiväljadele sisestatud toote info tooodete nimekirja.
         * 
         */
        private void LisaToode_Click(object sender, RoutedEventArgs e)
        {
            bool HindIsNumeric = int.TryParse(TooteHind.Text, out int n);
            bool KogusIsNumeric = int.TryParse(TooteKogus.Text, out n);
            if (TooteNimi.Text != null && TooteHind.Text != null && HindIsNumeric == true
                && TooteKogus.Text != null && KogusIsNumeric == true)
            {
                items.Add(new LisaSeeToode() { Nimi = TooteNimi.Text, Hind = int.Parse(TooteHind.Text), Kogus = int.Parse(TooteKogus.Text) });
                TootedListBox.ItemsSource = items;
                string message = string.Format("Lisasime:\n\tToode : " + TooteNimi.Text + "\n\tHind : " + TooteHind.Text + " €\n\tKogus : " + TooteKogus.Text);
                MessageBox.Show(message, appTitle);
                TootedListBox.Items.Refresh();
            }
            else
            {
                string errormessage = "Palun täida kõik lahtrid õigesti";
                MessageBox.Show(errormessage, appTitle);
            }


        }


        /**
         * Ostukorvi nupule vajutus.
         * 
         * Ostukorvi nupule vajutades teeme ...
         * 
         */
        private void Ostukorvi_Click(object sender, RoutedEventArgs e)
        {
            if (TootedListBox.SelectedItem != null) // Kui oli midagi valitud, siis lisame ostukorvi
            {
                string ToodeNimi = (TootedListBox.SelectedItem as LisaSeeToode).Nimi;
                int ToodeHind = (TootedListBox.SelectedItem as LisaSeeToode).Hind;
                string text = System.Environment.NewLine + "Toote nimi: " + ToodeNimi + " Toote hind: " + ToodeHind + "€ Toote kogus: 1";
                File.AppendAllText(failOstukorv, text);
                int ToodeteKoguHind = int.Parse(TooteHind.Text);
                int Summa = Algmaksumus += ToodeteKoguHind;
                string KoguSumma = System.Environment.NewLine + "Kogu summa on: " + Summa + "€";
                File.AppendAllText(failOstukorv, KoguSumma);
                MessageBox.Show("Toode on lisatud ostukorvi!", appTitle);
            }
            else
            {
                MessageBox.Show(
                    "Vali toode enne kui lisad ostukorvi",
                    "Ostukorvi viga!",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                    );
            }
        }

        private void Vaata_Click(object sender, RoutedEventArgs e)
        {
            Process p = new Process();
            Process.Start(failOstukorv);
            p.Close();
        }

        private void Eemalda_Click(object sender, RoutedEventArgs e)
        {
            if (TootedListBox.SelectedItem != null)
            {
                string NimiTootel = (TootedListBox.SelectedItem as LisaSeeToode).Nimi;
                int Price = (TootedListBox.SelectedItem as LisaSeeToode).Hind;
                string text = System.Environment.NewLine + "Eemaldasite: " + NimiTootel + " Toote hind: " + Price + "€ Toote kogus: 1";
                File.AppendAllText(failOstukorv, text);
                int ToodeteKoguHind = int.Parse(TooteHind.Text);
                int Summa = Algmaksumus -= ToodeteKoguHind;
                string KoguSumma = System.Environment.NewLine + "Kogu summa on: " + Summa + "€";
                File.AppendAllText(failOstukorv, KoguSumma);
                MessageBox.Show("Toode on eemaldatud ostukorvist!", appTitle);
            }
            else
            {
                MessageBox.Show(
                    "Valige toode, mida soovite eemaldada!",
                    appTitle,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                    );
            }
        }

        private void TooteNimi_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TooteNimi.Text == AppTooteNimi)
                TooteNimi.Text = "";
        }

        private void TooteNimi_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TooteNimi.Text == "")
                TooteNimi.Text = AppTooteNimi;
        }



        private void Hind_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TooteHind.Text == "")
                TooteHind.Text = AppTooteHind;
        }

        private void Hind_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TooteHind.Text == AppTooteHind)
                TooteHind.Text = "";
        }
        private void Kogus_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TooteKogus.Text == AppTooteKogus)
                TooteKogus.Text = "";
        }

        private void Kogus_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TooteKogus.Text == "")
                TooteKogus.Text = AppTooteKogus;
        }


        /// <summary>
        /// Serialiseeri objekt faili
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializableObject"></param>
        /// <param name="fileName"></param>
        public void SerializeObject<T>(T serializableObject, string fileName)
        {
            if (serializableObject == null) { return; }

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, serializableObject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(fileName);
                    stream.Close();
                }
            }
            catch (Exception)
            {
                // Logi exception siin, kui vaja
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public T DeSerializeObject<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) { return default(T); }

            T objectOut = default(T);

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(T);
                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.Deserialize(reader);
                        reader.Close();
                    }
                }
            }
            catch (Exception)
            {
                // Logi Exception, kui on vajadus
            }

            return objectOut;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                SerializeObject<List<LisaSeeToode>>(items, failLaoseis);
            }
            catch (Exception)
            {

            }
            MessageBox.Show("Täname et kasutasite meie programmi!\nPeatse taasnägemiseni!", appTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
