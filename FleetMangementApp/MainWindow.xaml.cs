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
using FleetMangementApp.Models.Output;

namespace FleetMangementApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BestuurderManager _bestuurderManager;
        public MainWindow(BestuurderManager bestuurderManager)
        {
            _bestuurderManager = bestuurderManager;
            InitializeComponent();
        }

        private void ZoekBestuurderButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateBestuurdeeFields())
            {
                var x= _bestuurderManager.GeefGefilterdeBestuurder(
                    int.Parse(TextBoxBestuurderId.Text), 
                    null,
                    null, 
                    DateTime.MinValue, 
                    null, 
                    null, 
                    false);

                List<ResultBestuurder> bestuurders = new List<ResultBestuurder>();
                foreach (var bestuurder in x)
                {
                    bestuurders.Add(new ResultBestuurder(){Id = bestuurder.Id,Naam = bestuurder.Naam, Voornaam = bestuurder.Voornaam,Geboortedatum = bestuurder.Geboortedatum.ToShortDateString(),HeeftTankkaart = (bestuurder.Tankkaart!=null),HeeftVoertuig = (bestuurder.Voertuig!=null)});
                }

                ResultatenBestuurders.ItemsSource = bestuurders;
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
