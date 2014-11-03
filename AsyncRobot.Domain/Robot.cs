using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncRobot.Domain
{
    public class Robot
    {
        private Land Land;
        private LandPosition CurrentPosition;
        private List<LandPosition> Breadcrumb = new List<LandPosition>();
        private int Id;
        private char[] Compass = { 'W', 'N', 'E', 'S' };
        private bool HasReachedExit = false;
        public char Direction { get; set; }

        public event EventHandler<RobotMoveArgs> Moved;
        public event EventHandler<RobotMoveArgs> Reached;

        public Robot(Land land, int id, int positionX, int positionY)
        {
            this.Id = id;
            this.Land = land;
            CurrentPosition = new LandPosition(positionX, positionY);
            Direction = 'N';
        }

        private LandPosition SeeLand(char direction)
        {
            int seeX = CurrentPosition.X;
            int seeY = CurrentPosition.Y;

            if (direction == 'W') --seeX;
            if (direction == 'E') ++seeX;
            if (direction == 'N') --seeY;
            if (direction == 'S') ++seeY;

            return new LandPosition(seeX, seeY, this.Land.GetPositionValue(seeX, seeY));
        }

        public void Move()
        {
            int lastThereBefore = -1;
            char shouldGo = ' ';
            foreach (char direction in Compass)
            {
                LandPosition seeLand = this.SeeLand(direction);

                if (seeLand.Value == LandValue.OUTER_OF_LIMITS)
                {
                    HasReachedExit = true;
                    Reached(this, new RobotMoveArgs(this.Id, this.CurrentPosition.X, this.CurrentPosition.Y));
                }
                else if (seeLand.Value == LandValue.DEFAULT)
                {
                    var wasThereBeforeCount = this.Breadcrumb
                     .Where(horizontal => horizontal.X == seeLand.X)
                     .Where(vertical => vertical.Y == seeLand.Y).ToList().Count();

                    if (lastThereBefore == -1 || wasThereBeforeCount < lastThereBefore)
                    {
                        lastThereBefore = wasThereBeforeCount;
                        shouldGo = direction;
                    }
                }
            }

            char moveTo = shouldGo;

            //Set move to chosen position
            int moveX = CurrentPosition.X;
            int moveY = CurrentPosition.Y;

            if (moveTo == 'W') --moveX;
            if (moveTo == 'E') ++moveX;
            if (moveTo == 'N') --moveY;
            if (moveTo == 'S') ++moveY;

            this.CurrentPosition = new LandPosition(moveX, moveY);
            this.Breadcrumb.Add(CurrentPosition);

            Moved(this, new RobotMoveArgs(this.Id, CurrentPosition.X, CurrentPosition.Y));
        }

        public  void ExploreLandAsync()
        {
            do
            {
                Move();
            } while (!HasReachedExit);
        }
    }
}
