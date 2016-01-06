using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    class Controls
    {
        public const Keys JOUEUR_UP = Keys.Up;
        public const Keys JOUEUR_DOWN = Keys.Down;
        public const Keys JOUEUR_LEFT = Keys.Left;
        public const Keys JOUEUR_RIGHT = Keys.Right;

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
