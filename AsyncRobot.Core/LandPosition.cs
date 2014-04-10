﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncRobot.Core
{
    public class LandPosition
    {
        public int x { get; private set; }
        public int y { get; private set; }
        public char value { get; private set; }

        public LandPosition(int x, int y, char value)
        {
            this.x = x;
            this.y = y;
            this.value = value;
        }

        public void SetValue(char value)
        {
            this.value = value;
        }
    }
}