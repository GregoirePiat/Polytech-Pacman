using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Pacman
{
    public abstract class Entity : Microsoft.Xna.Framework.DrawableGameComponent
    {
        
        private const int DIRECTION = 1;
        private const float POSITION_Y = 20;
        private const float POSITION_X = 20;
        private const float SIZE = 20;
        private static Vector2 SPEED = new Vector2(2f, 2f);
        protected Vector2 positionInit; 

        SpriteBatch spriteBatch;


        protected Dictionary<int, string> textures;
        protected ObjetAnime entity;
        /*
         * 4 : up
         * 3 : down
         * 2 : left
         * 1 : right
          */
        protected int direction;

        public ObjetAnime element
        {
            get
            {
                return entity;
            }

            set
            {
                entity = value;
            }
        }

        public Entity(Game game, Dictionary<int, string> textures)
            : base(game)
        {
            this.Game.Components.Add(this);
            direction = DIRECTION;
            if(textures != null)
                setTextures(textures);
            
        }

        public void setTextures(Dictionary<int, string> textures) {
            this.textures = textures;        
        }
        public override void Initialize()
        {
            if(positionInit == null)
                positionInit =  new Vector2(20f, 20f);
            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            entity = new ObjetAnime(Game.Content.Load<Texture2D>(textures[1]), positionInit, new Vector2(SIZE, SIZE));
            base.LoadContent();
        }

        

        public override void Update(GameTime gameTime)
        {
            int lastDirection = direction;
            changeDirection();

            Vector2 p = this.move(entity.Position,direction);
            if (!runEngine(p))
            {
                p = this.move(entity.Position, lastDirection);
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
                entity.Position = p;

            return true;
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(entity.Texture, entity.Position, Color.Yellow);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        abstract protected void textureUpdate();
        

        private PacmanGame getPacmanGame() {
            return (PacmanGame)this.Game;
        }

        public void respawn()
        {
            entity.Position = positionInit;
        }

        abstract protected void changeDirection();
    }
}
