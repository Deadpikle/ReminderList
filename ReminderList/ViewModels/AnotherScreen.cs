﻿using ReminderList.Helpers;
using ReminderList.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ReminderList.ViewModels
{
    class AnotherScreen : BaseViewModel
    {
        public AnotherScreen(IChangeViewModel viewModelChanger) : base(viewModelChanger)
        {
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