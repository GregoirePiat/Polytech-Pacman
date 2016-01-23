using System;

namespace Pacman
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (PacmanGame game = new PacmanGame())
            {
                game.Run();
            }
        }
    }
#endif
}

