using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncRobot.Domain;
using NUnit.Framework;

namespace AsyncRobot.Test
{
    public class RobotTest
    {
        [Test]
        public void CreateRobot()
        {
            var robot = new Robot(new Land(100, 100), 1, 50, 50);
        }

        [Test]
        public void RobotMove()
        {
            var robot = new Robot(new Land(100, 100), 1, 50, 50);
            robot.Move();
        }

    }
}
