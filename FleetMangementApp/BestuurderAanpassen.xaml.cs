using DomainLayer.Managers;
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
        private readonly BestuurderManager _bestuurderManager;
        private readonly VoertuigManager _voertuigManager;
        private readonly TankkaartManager _tankkaartManager;
        public Voertuig GeselecteerdVoertuig; 
        internal bool tankkaartChanged = false;

        public BestuurderAanpassen()
        {
            InitializeComponent();
        }

        public BestuurderAanpassen(Bestuurder bestuurder, BestuurderManager bestuurderManager, VoertuigManager voertuigManager, TankkaartManager tankkaartManager)
        {
            InitializeComponent();
            
            _bestuurder = bestuurder;
            _bestuurderManager = bestuurderManager;
            _voertuigManager = voertuigManager;
            _tankkaartManager = tankkaartManager;
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
            if (bestuurder.Voertuig != null)
                VoertuigTextBox.Text = $"Id: {bestuurder.Voertuig.Id}, Wagen: {bestuurder.Voertuig.Merk} met nummerplaat {bestuurder.Voertuig.Nummerplaat}";
            else
                VoertuigTextBox.Text = "Geen Voertuig";
            GeselecteerdVoertuig = bestuurder.Voertuig;
            if (bestuurder.Tankkaart != null)
                TankkaartTextBox.Text = $"Tankkaart met kaartnummer: {bestuurder.Tankkaart.Kaartnummer}";
            else
                TankkaartTextBox.Text = "Geen Tankkaart";

        }

        private void AnnulerenButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AanpassenButton_Click(object sender, RoutedEventArgs e)
        {
            List<RijbewijsType> rijbewijzen = new List<RijbewijsType>();
            var rijbewijzenInString = RijbewijzenListBox.ItemsSource?.Cast<string>() ?? new List<string>();
            rijbewijzen = ((MainWindow)Application.Current.MainWindow)._allRijbewijsTypes.Where(r => rijbewijzenInString.Contains(r.Type)).ToList();

            Bestuurder aangepasteBestuurder = new Bestuurder(_bestuurder.Id, TextBoxBestuurderNaam.Text, TextBoxVoornaamBestuurder.Text, PickerGeboorteDatum.SelectedDate.Value, Rijksregisternummer.Text, rijbewijzen, _bestuurder.IsGearchiveerd);
            
            if(GeselecteerdVoertuig != null)
            {
                aangepasteBestuurder.ZetVoertuig(GeselecteerdVoertuig);
            }



            //if (!tankkaartChanged)
                //aangepasteBestuurder.ZetTankkaart(_bestuurder.Tankkaart);



            _bestuurderManager.UpdateBestuurder(aangepasteBestuurder);
            VulBestuurderDataAan(aangepasteBestuurder);
            //MessageBox.Show("Bestuurder is aangepast");
            Close();
        }

        private void ButtonSelecteerVoertuig_Click(object sender, RoutedEventArgs e)
        {
            new VoertuigSelecteren(_voertuigManager)
            {
                Owner = this
            }.ShowDialog();
        }
    }
}
