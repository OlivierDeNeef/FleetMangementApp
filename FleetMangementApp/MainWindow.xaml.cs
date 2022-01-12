using DomainLayer.Managers;
using DomainLayer.Models;
using DomainLayer.Utilities;
using FleetMangementApp.Mappers;
using FleetMangementApp.Models.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace FleetMangementApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BestuurderManager _bestuurderManager;
        private readonly VoertuigManager _voertuigManager;
        private readonly BrandstofTypeManager _brandstofTypeManager;
        private readonly WagenTypeManager _wagenTypeManager;
        private readonly RijbewijsTypeManager _rijbewijsTypeManager;
        private readonly TankkaartManager _tankkaartManager;
        public Tankkaart tankkaart;
        private int _aantalDeuren;
        protected internal List<BrandstofType> _brandstoffen = new();
        protected internal List<WagenType> _wagentypes = new();
        private static readonly Regex _regex = new Regex("[^0-9.-]+");

        protected internal List<RijbewijsType> _allRijbewijsTypes = new();

        private int _selectedBestuurderId;

        public MainWindow(BestuurderManager bestuurderManager, VoertuigManager voertuigManager, RijbewijsTypeManager rijbewijsTypeManager,
            WagenTypeManager wagenTypeManager, BrandstofTypeManager brandstofTypeManager, TankkaartManager tankkaartManager)
        {
            _rijbewijsTypeManager = rijbewijsTypeManager;
            _wagenTypeManager = wagenTypeManager;
            _brandstofTypeManager = brandstofTypeManager;
            _bestuurderManager = bestuurderManager;
            _voertuigManager = voertuigManager;
            _tankkaartManager = tankkaartManager;
            InitializeComponent();
            SetupBestuurderView();
            SetupVoertuigWindowView();
            SetupTankkaartWindowView();
        }

    
        #region BestuurderTab

        private void SetupBestuurderView()
        {
            _allRijbewijsTypes = _rijbewijsTypeManager.GeefAlleRijsbewijsTypes().ToList();
            ComboBoxRijbewijzen.ItemsSource = _allRijbewijsTypes.Select(r => r.Type).OrderBy(r => r);
        }
        private void ZoekBestuurderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Valideerd de velden van de zoek parameters 
                if (!ValidateBestuurderFields()) return;
                //Input omzetten naar correcte types
                var id = string.IsNullOrWhiteSpace(TextBoxBestuurderId.Text) ? 0 : int.Parse(TextBoxBestuurderId.Text);
                var geboortedatum = DatePickerGeboortedatumBestuurder.SelectedDate ?? DateTime.MinValue;
                var rijbewijzenInString = ListBoxRijbewijzen.ItemsSource?.Cast<string>() ?? new List<string>();
                var selectedRijbewijzen = _allRijbewijsTypes.Where(r => rijbewijzenInString.Contains(r.Type)).ToList();
                var rijksregisternummer = string.IsNullOrWhiteSpace(TextBoxRijksregisternummerBestuurder.Text) ? "" : RijksregisternummerChecker.ParseWithoutDate(TextBoxRijksregisternummerBestuurder.Text);
                var voornaam = TextBoxNaamBestuurder.Text;
                var naam = TextBoxVoornaamBestuurder.Text;
                var gearchiveerd = CheckBoxGearchiveerBestuurder.IsChecked.Value;
                
                //Bestuurders op vragen voor de ingevulde parameters
                var result = _bestuurderManager.GeefGefilterdeBestuurder(id, naam, voornaam, geboortedatum, selectedRijbewijzen, rijksregisternummer, gearchiveerd).ToList();

                //Gevonden bestuurder mappen aan datagrid
                ResultatenBestuurders.ItemsSource = result.Select(BestuurderUIMapper.ToUI);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Fout", MessageBoxButton.OK);
            }

        }
        private bool ValidateBestuurderFields()
        {
            var result = true;
            if (!string.IsNullOrWhiteSpace(TextBoxBestuurderId.Text) && !int.TryParse(TextBoxBestuurderId.Text, out var _result))
            {
                MessageBox.Show("Id kan alleen nummers bevatten", "Invalid field");
                result = false;
            }

            if (!string.IsNullOrWhiteSpace(TextBoxRijksregisternummerBestuurder.Text) && TextBoxRijksregisternummerBestuurder.Text.Length is > 15 or < 11)
            {
                MessageBox.Show("Rijksregisternummer kan enkel 11-15 karakters bevatten", "Invalid field");
                result = false;
            }

            if (DatePickerGeboortedatumBestuurder.SelectedDate.HasValue && DatePickerGeboortedatumBestuurder.SelectedDate.Value > DateTime.Today.AddYears(-18))
            {
                MessageBox.Show("Een bestuurder moet minstens 18 jaar oud zijn", "Invalid field");
                result = false;
            }
            return result;
        }
        private void RowGotFocus(object sender, RoutedEventArgs e)
        {
            ButtonDetailBestuurder.IsEnabled = true;
            ButtonEditBestuurder.IsEnabled = true;
            ButtonArchiveerBestuurder.IsEnabled = true;

            ButtonDetailsVoertuig.IsEnabled = true;
            ButtonEditVoertuig.IsEnabled = true;
            ButtonArchiveerVoertuig.IsEnabled = true;

            ButtonDetailsTankkaart.IsEnabled = true;
            ButtonEditTankkaart.IsEnabled = true;
            ButtonArchiveerTankkaart.IsEnabled = true;

            // Neemt de geselecteerde bestuurderId uit het datagrid 
            var datagridrow = sender as DataGridRow;
            if (datagridrow?.Item is ResultBestuurder selectedResult) _selectedBestuurderId = selectedResult.Id;

        }
        private void RowLostFocus(object sender, RoutedEventArgs e)
        {
            ButtonDetailBestuurder.IsEnabled = false;
            ButtonEditBestuurder.IsEnabled = false;
            ButtonArchiveerBestuurder.IsEnabled = false;
            ButtonDetailsVoertuig.IsEnabled = false;
            ButtonEditVoertuig.IsEnabled = false;
            ButtonArchiveerVoertuig.IsEnabled = false;
            ButtonDetailsTankkaart.IsEnabled = false;
            ButtonEditTankkaart.IsEnabled = false;
            ButtonArchiveerTankkaart.IsEnabled = false;
        }

        private void ListBoxRijbewijzen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxRijbewijzen.SelectedItem = ListBoxRijbewijzen.SelectedItem;
        }

        private void ToevoegenRijbewijsButton_OnClick(object sender, RoutedEventArgs e)
        {
            string r = (string)ComboBoxRijbewijzen.SelectedValue;
            if (!ListBoxRijbewijzen.Items.Contains(r))
                ListBoxRijbewijzen.Items.Add(r);

        }

        private void VerwijderRijbewijsButton_OnClick(object sender, RoutedEventArgs e)
        {
            string r = (string)ComboBoxRijbewijzen.SelectedValue;
                ListBoxRijbewijzen.Items.Remove(r);
        }

        private void ButtonNieuweBestuurder_OnClick(object sender, RoutedEventArgs e)
        {
           
            new BestuurderToevoegen(_bestuurderManager, _voertuigManager, _tankkaartManager)
            {
                Owner = this
            }.ShowDialog();

        }

        private void ButtonBestuurderAanpassen_Click(object sender, RoutedEventArgs e)
        {
            if (ResultatenBestuurders.SelectedItem != null)
            {
                var selectedBestuurder = (ResultBestuurder)ResultatenBestuurders.SelectedItem;
                new BestuurderAanpassen(_bestuurderManager.GeefBestuurder(selectedBestuurder.Id), _bestuurderManager, _voertuigManager, _tankkaartManager)
                {
                    Owner = this
                }.ShowDialog();
                ZoekBestuurderButton_Click(this,e);
            }
        }

        private void ButtonDetailsBestuurder_OnClick(object sender, RoutedEventArgs e)
        {
            if(ResultatenBestuurders.SelectedItem != null)
            {
                var selectedBestuurder = (ResultBestuurder)ResultatenBestuurders.SelectedItem;
                new Details(_bestuurderManager.GeefBestuurder(selectedBestuurder.Id))
                {
                    Owner = this
                }.ShowDialog();
            }
            
        }

        private void ButtonArchiveerBestuurder_Click(object sender, RoutedEventArgs e)
        {
            if (ResultatenBestuurders.SelectedItem != null)
            {
                var selectedItem = (ResultBestuurder)ResultatenBestuurders.SelectedItem;
                Bestuurder selectedBestuurder = _bestuurderManager.GeefBestuurder(selectedItem.Id);
                selectedBestuurder.ZetGearchiveerd(!selectedBestuurder.IsGearchiveerd);
                _bestuurderManager.UpdateBestuurder(selectedBestuurder);
            }
        }



        #endregion

        #region VoertuigenTab


        private void SetupVoertuigWindowView()
        {
            _brandstoffen = _brandstofTypeManager.GeefAlleBrandstofTypes().ToList(); // ADO methode returned list van Brandstoftype != 
            VoertuigComboBoxBrandstof.ItemsSource = _brandstoffen.Select(b => b.Type);
            _wagentypes = _wagenTypeManager.GeefAlleWagenTypes().ToList();
            VoertuigComboBoxTypeWagen.ItemsSource = _wagentypes.Select(w => w.Type);
        }
        

        private void VerhoogAantalDeurenButton_OnClick(object sender, RoutedEventArgs e)
        {
            _aantalDeuren += 1;
            TextBoxAantalDeuren.Text = _aantalDeuren.ToString();
        }

        private void VerlaagAantalDeurenButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_aantalDeuren != 0)
            {
                _aantalDeuren -= 1;
                TextBoxAantalDeuren.Text = _aantalDeuren.ToString();

            }
        }


        private void ZoekVoertuigButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Valideerd de velden van de zoek parameters 
                if (!ValidateVoertuigFields()) return;
                //Input omzetten naar correcte types
                var id = string.IsNullOrWhiteSpace(TextBoxVoertuigId.Text) ? 0 : int.Parse(TextBoxVoertuigId.Text);
                
                var merk = TextBoxMerkVoertuig.Text;
                var model = TextBoxModelVoertuig.Text;
                var aantalDeuren = string.IsNullOrWhiteSpace(TextBoxAantalDeuren.Text) ? 0 : int.Parse(TextBoxAantalDeuren.Text);
                var nummerplaat = TextBoxVoertuigenNummerplaat.Text;
                var chassisnummer = TextBoxChassisnummerVoertuigen.Text;
                var kleur = TextBoxKleurVoertuigen.Text;
                var gearchiveerd = CheckBoxGearchiveerdVoertuig.IsChecked.Value;
                var isHybride = false;

                var wagenType = VoertuigComboBoxTypeWagen.SelectedItem == null ? null :  _wagenTypeManager.GeefAlleWagenTypes().FirstOrDefault(w => w.Type == VoertuigComboBoxTypeWagen.SelectedItem.ToString());
                var brandstofType = VoertuigComboBoxBrandstof.SelectedItem == null ? null : _brandstofTypeManager.GeefAlleBrandstofTypes().FirstOrDefault(b => b.Type == VoertuigComboBoxBrandstof.SelectedItem.ToString());

                var result = _voertuigManager.GeefGefilterdeVoertuigen(id, merk, model, aantalDeuren, nummerplaat, chassisnummer, kleur, wagenType, brandstofType, gearchiveerd, isHybride).ToList();
                //Gevonden bestuurder mappen aan datagrid
                ResultatenVoertuigen.ItemsSource = result.Select(VoertuigUIMapper.ToUI);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Fout", MessageBoxButton.OK);
            }

        }
        private bool ValidateVoertuigFields()
        {
            var result = true;
            if (!string.IsNullOrWhiteSpace(TextBoxVoertuigId.Text) && !int.TryParse(TextBoxVoertuigId.Text, out var _resultIdParse))
            {
                MessageBox.Show("Id kan alleen nummers bevatten", "Invalid field");
                result = false;
            }

            if (!string.IsNullOrWhiteSpace(TextBoxAantalDeuren.Text) && !int.TryParse(TextBoxAantalDeuren.Text, out var _resultDeurenParse))
            {
                MessageBox.Show("Aantal Deuren mag enkel nummers bevatten", "Invalid field");
                result = false;
            }

            if (!string.IsNullOrWhiteSpace(TextBoxChassisnummerVoertuigen.Text) && TextBoxChassisnummerVoertuigen.Text.Length != 17)
            {
                MessageBox.Show("Het chassisnummer moet 17 tekens bevatten", "Invalid field");
                result = false;
            }

            return result;
        }

        private void ButtonNieuwVoertuig_OnClick(object sender, RoutedEventArgs e)
        {
            new VoertuigToevoegen(_bestuurderManager, _voertuigManager, _brandstofTypeManager, _wagenTypeManager, _rijbewijsTypeManager)
            {
                Owner = this

            }.ShowDialog();
        }

        private void ButtonDetailsVoertuig_OnClick(object sender, RoutedEventArgs e)
        {
            if (ResultatenVoertuigen.SelectedItem != null)
            {
                var selectedVoertuig = (ResultVoertuig)ResultatenVoertuigen.SelectedItem;
                new Details(_voertuigManager.GeefVoertuig(selectedVoertuig.Id))
                {
                    Owner = this
                }.ShowDialog();
            }

        }

        private void ButtonPasVoertuigAan_Click(object sender, RoutedEventArgs e)
        {
            if (ResultatenVoertuigen.SelectedItem != null)
            {
                var selectedVoertuig = (ResultVoertuig)ResultatenVoertuigen.SelectedItem;
                new VoertuigAanpassen(_voertuigManager.GeefVoertuig(selectedVoertuig.Id), _voertuigManager, _bestuurderManager, _rijbewijsTypeManager)
                {
                    Owner = this
                }.ShowDialog();
            }
            ZoekVoertuigButton_Click(this, e);
        }

        private void ButtonArchiveerVoertuig_Click(object sender, RoutedEventArgs e)
        {
            if (ResultatenVoertuigen.SelectedItem != null)
            {
                var selectedItem = (ResultVoertuig)ResultatenVoertuigen.SelectedItem;
                Voertuig selectedVoertuig = _voertuigManager.GeefVoertuig(selectedItem.Id);
                selectedVoertuig.ZetGearchiveerd(!selectedVoertuig.IsGearchiveerd);
                _voertuigManager.UpdateVoertuig(selectedVoertuig);
            }
        }

        #endregion

        #region Tankkaart
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void SetupTankkaartWindowView()
        {
            _brandstoffen = _brandstofTypeManager.GeefAlleBrandstofTypes().ToList(); // ADO methode returned list van Brandstoftype != 
            BrandstoftypeTankkaartCombobox.ItemsSource = _brandstoffen.Select(b => b.Type);
        }



        private void ListBoxBrandstofTypesTankkaart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BrandstoftypeTankkaartCombobox.SelectedItem = ListBoxBrandstofTypesTankkaart.SelectedItem;
        }

        private void ToevoegenTankkaartButton_OnClick(object sender, RoutedEventArgs e)
        {
            string r = (string)BrandstoftypeTankkaartCombobox.SelectedValue;
            if (!ListBoxBrandstofTypesTankkaart.Items.Contains(r))
                ListBoxBrandstofTypesTankkaart.Items.Add(r);
        }

        private void VerwijderTankkaartButton_OnClick(object sender, RoutedEventArgs e)
        {
            string r = (string)BrandstoftypeTankkaartCombobox.SelectedValue;
            ListBoxBrandstofTypesTankkaart.Items.Remove(r);
        }
        

      
        private void ButtonNieuwTankkaart_OnClick(object sender, RoutedEventArgs e)
        {
            new TankkaartToevoegen(_brandstofTypeManager,_tankkaartManager, _bestuurderManager, _rijbewijsTypeManager)
            {
                Owner = this
            }.ShowDialog();
        }

        private void ButtonDetailsTankkaart_Click(object sender, RoutedEventArgs e)
        {
            if (ResultatenTankkaarten.SelectedItem != null)
            {
                var selectedTankkaart = (ResultTankkaart)ResultatenTankkaarten.SelectedItem;
                new Details(_tankkaartManager.GeefTankkaart(selectedTankkaart.Id))
                {
                    Owner = this
                }.ShowDialog();
            }
        }

        private void ZoekenButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var kaartnummer = TankkaartKaartnummer.Text;
                var geldigheidsdatum = DatePickerGeldigheidsdatumTankkaart.SelectedDate ?? DateTime.MinValue;
                var brandstoffenInString = ListBoxBrandstofTypesTankkaart.ItemsSource?.Cast<string>() ?? new List<string>();
                var lijstBrandstoftypes = _brandstoffen.Where(r => brandstoffenInString.Contains(r.Type)).ToList();
                var gearchiveerd = CheckBoxGearchiveerdTankkaart.IsChecked.Value;

                ResultatenTankkaarten.ItemsSource = _tankkaartManager.GeefGefilterdeTankkaarten(kaartnummer, geldigheidsdatum, lijstBrandstoftypes, gearchiveerd).Select(TankkaartUIMapper.ToUI);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Fout", MessageBoxButton.OK);
            }
        }


        private void ButtonEditTankkaart_OnClick(object sender, RoutedEventArgs e)
        {

            var selectedTankkaart = (ResultTankkaart)ResultatenTankkaarten.SelectedItem;
            new TankkaartAanpassen(_tankkaartManager.GeefTankkaart(selectedTankkaart.Id), _brandstofTypeManager, _tankkaartManager, _bestuurderManager, _rijbewijsTypeManager)
            {
                Owner = this
            }.ShowDialog();
            ZoekenButton_Click(this,e);
        }

        private void ButtonArchiveerTankkaart_OnClick(object sender, RoutedEventArgs e)
        {
            if (ResultatenTankkaarten.SelectedItem != null)
            {
                var selectedItem = (ResultTankkaart)ResultatenTankkaarten.SelectedItem;
                Tankkaart selectedTankkaart = _tankkaartManager.GeefTankkaart(selectedItem.Id);
                selectedTankkaart.ZetGearchiveerd(!selectedTankkaart.IsGearchiveerd);
                _tankkaartManager.UpdateTankkaart(selectedTankkaart);
                MessageBox.Show("(De)Archiveren gelukt");
            }
        }




        #endregion

        
    }
}
