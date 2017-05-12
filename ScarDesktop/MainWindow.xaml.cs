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
        public ObservableCollection<Transaction> Transactions;

        public MainWindow()
        {
            InitializeComponent();

            string[] consoleArgs = {""};

            var ReadConsoleTask = Task.Factory.StartNew(readConsole);
            //Messaging.StartWebServer();
        }

        private void readConsole()
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (input.Length > 1)
                    Messaging.executeCommand(input);
            }
        }
    }
}
