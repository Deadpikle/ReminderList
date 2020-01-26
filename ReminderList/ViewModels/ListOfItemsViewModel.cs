using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;
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

        public ListOfItemsViewModel()
        {
        }

        public ListOfItemsViewModel(IChangeViewModel viewModelChanger) : base(viewModelChanger)
        {
            _itemList = new ListOfItems();
            _itemList.Name = "Untitled List";
        }

        public ListOfItemsViewModel(IChangeViewModel viewModelChanger, string listName) : base(viewModelChanger)
        {
            _itemList = new ListOfItems();
            _itemList.Name = listName;
        }

        public ListOfItemsViewModel(IChangeViewModel viewModelChanger, ListOfItems list) : base(viewModelChanger)
        {
            _itemList = list;
        }

        [JsonIgnore]
        public IListHandler ListHandler { get; set; }

        [JsonIgnore]
        public IDialogCoordinator DialogCoordinator { get; set; }

        public ListOfItems ItemList
        {
            get => _itemList;
            set { _itemList = value; NotifyPropertyChanged(); }
        }

        [JsonIgnore]
        public ICommand AddListItem => new RelayCommand(AddListItemToList);

        [JsonIgnore]
        public ICommand DeleteList => new RelayCommand(DeleteListFromApp);

        private void AddListItemToList()
        {
            ItemList.Add(new ReminderItem()
            {
                Item = "Name Here",
                Notes = "N/A",
                IsToggledOn = false,
                LastToggleDate = DateTime.Now
            });
        }

        public void Refresh()
        {
            ItemList.Refresh();
        }

        private async void DeleteListFromApp()
        {
            var settings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Yes",
                NegativeButtonText = "No"
            };
            var shouldDelete = await DialogCoordinator.ShowMessageAsync(this, "Warning!", "Are you sure you want to delete this list? " +
                "It will be gone forever!",
                MessageDialogStyle.AffirmativeAndNegative, settings);
            if (shouldDelete == MessageDialogResult.Affirmative)
            {
                ListHandler?.DeleteFromApp(this);
            }
        }
    }
}
