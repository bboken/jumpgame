using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace assg
{
    abstract class Ground : DrawableGameComponent
    {
        public Color[] data;
        public Ground(Game g, int i) : base(g) { createNum = i; }
        public Texture2D texture;
        public SpriteBatch spriteBatch;
        public Vector2 position, velocity, center;
        double frameElapsedTime, frameTimeStep;
        public Color col = Color.White;
        SpriteEffects direction = SpriteEffects.None;
        public int temp, createNum;
        static Random r = new Random();
        
        bool isstart = true;
        abstract protected override void LoadContent();


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(texture, position, null, col, 0f, Vector2.Zero, 1.0f, direction, 0);

            spriteBatch.End();

            base.Draw(gameTime);
        }


        public override void Initialize()
        {
            temp = r.Next(0, 3);
          
            position.X = r.Next(GraphicsDevice.Viewport.Width - 100);

            velocity.Y = (float)r.NextDouble() * 1f + 0.3f;
            frameTimeStep = 1000 / 25f;

            


            if (isstart)
            {
                position.Y = GraphicsDevice.Viewport.Height - 250 - 50 * createNum;
            }
            else
                position.Y = -50;

            base.Initialize();

        }

        public override void Update(GameTime gameTime)
        {
            frameElapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (frameElapsedTime >= frameTimeStep)
            {
                position += velocity;



                frameElapsedTime = 0;

            }

            if (position.Y > GraphicsDevice.Viewport.Height + 20)
            {
                isstart = false;
                Initialize();
            }

            base.Update(gameTime);
        }
    }

}
