using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    class Controls
    {
        public const Keys JOUEUR_UP = Keys.UP;
        public const Keys JOUEUR_DOWN = Keys.DOWN;
        public const Keys JOUEUR_LEFT = Keys.LEFT;
        public const Keys JOUEUR_RIGHT = Keys.RIGHT;

        // Vérifie si le joueur passé en paramètre a effectué l'action "monter la raquette"
        public static Boolean CheckActionUp(Joueur joueur)
        {
            Boolean checkActionUp = false;
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(JOUEUR_UP)) {

            }
            if (keyboard.IsKeyDown(JOUEUR_UP))
            {
            }
            if (keyboard.IsKeyDown(JOUEUR_UP))
            {
            }
            if (keyboard.IsKeyDown(JOUEUR_UP))
            {
            }

            return checkActionUp;
        }

        
    }
}
