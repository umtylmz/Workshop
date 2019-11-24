using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ref1
{
    class Program
    {
        static void Main(string[] args)
        {
            User umit = new User() { ID = 1988, Name = "Ümit", Surname = "Yılmaz" };

            var type = umit.GetType();

            var properties = type.GetProperties();

            var aProperty = type.GetProperty("Name");
            var value = aProperty.GetValue();
            

        }
    }

    class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
