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
    class player : DrawableGameComponent
    {
        public Color[] data;
        public player(Game g) : base(g) { }

        public Texture2D texture;
        public Vector2 position, velocity, temp,  bgPosition, bgVel;
        public int totalFram, currentFrame, scale = 3;
        double frameElapsedTime, frameElapsedTime2, frameTimeStep, frameTimeStep2;
        public Rectangle frameRect;
        public Color col = Color.White;

        public Vector2 stay, die, click, jump, juming;

        int speed = 10;
        public bool isclick = false, isjump = false, isdie = false, istemp = false, istemp2 = false, isstay = true, isstop = false;
       

        SpriteBatch spriteBatch;
        SpriteEffects direction = SpriteEffects.None;

        public override void Initialize()
        {
            totalFram = 23;
            stay = new Vector2(0, 3);
            click = new Vector2(4, 7);
            jump = new Vector2(8, 10);
            juming = new Vector2(11, 15);
            die = new Vector2(16, 22);

            position.X = GraphicsDevice.Viewport.Width / 2;
            position.Y = 320;

            frameTimeStep = 1000 / 60f;
            frameTimeStep2 = 1000 / 13f;
            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Game.Content.Load<Texture2D>("images\\newcharacter2");
            
            frameRect = new Rectangle(0, 0, texture.Width / totalFram, texture.Height);

            font = Game.Content.Load<SpriteFont>("fonts\\gamefont");

            data = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(data);
        }


        protected override void UnloadContent()
        {

        }

        public int tempcount = 0;
        MouseState ms;
        public override void Update(GameTime gameTime)
        {
             ms = Mouse.GetState();
            if (velocity.X > 0)
                direction = SpriteEffects.None;
            if (velocity.X < 0)
                direction = SpriteEffects.FlipHorizontally;


            frameElapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            frameElapsedTime2 += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (frameElapsedTime >= frameTimeStep)
            {
                if (false) 
                    position = new Vector2(ms.X, ms.Y);
                else
                 position += velocity;
                if (!isstay|| isjump)
                    velocity.Y++;

                updateframe();
                frameElapsedTime = 0;

            }
            
            base.Update(gameTime);


            if (position.X <= 0)
            {
                position.X = 0;
                velocity.X = -velocity.X;
            }

            if (position.X >= (Game.GraphicsDevice.Viewport.Width- frameRect.Width* scale))
            {
                position.X = Game.GraphicsDevice.Viewport.Width - frameRect.Width* scale;
                velocity.X = -velocity.X;
            }

            if (position.Y > bgPosition.Y - frameRect.Height * scale + 40)
            {
                check();
            }
            if (isstop && velocity == Vector2.Zero)
            {
                velocity.Y = bgVel.Y/60*20;
            }
            else
            {
                isstop = !isstop;
            }
            
            if ((position.Y > GraphicsDevice.Viewport.Height + frameRect.Height* scale)  )
            {
                isdie = true;
            }
            temp = position;
        }

        SpriteFont font;
        public override void Draw(GameTime gameTime)
        {
            Texture2D rectangleTexture = new Texture2D(Game.GraphicsDevice, frameRect.Width, frameRect.Height);
            spriteBatch.Begin();
            //spriteBatch.DrawString(font, "" + tempV, new Vector2(20, GraphicsDevice.Viewport.Height - 150), Color.White);
            //spriteBatch.DrawString(font, "" + bgPosition, new Vector2(20, GraphicsDevice.Viewport.Height - 120), Color.White);
            spriteBatch.Draw(texture, position, frameRect, col, 0f, Vector2.Zero, scale, direction, 0);
            //spriteBatch.Draw(rectangleTexture, position, frameRect, Color.Yellow, 0f, Vector2.Zero, scale, direction, 0);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void setVel(Vector2 V)
        {
            float limit = 0.01f;
            if (V == Vector2.Zero)
                velocity = V;
            if (V != Vector2.Zero)
            {
                if (V.Y < -220)

                    velocity.Y = speed * -220 * limit;
                else if (V.Y > 0)
                    return;
                else
                    velocity.Y = speed * V.Y * limit;

                if (V.X < -150)

                    velocity.X = speed * -150 * limit;
                else if (V.X >150)

                    velocity.X = speed * 150 * limit;
                else
                    velocity.X = speed * V.X * limit;

                
            }
        }

        public void check()
        {
            velocity = Vector2.Zero;
            position.Y = temp.Y;
            isstop = true;

            istemp = false;
            istemp2 = false;
            isjump = false;
            isstay = true;
        }

        public void updateframe()
        {
            if (frameElapsedTime2 >= frameTimeStep2)
            {
               
                
                if (isclick)
                {
                    if (currentFrame >= click.Y || currentFrame < click.X)
                        currentFrame = (int)click.X;
                    currentFrame++;

                }
                else if (isjump)
                {
                    if (!istemp2)
                    {
                        if (currentFrame > juming.Y || currentFrame < jump.X)
                            currentFrame = (int)jump.X;

                        currentFrame++;
                        if (currentFrame == juming.Y)
                            istemp2 = true;
                    }

                }
                else if (isdie)
                {
                    if (currentFrame >= die.Y)
                        return;
                    if (currentFrame < die.X)
                        currentFrame = (int)die.X;
                    currentFrame++;
                }
                else if (isstay)
                {
                    if (currentFrame >= stay.Y)
                        currentFrame = 0;
                    currentFrame++;
                }


                frameRect.X = currentFrame * frameRect.Width;
                frameElapsedTime2 = 0;

            }
        }

        public float setg(float groundY)
        {
            return groundY;
        }
    }
}
