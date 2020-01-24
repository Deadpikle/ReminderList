using ReminderList.Helpers;
using ReminderList.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ReminderList.ViewModels
{
    class HomeScreenViewModel : BaseViewModel
    {
        private ObservableCollection<ListOfItemsViewModel> _tabs;

        public HomeScreenViewModel(IChangeViewModel viewModelChanger) : base(viewModelChanger)
        {
            Tabs = new ObservableCollection<ListOfItemsViewModel>()
            {
                new ListOfItemsViewModel(this)
            };
        }

        public ObservableCollection<ListOfItemsViewModel> Tabs 
        { 
            get => _tabs;
            private set => _tabs = value;
        }
    }
}
