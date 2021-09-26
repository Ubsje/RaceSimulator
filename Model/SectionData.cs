using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SectionData : IParticipant
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Points { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IEquipment IEquipment { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IParticipant.TeamColors TeamColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IParticipant Left { get; set; }
        public int DistanceLeft { get; set; }
        public IParticipant Right { get; set; }
        public int DistanceRight { get; set; }
    }
}
