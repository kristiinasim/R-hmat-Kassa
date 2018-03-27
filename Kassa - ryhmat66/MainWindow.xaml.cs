using System;
using System.Collections.Generic;
using System.Linq;
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
        List<LisaSeeToode> items;
        public MainWindow()
        {
            InitializeComponent();
            items = new List<LisaSeeToode>();
            

            items.Add(new LisaSeeToode() { Nimi = "aferg", Hind = 7, Kogus = 1 });
            TootedListBox.ItemsSource = items;
        }

        private void LisaToode_Click(object sender, RoutedEventArgs e)
        {
            items.Add(new LisaSeeToode() { Nimi = TooteNimi.Text, Hind = int.Parse(TooteHind.Text), Kogus = int.Parse(TooteKogus.Text) });
            TootedListBox.ItemsSource = items;
            string message = string.Format("Listis on nüüd: " + TooteNimi.Text + "; Hind: " + TooteHind.Text + " € Kogus: " + TooteKogus.Text);

            MessageBox.Show(message);
        }

        public class LisaSeeToode
        {
            public string Nimi;
            public int Hind;
            public int Kogus;
        }
    }
}
