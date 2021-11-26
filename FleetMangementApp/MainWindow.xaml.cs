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
using DomainLayer.Managers;
using DomainLayer.Models;
using FleetMangementApp.Mappers;
using FleetMangementApp.Models.Output;

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
            _allRijbewijsTypes = _rijbewijsTypeManager.GeefAlleRijsbewijsTypes().ToList();
            InitializeComponent();
            ComboBoxRijbewijzen.ItemsSource = _allRijbewijsTypes.Select(r=>r.Type) ;
           
        }

        private void ZoekBestuurderButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateBestuurdeeFields()) return;
            try
            {
                var geboortedatum = DatePickerGeboortedatumBestuurder.SelectedDate ?? DateTime.MinValue;
                var rijbewijzen = ListBoxRijbewijzen.ItemsSource?.Cast<string>();
                var id = string.IsNullOrWhiteSpace(TextBoxBestuurderId.Text) ? 0 : int.Parse(TextBoxBestuurderId.Text);
                var result= _bestuurderManager.GeefGefilterdeBestuurder(
                    id, 
                    TextBoxVoornaamBestuurder.Text,
                    TextBoxNaamBestuurder.Text, 
                    geboortedatum, 
                    _allRijbewijsTypes.Where( r=>rijbewijzen.Contains(r.Type)).ToList(), 
                    TextBoxRijksregisternummerBestuurder.Text, 
                    CheckBoxGearchiveerBestuurder.IsChecked.Value
                ).ToList();
                var bestuurders = result.Select(BestuurderUIMapper.ToUI);
                ResultatenBestuurders.ItemsSource = bestuurders;
            }
            catch (Exception exception)
            {
                MessageBox.Show( exception.Message,"Fout", MessageBoxButton.OK);
            }

        }

        private bool ValidateBestuurdeeFields()
        {
            return true;
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

      
    }
}
