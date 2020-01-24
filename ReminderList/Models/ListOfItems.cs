using ReminderList.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReminderList.Models
{
    class ListOfItems : ChangeNotifier
    {
        string _name;
        string _description;
        string _numberOfDaysForReminders;
        string _leftSideText;
        string _rightSideText;

        ObservableCollection<ReminderItem> _items;

        public ListOfItems()
        {
            Items = new ObservableCollection<ReminderItem>();
        }

        // TODO: getters and setters

        public ObservableCollection<ReminderItem> Items
        {
            get => _items;
            set { _items = value; NotifyPropertyChanged(); }
        }
    }
}
