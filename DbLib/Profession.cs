using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLib
{
    public class Profession
    {
        public int ProfId { get; set; }
        public string Name { get; set; }
        public int Experience { get; set; }
        public Profession(int profId, string name, int experience)
        {
            ProfId = profId;
            Name = name;
            Experience = experience;
        }
    }
}
