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
        private BrandstofTypeManager _brandstofManager;
        private BestuurderManager _bestuurderManager;

        private List<BrandstofType> _brandstoffen = new();
        public TankkaartToevoegen(BrandstofTypeManager brandstofManager, BestuurderManager bestuurderManager)
        {
            _bestuurderManager = bestuurderManager;
            _brandstofManager = brandstofManager;
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

        private void ToevoegenTankkaartButtonBrandstof_OnClick(object sender, RoutedEventArgs e)
        {
            string r = (string)BrandstofTankkaartComboBox.SelectedValue;
            if (!BrandstoffenListBox.Items.Contains(r))
                BrandstoffenListBox.Items.Add(r);
        }

        private void ToevoegenButton_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ok");
        }

        private void BrandstoffenListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BrandstofTankkaartComboBox.SelectionBoxItem = BrandstoffenListBox.SelectedValue;
        }
    }
}
