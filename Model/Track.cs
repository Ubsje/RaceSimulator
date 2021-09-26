using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Model.Section;

namespace Model
{
    public class Track
    {
        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }
        public Track(string name, SectionTypes[] sections)
        {
            Sections = new LinkedList<Section>();
            this.Name = name;
            for (int i = 0; i < sections.Length; i++)
            {
                Section s = new Section();
                s.SectionType = sections[i];
                this.Sections.AddLast(s);
            }
        }
    }
}
