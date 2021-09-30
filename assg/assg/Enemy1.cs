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
    class Enemy1 : Enemy
    {
        public Enemy1(Game g) : base(g) { }

        protected override void LoadContent()
        {
            base.spriteBatch = new SpriteBatch(GraphicsDevice);
            totalFram = 1;
            texture = Game.Content.Load<Texture2D>("enemy\\balota");

            data = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(data);
            
            frameRect = new Rectangle(0,0, texture.Width, texture.Height);
            center = new Vector2(frameRect.Height / 2, frameRect.Width / 2);
        }
        //
        public override void Initialize()
        {
            int temp = r.Next(0, 2);
            if (temp == 0)
            {
                position.X = 0 - 50;
                velocity.X = (float)r.NextDouble() * 3f + 1f;
                direction = SpriteEffects.FlipHorizontally;
            }
            else
            {
                position.X = GraphicsDevice.Viewport.Width;
                velocity.X = (float)r.NextDouble() * -3f - 1f;
                direction = SpriteEffects.None;
            }
            position.Y = (float)r.NextDouble() * (GraphicsDevice.Viewport.Height-150);
            rotateSpeed = 0;
            base.Initialize();
        }
        
    }
}
