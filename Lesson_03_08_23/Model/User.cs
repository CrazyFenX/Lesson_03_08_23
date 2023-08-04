using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_03_08_23.Model
{
    internal class User
    {
        public User(int _id, string _name, string _surName)
        {
            Id = _id;
            Name = _name;
            SurName = _surName;
        }

        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string SurName { get; set; } = "";
    }
}
