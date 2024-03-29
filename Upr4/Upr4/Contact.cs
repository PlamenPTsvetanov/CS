using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upr4
{
    public class Contact
    {
        private static readonly Random random = new Random();

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value ?? GenerateRandomName(); }
        }

        public string Email { get; }

        public Contact(string name, string email)
        {
            Name = name;
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        private string GenerateRandomName()
        {
            string randomName = "user";
            for (int i = 0; i < 5; i++)
            {
                randomName += random.Next(0, 9);
            }
            return randomName;
        }

        public override string ToString() => $"{Name} <{Email}>";
    }
}
