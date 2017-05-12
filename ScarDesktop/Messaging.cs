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
            clientSocket.Connect("127.0.0.1", 8888);
            serverStream = clientSocket.GetStream();


            var msgTask = Task.Factory.StartNew(getMessage);
        }

        private static void getMessage()
        {
            while (true)
            {
                serverStream = clientSocket.GetStream();
                int buffSize = 0;
                byte[] inStream = new byte[10025];
                buffSize = clientSocket.ReceiveBufferSize;
                serverStream.Read(inStream, 0, buffSize);
                string returndata = System.Text.Encoding.ASCII.GetString(inStream);

                if (returndata.Length > 1)
                    executeCommand("$" + returndata);
            }
        }

        public static void sendToWebServer(string input)
        {
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(input + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
        }


        public static void executeCommand(string input)
        {
            //$ server commands
            if (input.Equals("$stop"))
            {
                Console.WriteLine("Stopping");
            }

            //& console commands
        }
    }
}
