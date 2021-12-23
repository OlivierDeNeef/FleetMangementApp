using DomainLayer.Managers;
using DomainLayer.Models;
using FleetMangementApp.Mappers;
using FleetMangementApp.Models.Output;
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
    /// Interaction logic for TankkaartSelecteren.xaml
    /// </summary>
    public partial class TankkaartSelecteren : Window
    {
        private readonly TankkaartManager _manager;
        public TankkaartSelecteren(TankkaartManager manager)
        {
            InitializeComponent();
            _manager = manager;
            BrandstoftypeTankkaartCombobox.ItemsSource = ((MainWindow)Application.Current.MainWindow)._brandstoffen.Select(b => b.Type).OrderBy(b => b);
        }

        private void RowGotFocus(object sender, RoutedEventArgs e)
        {
            SelectieToevoegenButton.IsEnabled = true;

        }
        private void RowLostFocus(object sender, RoutedEventArgs e)
        {
            SelectieToevoegenButton.IsEnabled = false;

        }


        private void GeenTankkaartButton_Click(object sender, RoutedEventArgs e)
        {
            if (Owner.GetType() == typeof(BestuurderToevoegen))
            {
                var main = Owner as BestuurderToevoegen;
                main.GeselecteerdeTankkaart = null;
                main.TankkaartTextBox.Text = "Geen Tankkaart";
            }
            else if (Owner.GetType() == typeof(BestuurderAanpassen))
            {
                var main = Owner as BestuurderAanpassen;
                main.GeselecteerdeTankkaart = null;
                main.TankkaartTextBox.Text = "Geen Tankkaart";
            }

            Close();
        }

        private void SelectieToevoegenButton_Click(object sender, RoutedEventArgs e)
        {
            if (ResultatenTankkaarten.SelectedItem != null)
            {
                
                if (Owner.GetType() == typeof(BestuurderAanpassen))
                {
                    var main = Owner as BestuurderAanpassen;
                    main.GeselecteerdeTankkaart = (Tankkaart)ResultatenTankkaarten.SelectedItem;
                    main.TankkaartTextBox.Text = $"Tankkaart met kaartnummer: {main.GeselecteerdeTankkaart.Kaartnummer}"; ;
                    Close();

                }
                else if (Owner.GetType() == typeof(BestuurderToevoegen))
                {
                   var main = Owner as BestuurderToevoegen;
                   main.GeselecteerdeTankkaart = (Tankkaart)ResultatenTankkaarten.SelectedItem;
                   main.TankkaartTextBox.Text = $"Tankkaart met kaartnummer: {main.GeselecteerdeTankkaart.Kaartnummer}"; ;
                   Close();
                }
                
            }
        }

        private void AnnulerenButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ZoekenButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var kaartnummer = TankkaartKaartnummer.Text;
                var geldigheidsdatum = DatePickerGeldigheidsdatumTankkaart.SelectedDate ?? DateTime.MinValue;
                var brandstoffenInString = ListBoxBrandstofTypesTankkaart.ItemsSource?.Cast<string>() ?? new List<string>();
                var lijstBrandstoftypes = ((MainWindow)Application.Current.MainWindow)._brandstoffen.Where(r => brandstoffenInString.Contains(r.Type)).ToList();
                var gearchiveerd = CheckBoxGearchiveerdTankkaart.IsChecked.Value;

                ResultatenTankkaarten.ItemsSource = _manager.GeefGefilterdeTankkaarten(kaartnummer, geldigheidsdatum, lijstBrandstoftypes, gearchiveerd);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Fout", MessageBoxButton.OK);
            }
        }

        private void ToevoegenTankkaartButton_OnClick(object sender, RoutedEventArgs e)
        {
            string r = (string)BrandstoftypeTankkaartCombobox.SelectedValue;
            if (!ListBoxBrandstofTypesTankkaart.Items.Contains(r))
                ListBoxBrandstofTypesTankkaart.Items.Add(r);
        }

        private void VerwijderTankkaartButton_OnClick(object sender, RoutedEventArgs e)
        {
            string r = (string)BrandstoftypeTankkaartCombobox.SelectedValue;
            ListBoxBrandstofTypesTankkaart.Items.Remove(r);
        }

        private void ListBoxBrandstofTypesTankkaart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BrandstoftypeTankkaartCombobox.SelectedItem = ListBoxBrandstofTypesTankkaart.SelectedItem;
        }
    }
}
