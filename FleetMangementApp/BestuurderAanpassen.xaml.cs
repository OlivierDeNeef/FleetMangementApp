using DomainLayer.Managers;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public Tankkaart GeselecteerdeTankkaart;
        private ObservableCollection<string> _rijbewijzen ;
        
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

            if (bestuurder.Adres is not null)
            {

                if (bestuurder.Adres.Straat is not null)
                    TextBoxBestuurderStraat.Text = bestuurder.Adres.Straat;
                if (bestuurder.Adres.Huisnummer is not null)
                    TextBoxBestuurderHuisnummer.Text = bestuurder.Adres.Huisnummer;
                if (bestuurder.Adres.Stad is not null)
                    TextBoxBestuurderStad.Text = bestuurder.Adres.Stad;
                if (bestuurder.Adres.Postcode is not null)
                    TextBoxBestuurderPostcode.Text = bestuurder.Adres.Postcode;
                if (bestuurder.Adres.Land is not null)
                    TextBoxBestuurderLand.Text = bestuurder.Adres.Land;

            }

            _rijbewijzen = new ObservableCollection<string>(bestuurder.GeefRijbewijsTypes().Select(r => r.Type));
            RijbewijzenListBox.ItemsSource = _rijbewijzen ;
            if (bestuurder.Voertuig != null)
                VoertuigTextBox.Text = $"Id: {bestuurder.Voertuig.Id}, Wagen: {bestuurder.Voertuig.Merk} met nummerplaat {bestuurder.Voertuig.Nummerplaat}";
            else
                VoertuigTextBox.Text = "Geen Voertuig";
            GeselecteerdVoertuig = bestuurder.Voertuig;
            GeselecteerdeTankkaart = bestuurder.Tankkaart;
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
            try
            {
                List<RijbewijsType> rijbewijzen = new List<RijbewijsType>();
                var rijbewijzenInString = RijbewijzenListBox.ItemsSource?.Cast<string>() ?? new List<string>();
                rijbewijzen = ((MainWindow)Application.Current.MainWindow)._allRijbewijsTypes.Where(r => rijbewijzenInString.Contains(r.Type)).ToList();

                Bestuurder aangepasteBestuurder = new Bestuurder(_bestuurder.Id, TextBoxBestuurderNaam.Text, TextBoxVoornaamBestuurder.Text, PickerGeboorteDatum.SelectedDate.Value, Rijksregisternummer.Text, rijbewijzen, _bestuurder.IsGearchiveerd);


                if (!string.IsNullOrWhiteSpace(TextBoxBestuurderStraat.Text) ||
                   !string.IsNullOrWhiteSpace(TextBoxBestuurderHuisnummer.Text) ||
                   !string.IsNullOrWhiteSpace(TextBoxBestuurderStad.Text) ||
                   !string.IsNullOrWhiteSpace(TextBoxBestuurderPostcode.Text) ||
                   !string.IsNullOrWhiteSpace(TextBoxBestuurderLand.Text))

                {
                    if (!string.IsNullOrWhiteSpace(TextBoxBestuurderStraat.Text) &&
                        !string.IsNullOrWhiteSpace(TextBoxBestuurderHuisnummer.Text) &&
                        !string.IsNullOrWhiteSpace(TextBoxBestuurderStad.Text) &&
                        !string.IsNullOrWhiteSpace(TextBoxBestuurderPostcode.Text) &&
                        !string.IsNullOrWhiteSpace(TextBoxBestuurderLand.Text))
                    {

                        var adres = new Adres(TextBoxBestuurderStraat.Text,
                            TextBoxBestuurderHuisnummer.Text, TextBoxBestuurderStad.Text,
                            TextBoxBestuurderPostcode.Text, TextBoxBestuurderLand.Text);
                        aangepasteBestuurder.ZetAdres(adres);
                    }
                    else
                    {
                        MessageBox.Show("Adres is onvolledig ingevuld");
                    }
                }


                if (GeselecteerdVoertuig != null)
                {
                    aangepasteBestuurder.ZetVoertuig(GeselecteerdVoertuig);
                }

                if(GeselecteerdeTankkaart != null)
                {
                    aangepasteBestuurder.ZetTankkaart(GeselecteerdeTankkaart);
                }

                _bestuurderManager.UpdateBestuurder(aangepasteBestuurder);
                VulBestuurderDataAan(aangepasteBestuurder);
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.InnerException?.InnerException?.Message ?? exception.Message, "Fout", MessageBoxButton.OK);
            }
        }

        private void ButtonSelecteerVoertuig_Click(object sender, RoutedEventArgs e)
        {
            new VoertuigSelecteren(_voertuigManager)
            {
                Owner = this
            }.ShowDialog();
        }

        private void ButtonSelecteerTankkaart_Click(object sender, RoutedEventArgs e)
        {
            new TankkaartSelecteren(_tankkaartManager)
            {
                Owner = this
            }.ShowDialog();
        }

        private void ListBoxRijbewijzen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RijbewijsComboBox.SelectedItem = RijbewijzenListBox.SelectedItem;
            
        }

        private void ToevoegenRijbewijsButton_OnClick(object sender, RoutedEventArgs e)
        {
            string r = (string)RijbewijsComboBox.SelectedValue;
            if (!RijbewijzenListBox.Items.Contains(r))
                _rijbewijzen.Add(r);

            VerplichteVeldenChecker();
                
        }

        private void VerwijderRijbewijsButton_OnClick(object sender, RoutedEventArgs e)
        {
            string r = (string)RijbewijsComboBox.SelectedValue;
            _rijbewijzen.Remove(r);
            VerplichteVeldenChecker();
        }

        private void VerplichteVeldenChecker()
        {
            if (string.IsNullOrWhiteSpace(TextBoxBestuurderNaam.Text) || string.IsNullOrWhiteSpace(TextBoxVoornaamBestuurder.Text)
                || string.IsNullOrWhiteSpace(Rijksregisternummer.Text) || PickerGeboorteDatum.SelectedDate == null
                || RijbewijzenListBox.Items.Count < 1)
            {
                AanpassenButton.IsEnabled = false;
            }
            else
            {
                AanpassenButton.IsEnabled = true;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            VerplichteVeldenChecker();
        }

        private void PickerGeboorteDatum_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            VerplichteVeldenChecker();
        }
    }
}
