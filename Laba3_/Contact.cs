using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba3_
{
    public class Contact
    {
        [Key]
        public string Nickname { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public DateTime Birthday { get; set; }


        public override string ToString()
        {
            return "Nickname: " + Nickname + Environment.NewLine + "Name: " + Name + Environment.NewLine + "Surname: " + Surname + Environment.NewLine + "Phone: " + Phone + Environment.NewLine + "E-mail: " + Email + Environment.NewLine + "Birthday: " + Birthday.ToString();

        }
    }
}