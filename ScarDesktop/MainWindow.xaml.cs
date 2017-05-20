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

namespace ScarDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ObservableCollection<Transaction> Transactions = new ObservableCollection<Transaction>();
        public static List<User> Users = new List<User>();
        public static User CurrentUser;

        public MainWindow()
        {
            Users.Add(new User("kana", "kana"));
            CurrentUser = null;
            bool isLogged = false;
            while (!isLogged)
            {
                Console.Write(CurrentUser.Name + "> " + "Username: ");
                string username = Console.ReadLine();
                Console.Write(CurrentUser.Name + "> " + "Password: ");
                string password = Console.ReadLine();
                for (int i = 0; i < Users.Count; i++)
                {
                    if (username.Equals(Users[i].Name))
                    {
                        if (password.Equals(Users[i].GetPassword(0xFFFFFFFF)))
                        {
                            CurrentUser = Users[i];
                            isLogged = true;
                            break;
                        }
                        else
                            Console.WriteLine("Invalid password");
                    }
                    else
                        Console.WriteLine("Invalid username");
                }
            }

            InitializeComponent();
            Load();
        }

        private void Load()
        {
            var ReadConsoleTask = Task.Factory.StartNew(Messaging.ReadConsole);

            Transactions.Add(new Transaction("Kana", Time: DateTime.Now, Sum: 2400f, Shared: new List<User>(), Owner: Users[0]));
            Console.WriteLine(Transactions[0].Time);

            TransactionsListBox.ItemsSource = Transactions;
            TransactionsListBox.SelectionChanged += (kana, podouble) =>
            {

            };

            Messaging.StartWebServer();
        }
    }
}
