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
    /// Interaction logic for BestuurderSelecteren.xaml
    /// </summary>
    public partial class BestuurderSelecteren : Window
    {
        public BestuurderSelecteren()
        {
            InitializeComponent();
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
            throw new NotImplementedException();

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
    }
}
