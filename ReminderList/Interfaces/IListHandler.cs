using ReminderList.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReminderList.Interfaces
{
    interface IListHandler
    {
        void DeleteFromApp(ListOfItemsViewModel viewModel);
    }
}
