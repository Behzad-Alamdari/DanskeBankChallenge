using DBC.Contracts.DataContracts;
using DBC.Proxies;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace DBC.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _vm;
        public MainWindow()
        {
            InitializeComponent();
            _vm = new MainWindowViewModel();
            mainWindow.DataContext = _vm;
        }

        private async void Window_LoadedAsync(object sender, RoutedEventArgs e)
        {
            var proxy = new MunicipalityTaxClient();

            var municipalities = await proxy.AddMunicipalitiesAsync();
            _vm.Municipalities = new ObservableCollection<MunicipalityVw>(municipalities ?? new List<MunicipalityVw>());

            proxy.Close();
        }

        private async void btnAdd_ClickAsync(object sender, RoutedEventArgs e)
        {
            var newName = txtNewMunicipalityName.Text;
            if (string.IsNullOrWhiteSpace(newName))
                return;

            var proxy = new MunicipalityTaxClient();
            if (await proxy.DoesMunicipalityExistAsync(newName))
            {
                MessageBox.Show("A municipality with the same name already exist!");
                return;
            }

            var municipality = await proxy.AddMunicipalityAsync(newName);
            if(_vm.Municipalities == null)
                _vm.Municipalities = new ObservableCollection<MunicipalityVw>();

            _vm.Municipalities.Add(municipality);

            txtNewMunicipalityName.Text = null;

            proxy.Close();
        }
    }
}
