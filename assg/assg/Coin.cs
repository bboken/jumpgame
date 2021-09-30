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
    class Coin : DrawableGameComponent
    {
        public Color[] data;
        public Coin(Game g) : base(g) { }

        public Texture2D texture;
        public Vector2 position, velocity, center;
        public int totalFram, currentFrame, scale =2;
        double frameElapsedTime, frameTimeStep;
        public Rectangle frameRect;
        public Color col = Color.White;
        protected static Random r = new Random();
       // public bool isclick = false, isjump = false, isdie = false, istemp = false, istemp2 = false, isstay = true, isstop = false;


        SpriteBatch spriteBatch;
        SpriteEffects direction = SpriteEffects.None;

        public override void Initialize()
        {
            position.X = (float)r.NextDouble() * (GraphicsDevice.Viewport.Width-100) +50;
            position.Y = -50;
            velocity.Y = (float)r.NextDouble() * 3f + 0.3f;

            frameTimeStep = 1000 / 25f;
            base.Initialize();
        }


        protected override void LoadContent()
        {
            totalFram = 1;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Game.Content.Load<Texture2D>("gem");
            frameRect = new Rectangle(0, 0, texture.Width / totalFram, texture.Height);

            data = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(data);
            center = new Vector2(texture.Height / 2, texture.Width / 2);
        }


        protected override void UnloadContent()
        {

        }

       
        public override void Update(GameTime gameTime)
        {
          

            frameElapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (frameElapsedTime >= frameTimeStep)
            {
                position += velocity;

                currentFrame= (currentFrame+1)%totalFram;
                frameRect.X = currentFrame * frameRect.Width;
                frameElapsedTime = 0;

            }

            if (position.Y > GraphicsDevice.Viewport.Width + frameRect.Height)
                Initialize();
            

            base.Update(gameTime);

        }

        SpriteFont font;
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            
            spriteBatch.Draw(texture, position, frameRect, col, 0f, Vector2.Zero, scale, direction, 0);

            spriteBatch.End();




            base.Draw(gameTime);
        }

        

       

      
    }
}
