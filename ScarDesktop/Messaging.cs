using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScarDesktop
{
    public class Messaging
    {
        static System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        static NetworkStream serverStream = default(NetworkStream);


        public static void StartWebServer()
        {
            sendToConsole( "Conected to Chat Server ...");
            clientSocket.Connect("127.0.0.1", 8888);
            serverStream = clientSocket.GetStream();


            Thread msgThread = new Thread(getMessage);
            msgThread.Start();
        }

        public static void getMessage()
        {
            while (true)
            {
                serverStream = clientSocket.GetStream();
                int buffSize = 0;
                byte[] inStream = new byte[10025];
                buffSize = clientSocket.ReceiveBufferSize;
                serverStream.Read(inStream, 0, buffSize);
                string returndata = System.Text.Encoding.ASCII.GetString(inStream);

                if(returndata.Length > 1)
                executeCommand("$" + returndata);
            }
        }

        public static void sendToWebServer(string input)
        {
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(input + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
        }

        



        //Pipe Server

        private static StreamWriter writer;

        public static void StartPipeServer()
        {
            Task.Factory.StartNew(() =>
            {
                var server = new NamedPipeServerStream("ScarPipes");
                server.WaitForConnection();
                StreamReader reader = new StreamReader(server);
                StreamWriter writer = new StreamWriter(server);
                while (true)
                {
                    string input = reader.ReadLine();
                    if (input.Length > 1)
                        executeCommand(input);
                }
            });
        }

        public static void sendToConsole(string input)
        {
            writer.WriteLine(input);
            writer.Flush();
        }





        private static void executeCommand(string input)
        {
            //$ server commands
            if (input.Equals("$started"))
            {
                MainWindow.syncEvent.Set();
            }

            //& console commands
        }
    }
}
