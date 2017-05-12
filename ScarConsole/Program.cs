using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScarDesktop
{
    public class Program
    {
        static NamedPipeClientStream client = new NamedPipeClientStream("ScarPipes");
        
        static StreamReader reader;
        static StreamWriter writer;

        public static void Main(string[] args)
        {
            client.Connect();
            writer = new StreamWriter(client);

            Thread ScarDesktopReadThread = new Thread(ScarDesktopRead);

            writer.WriteLine("$started");
        }

        private static void ScarDesktopRead()
        {
            reader = new StreamReader(client);

            while (true)
            {
                string input = Console.ReadLine();
                if (String.IsNullOrEmpty(input)) break;
                writer.WriteLine(input);
                writer.Flush();
                Console.WriteLine(reader.ReadLine());
            }
        }
    }
}
