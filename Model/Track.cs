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
            Sections = AddSections(sections);
            this.Name = name;
            
        }
        private LinkedList<Section> AddSections(Section.SectionTypes[] sections)
        {
            LinkedList < Section > sects = new LinkedList<Section>();

            for (int i = 0; i < sections.Length; i++)
            {
                Section s = new Section();
                s.SectionType = sections[i];
                sects.AddLast(s);
            }

            return sects;
        }
    }
}
