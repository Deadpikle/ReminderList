using Newtonsoft.Json;
using ReminderList.Helpers;
using ReminderList.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ReminderList.Models
{
    class ReminderItem : ChangeNotifier
    {
        string _item;
        string _notes;
        bool _isToggledOn;
        DateTime _lastToggleDate;
        IReminderItemHandler _handler;

        public ReminderItem()
        {
            Item = "";
            Notes = "";
            IsToggledOn = false;
            LastToggleDate = DateTime.Now;
        }

        [JsonIgnore]
        public IReminderItemHandler ItemHandler
        {
            get => _handler;
            set => _handler = value;
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
            set 
            { 
                _isToggledOn = value;
                NotifyPropertyChanged();
                LastToggleDate = DateTime.Now;
            }
        }

        public DateTime LastToggleDate
        {
            get => _lastToggleDate;
            set 
            { 
                _lastToggleDate = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(LastToggleDateInfo));
            }
        }

        private double GetDaysAgoForElapsedTime()
        {
            TimeSpan elapsed = DateTime.Now.Subtract(LastToggleDate);
            return Math.Round(elapsed.TotalDays, 2);
        }

        public string LastToggleDateInfo
        {
            get
            {
                double daysAgo = GetDaysAgoForElapsedTime();
                return _lastToggleDate.ToString() + string.Format("\n({0} days ago)", daysAgo);
            }
        }

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(LastToggleDateInfo));
            double daysAgo = GetDaysAgoForElapsedTime();
            int daysForReminders = ItemHandler?.GetDaysForReminders() ?? 30;
            if (Math.Floor(daysAgo) >= daysForReminders && IsToggledOn == false)
            {
                _isToggledOn = true;
                NotifyPropertyChanged(nameof(IsToggledOn));
            }
        }

        [JsonIgnore]
        public ICommand Remove => new RelayCommand(RemoveFromList);

        private void RemoveFromList()
        {
            ItemHandler?.RemoveItem(this);
        }
    }
}
