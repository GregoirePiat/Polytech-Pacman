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
            int lastDirection = direction;
            changeDirection();

            Vector2 p = this.move(pacman.Position,direction);
            if (!runEngine(p))
            {
                p = this.move(pacman.Position, lastDirection);
                if (!runEngine(p))
                {
                    direction = 0;
                }
                else {
                    direction = lastDirection;
                }
            }



            this.textureUpdate();     
            base.Update(gameTime);
        }



        public Vector2 move(Vector2 p,int direction) {            
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
            return p;
        }

        public Boolean runEngine(Vector2 p) {
            

            p = getPacmanGame().getGameEngine().tp(p);
            bool collision = getPacmanGame().getGameEngine().wallCollision(p, direction);

            // fin debug
            if (collision)
            {
                return false;
            }
            else
                pacman.Position = p;

            return true;
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

        private void changeDirection() {
            int direction;
            if ((direction = Controls.CheckAction()) != 0)
                this.direction = direction;            
        }


    }
}
