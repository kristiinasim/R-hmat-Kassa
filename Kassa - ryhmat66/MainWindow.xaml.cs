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
using System.IO;

namespace Kassa___ryhmat66
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string path = @"C:\Users\Krizzie\Documents\GitHub\Ryhmat66-Kassa\Tooted.txt";


        List<LisaSeeToode> items = new List<LisaSeeToode>();
        public MainWindow()
        {
            InitializeComponent();
            //List<LisaSeeToode> items = new List<LisaSeeToode>();
            

            items.Add(new LisaSeeToode() { Nimi = "Piim", Hind = 45, Kogus = 10 });

            items.Add(new LisaSeeToode() { Nimi = "Perenaise Sai", Hind = 80, Kogus = 12 });

            items.Add(new LisaSeeToode() { Nimi = "Kange Walter", Hind = 100, Kogus = 5 });

            items.Add(new LisaSeeToode() { Nimi = "Lotte Limonaad", Hind = 10, Kogus = 8 });

            items.Add(new LisaSeeToode() { Nimi = "Maasikad", Hind = 40, Kogus = 30 });

            TootedListBox.ItemsSource = items;

            //TootedListBox.ItemsSource = items;

            //items.Add(new LisaSeeToode() { Nimi = "aferg", Hind = 7, Kogus = 1 });
            //TootedListBox.ItemsSource = items;
        }

        private void LisaToode_Click(object sender, RoutedEventArgs e)
        {

            items.Add(new LisaSeeToode() { Nimi = TooteNimi.Text, Hind = int.Parse(TooteHind.Text), Kogus = int.Parse(TooteKogus.Text) });
            TootedListBox.ItemsSource = items;
            string message = string.Format("Listis on nüüd: " + TooteNimi.Text + "; Hind: " + TooteHind.Text + " € Kogus: " + TooteKogus.Text);

            MessageBox.Show(message);

            //List<LisaSeeToode> items = new List<LisaSeeToode>();
            //items.Add(new LisaSeeToode() { Nimi = TooteNimi.Text, Hind = int.Parse(TooteHind.Text), Kogus = int.Parse(TooteKogus.Text) });
            //TootedListBox.ItemsSource = items;

        }


        private void TootedListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TootedListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
         
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

                if (TootedListBox.SelectedItems != null)
                {
                    foreach (var item in TootedListBox.SelectedItems)
                    {
                    var text = TootedListBox.SelectedValue.ToString();
                    File.AppendAllText(path, text);
                    
                }
              
            }
            }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (TootedListBox.SelectedItems != null)
            {
                foreach (var item in TootedListBox.SelectedItems)
                {
                    var text = TootedListBox.SelectedValue.ToString();
                    File.AppendAllText(path, text);
                    //File.AppendAllText(path, TooteNimi.Text);

                }

            }
        }
    }
}
