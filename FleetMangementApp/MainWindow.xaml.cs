using DomainLayer.Managers;
using DomainLayer.Models;
using DomainLayer.Utilities;
using FleetMangementApp.Mappers;
using FleetMangementApp.Models.Output;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly BrandstofTypeManager _brandstofTypeManager;
        private readonly WagenTypeManager _wagenTypeManager;
        private readonly RijbewijsTypeManager _rijbewijsTypeManager;
        private List<BrandstofType> _brandstoffen = new();
        private List<WagenType> _wagentypes = new();

        protected internal List<RijbewijsType> _allRijbewijsTypes = new();

        private int _selectedBestuurderId;

        public MainWindow(BestuurderManager bestuurderManager, RijbewijsTypeManager rijbewijsTypeManager, WagenTypeManager wagenTypeManager, BrandstofTypeManager brandstofTypeManager)
        {
            _rijbewijsTypeManager = rijbewijsTypeManager;
            _wagenTypeManager = wagenTypeManager;
            _brandstofTypeManager = brandstofTypeManager;
            _bestuurderManager = bestuurderManager;
            InitializeComponent();
            SetupBestuurderView();
            SetupVoertuigWindowView();
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

            // Neemt de geselecteerde bestuurderId uit het datagrid 
            var datagridrow = sender as DataGridRow;
            if (datagridrow?.Item is ResultBestuurder selectedResult) _selectedBestuurderId = selectedResult.Id;

        }
        private void RowLostFocus(object sender, RoutedEventArgs e)
        {
            ButtonDetailBestuurder.IsEnabled = false;
            ButtonEditBestuurder.IsEnabled = false;
            ButtonArchiveerBestuurder.IsEnabled = false;
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
           
            new BestuurderToevoegen()
            {
                Owner = this
            }.ShowDialog();

        }

        private void ButtonBestuurderAanpassen_Click(object sender, RoutedEventArgs e)
        {
            if (ResultatenBestuurders.SelectedItem != null)
            {
                var selectedBestuurder = (ResultBestuurder)ResultatenBestuurders.SelectedItem;
                new BestuurderAanpassen(_bestuurderManager.GeefBestuurder(selectedBestuurder.Id))
                {
                    Owner = this
                }.ShowDialog();
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

        private void ButtonArchiveerVoertuig_Click(object sender, RoutedEventArgs e)
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
            VoertuigConcoboxBrandstof.ItemsSource = _brandstoffen.Select(b => b.Type);
            _wagentypes = _wagenTypeManager.GeefAlleWagenTypes().ToList();
            VoertuigComboBoxTypeWagen.ItemsSource = _wagentypes.Select(w => w.Type);
        }
        #endregion

        private void ButtonNieuwVoertuig_OnClick(object sender, RoutedEventArgs e)
        {
            new VoertuigToevoegen(_brandstofTypeManager, _wagenTypeManager)
            {
                Owner = this

            }.ShowDialog();
        }

    }
}
