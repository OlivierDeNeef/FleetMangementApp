using DomainLayer.Models;
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
using System.Windows.Shapes;

namespace FleetMangementApp
{
    /// <summary>
    /// Interaction logic for BestuurderAanpassen.xaml
    /// </summary>
    public partial class BestuurderAanpassen : Window
    {
        Bestuurder _bestuurder;
        
        public BestuurderAanpassen()
        {
            InitializeComponent();
        }

        public BestuurderAanpassen(Bestuurder bestuurder)
        {
            InitializeComponent();
            
            _bestuurder = bestuurder;
            VulBestuurderDataAan(bestuurder);
            RijbewijsComboBox.ItemsSource = ((MainWindow)Application.Current.MainWindow)._allRijbewijsTypes.Select(r => r.Type).OrderBy(r => r);
        }

        private void VulBestuurderDataAan(Bestuurder bestuurder)
        {
            TextBoxBestuurderNaam.Text = bestuurder.Naam;
            TextBoxVoornaamBestuurder.Text = bestuurder.Voornaam;
            PickerGeboorteDatum.SelectedDate = bestuurder.Geboortedatum;
            Rijksregisternummer.Text = bestuurder.Rijksregisternummer;
            RijbewijzenListBox.ItemsSource = bestuurder.GeefRijbewijsTypes().Select(r => r.Type);

        }

        private void AnnulerenButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
