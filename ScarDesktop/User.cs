using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarDesktop
{
    public class User
    {
        public string Name { get; private set; }
        private string Password { get; set; }
        private static float key = 0xFFFFFFFF;

        public User(string Name, string Password)
        {
            this.Name = Name;
            this.Password = Password;
        }

        public string GetPassword(float key)
        {
            if(User.key == key)
            {
                return Password;
            }
            else
            {
                var random = new Random();
                return random.Next().ToString();
            }
        }
    }
}
