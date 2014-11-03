using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsyncRobot.Domain
{
    public class InvalidPositionException : Exception
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public InvalidPositionException(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
