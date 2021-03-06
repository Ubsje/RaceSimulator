using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IParticipant
    {
        public enum TeamColors
        {
            Red, Green, Yellow, Grey, Blue
        }

        public string Name { get; set; }
        public int Points { get; set; }
        public IEquipment IEquipment { get; set; }
        public TeamColors TeamColor { get; set; }
        public int RoundsDriven { get; set; }
    }
}
