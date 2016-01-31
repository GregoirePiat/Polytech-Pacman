using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pacman
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class PacmanGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont textFont;
        private ObjetAnime mur;
        private ObjetAnime bean;
        private ObjetAnime booster;
        private Pacman pacman;
        private List<Ghost> ghosts;
        private Joueur joueur;
        private GameEngine engine;
        private const int VY = 31;
        private const int VX = 28;
        private byte[,] map;
        private List<Message> messages;
        private bool test = true;
        private SoundEffect pacmanDeadSound;
        private SoundEffect eatBeanSound;
        private SoundEffect pacmanInvicibleSound;
        public int nbBeanTotal = 0;
        public int nbBeanRemaining = 308;

        // Gestion des scores
        public int scoreEatBean = 100;
        public int scoreEatBooster = 200;
        public int scoreEatGhost = 1000;




        public PacmanGame()
        {

            map = new byte[VY, VX]{
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 2, 2, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 0, 2, 2, 2, 2, 0, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 2, 2, 2, 2, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0},
            {0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0},
            {0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0},
            {0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0},
            {0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            {0, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},

        };
            pacman = new Pacman(this);
            ghosts = new List<Ghost>();
            ghosts.Add(new Ghost(this, "red"));
            System.Threading.Thread.Sleep(50);
            ghosts.Add(new Ghost(this, "blue"));
            System.Threading.Thread.Sleep(50);
            ghosts.Add(new Ghost(this, "pink"));
            System.Threading.Thread.Sleep(50);
            ghosts.Add(new Ghost(this, "green"));

            joueur = new Joueur();
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            engine = new GameEngine(map, VX, VY);
            messages = new List<Message>();
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //spriteBatch.DrawString(textFont, "Score :", new Vector2(580, 20), Color.White);
            base.Initialize();
        }
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            //  changing the back buffer size changes the window size (when in windowed mode)
            graphics.PreferredBackBufferWidth = 960;
            graphics.PreferredBackBufferHeight = 620;
            graphics.ApplyChanges();
            // on charge un objet mur 
            mur = new ObjetAnime(Content.Load<Texture2D>("Images\\mur"), new Vector2(0f, 0f), new Vector2(20f, 20f));
            bean = new ObjetAnime(Content.Load<Texture2D>("Images\\bean"), new Vector2(0f, 0f), new Vector2(20f, 20f));
            booster = new ObjetAnime(Content.Load<Texture2D>("Images\\gros_bean"), new Vector2(0f, 0f), new Vector2(20f, 20f));
            textFont = Content.Load<SpriteFont>("aFont");
            pacmanDeadSound = Content.Load<SoundEffect>("Musiques\\PacmanEaten");
            eatBeanSound = Content.Load<SoundEffect>("Musiques\\PelletEat1");
            pacmanInvicibleSound = Content.Load<SoundEffect>("Musiques\\Invincible");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            BeginDraw();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            ghostCollision();
            eatBean();
            eatBooster();
            if (joueur.Life <= 0)
            {
                Exit();
            }
            if (nbBeanRemaining == 0)
            {
                Exit();
            }

            spriteBatch.End();
            updateGhostsTexture();
            base.Update(gameTime);
        }

        private void eatBean()
        {
            if (engine.eatBean(pacman)) {
                pacmanEatBean();
            }
        }

        private void eatBooster()
        {
            if (engine.eatBooster(pacman))
            {
                pacmanEatBooster();
            }
        }

        private void ghostCollision() {
            bool ghostCollision = false;
            foreach (Ghost ghost in ghosts)
            {
                if (engine.ghostCollision(pacman, ghost))
                {
                    ghostCollision = true;
                    ghost.respawn();
                }
            }
            if (ghostCollision)
            {
                if (pacman.IsInvincible)
                {
                    pacmanEatGhost();
                }
                else {
                    pacman.respawn();
                    joueur.Life-=1;
                    soundEffect(pacmanDeadSound);
                }


            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            messages.Add(new Message("Score : " + joueur.Score.ToString(), new Vector2(580, 100), 10));
            messages.Add(new Message("Vie : " + joueur.Life.ToString(), new Vector2(580, 140), 10));
            drawMap();
            drawMessage();
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void addMessage(Message message)
        {
            messages.Add(message);
        }

        public void drawMessage()
        {
            for (int i = 0; i < messages.Count; i++)
            {
                Message message = messages.ElementAt(i);
                spriteBatch.DrawString(this.textFont, message.Text, message.Position, Color.DarkRed);
                if (message.isObsolete())
                {
                    messages.Remove(message);
                    i--;
                }
            }
        }

        private void drawMap()
        {
            for (int y = 0; y < VY; y++)
            {
                for (int x = 0; x < VX; x++)
                {
                    if (false)
                    {
                        String str = string.Format("pacman : X = {0}   Y = {1} = {2}", x, y, map[y, x]);
                        Console.WriteLine(str);
                    }
                    if (map[y, x] == 0)
                    {
                        int xpos, ypos;
                        xpos = x * 20;
                        ypos = y * 20;
                        Vector2 pos = new Vector2(xpos, ypos);
                        spriteBatch.Draw(mur.Texture, pos, Color.White);
                    }
                    else if (map[y, x] == 1)
                    {
                        int xpos, ypos;
                        xpos = x * 20;
                        ypos = y * 20;
                        Vector2 pos = new Vector2(xpos, ypos);
                        spriteBatch.Draw(bean.Texture, pos, Color.White);
                    }
                    else if (map[y, x] == 3)
                    {
                        int xpos, ypos;
                        xpos = x * 20;
                        ypos = y * 20;
                        Vector2 pos = new Vector2(xpos, ypos);
                        spriteBatch.Draw(booster.Texture, pos, Color.White);
                    }
                }
            }
            test = false;
        }

        public GameEngine getGameEngine()
        {
            return engine;
        }

        public void pacmanEatBooster()
        {
            pacmanEatBean();
            joueur.Score += scoreEatBooster;
            soundEffect(pacmanInvicibleSound);
            messages.Add(new Message("You ate a booster, you are now INVICIBLE !", new Vector2(580,400), 15000));
        }

        public void pacmanEatBean()
        {
            --nbBeanRemaining;
            joueur.Score += scoreEatBean;
            soundEffect(eatBeanSound);            
        }

        public void pacmanEatGhost()
        {
            joueur.Score += scoreEatGhost;
            messages.Add(new Message("You ate a ghost !", new Vector2(580, 500), 3000));
        }

        public void eatedByGhost()
        {
            --joueur.Life;
        }

        public void updateGhostsTexture()
        {

        }

        private void soundEffect(SoundEffect soundEffect)
        {
            SoundEffectInstance soundInstance= soundEffect.CreateInstance();
            soundInstance.Pitch = 0;
            soundInstance.Play();
        }
    }
}