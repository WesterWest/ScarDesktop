using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarDesktop
{
    public class Transaction
    {
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public float Sum { get; set; }

        public Transaction(string name, DateTime Time, float Sum)
        {
            this.Time = Time;
            this.Sum = Sum;
            this.Name = Name;
        }
    }
}
