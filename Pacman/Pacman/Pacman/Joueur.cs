using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    class Joueur
    {
        private int score;
        private Pacman pacman;

        public Joueur(Pacman pacman) {
            this.pacman = pacman;
            this.score = 0;
        }

        public int Score
        {
            get
            {
                return score;
            }

            set
            {
                score = value;
            }
        }

        public Pacman Pacman
        {
            get
            {
                return pacman;
            }

            set
            {
                pacman = value;
            }
        }

        public void actionRight()
        {
            
        }

        public void actionLeft()
        {

        }
        public void actionUp()
        {

        }
        public void actionDown()
        {

        }
    }
}
