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
        public static ManualResetEvent syncEvent = new ManualResetEvent(false);

        public ObservableCollection<Transaction> Transactions;

        public MainWindow()
        {
            InitializeComponent();

            string[] consoleArgs = {""};
            ScarDesktop.Program console = new ScarDesktop.Program();

            Messaging.StartPipeServer();


            syncEvent.WaitOne();
            Messaging.StartWebServer();
        }



    }
}
