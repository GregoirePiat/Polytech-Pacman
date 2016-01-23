using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class Pacman : Microsoft.Xna.Framework.DrawableGameComponent
    {
        
        private const int DIRECTION = 1;
        private const float POSITION_Y = 20;
        private const float POSITION_X = 20;
        private const float SIZE = 20;
        private static Vector2 SPEED = new Vector2(2f, 2f);
        private const int REFRESH_RATE = 10;

        SpriteBatch spriteBatch;
        private Boolean textureSwitch;
        private int refreshSwitch;

        private ObjetAnime pacman;
        /*
         * 4 : up
         * 3 : down
         * 2 : left
         * 1 : right
          */
        private int direction;
        private Dictionary<int, string> textures;

        public Pacman(Game game, Dictionary<int, string> textures)
            : base(game)
        {
            this.Game.Components.Add(this);
            direction = DIRECTION;
            setTextures(textures);
            textureSwitch = false;
        }

        public void setTextures(Dictionary<int, string> textures) {
            for (int i = 0; i <= 7; i++) {
                if (!textures.ContainsKey(i))
                    throw new Exception("The textures isn't full(need 8 Textures with indice 1 to 8)");
                this.textures = textures;
            }            
        }
        public override void Initialize()
        {

            base.Initialize();
        }

        // On affiche les raquettes
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            pacman = new ObjetAnime(Game.Content.Load<Texture2D>(textures[1]), new Vector2(POSITION_X, POSITION_Y), new Vector2(SIZE, SIZE));
            base.LoadContent();
        }

        

        public override void Update(GameTime gameTime)
        {
            int direction;
            if ((direction = Controls.CheckAction()) != 0)
                this.direction = direction;
            this.move();
            this.textureUpdate();     
            base.Update(gameTime);
        }

        public void move() {
            
            Vector2 p = pacman.Position;
            switch(direction) {
                case 1:
                    p.X += SPEED.X;
                    p.Y -= p.Y % 20;
                    break;
                case 2:
                    p.X -= SPEED.X;
                    p.Y -= p.Y % 20;
                    break;
                case 3:
                    p.Y += SPEED.Y;
                    p.X -= p.X % 20;
                    break;
                case 4:
                    p.Y -= SPEED.Y;
                    p.X -= p.X % 20;
                    break;                                          
                
                default:
                    break;
            }
            bool collision = getPacmanGame().getGameEngine().wallCollision(p, direction);

            // begin debug
            Vector2 position = new Vector2(60f, 20f);
            getPacmanGame().addMessage(new Message((String)string.Format("pacman : X = {0}   Y = {1}", p.X/20, p.Y / 20), position, 10));
            Vector2 position3 = new Vector2(60f, 60f);
            getPacmanGame().addMessage(new Message(string.Format("pacman : Collision = {0}", collision), position3, 10));

            // fin debug
            if (collision)
            {
                direction = 0;
            }
            else
                pacman.Position = p;

        }

        private void refocus() {
            Vector2 p = pacman.Position;
            p.X = p.X  - (p.X % 20);
            p.Y = p.Y  - (p.Y % 20);
            pacman.Position = p;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(pacman.Texture, pacman.Position, Color.Yellow);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void textureUpdate() {
            if (refreshSwitch >= REFRESH_RATE)
            {
                textureSwitch = !textureSwitch;
                refreshSwitch = 0;
            }
            if (direction != 0)
            {
                pacman.Texture = Game.Content.Load<Texture2D>(textures[(direction - 1) * 2 + (textureSwitch ? 1 : 0)]);
                refreshSwitch++;
            }

        }

        private PacmanGame getPacmanGame() {
            return (PacmanGame)this.Game;
        }


    }
}
