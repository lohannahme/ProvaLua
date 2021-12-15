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
    class Bullet : Object2D
    {

        float speed;
        Rectangle rectBullet;
        public bool gatilho = false;

        public Bullet(Vector2 position, Point size, Texture2D texture2d, float speed) : base(position,size,texture2d)
        {
            this.speed = speed;
            rectBullet = new Rectangle((int)position.X, (int)position.Y, size.X, size.Y);
        }

        public void Update(GameTime gameTime)
        {
            if (gatilho)
            {
                position += new Vector2(speed, 0);
            }

            rectBullet = new Rectangle((int)position.X, (int)position.Y, size.X, size.Y);
            
        }

        public void StartGatilho()
        {
            gatilho = true;
        }

        public void SetPos(Vector2 pos)
        {
            this.position = pos;
        }

        public bool OutOfWindow()
        {
            return this.position.X > 400;

        }

        public Rectangle GetRect()
        {
            return rectBullet;
        }
    }
}
