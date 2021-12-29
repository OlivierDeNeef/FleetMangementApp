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
using DomainLayer.Managers;
using DomainLayer.Models;
using FleetMangementApp.Mappers;
using FleetMangementApp.Models.Output;

namespace FleetMangementApp
{
    /// <summary>
    /// Interaction logic for BestuurderSelecteren.xaml
    /// </summary>
    public partial class BestuurderSelecteren : Window
    {
        private readonly RijbewijsTypeManager _rijbewijsTypeManager;
        private readonly BestuurderManager _bestuurderManager;
        protected internal List<RijbewijsType> _allRijbewijsTypes = new();

        public BestuurderSelecteren(BestuurderManager bestuurderManager, RijbewijsTypeManager rijbewijsTypeManager)
        {
            InitializeComponent();
            _bestuurderManager = bestuurderManager;
            _rijbewijsTypeManager = rijbewijsTypeManager;
            SetupBestuurderSelecteren();
        }

        private void SetupBestuurderSelecteren()
        {
            _allRijbewijsTypes = _rijbewijsTypeManager.GeefAlleRijsbewijsTypes().ToList();
            ComboBoxRijbewijzen.ItemsSource = _allRijbewijsTypes.Select(r => r.Type).OrderBy(r => r);
        }

        private void RowGotFocus(object sender, RoutedEventArgs e)
        {
            SelectieToevoegenButton.IsEnabled = true;
        }
        private void RowLostFocus(object sender, RoutedEventArgs e)
        {
            SelectieToevoegenButton.IsEnabled = false;
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
        private void ZoekBestuurderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if(!ValidateBestuurderFields()) return;
                var id = string.IsNullOrWhiteSpace(TextBoxBestuurderId.Text) ? 0 : int.Parse(TextBoxBestuurderId.Text);
                var naam = TextBoxNaamBestuurder.Text;
                var voornaam = TextBoxVoornaamBestuurder.Text;
                var geboortedatum = DatePickerGeboortedatumBestuurder.SelectedDate ?? DateTime.MinValue;
                var rijksregisternummer = TextBoxRijksregisternummerBestuurder.Text;

                List<RijbewijsType> lijstRijbewijzen = new List<RijbewijsType>();
                foreach (RijbewijsType rijbewijs in ListBoxRijbewijzen.Items)
                {
                    lijstRijbewijzen.Add(rijbewijs);
                }

                var result = _bestuurderManager.GeefGefilterdeBestuurder(id, voornaam, naam, geboortedatum, lijstRijbewijzen, rijksregisternummer, false);
                ResultatenBestuurders.ItemsSource = result.Select(BestuurderUIMapper.ToUI);


            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Fout ", MessageBoxButton.OK);
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
        private void ListBoxRijbewijzen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxRijbewijzen.SelectedItem = ListBoxRijbewijzen.SelectedValue;
        }
        private void AnnulerenButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void GeenBestuurderButton_Click(object sender, RoutedEventArgs e)
        {
            if (Owner.GetType() == typeof(TankkaartToevoegen))
            {
                var main = Owner as TankkaartToevoegen;
                main.GeselecteerdBestuurder = null;
                main.BestuurderTextBoxTankaartToevoegen.Text = "Geen bestuuder";

            }
            else if (Owner.GetType() == typeof(TankkaartAanpassen))
            {
                var main = Owner as TankkaartAanpassen;
                main.GeselecteerdBestuurder = null;
                main.TankkaartAanpassenBestuurderTextBox.Text = "Geen bestuurder";
            }
            Close();
        }
        private void SelectieToevoegenButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (ResultatenBestuurders.SelectedItem != null)
            {
 
                if (Owner.GetType() == typeof(TankkaartToevoegen))
                {
                    var main = Owner as TankkaartToevoegen;
                   main.GeselecteerdBestuurder = BestuurderUIMapper.FromUI((ResultBestuurder)ResultatenBestuurders.SelectedItem, _bestuurderManager);
                    main.BestuurderTextBoxTankaartToevoegen.Text = $"Bestuurder met naam: {main.GeselecteerdBestuurder.Voornaam} {main.GeselecteerdBestuurder.Naam}";
                }
                else if (Owner.GetType() == typeof(TankkaartAanpassen))
                {
                    var main = Owner as TankkaartAanpassen;
                    main.GeselecteerdBestuurder =
                        BestuurderUIMapper.FromUI((ResultBestuurder) ResultatenBestuurders.SelectedItem,
                            _bestuurderManager);
                    main.TankkaartAanpassenBestuurderTextBox.Text = $"Bestuurder met naam: {main.GeselecteerdBestuurder.Voornaam} {main.GeselecteerdBestuurder.Naam}";
                }
            }
            else
            {
                MessageBox.Show("Geen bestuurder geselecteerd");
            }
            Close();
        }

      
    }
}
