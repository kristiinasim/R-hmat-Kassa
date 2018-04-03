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
        }

        private void LisaToode_Click(object sender, RoutedEventArgs e)
        {
            //if (TooteNimi.Text != null && TooteHind.Text != null && TooteHind != )
            items.Add(new LisaSeeToode() { Nimi = TooteNimi.Text, Hind = int.Parse(TooteHind.Text), Kogus = int.Parse(TooteKogus.Text) });
            TootedListBox.ItemsSource = items;
            string message = string.Format("Listis on nüüd: " + TooteNimi.Text + "; Hind: " + TooteHind.Text + " € Kogus: " + TooteKogus.Text);
            MessageBox.Show(message);
            TootedListBox.Items.Refresh();

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
            }
        }

        private void Vaata_Click(object sender, RoutedEventArgs e)
        {
            Process p = new Process();
            Process.Start(@"..\..\..\kassa.txt");
            p.Close();
        }
    }
}
