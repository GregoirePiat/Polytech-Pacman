using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    public class Controls
    {
        public const Keys JOUEUR_UP = Keys.Up;
        public const Keys JOUEUR_DOWN = Keys.Down;
        public const Keys JOUEUR_LEFT = Keys.Left;
        public const Keys JOUEUR_RIGHT = Keys.Right;

        // Vérifie si le joueur passé en paramètre a effectué l'action "monter la raquette"
        public static int CheckAction()
        {
            int checkAction = 0;
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(JOUEUR_UP))
            {
                checkAction = 4;
            }
            if (keyboard.IsKeyDown(JOUEUR_DOWN))
            {
                checkAction = 3;
            }
            if (keyboard.IsKeyDown(JOUEUR_LEFT))
            {
                checkAction = 2;
            }
            if (keyboard.IsKeyDown(JOUEUR_RIGHT)) { 
                checkAction = 1;
            }

            return checkAction;
        }

        
    }
}
