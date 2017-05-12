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
        public static void StartWebServer()
        {


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

        public static void readConsole()
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (input.Length > 1)
                    executeCommand(input);
            }
        }
    }
}
