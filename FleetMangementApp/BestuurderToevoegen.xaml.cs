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
    /// Interaction logic for BestuurderToevoegen.xaml
    /// </summary>
    public partial class BestuurderToevoegen : Window
    {
        public BestuurderToevoegen()
        {
            InitializeComponent();
        }


        private void AnnulerenButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ToevoegenButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
