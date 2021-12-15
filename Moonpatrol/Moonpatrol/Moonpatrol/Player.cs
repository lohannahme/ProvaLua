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
using KopiLua;
using NLua;

namespace Moonpatrol
{
    class Player : Object2D
    {
        float groundPos;
        float speed;
        Vector2 velocity;
        bool grounded = true;
        float jumpForce;
        Rectangle rectPlayer;
        public float distance;
        public ObstacleType obstacleType;
        Bullet bullet;
        NLua.Lua lua;


        public Player(Vector2 position, Point size, Texture2D texture2d, float speed, float jumpForce)
            : base(position, size, texture2d)
        {
            this.speed = speed;
            this.jumpForce = jumpForce;
            this.groundPos = position.Y;
            rectPlayer = new Rectangle((int)position.X, (int)position.Y, size.X, size.Y);

            this.lua = new NLua.Lua();

            
            this.lua.RegisterFunction("Jump", this, this.GetType().GetMethod("Jump"));
            this.lua.RegisterFunction("Fire", this, this.GetType().GetMethod("Fire"));

            this.lua["dist"] = distance;
            this.lua["obstacle"] = obstacleType.ToString();

        }


        public void Update(GameTime gameTime) 
        {
            this.lua["dist"] = distance;
            this.lua["obstacle"] = obstacleType.ToString();

            try
            {
                ScriptLua("Update");
            }
            catch (Exception e) { Console.WriteLine(e.Message); }

            if (!grounded)
            {
                if(position.Y > groundPos)
                {
                    grounded = true;
                    velocity = new Vector2(velocity.X, 0);
                    position = new Vector2(position.X, groundPos);
                    return;
                }
                velocity += Vector2.UnitY * (float)gameTime.ElapsedGameTime.TotalSeconds * 10;
            }
            position += velocity;
            rectPlayer = new Rectangle((int)position.X, (int)position.Y,size.X, size.Y);
        }

        public void SetBullet(Bullet bullet)
        {
            this.bullet = bullet;
        }

        public void Fire()
        {
            bullet.StartGatilho();
        }

        public void SetVelocity(Vector2 direction)
        {
            direction.Normalize();
            velocity = new Vector2((direction * speed).X, velocity.Y);
            Console.WriteLine(velocity);
        }

        public void Jump()
        {
            if (grounded)
            {
                velocity = new Vector2(velocity.X, -jumpForce);
                grounded = false;
            }
        }

        public Rectangle GetRect()
        {
            return rectPlayer;
        }

        public void SetDistance(float dist)
        {
            distance = dist;
        }


        public void SetObstacle(ObstacleType obst)
        {
            obstacleType = obst;
        }

        public Vector2 GetPos()
        {
            return this.position;
        }

        private void ScriptLua(string function)
        {
            this.lua.DoFile(@"PlayerLua.txt");
            ((LuaFunction)this.lua[function]).Call();
        }
    }

    public enum ObstacleType
    {
        stone,
        hole
    }
}
