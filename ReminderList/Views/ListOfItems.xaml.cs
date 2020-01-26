using MahApps.Metro.Controls.Dialogs;
using ReminderList.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReminderList.Views
{
    /// <summary>
    /// Interaction logic for ListOfItems.xaml
    /// </summary>
    public partial class ListOfItems : UserControl
    {
        public ListOfItems()
        {
            DataContextChanged += ListOfItems_DataContextChanged;
            InitializeComponent();
        }

        private void ListOfItems_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is ListOfItemsViewModel)
            {
                (DataContext as ListOfItemsViewModel).DialogCoordinator = DialogCoordinator.Instance;
            }
            DataContextChanged -= ListOfItems_DataContextChanged;
        }
    }
}
