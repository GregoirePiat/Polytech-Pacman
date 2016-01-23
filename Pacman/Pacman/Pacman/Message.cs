using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
    public class Message
    {
        private String text;
        private Vector2 position;
        private Stopwatch watch;
        private int timeMessage;


        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public void restart() {
            watch.Restart();
        }

        public Message(string text, Vector2 position, int time)
        {
            this.Text = text;
            this.Position = position;
            this.timeMessage = time;
            watch = Stopwatch.StartNew();
        }

        public Message(string text, Vector2 position) : this(text, position, 5000)
        {}

        public bool isObsolete() {
            return watch.ElapsedMilliseconds > timeMessage;
        }
    }
}
