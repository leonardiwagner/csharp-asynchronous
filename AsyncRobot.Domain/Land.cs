using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncRobot.Domain
{
    public class Land
    {
        public ICollection<LandPosition> Map;

        public Land(int width, int height)
        {
            Map = new List<LandPosition>();
            this.CreateLand(width, height);
            
        }

        public void SetPosition(int x, int y, LandValue value)
        {
            var landPosition = this.GetPosition(x, y);
            landPosition.SetValue(value);
        }

        public LandValue GetPositionValue(int x, int y)
        {
            try
            {
                return this.GetPosition(x, y).Value;
            }
            catch (InvalidPositionException)
            {
                return LandValue.OUTER_OF_LIMITS;
            }
        }

        private LandPosition GetPosition(int x, int y)
        {
            var position = this.Map.FirstOrDefault(pos => pos.X == x && pos.Y == y);
            if (position != null)
            {
                return position;
            }
            else
            {
                throw new InvalidPositionException(x, y);
            }
        }

        private void CreateLand(int width, int height)
        {
            int area = width * height;
            int y = 0;
            int x = 0;

            for (int i = 0; i < area; i++)
            {
                if (x == height)
                {
                    x = 0;
                    y++;
                }

                this.Map.Add(new LandPosition(x, y));

                x++;
            }
        }

    }
}
