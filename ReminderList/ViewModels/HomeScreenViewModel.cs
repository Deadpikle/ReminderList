using ReminderList.Helpers;
using ReminderList.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ReminderList.ViewModels
{
    class HomeScreenViewModel : BaseViewModel
    {

        public HomeScreenViewModel(IChangeViewModel viewModelChanger) : base(viewModelChanger)
        {
        }

        public ICommand MoveToAnotherScreen
        {
            get { return new RelayCommand(LoadAnotherScreen); }
        }

        private void LoadAnotherScreen()
        {
            PushViewModel(new AnotherScreen(ViewModelChanger));
        }
    }
}
