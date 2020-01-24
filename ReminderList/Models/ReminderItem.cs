using ReminderList.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReminderList.Models
{
    class ReminderItem : ChangeNotifier
    {
        string _item;
        string _notes;
        bool _isToggledOn;
        DateTime _lastToggleDate;

        public ReminderItem()
        {
            Item = "";
            Notes = "";
            IsToggledOn = false;
            LastToggleDate = DateTime.Now;
        }

        public string Item
        {
            get => _item;
            set { _item = value; NotifyPropertyChanged(); }
        }

        public string Notes
        {
            get => _notes;
            set { _notes = value; NotifyPropertyChanged(); }
        }

        public bool IsToggledOn
        {
            get => _isToggledOn;
            set { _isToggledOn = value; NotifyPropertyChanged(); }
        }

        public DateTime LastToggleDate
        {
            get => _lastToggleDate;
            set { _lastToggleDate = value; NotifyPropertyChanged(); }
        }
    }
}
