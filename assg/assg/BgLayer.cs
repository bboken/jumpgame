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
    class BgLayer : DrawableGameComponent
    {
        public BgLayer(Game g) : base(g) { }
        Texture2D[] layer;
        SpriteBatch spriteBatch;
        Vector2 position,temp, velocity;
        double frameElapsedTime, frameTimeStep;

        protected override void LoadContent()
        {
            layer = new Texture2D[7];
            spriteBatch = new SpriteBatch(GraphicsDevice);
            layer[0] = Game.Content.Load<Texture2D>("layer\\sky_lightened");
            layer[1] = Game.Content.Load<Texture2D>("layer\\cloud_lonely");
            layer[2] = Game.Content.Load<Texture2D>("layer\\clouds_BG");
            layer[3] = Game.Content.Load<Texture2D>("layer\\mountains_lightened");
            layer[4] = Game.Content.Load<Texture2D>("layer\\clouds_MG_3");
            layer[5] = Game.Content.Load<Texture2D>("layer\\clouds_MG_2");
            layer[6] = Game.Content.Load<Texture2D>("layer\\clouds_MG_1");
            //position.Y = -50;
            velocity.Y = 0.005f;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            //spriteBatch.DrawString(font, "" + tempV, new Vector2(20, GraphicsDevice.Viewport.Height - 150), Color.White);
            //spriteBatch.DrawString(font, "" + bgPosition, new Vector2(20, GraphicsDevice.Viewport.Height - 120), Color.White);
            for(int i = 0; i<layer.Length;i++)
                spriteBatch.Draw(layer[i], position+temp*i, null, Color.White, 0f, Vector2.Zero, 2.3f, SpriteEffects.None, 0);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
           
            frameElapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            

            if (frameElapsedTime >= frameTimeStep)
            {
                temp += velocity;
                 frameElapsedTime = 0;
            }

            base.Update(gameTime);


        }

    }
}
