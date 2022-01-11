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
using DomainLayer.Managers;
using DomainLayer.Models;

namespace FleetMangementApp
{
    /// <summary>
    /// Interaction logic for TankkaartAanpassen.xaml
    /// </summary>
    public partial class TankkaartAanpassen : Window
    {
        private Tankkaart _tankkaart;
        private readonly BrandstofTypeManager _brandstofManager;
        private readonly BestuurderManager _bestuurderManager;
        private readonly TankkaartManager _tankkaartManager;
        private readonly RijbewijsTypeManager _rijbewijsTypeManager;
        public Bestuurder GeselecteerdBestuurder { get; set; }
        private ObservableCollection<string> _brandstoffen = new();
        public TankkaartAanpassen(Tankkaart tankkaart,BrandstofTypeManager brandstofManager, TankkaartManager tankkaartManager, BestuurderManager bestuurderManager, RijbewijsTypeManager rijbewijsTypeManager)
        {
            _tankkaart = tankkaart;
            _bestuurderManager = bestuurderManager;
            _brandstofManager = brandstofManager;
            _tankkaartManager = tankkaartManager;
            _rijbewijsTypeManager = rijbewijsTypeManager;
            InitializeComponent();
            VulTankkaartDataAan(tankkaart);
            SetupTankaartAanpassen();
        }

        private void VulTankkaartDataAan(Tankkaart tankkaart)
        {
            
            TextBoxTankkaartAanpassenKaarnummer.Text = tankkaart.Kaartnummer;
            TextBoxTankkaartAanpassenPincode.Text = tankkaart.Pincode;
            PickerGeldigheidsDatumTankkaartAanpassen.SelectedDate = tankkaart.Geldigheidsdatum;
           
             _brandstoffen = new ObservableCollection<string>(tankkaart.GeefBrandstofTypes().Select(r => r.Type));
            if (tankkaart.Bestuurder != null)
            {
                TankkaartAanpassenBestuurderTextBox.Text =
                    $"{tankkaart.Bestuurder.Id},Naam: {tankkaart.Bestuurder.Naam}, Voornaam: {tankkaart.Bestuurder.Voornaam}";
            }
            else
                TankkaartAanpassenBestuurderTextBox.Text = "Geen bestuurder";

            GeselecteerdBestuurder = tankkaart.Bestuurder;



        }

        private void SetupTankaartAanpassen()
        {
             // ADO methode returned list van Brandstoftype != 
            BrandstofTankkaartAanpassenComboBox.ItemsSource = _brandstofManager.GeefAlleBrandstofTypes().Select(t => t.Type);
            BrandstoffenTankkaartAanpassenListBox.ItemsSource = _brandstoffen;
        }

        //Brandstoffen aanpassen

        private void ToevoegenTankkaartButtonBrandstof_OnClick(object sender, RoutedEventArgs e)
        {
            string r = (string)BrandstofTankkaartAanpassenComboBox.SelectedValue;
            if (!BrandstoffenTankkaartAanpassenListBox.Items.Contains(r))
                _brandstoffen.Add(r);
        }
        private void VerwijderenTankkaartAanpassen_OnClick(object sender, RoutedEventArgs e)
        {
            string r = (string)BrandstofTankkaartAanpassenComboBox.SelectedValue;
            if (BrandstoffenTankkaartAanpassenListBox.Items.Contains(r))
                _brandstoffen.Remove(r);
        }
        private void BrandstoffenTankkaartAanpassenListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BrandstofTankkaartAanpassenComboBox.SelectedItem = BrandstoffenTankkaartAanpassenListBox.SelectedValue;
        }
        //Bestuurder
        private void ButtonTankkaartAanpassenSelecteerBestuurder_OnClick(object sender, RoutedEventArgs e)
        {
            new BestuurderSelecteren(_bestuurderManager, _rijbewijsTypeManager)
            {
                Owner = this
            }.ShowDialog();
        }
        
        //Tankkaart Aanpassen / Annulerens
        
        private void AnnulerenButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //TODO knop fixen
        private void TankkaartAanpassenButton_OnClick(object sender, RoutedEventArgs e)
        {
            
            List<BrandstofType> brandstoffen = new List<BrandstofType>();
            var brandstoffenstring =
                BrandstoffenTankkaartAanpassenListBox.ItemsSource.Cast<string>() ?? new List<string>();
            brandstoffen = ((MainWindow) Application.Current.MainWindow)._brandstoffen
                .Where(b => brandstoffenstring.Contains(b.Type)).ToList();

            Tankkaart aangepasteTankkaart = new Tankkaart(_tankkaart.Id,TextBoxTankkaartAanpassenKaarnummer.Text,
                PickerGeldigheidsDatumTankkaartAanpassen.SelectedDate.Value, TextBoxTankkaartAanpassenPincode.Text, 
                _tankkaart.IsGeblokkeerd, _tankkaart.IsGearchiveerd, brandstoffen);


            if (GeselecteerdBestuurder != null && _tankkaart.Bestuurder != GeselecteerdBestuurder)
            {

                aangepasteTankkaart.ZetBestuurder(GeselecteerdBestuurder);
                if(_tankkaart.Bestuurder != null) _bestuurderManager.UpdateBestuurder(_tankkaart.Bestuurder);
                _bestuurderManager.UpdateBestuurder(GeselecteerdBestuurder);
            } else if(GeselecteerdBestuurder == null)
            {
                var oldBestuurder = _tankkaart.Bestuurder;
                _tankkaart.Bestuurder.VerwijderTankkaart();
                _bestuurderManager.UpdateBestuurder(oldBestuurder);
            }

            _tankkaartManager.UpdateTankkaart(aangepasteTankkaart);
            VulTankkaartDataAan(aangepasteTankkaart);
            
            Close();
        }
    }
}
