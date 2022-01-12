using DomainLayer.Managers;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
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
    /// Interaction logic for BestuurderToevoegen.xaml
    /// </summary>
    /// 
    

    public partial class BestuurderToevoegen : Window 
    {
        private readonly BestuurderManager _bestuurderManager;
        private readonly VoertuigManager _voertuigManager;
        private readonly TankkaartManager _tankkaartManager;
        public Voertuig GeselecteerdVoertuig { get; set; }
        public Tankkaart GeselecteerdeTankkaart { get; set; }
        private ObservableCollection<string> _rijbewijzen = new();

        public BestuurderToevoegen()
        {
            InitializeComponent();
        }

        public BestuurderToevoegen( BestuurderManager bestuurderManager, VoertuigManager voertuigManager, TankkaartManager tankkaartManager)
        {
            InitializeComponent();

            _bestuurderManager = bestuurderManager;
            _voertuigManager = voertuigManager;
            _tankkaartManager = tankkaartManager;
            RijbewijsComboBox.ItemsSource = ((MainWindow)Application.Current.MainWindow)._allRijbewijsTypes.Select(r => r.Type).OrderBy(r => r);
            RijbewijzenListBox.ItemsSource = _rijbewijzen;
        }


        private void AnnulerenButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ToevoegenButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<RijbewijsType> rijbewijzen = new List<RijbewijsType>();
                var rijbewijzenInString = RijbewijzenListBox.ItemsSource?.Cast<string>() ?? new List<string>();
                rijbewijzen = ((MainWindow)Application.Current.MainWindow)._allRijbewijsTypes.Where(r => rijbewijzenInString.Contains(r.Type)).ToList();

                Bestuurder nieuweBestuurder = new Bestuurder( TextBoxBestuurderNaam.Text, TextBoxVoornaamBestuurder.Text, PickerGeboorteDatum.SelectedDate.Value, Rijksregisternummer.Text, rijbewijzen, false);
                
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
                        !string.IsNullOrWhiteSpace(TextBoxBestuurderLand.Text)) {
                        
                    var adres = new Adres(TextBoxBestuurderStraat.Text, 
                        TextBoxBestuurderHuisnummer.Text, TextBoxBestuurderStad.Text,
                        TextBoxBestuurderPostcode.Text, TextBoxBestuurderLand.Text);
                    nieuweBestuurder.ZetAdres(adres);
                    }
                    else
                    {
                        MessageBox.Show("Adres is onvolledig ingevuld");
                    }
                }

                if (GeselecteerdVoertuig != null)
                {
                    nieuweBestuurder.ZetVoertuig(GeselecteerdVoertuig);
                }

                if (GeselecteerdeTankkaart != null)
                {
                    nieuweBestuurder.ZetTankkaart(GeselecteerdeTankkaart);
                }

                _bestuurderManager.VoegBestuurderToe(nieuweBestuurder);

                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Er ging iets mis met de registratie:  " + exception.Message);
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
            if(string.IsNullOrWhiteSpace(TextBoxBestuurderNaam.Text) || string.IsNullOrWhiteSpace(TextBoxVoornaamBestuurder.Text) 
                || string.IsNullOrWhiteSpace(Rijksregisternummer.Text) || PickerGeboorteDatum.SelectedDate == null 
                || RijbewijzenListBox.Items.Count < 1  )
            {
                ToevoegenButton.IsEnabled = false;
            }
            else
            {
                ToevoegenButton.IsEnabled = true;
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
