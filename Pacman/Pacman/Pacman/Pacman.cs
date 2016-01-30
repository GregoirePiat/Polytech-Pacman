using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Pacman
{
    public class Pacman : Entity
    {
        private const int REFRESH_RATE = 10;
        private const int TIME_INVICIBLE = 15000;
        private int refreshSwitch;
        private bool isInvincible;

        private Stopwatch watch;
        private Boolean textureSwitch;

        private static Dictionary<int, string> Textures()
        {
            Dictionary<int, string> textures = new Dictionary<int, string>();
            textures.Add(0, "Images\\pacman");
            textures.Add(1, "Images\\pacman_f");
            textures.Add(2, "Images\\pacman_2");
            textures.Add(3, "Images\\pacman_2f");
            textures.Add(4, "Images\\pacman_3");
            textures.Add(5, "Images\\pacman_3f");
            textures.Add(6, "Images\\pacman_4");
            textures.Add(7, "Images\\pacman_4f");
            return textures;
        }

        public Pacman(Game game)
            : base(game, Textures())
        {
            textureSwitch = false;
            isInvincible = false;
            watch = new Stopwatch();
        }

        public override void Initialize()
        {       
            positionInit = new Vector2(20f, 20f);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (watch.ElapsedMilliseconds > TIME_INVICIBLE) {
                watch.Reset();
                IsInvincible = false;
            }

            base.Update(gameTime);
        }

        override protected void changeDirection() {
            int direction;
            if ((direction = Controls.CheckAction()) != 0)
                this.direction = direction;            
        }
        override
        protected void textureUpdate()
        {
            if (refreshSwitch >= REFRESH_RATE)
            {
                textureSwitch = !textureSwitch;
                refreshSwitch = 0;
            }
            if (direction != 0)
            {
                entity.Texture = Game.Content.Load<Texture2D>(textures[(direction - 1) * 2 + (textureSwitch ? 1 : 0)]);
                refreshSwitch++;
            }

        }

        public void startInvincible() {
            watch.Start();
            IsInvincible = true;
        }

        public bool IsInvincible
        {
            get { return isInvincible; }
            set { isInvincible = value; }
        }
    }
}
