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
using System.Windows.Shapes;

namespace FleetMangementApp
{
    /// <summary>
    /// Interaction logic for Details.xaml
    /// </summary>
    public partial class Details : Window
    {
        private Bestuurder _bestuurder;
        private Voertuig _voertuig;
        private Tankkaart _tankkaart;
        public Details()
        {
            InitializeComponent();
        }

        public Details(Bestuurder selectedBestuurder)
        {
            InitializeComponent();
            _bestuurder = selectedBestuurder;
            _voertuig = selectedBestuurder.Voertuig;
           _tankkaart = selectedBestuurder.Tankkaart;
            VulGegevensBestuurderAan();
            if(_voertuig != null)
                VulGegevensVoertuigAan();
            if(_tankkaart != null)
                VulGegevensTankkaartAan();
        }

        public Details(Voertuig selectedVoertuig)
        {
            InitializeComponent();

            _voertuig = selectedVoertuig;
            _bestuurder = selectedVoertuig.Bestuurder;
            if(selectedVoertuig.Bestuurder != null)
               _tankkaart = selectedVoertuig.Bestuurder.Tankkaart;
            VulGegevensVoertuigAan();
            if(_bestuurder != null)
                VulGegevensBestuurderAan();
            if (_tankkaart != null)
                VulGegevensTankkaartAan();
        }

        public Details(Tankkaart selectedTankkaart)
        {
            InitializeComponent();

            _tankkaart = selectedTankkaart;
            if(selectedTankkaart.Bestuurder != null) {
                _bestuurder = selectedTankkaart.Bestuurder;
                if (selectedTankkaart.Bestuurder.Voertuig != null)
                    _voertuig = selectedTankkaart.Bestuurder.Voertuig;
            }
            

            VulGegevensTankkaartAan();
            if(_bestuurder != null)
                VulGegevensBestuurderAan();
            if (_voertuig != null) 
                VulGegevensVoertuigAan();
        }


        private void VulGegevensBestuurderAan()
        {
            BestuurderId.Text += _bestuurder.Id;
            BestuurderNaam.Text += _bestuurder.Naam;
            BestuurderVoornaam.Text += _bestuurder.Voornaam;
            BestuurderGeboortedatum.Text += _bestuurder.Geboortedatum.ToString("d");
            Rijksregisternummer.Text += _bestuurder.Rijksregisternummer;
            Straat.Text += _bestuurder?.Adres?.Straat;
            
            foreach(RijbewijsType type in _bestuurder.GeefRijbewijsTypes())
            {
                Rijbewijzen.Text +="\n" +"-" + type.Type  ;
            }

            BestuurderGearchiveerdCheckBox.IsChecked = _bestuurder.IsGearchiveerd;

        }

        private void VulGegevensVoertuigAan()
        {
            VoertuigId.Text += _voertuig.Id;
            Merk.Text += _voertuig.Merk;
            Model.Text += _voertuig.Model;
            AantalDeuren.Text += _voertuig.AantalDeuren;
            Nummerplaat.Text += _voertuig.Nummerplaat;
            Chassisnummer.Text += _voertuig.Chassisnummer;
            Kleur.Text += _voertuig.Kleur;
            BrandstofVoertuig.Text += _voertuig.BrandstofType.Type;
            VoertuigWagenType.Text += _voertuig.WagenType.Type;
            VoertuigGearchiveerdCheckbox.IsChecked = _voertuig.IsGearchiveerd;
        }

        private void VulGegevensTankkaartAan()
        {
                TankkaartId.Text += _tankkaart.Id;
                Kaartnummer.Text += _tankkaart.Kaartnummer;
                Pincode.Text += _tankkaart.Pincode;
                Vervaldatum.Text += _tankkaart.Geldigheidsdatum.ToString("d");

                foreach(BrandstofType type in _tankkaart.GeefBrandstofTypes())
                {
                    Brandstoftypes.Text +="\n"+ "-" + type.Type  ;
                }

                TankkaartGearchiveerdCheckBox.IsChecked = _tankkaart.IsGearchiveerd;
        }

        private void SluitDetailsButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
