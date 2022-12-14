using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLib
{
    public class Person
    {
        public int PersonId { get; set; }
        public string Fio { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Profession { get; set; }
        public Person(int person_id, string fio, string gender, int age, string profession) 
        {
            PersonId = person_id;
            Fio = fio;
            Gender = gender;
            Age = age;
            Profession = profession;
        }
    }
}
