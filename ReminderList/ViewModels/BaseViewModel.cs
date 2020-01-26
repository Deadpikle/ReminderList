using ReminderList.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReminderList.Helpers;
using Newtonsoft.Json;

namespace ReminderList.ViewModels
{
    class BaseViewModel : ChangeNotifier, IChangeViewModel
    {
        IChangeViewModel _viewModelChanger;

        public BaseViewModel()
        {
        }

        public BaseViewModel(IChangeViewModel viewModelChanger)
        {
            ViewModelChanger = viewModelChanger;
        }

        [JsonIgnore]
        public IChangeViewModel ViewModelChanger
        {
            get { return _viewModelChanger; }
            set { _viewModelChanger = value; }
        }

        #region IChangeViewModel

        public void PopViewModel()
        {
            _viewModelChanger?.PopViewModel();
        }

        public void PushViewModel(BaseViewModel model)
        {
            _viewModelChanger?.PushViewModel(model);
        }

        #endregion
    }
}
