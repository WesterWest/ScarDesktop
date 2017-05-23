using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarDesktop
{
    public class Transaction
    {
        public enum SharingFlags { hide, see, edit, paySplit, payFull, own }

        public string Name { get; set; }
        public DateTime Time { get; }
        public float Sum { get; }
        public Dictionary<User, SharingFlags> Shared { get; set; }

        /* [note lang="cz"]
         *  Jen by to chtělo zauvažovat nad gettrama a settrama... Něco by nemělo být měněno a něco zas jo.
         * [/note]
         */
         
        public Transaction(string Name, DateTime Time, float Sum, Dictionary<User, SharingFlags> Shared)
        {
            this.Time = Time;
            this.Sum = Sum;
            this.Name = Name;
            this.Shared = Shared;
        }
    }
}
