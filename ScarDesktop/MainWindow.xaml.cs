using ScarDesktop.TransactionTabs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Specialized;

namespace ScarDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ObservableCollection<Transaction> TransactionPool = new ObservableCollection<Transaction>();
        public static ObservableCollection<Transaction> ThisUserTransactionPool = new ObservableCollection<Transaction>();
        public static List<User> Users = new List<User>();
        public static User CurrentUser;

        public MainWindow()
        {
            
            Users.Add(new User("kana", "kana"));
            CurrentUser = null;
            bool isLogged = false;
            while (!isLogged)
            {
                Console.Write("ScarDesktop> " + "Username: ");
                string username = Console.ReadLine();
                Console.Write("ScarDesktop> " + "Password: ");
                string password = Console.ReadLine();

                var SelectedUser = (from c in Users
                                    where c.Name == username && c.GetPassword(0xFFFFFFFF) == password
                                    select c);
                CurrentUser = SelectedUser.Any() ? SelectedUser.First() : null; 

                if (CurrentUser == null)
                    Console.WriteLine("Invalid username or password");
                else
                    isLogged = true;
            }

            InitializeComponent();

            Crypt.Encrypt("kana", "Mam rad vlaky");
            Load();
        }

        private void Load()
        {
            var ReadConsoleTask = Task.Factory.StartNew(Messaging.ReadConsole);

            TransactionPool.Add( new Transaction( "Kana", Time: DateTime.Now, Sum: 2400f, Shared: new Dictionary<User, Transaction.SharingFlags> { { Users[0], Transaction.SharingFlags.see } }) );
            Console.WriteLine(TransactionPool[0].Time);
            
            TransactionPool.CollectionChanged += (kana, podouble) => { Transactions_CollectionChanged(); };
            //this is actually invoking the same event, jusst i need to invoke it manually
            Transactions_CollectionChanged();

            TransactionsListBox.ItemsSource = ThisUserTransactionPool;
            TransactionsListBox.SelectionChanged += (kana, podouble) =>
            {
                var permission = (from t in ((Transaction)TransactionsListBox.SelectedItem).Shared
                where t.Key == CurrentUser
                select t.Value);

                switch (permission.Any() ? permission.First() : Transaction.SharingFlags.hide)
                {
                    case Transaction.SharingFlags.hide:
                        DynamicSideGrid.Content = new TransactionHide();
                        break;
                    case Transaction.SharingFlags.see:
                        DynamicSideGrid.Content = new TransactionViewOnly(TransactionsListBox.SelectedItem);
                        break;
                    default:
                        DynamicSideGrid.Content = new TransactionViewOnly(TransactionsListBox.SelectedItem);
                        break;
                }
            };


            UserMenuItem.Header = CurrentUser.Name;

            Messaging.StartWebServer();
        }

        private void Transactions_CollectionChanged()
        {
            ThisUserTransactionPool.Clear();

            Transaction.SharingFlags SharingFlags;
            //this is actually the same as below, just this is easier to understand
            //var nonHidden = from x in Transactions
            //                where x.Shared.TryGetValue(CurrentUser, out SharingFlags) && SharingFlags != Transaction.SharingFlags.hide
            //                select x;
            //ShowingTransactions = new ObservableCollection<Transaction>(nonHidden.OfType<Transaction>());

            //selects all transactions, that can be viewed by the user, by checking if he is in the Shared dictionary and he has enough permissions
            ThisUserTransactionPool = new ObservableCollection<Transaction>(TransactionPool.Where(t => t.Shared.TryGetValue(CurrentUser, out SharingFlags)
            && SharingFlags != Transaction.SharingFlags.hide).OfType<Transaction>());
        }

        private void MenuTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TransactionsTab.IsSelected) ;
                //do stuff
        }
    }
}
