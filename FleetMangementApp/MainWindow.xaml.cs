using DomainLayer.Managers;
using DomainLayer.Models;
using FleetMangementApp.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DomainLayer.Utilities;

namespace FleetMangementApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BestuurderManager _bestuurderManager;
        private readonly RijbewijsTypeManager _rijbewijsTypeManager;

        private List<RijbewijsType> _allRijbewijsTypes = new List<RijbewijsType>();

        public MainWindow(BestuurderManager bestuurderManager, RijbewijsTypeManager rijbewijsTypeManager)
        {
            _rijbewijsTypeManager = rijbewijsTypeManager;
            _bestuurderManager = bestuurderManager;
            InitializeComponent();
            SetupBestuurderView();
        }

        private void SetupBestuurderView()
        {
            _allRijbewijsTypes = _rijbewijsTypeManager.GeefAlleRijsbewijsTypes().ToList();
            ComboBoxRijbewijzen.ItemsSource = _allRijbewijsTypes.Select(r => r.Type);
        }
        private void ZoekBestuurderButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateBestuurdeeFields()) return;
            try
            {
                var geboortedatum = DatePickerGeboortedatumBestuurder.SelectedDate ?? DateTime.MinValue;
                var rijbewijzen = ListBoxRijbewijzen.ItemsSource?.Cast<string>() ?? new List<string>();
                var id = string.IsNullOrWhiteSpace(TextBoxBestuurderId.Text) ? 0 : int.Parse(TextBoxBestuurderId.Text);
                var result = _bestuurderManager.GeefGefilterdeBestuurder(
                    id,
                    TextBoxVoornaamBestuurder.Text,
                    TextBoxNaamBestuurder.Text,
                    geboortedatum,
                    _allRijbewijsTypes.Where(r => rijbewijzen.Contains(r.Type)).ToList(),
                    string.IsNullOrWhiteSpace(TextBoxRijksregisternummerBestuurder.Text) ? "" :RijksregisternummerChecker.ParseWithoutDate(TextBoxRijksregisternummerBestuurder.Text) ,
                    CheckBoxGearchiveerBestuurder.IsChecked.Value
                ).ToList();
                var bestuurders = result.Select(BestuurderUIMapper.ToUI);
                ResultatenBestuurders.ItemsSource = bestuurders;
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

            if (!string.IsNullOrWhiteSpace(TextBoxRijksregisternummerBestuurder.Text)&& TextBoxRijksregisternummerBestuurder.Text.Length is > 15 or < 11)
            {
                MessageBox.Show("Rijksregisternummer kan enkel 11-15 karakters bevatten", "Invalid field");
                result = false;
            }

            if (DatePickerGeboortedatumBestuurder.SelectedDate.HasValue && DatePickerGeboortedatumBestuurder.SelectedDate.Value > DateTime.Today)
            {
                MessageBox.Show("Een geboortedatum kan niet in de toekomst liggen", "Invalid field");
                result = false;
            }
            return result;
        }
        private void RowGotFocus(object sender, RoutedEventArgs e)
        {
            ButtonDetailBestuurder.IsEnabled = true;
            ButtonEditBestuuder.IsEnabled = true;
            ButtonArchiveerBestuurder.IsEnabled = true;
        }
        private void RowLostFocus(object sender, RoutedEventArgs e)
        {
            ButtonDetailBestuurder.IsEnabled = false;
            ButtonEditBestuuder.IsEnabled = false;
            ButtonArchiveerBestuurder.IsEnabled = false;
        }


        private void ToevoegenRijbewijsButton_OnClick(object sender, RoutedEventArgs e)
        {
            string r =  (string)ComboBoxRijbewijzen.SelectedValue;
            if(!ListBoxRijbewijzen.Items.Contains(r))
                ListBoxRijbewijzen.Items.Add(r); 

        }

        private void VerwijderRijbewijsButton_OnClick(object sender, RoutedEventArgs e)
        {
            string r = (string)ComboBoxRijbewijzen.SelectedValue;
            if (ListBoxRijbewijzen.Items.Contains(r))
                ListBoxRijbewijzen.Items.Remove(r);
        }
    }
}
