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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 playerPosition;
        Player player;
        Point playerSize;
        Object2D floor;
        Hole hole;
        Vector2 holePos;
        Obstacle obstacle;
        Stone stone;
        Vector2 stonePos;
        Bullet bullet;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {


            playerPosition = new Vector2(4, Window.ClientBounds.Height / 2);

            Texture2D playerTexture = Content.Load<Texture2D>(@"Images\tankbom");
            Texture2D floorTexture = Content.Load<Texture2D>(@"Images\Floorbom");
            Texture2D holeTexture = Content.Load<Texture2D>(@"Images\buracoso");
            Texture2D stoneTexture = Content.Load<Texture2D>(@"Images\pedraboa");
            Texture2D bulletTexture = Content.Load<Texture2D>(@"Images\tiro");

            playerSize = new Point(playerTexture.Width / 6, playerTexture.Height / 6);
            Point holeSize = new Point(holeTexture.Width/6, holeTexture.Height/6);
            Point stoneSize = new Point(stoneTexture.Width/4, stoneTexture.Height/4);
            Point bulletSize = new Point(bulletTexture.Width / 100, bulletTexture.Height / 100);

            spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new Player(playerPosition, playerSize, playerTexture, 3, 6);

            
            //player.SetVelocity(new Vector2(1,0));

            float floorPos = Window.ClientBounds.Height / 2 + playerSize.Y;
            holePos = new Vector2(Window.ClientBounds.Width, floorPos);
            stonePos = new Vector2(Window.ClientBounds.Width, floorPos - stoneSize.Y);


            floor = new Object2D(new Vector2(0, floorPos), new Point(Window.ClientBounds.Width, Window.ClientBounds.Height - (int)floorPos), floorTexture);
            stone = new Stone(stonePos, stoneSize, stoneTexture, 5);
            hole = new Hole(holePos, holeSize, holeTexture, 5);
            bullet = new Bullet(playerPosition, bulletSize, bulletTexture, 10);

            obstacle = stone;
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {

          
            obstacle.Update(gameTime);

            if (obstacle.GetRect().Intersects(player.GetRect()))
            {
                LoadContent();
            }

            if (obstacle.GetRect().Intersects(bullet.GetRect()))
            {
                obstacle.SetPos(-stonePos);
            }

            if (obstacle.OutOfWindow())
            {
                int random = new Random().Next(2);

                if (random == 0)
                {
                    obstacle = hole;
                }
                else
                {
                    obstacle = stone;
                }
                if (obstacle == hole)
                {
                    obstacle.SetPos(holePos);
                }
                if (obstacle == stone)
                {
                    obstacle.SetPos(stonePos);
                }
            }
            bullet.Update(gameTime);

            if (bullet.OutOfWindow())
            {
                bullet.SetPos(playerPosition);
                bullet.gatilho = false;
            }

 
      
            player.Update(gameTime);

            player.SetDistance(Vector2.Distance(obstacle.GetPos(), player.GetPos()));

            player.SetObstacle(obstacle == stone ? ObstacleType.stone : ObstacleType.hole);

            
            //Fabio desculpa, tentei implementar por lua e fiz até os comandos, porém deu algum erro na hora de abrir o arquivo txt
            //e não consegui achar/resolver de jeito nenhum, deixei a lógica para voce ver que a IA funciona, porém sem o lua :(
                if (player.obstacleType == ObstacleType.hole)
                {
                    if (player.distance < 200)
                    {
                        player.Jump();
                    }
                }
                else
                {
                    if(player.distance < 400)
                    {
                        bullet.StartGatilho();
                    }
                }
            

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            player.Draw(spriteBatch);
            floor.Draw(spriteBatch);
            obstacle.Draw(spriteBatch);
            bullet.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
