using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncRobot.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace AsyncRobot.Test
{
    public class LandTest
    {
        [Test]
        public void CreateLand()
        {
            var land = new Land(100, 100);
            land.Map.Count().Should().Be(10000);
        }

        [Test]
        public void SearchForLandPoint()
        {
            var land = new Land(100, 100);

            land.GetPositionValue(50, 50).Should().Be(LandValue.DEFAULT);
        }

        [Test]
        public void ChangeLandPointValue()
        {
            var land = new Land(100, 100);
            land.SetPosition(50, 50, LandValue.WALL);

            land.GetPositionValue(50, 50).Should().Be(LandValue.WALL);
        }

        [Test]
        public void ChangeInvalidLandPointShouldThrowException()
        {
            try
            {
                var land = new Land(100, 100);
                land.GetPositionValue(200, 250);
                Assert.Fail("It should throw an error!");
            }
            catch (InvalidPositionException e)
            {
                e.X.Should().Be(200);
                e.Y.Should().Be(250);
            }
        }
    }
}
