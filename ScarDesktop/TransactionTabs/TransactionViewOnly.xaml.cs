using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ScarDesktop.TransactionTabs
{
    /// <summary>
    /// Interaction logic for TransactionViewOnly.xaml
    /// </summary>
    public partial class TransactionViewOnly : UserControl
    {
        public TransactionViewOnly(object SelectedTransaction)
        {
            Transaction CurrentTransaction = (Transaction)SelectedTransaction;
            InitializeComponent();
            TransactionNameLabel.Content = CurrentTransaction.Name;
            TransactionDateLabel.Content = CurrentTransaction.Time;

            ObservableCollection<string> Users = new ObservableCollection<string>();


            foreach (var User in CurrentTransaction.Shared)
            {
                Users.Add(string.Format("{0}  {1}", User.Value, User.Key.Name));
            }

            UsersListBox.ItemsSource = Users;
        }
    }
}

