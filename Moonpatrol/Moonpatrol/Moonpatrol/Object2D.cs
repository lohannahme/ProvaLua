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
    class Object2D
    {
        protected Vector2 position;
        protected Point size;
        protected Texture2D texture2d;

        public Object2D(Vector2 position, Point size, Texture2D texture2d)
        {
            this.position = position;
            this.size = size;
            this.texture2d = texture2d;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture2d, new Rectangle((int)this.position.X, (int)this.position.Y, this.size.X, this.size.Y), Color.White);
        }


    }
}
