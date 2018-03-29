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
        List<LisaSeeToode> items = new List<LisaSeeToode>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LisaToode_Click(object sender, RoutedEventArgs e)
        {

            items.Add(new LisaSeeToode() { Nimi = TooteNimi.Text, Hind = int.Parse(TooteHind.Text), Kogus = int.Parse(TooteKogus.Text) });
            TootedListBox.ItemsSource = items;
            string message = string.Format("Listis on nüüd: " + TooteNimi.Text + "; Hind: " + TooteHind.Text + " € Kogus: " + TooteKogus.Text);
            MessageBox.Show(message);
            TootedListBox.Items.Refresh();

        }

       

        private void TootedListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
