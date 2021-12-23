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

namespace FleetMangementApp
{
    /// <summary>
    /// Interaction logic for TankkaartToevoegen.xaml
    /// </summary>
    public partial class TankkaartToevoegen : Window
    {
        private readonly BrandstofTypeManager _brandstofManager;
        private readonly BestuurderManager _bestuurderManager;
        private readonly TankkaartManager _tankkaartManager;
        public Bestuurder GeselecteerdBestuurder { get; set; }
        private List<BrandstofType> _brandstoffen = new();
        public TankkaartToevoegen(BrandstofTypeManager brandstofManager, TankkaartManager tankkaartManager, BestuurderManager bestuurderManager)
        {
            _bestuurderManager = bestuurderManager;
            _brandstofManager = brandstofManager;
            _tankkaartManager = tankkaartManager;
            InitializeComponent();
            SetupTankaart();
        }

        private void SetupTankaart()
        {
            _brandstoffen = _brandstofManager.GeefAlleBrandstofTypes().ToList(); // ADO methode returned list van Brandstoftype != 
            BrandstofTankkaartComboBox.ItemsSource = _brandstoffen.Select(b => b.Type);
        }


        private void AnnulerenButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
            
        }

        private void ToevoegenTankkaartButtonBrandstof_OnClick(object sender, RoutedEventArgs e)
        {
            string r = (string)BrandstofTankkaartComboBox.SelectedValue;
            if (!BrandstoffenListBox.Items.Contains(r))
                BrandstoffenListBox.Items.Add(r);
        }
       
        private void VerwijderTankkaartBrandstofButton_OnClick(object sender, RoutedEventArgs e)
        {
            string r = (string)BrandstofTankkaartComboBox.SelectedValue;
            if (BrandstoffenListBox.Items.Contains(r))
                BrandstoffenListBox.Items.Remove(r);
        }

        private void ButtonSelecteerBestuurder_Click(object sender, RoutedEventArgs e)
        {
            new BestuurderSelecteren()
            {
                Owner = this
            }.ShowDialog();
        }


        private void ToevoegenButton_OnClick(object sender, RoutedEventArgs e)
        {
            List<BrandstofType> brandstoffen = new List<BrandstofType>();
            var brandstoffenString = BrandstoffenListBox.ItemsSource?.Cast<string>() ?? new List<string>();
            var bestuurder = BestuurderTextBox.Text;
            brandstoffen = ((MainWindow) Application.Current.MainWindow)._brandstoffen
                .Where(b => brandstoffenString.Contains(b.Type)).ToList();

            Tankkaart nieuweTankkaart = new Tankkaart(TextBoxTankkaartKaarnummer.Text,
                PickerGeldigheidsDatum.SelectedDate.Value, TextBoxTankkaartPincode.Text, null, false, false, brandstoffen);

               
               //false, false, brandstoffen
               //);

            if (GeselecteerdBestuurder != null)
            {
                nieuweTankkaart.ZetBestuurder(GeselecteerdBestuurder);
            }
            _tankkaartManager.VoegTankkaartToe(nieuweTankkaart);
            MessageBox.Show("Tankkaart Toegevoegd");
            Close();
        }

        private void BrandstoffenListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BrandstofTankkaartComboBox.SelectedItem = BrandstoffenListBox.SelectedValue;
        }
    }
}
