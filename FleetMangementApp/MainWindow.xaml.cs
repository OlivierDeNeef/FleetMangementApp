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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DomainLayer.Managers;

namespace FleetMangementApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BestuurderManager _bestuurderManager;
        public MainWindow(BestuurderManager bestuurderManager)
        {
            _bestuurderManager = bestuurderManager;
            InitializeComponent();
        }

        private void ZoekBestuurderButton_Click(object sender, RoutedEventArgs e)
        {
            var x = _bestuurderManager.GeefGefilterdeBestuurder(1, null, null, DateTime.MinValue, null, null, false);
        }
    }
}
