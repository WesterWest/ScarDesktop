using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;

namespace ScarDesktop
{
    public class User
    {
        public string Name { get; private set; }
        private SecureString Password;  // Never encapsulate this variable again!
        private static float Key = 0xFFFFFFFF;

        public User(string Name, string Password)
        {
            this.Name = Name;
            this.Password = SetPassword(Password);
        }

        public string GetPassword(float Key)
        {
            if(User.Key == Key)
            {
                return Password.ToString();
            }
            else
            {
                var RandomInt = new Random();
                return RandomInt.Next().ToString();
            }
        }
        
        public void SetPassword(String Password) {
           if (String.IsNullOrWhiteSpace(Password))
                throw new ArgumentNullException("Password is not in correct format.");

            var SecStr = new SecureString();

            foreach (char C in Password)
                SecStr.AppendChar(C);

            SecStr.MakeReadOnly();
            return SecStr;
        }
    }
}
