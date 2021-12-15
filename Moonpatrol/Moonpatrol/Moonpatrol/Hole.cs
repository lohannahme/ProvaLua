using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Moonpatrol
{
    class Hole : Object2D, Obstacle
    {
        float speed;
        Rectangle rectHole;

        public Hole(Vector2 position, Point size, Texture2D texture2d, float speed) : base(position,size,texture2d)
        {
            this.speed = speed;
            rectHole = new Rectangle((int)position.X +5, (int)position.Y-3, size.X - 10, size.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture2d, new Rectangle((int)this.position.X, (int)this.position.Y, this.size.X, this.size.Y), new Rectangle(0,texture2d.Height/2, texture2d.Width, texture2d.Height/2), Color.White);
        }

        public void Update(GameTime gameTime)
        {
            position += new Vector2(-speed,0);
            rectHole = new Rectangle((int)position.X +5, (int)position.Y -3, size.X - 10, size.Y);

        }

        public bool OutOfWindow()
        {
            return this.position.X < -size.X;
        }

        public void SetPos(Vector2 pos)
        {
            this.position = pos;
        }

        public Rectangle GetRect()
        {
            return rectHole;
        }


        public Vector2 GetPos()
        {
            return this.position;
        }
    }
}
