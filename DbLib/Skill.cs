using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLib
{
    public class Skill
    {
        public int SkillId { get; set; }
        public string Name { get; set; }
        public string Degree { get; set; }
        public Skill(int skillId, string name, string degree)
        {
            SkillId = skillId;
            Name = name;
            Degree = degree;
        }
    }
}
