using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncRobot.Domain
{
    public class RobotMoveArgs : EventArgs
    {
        public int RobotId { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public RobotMoveArgs(int id, int x, int y)
        {
            this.RobotId = id;
            this.X = x;
            this.Y = y;
        }
    }
}
