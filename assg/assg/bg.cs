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
    class BackGround : DrawableGameComponent
    {
        public Color[] data;
        public BackGround(Game g) : base(g) { }

        public Texture2D texture;
        public Vector2 position, velocity;
        double frameElapsedTime, frameTimeStep;
        public Color col = Color.White;

        int speed = 10;
        SpriteBatch spriteBatch;
        SpriteEffects direction = SpriteEffects.None;

        public override void Initialize()
        {

            
           // velocity.Y = 0.1f;
            position.Y = 400f;

            frameTimeStep = 1000 / 25f;
            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Game.Content.Load<Texture2D>("images\\bg");
            
            //bg = Game.Content.Load<Texture2D>("images\\bg");
            //frameRect = new Rectangle(0, 0, texture.Width / totalFram, texture.Height);

            data = new Color[texture.Width * texture.Height];

            texture.GetData<Color>(data);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, null, col, 0f, Vector2.Zero, 1.02f, direction, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            frameElapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (frameElapsedTime >= frameTimeStep)
            {
                position += velocity;

                

                frameElapsedTime = 0;

            }

            if (position.Y > GraphicsDevice.Viewport.Height+50)
               
                velocity.Y = 0;

            base.Update(gameTime);
        }



    }
}
