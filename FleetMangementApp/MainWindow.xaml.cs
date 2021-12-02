using DomainLayer.Managers;
using DomainLayer.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FleetMangementApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BestuurderManager _bestuurders;
        private TankkaartManager _tankkaarten;
        private VoertuigManager _voertuigen;

        private BrandstofTypeManager _brandstofTypes;
        private RijbewijsTypeManager _rijbewijsTypes;
        private WagenTypeManager _wagenTypes;
        public MainWindow()
        {
            InitializeComponent();
            //ComboBoxRijbewijzen.ItemsSource = _rijbewijsTypes.GeefAlleRijsbewijsTypes();
        }

        private void ZoekBestuurderButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            DateTime? date = PickerGeboorteDatum.SelectedDate;
            try {
                id = int.Parse(TextBoxBestuurderId.Text);
            }
            catch
            {
                id = 0; 
            }
            
            List<RijbewijsType> rijbewijzen = (List<RijbewijsType>)TextBoxRijbewijzen.ItemsSource;
            IReadOnlyList<Bestuurder> data = _bestuurders.GeefGefilterdeBestuurder(id, TextBoxVoornaamBestuurder.Text, TextBoxNaamBestuurder.Text, date.Value, rijbewijzen, TextBoxRijksregisternummerBestuurder.Text, CheckBoxGearchiveerd.IsChecked.Value);
            ResultatenBestuurders.ItemsSource = data;
        }

        private void TextBoxBestuurderId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TextBoxBestuurderId.Text))
            {
                if (!int.TryParse(TextBoxBestuurderId.Text, out int result))
                {

                    ZoekBestuurderButton.IsEnabled = false;
                    TextBoxBestuurderId.Background = new SolidColorBrush(Colors.Red);
                    MessageBox.Show("Id van de bestuurder moet een getal zijn");
                }
                else
                {
                    ZoekBestuurderButton.IsEnabled = true;
                    TextBoxBestuurderId.Background = new SolidColorBrush(Colors.White);
                }
            }
            else
            {
                TextBoxBestuurderId.Background = new SolidColorBrush(Colors.White);
            }

        }

        private void BestuurderToevoegenButton_Click(object sender, RoutedEventArgs e)
        {
            BestuurderToevoegen _bestuurderToevoegWindow = new();
            _bestuurderToevoegWindow?.Show();
        }

        private void BestuurderAanpassenButton_Click(object sender, RoutedEventArgs e)
        {
            BestuurderAanpassen _bestuurderAanpasWindow = new();
            _bestuurderAanpasWindow?.Show();
        }
    }
}
