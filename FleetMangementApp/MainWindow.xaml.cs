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
        private readonly RijbewijsTypeManager _rijbewijsTypeManager;

        private List<RijbewijsType> _allRijbewijsTypes = new();

        private int _selectedBestuurderId;

        public MainWindow(BestuurderManager bestuurderManager, RijbewijsTypeManager rijbewijsTypeManager)
        {
            _rijbewijsTypeManager = rijbewijsTypeManager;
            _bestuurderManager = bestuurderManager;
            InitializeComponent();
            SetupBestuurderView();
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
                if (!ValidateBestuurdeeFields()) return;
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
        private bool ValidateBestuurdeeFields()
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
            ButtonEditBestuuder.IsEnabled = true;
            ButtonArchiveerBestuurder.IsEnabled = true;

            // Neemt de geselecteerde bestuurderId uit het datagrid 
            var datagridrow = sender as DataGridRow;
            if (datagridrow?.Item is ResultBestuurder selectedResult) _selectedBestuurderId = selectedResult.Id;

        }
        private void RowLostFocus(object sender, RoutedEventArgs e)
        {
            ButtonDetailBestuurder.IsEnabled = false;
            ButtonEditBestuuder.IsEnabled = false;
            ButtonArchiveerBestuurder.IsEnabled = false;
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
            if (ListBoxRijbewijzen.Items.Contains(r))
                ListBoxRijbewijzen.Items.Remove(r);
        }

        private void ButtonNieuweBestuurder_OnClick(object sender, RoutedEventArgs e)
        {
            
            new BestuurderToevoegen()
            {
                Owner = this
            }.ShowDialog();

        }


        #endregion


    }
}
