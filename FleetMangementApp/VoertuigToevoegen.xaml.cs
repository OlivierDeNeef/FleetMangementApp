using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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
using DataAccessLayer.Repos;
using DomainLayer.Managers;
using DomainLayer.Models;

namespace FleetMangementApp
{
    /// <summary>
    /// Interaction logic for VoertuigToevoegen.xaml
    /// </summary>
    public partial class VoertuigToevoegen : Window
    {
        private BrandstofTypeManager _brandstofManager;
        private WagenTypeManager _wagenTypeManager;
        private List<BrandstofType> _brandstoffen = new();
        private List<WagenType> _wagentypes = new();
        private int _aantalDeuren;
        public VoertuigToevoegen(BrandstofTypeManager brandstofManager, WagenTypeManager wagenTypeManager)
        {
            _brandstofManager = brandstofManager;
            _wagenTypeManager = wagenTypeManager;
            InitializeComponent();
            SetupVoertuigWindowView();

        }


        private void SetupVoertuigWindowView()
        {
            _brandstoffen = _brandstofManager.GeefAlleBrandstofTypes().ToList(); // ADO methode returned list van Brandstoftype != 
            VoertuigToevoegenComboBoxBrandstof.ItemsSource = _brandstoffen.Select(b => b.Type);
            _wagentypes = _wagenTypeManager.GeefAlleWagenTypes().ToList();
            ToevoegenVoertuigWagenTypeTextbox.ItemsSource = _wagentypes.Select(w => w.Type);
        }
        private void VoertuigToevoegenButtenAnnuleren_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void VerhoogAantalDeurenButton_OnClick(object sender, RoutedEventArgs e)
        {
            _aantalDeuren += 1;
            ToevoegenVoertuigAantalDeurenTextbox.Text = _aantalDeuren.ToString();
        }

        private void VerlaagAantalDeurenButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_aantalDeuren != 0)
            {
                _aantalDeuren -= 1;
                ToevoegenVoertuigAantalDeurenTextbox.Text = _aantalDeuren.ToString();

            }
        }

        private void ToevoegenVoerthuigSelecteerBestuurderbutton_Click(object sender, RoutedEventArgs e)
        {
            new BestuurderSelecteren()
            {
                Owner = this
            }.ShowDialog();

        }
    }
}
