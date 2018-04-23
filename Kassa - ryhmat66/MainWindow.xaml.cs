using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Text;
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
        public string path;
        public int Algmaksumus = 0;
        List<LisaSeeToode> items = new List<LisaSeeToode>();
        public MainWindow()
        {
            InitializeComponent();
            path = @"..\..\..\kassa.txt";
            File.Delete(path);
            string tekst = "Ostukorvis on: ";
            File.AppendAllText(path, tekst);
            path = @"..\..\..\list.txt";
            File.Create(path).Close();

        }

        private void LisaToode_Click(object sender, RoutedEventArgs e)
        {
            int n=0;
            bool HindIsNumeric = int.TryParse(TooteHind.Text, out n);
            bool KogusIsNumeric = int.TryParse(TooteKogus.Text, out n);
            if (TooteNimi.Text != null && TooteHind.Text != null && HindIsNumeric == true 
                && TooteKogus.Text != null && KogusIsNumeric == true)
            {
                items.Add(new LisaSeeToode() { Nimi = TooteNimi.Text, Hind = int.Parse(TooteHind.Text), Kogus = int.Parse(TooteKogus.Text) });
                TootedListBox.ItemsSource = items;
                string message = string.Format("Listis on nüüd: " + TooteNimi.Text + "; Hind: " + TooteHind.Text + " € Kogus: " + TooteKogus.Text);
                MessageBox.Show(message);
                TootedListBox.Items.Refresh();
            }
            else
            {
                string errormessage = "Palun täida kõik lahtrid õigesti";
                MessageBox.Show(errormessage);
            }
                

        }

       

        private void TootedListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Ostukorvi_Click(object sender, RoutedEventArgs e)
        {
            if (TootedListBox.SelectedItem != null)
            {
                string text = System.Environment.NewLine + "Toote nimi: " + TooteNimi.Text + " Toote hind: " + TooteHind.Text + "€ Toote kogus: 1";
                File.AppendAllText(path, text);
                int ToodeteKoguHind = int.Parse(TooteHind.Text);
                int Summa = Algmaksumus += ToodeteKoguHind;
                string KoguSumma = System.Environment.NewLine + "Kogu summa on: " + Summa + "€";
                File.AppendAllText(path, KoguSumma);
                MessageBox.Show("Toode on lisatud ostukorvi!");
            } else
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
            Process.Start(path);
            p.Close();
        }

        private void Eemalda_Click(object sender, RoutedEventArgs e)
        {
            if (TootedListBox.SelectedItem != null)
            {
                string text = System.Environment.NewLine + "Eemaldasite: " + TooteNimi.Text + " Toote hind: " + TooteHind.Text + "€ Toote kogus: 1";
                File.AppendAllText(path, text);
                int ToodeteKoguHind = int.Parse(TooteHind.Text);
                int Summa = Algmaksumus -= ToodeteKoguHind;
                string KoguSumma = System.Environment.NewLine + "Kogu summa on: " + Summa + "€";
                File.AppendAllText(path, KoguSumma);
                MessageBox.Show("Toode on eemaldatud ostukorvist!");
            } else
            {
                MessageBox.Show(
                    "Valige toode, mida soovite eemaldada!",
                    "!!!",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                    );
            }
        }

        private void TooteNimi_GotFocus(object sender, RoutedEventArgs e)
        {
            if(TooteNimi.Text == "Toote nimi")
                TooteNimi.Text = "";
        }

        private void TooteNimi_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TooteNimi.Text == "")
                TooteNimi.Text = "Toote nimi";
        }

        

        private void Hind_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TooteHind.Text == "")
                TooteHind.Text = "Hind";
        }

        private void Hind_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TooteHind.Text == "Hind")
                TooteHind.Text = "";
        }
        private void Kogus_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TooteKogus.Text == "Kogus")
                TooteKogus.Text = "";
        }

        private void Kogus_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TooteKogus.Text == "")
                TooteKogus.Text = "Kogus";
        }

    }
}
