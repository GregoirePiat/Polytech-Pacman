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
        private int sizeX;
        private int sizeY;

        public GameEngine(byte[,] map,int sizeX,int sizeY)
        {
            this.map = map;
            this.sizeX = sizeX;
            this.sizeY = sizeY;
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

            
            if (map[y, x] == 0)
                return true;
            return false;
        }

        public Vector2 tp(Vector2 position) {
            if (position.X > ((sizeX - 1) * 20)) {
                position.X = 0;
            }
            if (position.Y > ((sizeY-1)*20))
            {
                position.Y = 0;
            }

            if (position.X < 0)
            {
                position.X = (sizeX - 1) * 20;
            }
            if (position.Y < 0)
            {
                position.Y = (sizeY - 1) * 20;
            }
            return position;
        }

        public bool eatBean(Pacman pacman) {
            int x = (int)(pacman.element.Position.X / 20);
            int y = (int)(pacman.element.Position.Y / 20);
            if (y < 0)
                y = 0;
            if (x < 0)
                x = 0;


            if (map[y, x] == 1)
            {
                map[y, x] = 2;
                return true;
            }
            return false;
        }


        public bool eatBooster(Pacman pacman)
        {
            int x = (int)(pacman.element.Position.X / 20);
            int y = (int)(pacman.element.Position.Y / 20);
            if (y < 0)
                y = 0;
            if (x < 0)
                x = 0;
            if (map[y, x] == 3)
            {
                map[y, x] = 2;
                pacman.startInvincible();
                return true;
            }
            return false;
        }

        public Boolean ghostCollision(Pacman pacman, Ghost ghost) {

            if ((pacman.element.Position.X < ghost.element.Position.X +20 &&
                pacman.element.Position.X > ghost.element.Position.X - 20)
                && (pacman.element.Position.Y < ghost.element.Position.Y + 20 &&
                pacman.element.Position.Y > ghost.element.Position.Y - 20))
                return true;
            return false;
        }
    }
}
