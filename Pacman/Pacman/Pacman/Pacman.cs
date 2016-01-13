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

        SpriteBatch spriteBatch;
        private Boolean textureSwitch;

        private ObjetAnime pacman;
        /*
         * 1 : up
         * 2 : down
         * 3 : left
         * 4 : right
          */
        private int direction;
        /*
        * 1 : pacman direction up open
        * 2 : pacman direction up close
        * 3 : pacman direction down open
        * 4 : pacman direction down close
        * 5 : pacman direction left open
        * 6 : pacman direction left close
        * 7 : pacman direction right open
        * 8 : pacman direction right close
        */
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
            for (int i = 1; i <= 8; i++) {
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

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(pacman.Texture, pacman.Position, Color.Yellow);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            this.move();
            this.textureUpdate();
            this.direction = 1;
            base.Update(gameTime);
        }

        public void move() {
            Vector2 p = pacman.Position;
            switch (direction) {
                case 1:
                    p.X -= (1/4)*SIZE;
                    break;
                case 2:
                    p.X += (1 / 4) * SIZE;
                    
                    break;
                case 3:
                    p.Y -= (1 / 4) * SIZE;
                    break;
                case 4:
                    p.Y += (1 / 4) * SIZE;
                    break;
                default:
                    break;
            }
        }
        private void textureUpdate() {
            if (pacman.Position.X % 20 > 10 || pacman.Position.X % 20 > 0 || pacman.Position.X % 20 > 10 || pacman.Position.X % 20 > 0) {
                textureSwitch = !textureSwitch;
            }
            pacman.Texture = Game.Content.Load<Texture2D>(textures[(direction)*2-(textureSwitch?1:0)]);
        }

    }
}
