using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json;
using ReminderList.Helpers;
using ReminderList.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ReminderList.ViewModels
{
    class HomeScreenViewModel : BaseViewModel, IListHandler
    {
        private ObservableCollection<ListOfItemsViewModel> _tabs;
        private object _selectedItem;

        private string _lastSaveFilePath;

        public HomeScreenViewModel(IChangeViewModel viewModelChanger) : base(viewModelChanger)
        {
            bool didLoad = LoadReminderList(Properties.Settings.Default.LastUsedTemplatePath);
            if (!didLoad)
            {
                Tabs = new ObservableCollection<ListOfItemsViewModel>() { };
                AddNewList();
                SelectedItem = Tabs.First();
            }
            else if (Tabs.Count > 0)
            {
                SelectedItem = Tabs.First();
            }
            // refresh info every 5 minutes
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 300000;
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            RefreshData();
        }

        public ObservableCollection<ListOfItemsViewModel> Tabs 
        { 
            get => _tabs;
            private set => _tabs = value;
        }

        public object SelectedItem
        {
            get => _selectedItem;
            set { _selectedItem = value; NotifyPropertyChanged(); }
        }

        [JsonIgnore]
        public IDialogCoordinator DialogCoordinator { get; set; }

        public ICommand AddList => new RelayCommand(AddNewList);
        public ICommand Load => new RelayCommand(LoadItemsFromDisk);
        public ICommand Save => new RelayCommand(SaveData);
        public ICommand Refresh => new RelayCommand(RefreshData);

        private void AddNewList()
        {
            var newTab = new ListOfItemsViewModel(this, "List " + (Tabs.Count + 1))
            {
                ListHandler = this,
                DialogCoordinator = DialogCoordinator
            };
            Tabs.Add(newTab);
            SelectedItem = newTab;
        }

        private void SaveData()
        {
            SaveListToDisk();
        }

        private void RefreshData()
        {
            foreach (ListOfItemsViewModel viewModel in Tabs)
            {
                viewModel.ViewModelChanger = ViewModelChanger; // set so that this is ready when loading from disk
                viewModel.ListHandler = this; // set so that this is ready when loading from disk
                viewModel.DialogCoordinator = DialogCoordinator;
                viewModel.Refresh();
            }
        }

        void IListHandler.DeleteFromApp(ListOfItemsViewModel viewModel)
        {
            Tabs.Remove(viewModel);
        }

        private void SaveListToDisk()
        {
            var saveFileDialog = new Ookii.Dialogs.Wpf.VistaSaveFileDialog();
            saveFileDialog.AddExtension = true;
            saveFileDialog.Filter = "Reminder List Files | *.prlf"; // person reminder list file
            saveFileDialog.DefaultExt = "prlf";
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.Title = "Choose Save Location";
            if (File.Exists(_lastSaveFilePath))
            {
                saveFileDialog.FileName = _lastSaveFilePath;
            }
            if (saveFileDialog.ShowDialog(Application.Current.MainWindow).GetValueOrDefault())
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(Tabs);
                File.WriteAllText(saveFileDialog.FileName, json);
                UpdateLastUsedPath(saveFileDialog.FileName);
            }
        }

        private void UpdateLastUsedPath(string path)
        {
            Properties.Settings.Default.LastUsedTemplatePath = path;
            Properties.Settings.Default.Save();
            _lastSaveFilePath = path;
        }


        private void LoadItemsFromDisk()
        {
            var openFileDialog = new Ookii.Dialogs.Wpf.VistaOpenFileDialog();
            openFileDialog.AddExtension = true;
            openFileDialog.Filter = "Reminder List Files | *.prlf"; // person reminder list file
            openFileDialog.DefaultExt = "prlf";
            openFileDialog.Title = "Choose ReminderList File";
            if (openFileDialog.ShowDialog(Application.Current.MainWindow).GetValueOrDefault())
            {
                LoadReminderList(openFileDialog.FileName);
                UpdateLastUsedPath(openFileDialog.FileName);
            }
        }

        private bool LoadReminderList(string path)
        {
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                var list = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<ListOfItemsViewModel>>(json);
                if (list != null)
                {
                    if (Tabs == null)
                    {
                        Tabs = new ObservableCollection<ListOfItemsViewModel>();
                    }
                    Tabs?.Clear();
                    foreach (ListOfItemsViewModel vm in list)
                    {
                        Tabs.Add(vm);
                    }
                    foreach (var tab in Tabs)
                    {
                        tab.DialogCoordinator = DialogCoordinator;
                    }
                    RefreshData();
                    _lastSaveFilePath = path;
                }
                return true;
            }
            return false;
        }
    }
}
