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
    class Enemy2 : Enemy
    {
        public Enemy2(Game g) : base(g) { }

        protected override void LoadContent()
        {
            base.spriteBatch = new SpriteBatch(GraphicsDevice);

            texture = Game.Content.Load<Texture2D>("enemy\\bomba");
            totalFram = 1;

            data = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(data);
            frameRect = new Rectangle(0, 0, texture.Width / 5, texture.Height);
            center = new Vector2(frameRect.Height / 2, frameRect.Width / 2);
        }

        public override void Initialize()
        {
            int temp = r.Next(0, 2);
            if (temp == 0)
            {
                velocity.X = (float)r.NextDouble() * 8f + 1f;
                position.X = 0;
            }
            else
            {
                position.X = GraphicsDevice.Viewport.Width;
                velocity.X = (float)r.NextDouble() * -8f - 1f;
            }
            // position.X = (float)r.NextDouble() * GraphicsDevice.Viewport.Width;
            position.Y = GraphicsDevice.Viewport.Height + 10;
            velocity.Y = (float)r.NextDouble() * -8f - 3f;
            scale = 2;
            rotateSpeed = velocity.Y / 10f;
            base.Initialize();


        }

        double frameElapsedTime, frameTimeStep = 1000 / 25;
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
                velocity.Y+=0.1f;

            }

        }
    }
}
