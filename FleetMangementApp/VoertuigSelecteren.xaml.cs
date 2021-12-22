using DomainLayer.Managers;
using DomainLayer.Models;
using FleetMangementApp.Mappers;
using FleetMangementApp.Models.Output;
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
    /// Interaction logic for VoertuigSelecteren.xaml
    /// </summary>
    public partial class VoertuigSelecteren : Window
    {
        private readonly VoertuigManager _voertuigManager;

        public VoertuigSelecteren(VoertuigManager manager)
        {
            InitializeComponent();
            _voertuigManager = manager;
            VoertuigComboBoxBrandstof.ItemsSource = ((MainWindow)Application.Current.MainWindow)._brandstoffen.Select(b => b.Type).OrderBy(b => b);
            VoertuigComboBoxTypeWagen.ItemsSource = ((MainWindow)Application.Current.MainWindow)._wagentypes.Select(w => w.Type).OrderBy(w => w);
        }

        private void RowGotFocus(object sender, RoutedEventArgs e)
        {
            SelectieToevoegenButton.IsEnabled = true;

            //var datagridrow = sender as DataGridRow;
            //if (datagridrow?.Item is ResultVoertuig selectedResult) _selectedBestuurderId = selectedResult.Id;

        }
        private void RowLostFocus(object sender, RoutedEventArgs e)
        {
            SelectieToevoegenButton.IsEnabled = false;
            //if (Owner.GetType() == typeof(BestuurderToevoegen))
            //{
            //    var main = Owner as BestuurderToevoegen;
            //    main.SelectedVoertuig = null;
            //}

        }

        private void SelectieToevoegenButton_Click(object sender, RoutedEventArgs e)
        {
            if (ResultatenVoertuigen.SelectedItem != null)
            {
                if (Owner.GetType() == typeof(BestuurderAanpassen))
                {
                    BestuurderAanpassen main = Owner as BestuurderAanpassen;
                    var selectedVoertuig = (ResultVoertuig)ResultatenVoertuigen.SelectedItem;

                    main.GeselecteerdVoertuig = VoertuigUIMapper.FromUI(selectedVoertuig, _voertuigManager);
                    main.VoertuigTextBox.Text = $"Id: {selectedVoertuig.Id}, Wagen: {selectedVoertuig.Merk} met nummerplaat {selectedVoertuig.Nummerplaat}";
                    Close();
                }

                if (Owner.GetType() == typeof(BestuurderToevoegen))
                {
                    BestuurderToevoegen main = Owner as BestuurderToevoegen;
                    var selectedVoertuig = (ResultVoertuig)ResultatenVoertuigen.SelectedItem;
                    main.GeselecteerdVoertuig = VoertuigUIMapper.FromUI(selectedVoertuig, _voertuigManager);
                    main.VoertuigTextBox.Text = $"Id: {selectedVoertuig.Id}, Wagen: {selectedVoertuig.Merk} met nummerplaat {selectedVoertuig.Nummerplaat}";

                }
            }
            
        }

        private void GeenBestuurderButton_Click(object sender, RoutedEventArgs e)
        {
            if (Owner.GetType() == typeof(BestuurderToevoegen))
            {
                var main = Owner as BestuurderToevoegen;
                main.GeselecteerdVoertuig = null;
                main.TankkaartTextBox.Text = "Geen Voertuig";
            }
            else if (Owner.GetType() == typeof(BestuurderAanpassen))
            {
                var main = Owner as BestuurderAanpassen;
                main.GeselecteerdVoertuig = null;
                main.TankkaartTextBox.Text = "Geen Voertuig";
            }

            Close();
        }

        private void ButtonZoekVoertuig_Click(object sender, RoutedEventArgs e)
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

                var wagenType = VoertuigComboBoxTypeWagen.SelectedItem == null ? null : ((MainWindow)Application.Current.MainWindow)._wagentypes.Where(w => w.Type == VoertuigComboBoxTypeWagen.SelectedItem.ToString()).FirstOrDefault();
                var brandstofType = VoertuigComboBoxBrandstof.SelectedItem == null ? null : ((MainWindow)Application.Current.MainWindow)._brandstoffen.Where(b => b.Type == VoertuigComboBoxBrandstof.SelectedItem.ToString()).FirstOrDefault();

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
    }
}
