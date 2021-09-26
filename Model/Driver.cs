using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Driver : IParticipant
    {
        string _Name;
        int _Points;
        IEquipment _IEquipment;
        IParticipant.TeamColors _TeamColor;
        public string Name { get => _Name; set => _Name = value; }
        public int Points { get => _Points; set => _Points = value; }
        public IEquipment IEquipment { get => _IEquipment; set => _IEquipment = value; }
        public IParticipant.TeamColors TeamColor { get => _TeamColor; set => _TeamColor = value; }
    }
}
