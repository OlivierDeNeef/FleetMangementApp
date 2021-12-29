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
        private readonly BrandstofTypeManager _brandstofManager;
        private readonly WagenTypeManager _wagenTypeManager;
        private readonly BestuurderManager _bestuurderManager;
        private readonly VoertuigManager _voertuigManager;
        private readonly RijbewijsTypeManager _rijbewijsTypeManager;
        private List<BrandstofType> _brandstoffen = new();
        private List<WagenType> _wagentypes = new();
        public Bestuurder GeselecteerdeBestuurder = null;
        private int _aantalDeuren;
        public VoertuigToevoegen(BestuurderManager bestuurderManager,VoertuigManager voertuigManager,BrandstofTypeManager brandstofManager, WagenTypeManager wagenTypeManager, RijbewijsTypeManager rijbewijsTypeManager)
        {
            _brandstofManager = brandstofManager;
            _bestuurderManager = bestuurderManager;
            _voertuigManager = voertuigManager;
            _wagenTypeManager = wagenTypeManager;
            _rijbewijsTypeManager = rijbewijsTypeManager;
            InitializeComponent();
            SetupVoertuigWindowView();

        }


        private void SetupVoertuigWindowView()
        {
            _brandstoffen = _brandstofManager.GeefAlleBrandstofTypes().ToList(); // ADO methode returned list van Brandstoftype != 
            VoertuigToevoegenBrandstofComboBox.ItemsSource = _brandstoffen.Select(b => b.Type);
            _wagentypes = _wagenTypeManager.GeefAlleWagenTypes().ToList();
            ToevoegenVoertuigWagenTypeComboBox.ItemsSource = _wagentypes.Select(w => w.Type);
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

        private void ToevoegenVoertuigSelecteerBestuurderbutton_Click(object sender, RoutedEventArgs e)
        {
            new BestuurderSelecteren(_bestuurderManager, _rijbewijsTypeManager )
            {
                Owner = this
            }.ShowDialog();

        }

        private void VoertuigToevoegenButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string brandstofString = (string)VoertuigToevoegenBrandstofComboBox.SelectedItem;
                string wagentypeString = (string)ToevoegenVoertuigWagenTypeComboBox.SelectedItem;
                BrandstofType brandstof = ((MainWindow)Application.Current.MainWindow)._brandstoffen.Where(b => b.Type == brandstofString).FirstOrDefault();
                WagenType wagen = ((MainWindow)Application.Current.MainWindow)._wagentypes.Where(w => w.Type == wagentypeString).FirstOrDefault();
                Voertuig nieuwVoertuig = new Voertuig(ToevoegenVoertuigMerkTextbox.Text, ToevoegenVoertuigModelTextbox.Text, ToevoegenVoertuigCNummerTextbox.Text, ToevoegenVoertuigNummerplaatTextbox.Text, brandstof, wagen);
                if(GeselecteerdeBestuurder != null)
                {
                    nieuwVoertuig.ZetBestuurder(GeselecteerdeBestuurder);
                }

                _voertuigManager.VoegVoertuigToe(nieuwVoertuig);
                MessageBox.Show("Voertuig toegevoegd");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Voertuig toevoegen mislukt:" + ex.Message);
            }
        }
    }
}
