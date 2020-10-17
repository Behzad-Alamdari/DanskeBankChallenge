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

            var municipalities = await proxy.GetMunicipalitiesAsync();
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
                proxy.Close();
                return;
            }

            var municipality = await proxy.AddMunicipalityAsync(newName);
            if (_vm.Municipalities == null)
                _vm.Municipalities = new ObservableCollection<MunicipalityVw>();

            _vm.Municipalities.Add(municipality);

            txtNewMunicipalityName.Text = null;

            proxy.Close();
        }

        private async void btnMunicipalityEdit_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (_vm.SelectedMunicipality == null)
            {
                MessageBox.Show("No municipality is selected");
                return;
            }


            var selectedMunicipality = _vm.SelectedMunicipality;
            var newName = _vm.EditedMunicipalityName;

            var proxy = new MunicipalityTaxClient();
            if (await proxy.DoesMunicipalityExistAsync(newName))
            {
                MessageBox.Show("A municipality with the same name already exist!");
                proxy.Close();
                return;
            }


            var municipality = await proxy.EditMunicipalityAsync(selectedMunicipality.Id, newName);

            _vm.Municipalities.Remove(selectedMunicipality);
            _vm.Municipalities.Add(municipality);

            _vm.EditedMunicipalityName = string.Empty;

            proxy.Close();
        }

        private void btnAddTaxRule_Click(object sender, RoutedEventArgs e)
        {
            _vm.IsTaxRuleReadOnly = false;
            _vm.IsTaxRuleInEditMode = false;
        }

        private void btnEditTaxRule_Click(object sender, RoutedEventArgs e)
        {
            _vm.IsTaxRuleReadOnly = false;
            _vm.IsTaxRuleInEditMode = true;
        }

        private void btnCancelTaxRule_Click(object sender, RoutedEventArgs e)
        {
            _vm.IsTaxRuleReadOnly = true;

            _vm.TaxRuleName = _vm.SelectedTaxRule?.Name;
            _vm.TaxRulePercentage = _vm.SelectedTaxRule?.Percentage ?? 0;
            _vm.TaxRulePriority = _vm.SelectedTaxRule?.Priority ?? 0;
        }

        private async void btnSaveTaxRule_ClickAsync(object sender, RoutedEventArgs e)
        {
            var taxRule = _vm.TaxRuleDto;

            if (taxRule == null)
                return;

            if (_vm.IsTaxRuleInEditMode && _vm.SelectedTaxRule == null)
            {
                MessageBox.Show("A tax rule should be selected");
                return;
            }

            if (_vm.MunicipalityTaxRules == null)
                _vm.MunicipalityTaxRules = new ObservableCollection<TaxRuleVw>();

            var proxy = new MunicipalityTaxRuleClient();

            TaxRuleVw taxRuleVw;
            if (_vm.IsTaxRuleInEditMode)
            {
                taxRuleVw = await proxy.EditTaxRuleAsync(_vm.SelectedTaxRule.Id, taxRule);
                _vm.MunicipalityTaxRules.Remove(_vm.SelectedTaxRule);
                _vm.MunicipalityTaxRules.Add(taxRuleVw);
            }
            else
            {
                taxRuleVw = await proxy.AddTaxRuleAsync(_vm.SelectedMunicipality.Id, taxRule);
                _vm.MunicipalityTaxRules.Add(taxRuleVw);
            }

            _vm.SelectedTaxRule = taxRuleVw;
            _vm.IsTaxRuleReadOnly = true;

            proxy.Close();
        }

        private void btnAddPeriod_Click(object sender, RoutedEventArgs e)
        {
            _vm.IsPeriodReadOnly = false;
            _vm.IsPeriodInEditMode = false;
        }

        private void btnEditPeriod_Click(object sender, RoutedEventArgs e)
        {
            _vm.IsPeriodReadOnly = false;
            _vm.IsPeriodInEditMode = true;
        }

        private void btnCancelPeriod_Click(object sender, RoutedEventArgs e)
        {
            _vm.IsPeriodReadOnly = true;

            _vm.PeriodFrom = _vm.SelectedPeriod?.From;
            _vm.PeriodTo = _vm.SelectedPeriod?.To;
        }

        private async void btnSavePeriod_ClickAsync(object sender, RoutedEventArgs e)
        {
            var periodDto = _vm.PeriodDto;

            if (periodDto == null)
                return;

            if (_vm.IsPeriodInEditMode && _vm.SelectedPeriod == null)
            {
                MessageBox.Show("A period should be selected");
                return;
            }

            if (_vm.TaxRulePeriods == null)
                _vm.TaxRulePeriods = new ObservableCollection<PeriodVw>();

            var proxy = new TaxRulePeriodClient();

            PeriodVw period;
            if (_vm.IsPeriodInEditMode)
            {
                period = await proxy.EditTaxRulePeriodAsync(_vm.SelectedPeriod.Id, periodDto);
                _vm.TaxRulePeriods.Remove(_vm.SelectedPeriod);
                _vm.TaxRulePeriods.Add(period);
            }
            else
            {
                period = await proxy.AddTaxRulePeriodAsync(_vm.SelectedTaxRule.Id, periodDto);
                _vm.TaxRulePeriods.Add(period);
            }

            _vm.SelectedPeriod = period;
            _vm.IsPeriodReadOnly = true;

            proxy.Close();
        }

        private async void btnDeletePeriod_ClickAsync(object sender, RoutedEventArgs e)
        {
            var proxy = new TaxRulePeriodClient();

            if (_vm.SelectedPeriod == null)
                return;

            await proxy.DeleteTaxRulePeriodAsync(_vm.SelectedPeriod.Id);
            _vm.TaxRulePeriods.Remove(_vm.SelectedPeriod);

            proxy.Close();
        }


        private async void btnFindTax_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (_vm.SelectedMunicipality == null)
                return;

            var proxy = new MunicipalityTaxClient();

            try
            {
                var percentage = await proxy.FindApplicableTaxAsync(_vm.SelectedMunicipality.Id, _vm.SelectedDate);
                _vm.SelectedPercentage = $"{percentage} %";
            }
            catch (Exception ex)
            {
            }


            proxy.Close();
        }
    }
}
