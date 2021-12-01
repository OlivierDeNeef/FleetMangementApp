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
            int id = int.Parse(TextBoxBestuurderId.Text);
            DateTime date = PickerGeboorteDatum.SelectedDate.Value;
            List<RijbewijsType> rijbewijzen = (List<RijbewijsType>)TextBoxRijbewijzen.ItemsSource;
            IReadOnlyList<Bestuurder> data = _bestuurders.GeefGefilterdeBestuurder(id, TextBoxVoornaamBestuurder.Text, TextBoxNaamBestuurder.Text, date, rijbewijzen, TextBoxRijksregisternummerBestuurder.Text, CheckBoxGearchiveerd.IsChecked.Value);
            ResultatenBestuurders.ItemsSource = data;
        }

        private void TextBoxBestuurderId_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            if(!int.TryParse(TextBoxBestuurderId.Text,out int result))
            {
                if(!string.IsNullOrWhiteSpace(TextBoxBestuurderId.Text))
                {
                    ZoekBestuurderButton.IsEnabled = false;
                    MessageBox.Show("Id van de bestuurder moet een getal zijn");
                }
                
            }
            else
            {
                ZoekBestuurderButton.IsEnabled = true;
            }
        }
    }
}
