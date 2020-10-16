using DBC.Contracts.DataContracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
