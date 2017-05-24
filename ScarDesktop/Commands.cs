using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarDesktop
{
    class Commands
    {
        public void stop(string[] args)
        {
            Console.WriteLine("Stopping");
        }

        public void save(string[] args)
        {
            Crypt.Encrypt("kana", "kana");
            Console.WriteLine("Saved succesfully!");
        }
    }
}
