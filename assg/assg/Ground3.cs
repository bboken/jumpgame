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
    class Ground3 : Ground
    {
        public Ground3(Game g, int i) : base(g, i) { }

        protected override void LoadContent()
        {
            base.spriteBatch = new SpriteBatch(GraphicsDevice);

            texture = Game.Content.Load<Texture2D>("images\\g3_2");

            data = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(data);
            center = new Vector2(texture.Height / 2, texture.Width / 2);
        }
    }
}
