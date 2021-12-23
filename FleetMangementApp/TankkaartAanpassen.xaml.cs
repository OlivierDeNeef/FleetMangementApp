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
        public Bestuurder GeselecteerdBestuurder { get; set; }
        private List<BrandstofType> _brandstoffen = new();
        public TankkaartAanpassen(Tankkaart tankkaart,BrandstofTypeManager brandstofManager, TankkaartManager tankkaartManager, BestuurderManager bestuurderManager)
        {
            _tankkaart = tankkaart;
            _bestuurderManager = bestuurderManager;
            _brandstofManager = brandstofManager;
            _tankkaartManager = tankkaartManager;
            InitializeComponent();
            SetupTankaartAanpassen();
            //VulTankkaartDataAan(tankkaart);
        }

        private void VulTankkaartDataAan(Tankkaart tankkaart)
        {
            //TODO FIX
            TextBoxTankkaartAanpassenKaarnummer.Text = tankkaart.Kaartnummer;
            TextBoxTankkaartAanpassenPincode.Text = tankkaart.Pincode;
            PickerGeldigheidsDatumTankkaartAanpassen.SelectedDate = tankkaart.Geldigheidsdatum;
           //TODO FIX
            // _brandstoffen = new ObservableCollection<string>(tankkaart.GeefBrandstofTypes().Select(r => r.Type));
            if (tankkaart.Bestuurder != null)
            {
                TankkaartAanpassenBestuurderTextBox.Text =
                    $"{tankkaart.Bestuurder.Id},Naam: {tankkaart.Bestuurder.Voornaam}, Voornaam: {tankkaart.Bestuurder.Voornaam}";
            }
            else
                TankkaartAanpassenBestuurderTextBox.Text = "Geen bestuurder";

            GeselecteerdBestuurder = tankkaart.Bestuurder;



        }

        private void SetupTankaartAanpassen()
        {
            _brandstoffen = _brandstofManager.GeefAlleBrandstofTypes().ToList(); // ADO methode returned list van Brandstoftype != 
            BrandstofTankkaartAanpassenComboBox.ItemsSource = _brandstoffen.Select(b => b.Type);
        }

        //Brandstoffen aanpassen

        private void ToevoegenTankkaartButtonBrandstof_OnClick(object sender, RoutedEventArgs e)
        {
            string r = (string)BrandstofTankkaartAanpassenComboBox.SelectedValue;
            if (!BrandstoffenTankkaartAanpassenListBox.Items.Contains(r))
                BrandstoffenTankkaartAanpassenListBox.Items.Add(r);
        }
        private void VerwijderenTankkaartAanpassen_OnClick(object sender, RoutedEventArgs e)
        {
            string r = (string)BrandstofTankkaartAanpassenComboBox.SelectedValue;
            if (BrandstoffenTankkaartAanpassenListBox.Items.Contains(r))
                BrandstoffenTankkaartAanpassenListBox.Items.Remove(r);
        }
        private void BrandstoffenTankkaartAanpassenListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BrandstofTankkaartAanpassenComboBox.SelectedItem = BrandstoffenTankkaartAanpassenListBox.SelectedValue;
        }

        //Bestuurder
        private void ButtonTankkaartAanpassenSelecteerBestuurder_OnClick(object sender, RoutedEventArgs e)
        {
            new BestuurderSelecteren()
            {
                Owner = this
            }.ShowDialog();
        }
        

        //Tankkaart Aanpassen / Annulerens
        private void AanpassenToevoegenButtonTankkaartAanpassen_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void AnnulerenButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
