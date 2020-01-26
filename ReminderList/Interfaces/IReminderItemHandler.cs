using ReminderList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReminderList.Interfaces
{
    interface IReminderItemHandler
    {
        void RemoveItem(ReminderItem item);
        int GetDaysForReminders();
    }
}
