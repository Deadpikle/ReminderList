using ReminderList.Helpers;
using ReminderList.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReminderList.Models
{
    class ListOfItems : ChangeNotifier, IReminderItemHandler
    {
        string _name;
        string _description;
        int _numberOfDaysForReminders;
        string _leftSideText;
        string _rightSideText;

        ObservableCollection<ReminderItem> _items;

        public ListOfItems()
        {
            Items = new ObservableCollection<ReminderItem>();
            NumberOfDaysForReminders = 30;
            LeftSideText = "Them";
            RightSideText = "Me";
        }

        public void Add(ReminderItem item)
        {
            Items.Add(item);
            item.ItemHandler = this;
        }

        public ObservableCollection<ReminderItem> Items
        {
            get => _items;
            set { _items = value; NotifyPropertyChanged(); }
        }

        public string Name
        {
            get => _name;
            set { _name = value; NotifyPropertyChanged(); }
        }

        public string Description
        {
            get => _description;
            set { _description = value; NotifyPropertyChanged(); }
        }

        public int NumberOfDaysForReminders
        {
            get => _numberOfDaysForReminders;
            set { _numberOfDaysForReminders = value; NotifyPropertyChanged(); }
        }

        public string LeftSideText
        {
            get => _leftSideText;
            set { _leftSideText = value; NotifyPropertyChanged(); }
        }

        public string RightSideText
        {
            get => _rightSideText;
            set { _rightSideText = value; NotifyPropertyChanged(); }
        }

        public void Refresh()
        {
            foreach (ReminderItem item in Items)
            {
                item.ItemHandler = this;
                item.Refresh();
            }
        }

        void IReminderItemHandler.RemoveItem(ReminderItem item)
        {
            Items.Remove(item);
        }

        int IReminderItemHandler.GetDaysForReminders()
        {
            return NumberOfDaysForReminders;
        }
    }
}
