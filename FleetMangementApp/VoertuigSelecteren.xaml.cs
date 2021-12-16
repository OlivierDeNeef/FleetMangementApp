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
    /// Interaction logic for VoertuigSelecteren.xaml
    /// </summary>
    public partial class VoertuigSelecteren : Window
    {
        public VoertuigSelecteren()
        {
            InitializeComponent();
        }

        private void RowGotFocus(object sender, RoutedEventArgs e)
        {
            SelectieToevoegenButton.IsEnabled = true;

        }
        private void RowLostFocus(object sender, RoutedEventArgs e)
        {
            SelectieToevoegenButton.IsEnabled = false;
            //if (Owner.GetType() == typeof(BestuurderToevoegen))
            //{
            //    var main = Owner as BestuurderToevoegen;
            //    main.SelectedVoertuig = null;
            //}
            
        }
    }
}
