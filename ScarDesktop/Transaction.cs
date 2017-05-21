using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarDesktop
{
    public class Transaction
    {
        public enum SharingFlags { hide, see, edit, paySplit, payFull }

        public string Name { get; set; }
        public DateTime Time { get; set; }
        public float Sum { get; set; }
        public User Owner { get; private set; }
        public Dictionary<SharingFlags, User> Shared { get; set; }



        public Transaction(string Name, DateTime Time, float Sum, Dictionary<SharingFlags, User> Shared, User Owner)
        {
            this.Time = Time;
            this.Sum = Sum;
            this.Name = Name;
            this.Owner = Owner;
            this.Shared = Shared;
        }
    }
}
