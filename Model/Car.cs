using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Car : IEquipment
    {
        private int _Quality;

        public int Quality
        {
            get { return _Quality; }
            set { _Quality = value; }
        }

        private int _Performance;

        public int Performance
        {
            get { return _Performance; }
            set { _Performance = value; }
        }

        private int _Speed;

        public int Speed
        {
            get { return _Speed; }
            set { _Speed = value; }
        }

        private bool _IsBroken;

        public bool IsBroken
        {
            get { return _IsBroken; }
            set { _IsBroken = value; }
        }
    }
}
