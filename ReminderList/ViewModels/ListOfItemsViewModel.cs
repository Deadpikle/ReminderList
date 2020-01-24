using ReminderList.Helpers;
using ReminderList.Interfaces;
using ReminderList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ReminderList.ViewModels
{
    class ListOfItemsViewModel : BaseViewModel
    {
        private ListOfItems _itemList;

        public ListOfItemsViewModel(IChangeViewModel viewModelChanger) : base(viewModelChanger)
        {
            _itemList = new ListOfItems();
        }

        public ListOfItems ItemList
        {
            get => _itemList;
            set { _itemList = value; NotifyPropertyChanged(); }
        }

        public ICommand GoToMainMenu
        {
            get { return new RelayCommand(PopToMainMenu); }
        }

        private void PopToMainMenu()
        {
            PopViewModel();
        }
    }
}
