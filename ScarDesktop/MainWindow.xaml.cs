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
        public ObservableCollection<FrameworkElement> TransactionFrame { get;set;}
        private ObservableCollection<Transaction> Transactions = new ObservableCollection<Transaction>();

        public MainWindow()
        {
            InitializeComponent();
            var LoadTask = Task.Factory.StartNew(InitializeComponent);
        }

        private void Load()
        {
            InitializeTransactionFrame();

            Transactions.CollectionChanged += (kana, kana2) =>
            {
                TransactionFrame.Clear();
                foreach (Transaction transaction in Transactions)
                {
                    Label label = new Label();
                    label.Content = transaction.Name + "  " + transaction.Time;
                    TransactionFrame.Add(label);
                    Console.WriteLine("Update Event Invoked");
                }

            };

            Transactions.Add(new Transaction("Kana", Time: new DateTime(2017, 5, 12, 18, 37, 56), Sum: 2400f));
            Console.WriteLine(Transactions[0].Time);

            var ReadConsoleTask = Task.Factory.StartNew(Messaging.readConsole);
            Messaging.StartWebServer();
        }

        private void InitializeTransactionFrame()
        {
            TransactionFrame = new ObservableCollection<FrameworkElement>();
        }
    }
}
