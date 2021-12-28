using DomainLayer.Managers;
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
    /// Interaction logic for VoertuigAanpassen.xaml
    /// </summary>
    public partial class VoertuigAanpassen : Window
    {
        private Voertuig _voertuig;
        private BestuurderManager _bestuurderManager;
        private VoertuigManager _voertuigManager;
        private int _aantalDeuren;
        public VoertuigAanpassen(Voertuig voertuig, VoertuigManager voertuigManager, BestuurderManager bestuurderManager)
        {

            InitializeComponent();
            _voertuig = voertuig;
            _bestuurderManager = bestuurderManager;
            _voertuigManager = voertuigManager;
        }

        private void AnnulerenButton_Click(object sender, RoutedEventArgs e)
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
    }
}
