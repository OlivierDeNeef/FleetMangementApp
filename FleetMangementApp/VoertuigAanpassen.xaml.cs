using DomainLayer.Managers;
using DomainLayer.Models;
using System;
using System.Linq;
using System.Windows;

namespace FleetMangementApp
{
    /// <summary>
    /// Interaction logic for VoertuigAanpassen.xaml
    /// </summary>
    public partial class VoertuigAanpassen : Window
    {
        private Voertuig _voertuig;
        private BestuurderManager _bestuurderManager;
        private VoertuigManager _voertuigManager;
        private int _aantalDeuren;
        public Bestuurder GeselecteerdeBestuurder = null;
        private RijbewijsTypeManager _rijbewijsTypeManager;

        public VoertuigAanpassen(Voertuig voertuig, VoertuigManager voertuigManager, BestuurderManager bestuurderManager, RijbewijsTypeManager rijbewijsTypeManager)
        {

            InitializeComponent();
            _voertuig = voertuig;
            _bestuurderManager = bestuurderManager;
            _voertuigManager = voertuigManager;
            _rijbewijsTypeManager = rijbewijsTypeManager;
            SetupVoertuigWindowView();
            VulVoertuigdataAan();
        }

        private void AnnulerenButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SetupVoertuigWindowView()
        {
            
            VoertuigAanpassenBrandstofComboBox.ItemsSource = ((MainWindow)Application.Current.MainWindow)._brandstoffen.Select(b => b.Type);
            AanpassenVoertuigWagenTypeComboBox.ItemsSource = ((MainWindow)Application.Current.MainWindow)._wagentypes.Select(w => w.Type);
        }

        private void VerhoogAantalDeurenButton_OnClick(object sender, RoutedEventArgs e)
        {
            _aantalDeuren += 1;
            ToevoegenVoertuigAantalDeurenTextbox.Text = _aantalDeuren.ToString();
            VerplichteVeldenChecker();
        }

        private void VerlaagAantalDeurenButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_aantalDeuren != 0)
            {
                _aantalDeuren -= 1;
                ToevoegenVoertuigAantalDeurenTextbox.Text = _aantalDeuren.ToString();

            }
            VerplichteVeldenChecker();
        }

        private void VulVoertuigdataAan()
        {
            ToevoegenVoertuigMerkTextbox.Text = _voertuig.Merk;
            ToevoegenVoertuigModelTextbox.Text = _voertuig.Model;
            ToevoegenVoertuigNummerplaatTextbox.Text = _voertuig.Nummerplaat;
            ToevoegenVoertuigCNummerTextbox.Text = _voertuig.Chassisnummer;

            VoertuigAanpassenBrandstofComboBox.SelectedItem = _voertuig.BrandstofType.Type;
            AanpassenVoertuigWagenTypeComboBox.SelectedItem = _voertuig.WagenType.Type;

            if (!string.IsNullOrEmpty(_voertuig.Kleur))
                ToevoegenVoertuigKleurTextbox.Text = _voertuig.Kleur;
            else
                ToevoegenVoertuigKleurTextbox.Text = "Geen kleur ingesteld";

            _aantalDeuren = _voertuig.AantalDeuren;
            ToevoegenVoertuigAantalDeurenTextbox.Text = _voertuig.AantalDeuren.ToString();

            GeselecteerdeBestuurder = _voertuig.Bestuurder;
            if (GeselecteerdeBestuurder == null)
            {
                ToevoegenVoertuigBestuurderTextbox.Text = "Geen bestuurder";
            }
            else
            {
                ToevoegenVoertuigBestuurderTextbox.Text = $"Bestuurder met naam {_voertuig.Bestuurder.Voornaam} {_voertuig.Bestuurder.Naam}.";
                GeselecteerdeBestuurder = _voertuig.Bestuurder;
            }
                


        }

        private void SelecteerBestuurderButton_Click(object sender, RoutedEventArgs e)
        {
            new BestuurderSelecteren(_bestuurderManager, _rijbewijsTypeManager)
            {
                Owner = this
            }.ShowDialog();
        }

        private void VoertuigAanpassenButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string brandstofString = (string)VoertuigAanpassenBrandstofComboBox.SelectedItem;
                string wagentypeString = (string)AanpassenVoertuigWagenTypeComboBox.SelectedItem;
                BrandstofType brandstof = ((MainWindow)Application.Current.MainWindow)._brandstoffen.FirstOrDefault(b => b.Type == brandstofString);
                WagenType wagen = ((MainWindow)Application.Current.MainWindow)._wagentypes.FirstOrDefault(w => w.Type == wagentypeString);
                Voertuig aangepastVoertuig = new Voertuig(_voertuig.Id, ToevoegenVoertuigMerkTextbox.Text, ToevoegenVoertuigModelTextbox.Text, ToevoegenVoertuigCNummerTextbox.Text, ToevoegenVoertuigNummerplaatTextbox.Text, brandstof, wagen);
                if(GeselecteerdeBestuurder != null)
                {
                    if (GeselecteerdeBestuurder != _voertuig.Bestuurder)
                    {
                        var bestuurder = _voertuig.Bestuurder;
                        aangepastVoertuig.ZetBestuurder(GeselecteerdeBestuurder);

                        if (bestuurder is not null)
                        {
                            bestuurder.VerwijderVoertuig();
                            _bestuurderManager.UpdateBestuurder(bestuurder);
                        }
                        _bestuurderManager.UpdateBestuurder(GeselecteerdeBestuurder);
                    }
                    
                }
                else if(_voertuig.Bestuurder is not null)
                {
                    var bestuurder = _voertuig.Bestuurder;
                    _voertuig.VerwijderBestuurder();
                    _bestuurderManager.UpdateBestuurder(bestuurder);
                }


                if (string.IsNullOrEmpty(ToevoegenVoertuigKleurTextbox.Text))
                    aangepastVoertuig.ZetKleur("Geen kleur ingesteld");
                else
                    aangepastVoertuig.ZetKleur(ToevoegenVoertuigKleurTextbox.Text);
                if (_aantalDeuren < 3)
                {
                    aangepastVoertuig.ZetAantalDeuren(3);
                }
                else
                    aangepastVoertuig.ZetAantalDeuren(_aantalDeuren);

                _voertuigManager.UpdateVoertuig(aangepastVoertuig);
                
                MessageBox.Show("Voertuig Aangepast");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Voertuig toevoegen mislukt:" + ex.Message);
            }
        }

        private void VerplichteVeldenChecker()
        {
            if (string.IsNullOrWhiteSpace(ToevoegenVoertuigMerkTextbox.Text) || string.IsNullOrWhiteSpace(ToevoegenVoertuigModelTextbox.Text)
                || string.IsNullOrWhiteSpace(ToevoegenVoertuigCNummerTextbox.Text) || string.IsNullOrWhiteSpace(ToevoegenVoertuigNummerplaatTextbox.Text)
                || string.IsNullOrWhiteSpace(ToevoegenVoertuigKleurTextbox.Text) || AanpassenVoertuigWagenTypeComboBox.SelectedItem == null
                || VoertuigAanpassenBrandstofComboBox.SelectedItem == null || _aantalDeuren < 3)
            {
                VoertuigAanpassenButton.IsEnabled = false;
            }
            else
                VoertuigAanpassenButton.IsEnabled = true;
        }

        private void Textbox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            VerplichteVeldenChecker();
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            VerplichteVeldenChecker();
        }
    }
}
