using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScarDesktop
{
    public class Messaging
    {
        private static List<List<string>> clientCommands = new List<List<string>>();


        public static void StartWebServer()
        {


        }

        public static void ReadConsole()
        {
            var Commands = new Commands();
            var ServerCommands = new ServerCommands();

            while (true)
            {
                Console.Write(MainWindow.CurrentUser.Name + "> ");
                string[] input = Console.ReadLine().Split(' ');
                if (input.Length > 0)
                    if (input[0] == "$")
                        if (ServerCommands.GetType().GetMethod(input[0]) != null)
                            ServerCommands.GetType().GetMethod(input[0].Substring(1).ToLower()).Invoke(ServerCommands, new[] { input.Skip(1) });
                        else
                            Console.WriteLine("Invalid command!");
                    else
                        if (Commands.GetType().GetMethod(input[0]) != null)
                        Commands.GetType().GetMethod(input[0].ToLower()).Invoke(Commands, new[] { input });
                    else
                        Console.WriteLine("Invalid command!");
            }
        }
    }
}
