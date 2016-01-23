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

        public Boolean wallCollision(Vector2 position) {
            int x = (int)((position.X + 20 )/ 20);
            int y = (int)((position.Y + 20 )/ 20);

            if (map[x, y] == 0)
                return true;
            return false;
        }
    }
}
