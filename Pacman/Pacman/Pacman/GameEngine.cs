using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace Pacman
{
    public class GameEngine
    {
        private byte[,] map;

        public GameEngine(byte[,] map)
        {
            this.map = map;
        }

        public Boolean wallCollision(Vector2 position, int direction) {
            int x = (int)(position.X  / 20);
            int y = (int)(position.Y  / 20);
            
            switch (direction)
            {
                case 1:
                    x += (((position.X % 20) > 0) ? 1 : 0);
                    break;
                case 3:
                    y += (((position.Y % 20) > 0) ? 1 : 0);
                    break;
            }
            if (y < 0)
                y = 0;
            if (x < 0)
                x = 0;

            Console.WriteLine(x);
            if (map[y, x] == 0)
                return true;
            return false;
        }

        public int tp()
    }
}
