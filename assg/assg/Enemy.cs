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
    abstract class Enemy : DrawableGameComponent
    {
        public Color[] data;
        public Enemy(Game g) : base(g) {  }
        public Texture2D texture;
        public SpriteBatch spriteBatch;
        public Vector2 position, velocity, center;
        double frameElapsedTime, frameTimeStep = 1000/25;
        public Color col = Color.White;
        protected SpriteEffects direction = SpriteEffects.None;
        public Rectangle frameRect;
        protected static Random r = new Random();
        public int totalFram, currentFrame, scale=1;
        public int buffer = 20;
        public float rotateAngle, rotateSpeed;

        abstract protected override void LoadContent();
        

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, frameRect, col, rotateAngle, center, scale, direction, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            frameElapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (frameElapsedTime >= frameTimeStep)
            {
                position += velocity;

                currentFrame = (currentFrame + 1) % totalFram;
                frameRect.X = currentFrame * frameRect.Width;
                rotateAngle = (rotateAngle + rotateSpeed) % MathHelper.TwoPi;

                frameElapsedTime = 0;

            }
            

            base.Update(gameTime);
        }


    }
}
