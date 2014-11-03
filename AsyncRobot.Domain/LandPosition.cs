using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncRobot.Domain
{
    public class LandPosition
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public LandValue Value { get; private set; }

        public LandPosition(int x, int y) : this(x, y, LandValue.DEFAULT) { }

        public LandPosition(int x, int y, LandValue value)
        {
            this.X = x;
            this.Y = y;
            this.Value = value;
        }

        public void SetValue(LandValue value)
        {
            this.Value = value;
        }
    }
}
