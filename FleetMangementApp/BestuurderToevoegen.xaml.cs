using DomainLayer.Managers;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for BestuurderToevoegen.xaml
    /// </summary>
    /// 
    

    public partial class BestuurderToevoegen : Window 
    {
        private readonly BestuurderManager _bestuurderManager;
        private readonly VoertuigManager _voertuigManager;
        private readonly TankkaartManager _tankkaartManager;
        public Voertuig GeselecteerdVoertuig { get; set; }
        public Tankkaart GeselecteerdeTankkaart { get; set; }
        private ObservableCollection<string> _rijbewijzen = new();

        public BestuurderToevoegen()
        {
            InitializeComponent();
        }

        public BestuurderToevoegen( BestuurderManager bestuurderManager, VoertuigManager voertuigManager, TankkaartManager tankkaartManager)
        {
            InitializeComponent();

            _bestuurderManager = bestuurderManager;
            _voertuigManager = voertuigManager;
            _tankkaartManager = tankkaartManager;
            RijbewijsComboBox.ItemsSource = ((MainWindow)Application.Current.MainWindow)._allRijbewijsTypes.Select(r => r.Type).OrderBy(r => r);
            RijbewijzenListBox.ItemsSource = _rijbewijzen;
        }


        private void AnnulerenButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ToevoegenButton_Click(object sender, RoutedEventArgs e)
        {
            List<RijbewijsType> rijbewijzen = new List<RijbewijsType>();
            var rijbewijzenInString = RijbewijzenListBox.ItemsSource?.Cast<string>() ?? new List<string>();
            rijbewijzen = ((MainWindow)Application.Current.MainWindow)._allRijbewijsTypes.Where(r => rijbewijzenInString.Contains(r.Type)).ToList();

            Bestuurder nieuweBestuurder = new Bestuurder( TextBoxBestuurderNaam.Text, TextBoxVoornaamBestuurder.Text, PickerGeboorteDatum.SelectedDate.Value, Rijksregisternummer.Text, rijbewijzen, false);

            if (GeselecteerdVoertuig != null)
            {
                nieuweBestuurder.ZetVoertuig(GeselecteerdVoertuig);
            }

            if (GeselecteerdeTankkaart != null)
            {
                nieuweBestuurder.ZetTankkaart(GeselecteerdeTankkaart);
            }

            _bestuurderManager.VoegBestuurderToe(nieuweBestuurder);
            
            Close();
        }

        private void ButtonSelecteerVoertuig_Click(object sender, RoutedEventArgs e)
        {
            new VoertuigSelecteren(_voertuigManager)
            {
                Owner = this
            }.ShowDialog();
        }

        private void ButtonSelecteerTankkaart_Click(object sender, RoutedEventArgs e)
        {
            new TankkaartSelecteren(_tankkaartManager)
            {
                Owner = this
            }.ShowDialog();
        }

        private void ListBoxRijbewijzen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RijbewijsComboBox.SelectedItem = RijbewijzenListBox.SelectedItem;

        }

        private void ToevoegenRijbewijsButton_OnClick(object sender, RoutedEventArgs e)
        {
            string r = (string)RijbewijsComboBox.SelectedValue;
            if (!RijbewijzenListBox.Items.Contains(r))
                _rijbewijzen.Add(r);

        }

        private void VerwijderRijbewijsButton_OnClick(object sender, RoutedEventArgs e)
        {
            string r = (string)RijbewijsComboBox.SelectedValue;
            _rijbewijzen.Remove(r);
        }
    }
}
