using DBC.Contracts.DataContracts;
using DBC.Proxies;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DBC.WpfClient
{
    public class MainWindowViewModel : NotifyPropertyChanged
    {
        private ObservableCollection<MunicipalityVw> _municipalities;
        public ObservableCollection<MunicipalityVw> Municipalities
        {
            get { return _municipalities; }
            set
            {
                _municipalities = value;
                OnPropertyChanged();
            }
        }


        private MunicipalityVw _selectedMunicipality;
        public MunicipalityVw SelectedMunicipality
        {
            get { return _selectedMunicipality; }
            set
            {
                _selectedMunicipality = value;
                if (value != null)
                {
                    EditedMunicipalityName = value.Name;
                    TaxRuleTitle = $"{value.Name} Tax Rules";

                    var proxy = new MunicipalityTaxClient();
                    var taxRuls = proxy.FindMunicipalityTaxRulesAsync(value.Id).Result;
                    MunicipalityTaxRules =
                        new ObservableCollection<TaxRuleVw>(taxRuls);
                }
                else
                {
                    TaxRuleTitle = "Municipality Tax Rules";
                    MunicipalityTaxRules =
                        new ObservableCollection<TaxRuleVw>();
                }
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsMunicipalitySelected));
            }
        }

        public bool IsMunicipalitySelected
        {
            get { return SelectedMunicipality != null; }
        }


        private string _editedMunicipalityName;
        public string EditedMunicipalityName
        {
            get { return _editedMunicipalityName; }
            set
            {
                _editedMunicipalityName = value;
                OnPropertyChanged();
            }
        }


        private string _taxRuleTitle = "Municipality Tax Rules";
        public string TaxRuleTitle
        {
            get { return _taxRuleTitle; }
            set
            {
                _taxRuleTitle = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<TaxRuleVw> _municipalityTaxRules;
        public ObservableCollection<TaxRuleVw> MunicipalityTaxRules
        {
            get { return _municipalityTaxRules; }
            set
            {
                _municipalityTaxRules = value;
                OnPropertyChanged();
            }
        }


        private TaxRuleVw _selectedTaxRule;
        public TaxRuleVw SelectedTaxRule
        {
            get { return _selectedTaxRule; }
            set
            {
                _selectedTaxRule = value;

                TaxRuleName = value?.Name;
                TaxRulePercentage = value?.Percentage ?? 0;
                TaxRulePriority = value?.Priority ?? 0;

                PeriodTitle = $"{(value?.Name ?? "Tax Rule")} Periods";

                if (value != null)
                {
                    var proxy = new MunicipalityTaxRuleClient();

                    var periods = proxy.GetPeriodsAsync(value.Id).Result;
                    TaxRulePeriods = new ObservableCollection<PeriodVw>(periods);

                    proxy.Close();
                }
                else
                    TaxRulePeriods = new ObservableCollection<PeriodVw>();


                OnPropertyChanged();
                OnPropertyChanged(nameof(IsTaxRuleSelected));
            }
        }


        public bool IsTaxRuleSelected
        {
            get { return SelectedTaxRule != null; }
        }


        private string _taxRuleName;
        public string TaxRuleName
        {
            get { return _taxRuleName; }
            set
            {
                _taxRuleName = value;
                OnPropertyChanged();
            }
        }


        private int _taxRulePriority;
        public int TaxRulePriority
        {
            get { return _taxRulePriority; }
            set
            {
                _taxRulePriority = value;
                OnPropertyChanged();
            }
        }


        private float _taxRulePercentage;
        public float TaxRulePercentage
        {
            get { return _taxRulePercentage; }
            set
            {
                _taxRulePercentage = value;
                OnPropertyChanged();
            }
        }


        public TaxRuleDto TaxRuleDto
        {
            get
            {
                var message = "";
                if (string.IsNullOrWhiteSpace(TaxRuleName))
                    message = $"Tax rule name must not be empty{Environment.NewLine}";
                if (TaxRulePercentage > 100 || TaxRulePercentage < 0)
                    message = $"{message}Percentage must be between 0 and 100{Environment.NewLine}";
                if (TaxRulePriority <= 0)
                    message = $"{message}Priority must be an integer bigger that 0";

                if (!string.IsNullOrWhiteSpace(message))
                {
                    MessageBox.Show(message);
                    return null;
                }

                var taxRule = new TaxRuleDto
                {
                    Name = TaxRuleName,
                    Percentage = TaxRulePercentage,
                    Priority = TaxRulePriority
                };

                return taxRule;
            }
        }


        private bool _isTaxRuleReadOnly = true;
        public bool IsTaxRuleReadOnly
        {
            get { return _isTaxRuleReadOnly; }
            set
            {
                _isTaxRuleReadOnly = value;
                OnPropertyChanged();
            }
        }


        private bool isTaxRuleInEditMode;
        public bool IsTaxRuleInEditMode
        {
            get { return isTaxRuleInEditMode; }
            set
            {
                isTaxRuleInEditMode = value;
                OnPropertyChanged();
            }
        }


        private string _periodTitle = "Tax Rule Periods";
        public string PeriodTitle
        {
            get { return _periodTitle; }
            set
            {
                _periodTitle = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<PeriodVw> _taxRulePeriods;
        public ObservableCollection<PeriodVw> TaxRulePeriods
        {
            get { return _taxRulePeriods; }
            set
            {
                _taxRulePeriods = value;
                OnPropertyChanged();
            }
        }


        private PeriodVw _selectedPeriod;
        public PeriodVw SelectedPeriod
        {
            get { return _selectedPeriod; }
            set
            {
                _selectedPeriod = value;

                PeriodFrom = value?.From;
                PeriodTo = value?.To;

                OnPropertyChanged();
                OnPropertyChanged(nameof(IsPeriodSelected));
            }
        }


        public bool IsPeriodSelected
        {
            get { return SelectedPeriod != null; }
        }


        private DateTime? _periodFrom;
        public DateTime? PeriodFrom
        {
            get { return _periodFrom; }
            set
            {
                _periodFrom = value;
                OnPropertyChanged();
            }
        }


        private DateTime? _periodTo;
        public DateTime? PeriodTo
        {
            get { return _periodTo; }
            set
            {
                _periodTo = value;
                OnPropertyChanged();
            }
        }


        public PeriodDto PeriodDto
        {
            get
            {
                if (PeriodFrom == null)
                {
                    MessageBox.Show($"The starting date can not be null");
                    return null;
                }

                // Remove the time part
                PeriodFrom = PeriodFrom.Value.Date;

                if (PeriodTo == null)
                    PeriodTo = PeriodFrom;

                if (PeriodFrom > PeriodTo)
                {
                    MessageBox.Show($"Starting date must be before end date");
                    return null;
                }

                return new PeriodDto { From = PeriodFrom.Value.Date, To = PeriodTo.Value.Date };
            }
        }



        private bool _isPeriodReadOnly = true;
        public bool IsPeriodReadOnly
        {
            get { return _isPeriodReadOnly; }
            set
            {
                _isPeriodReadOnly = value;
                OnPropertyChanged();
            }
        }

        private bool isPeriodInEditMode;
        public bool IsPeriodInEditMode
        {
            get { return isPeriodInEditMode; }
            set
            {
                isPeriodInEditMode = value;
                OnPropertyChanged();
            }
        }


        private DateTime _selectedDate = DateTime.Now;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged();
            }
        }


        private string _selectedPercentage;
        public string SelectedPercentage
        {
            get { return _selectedPercentage; }
            set
            {
                _selectedPercentage = value;
                OnPropertyChanged();
            }
        }


    }
}
