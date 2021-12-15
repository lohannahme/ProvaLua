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
    public interface Obstacle
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        bool OutOfWindow();
        void SetPos(Vector2 pos);
        Rectangle GetRect();
        Vector2 GetPos();

    }
}
