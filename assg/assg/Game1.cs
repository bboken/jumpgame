using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;     //WMV
using Microsoft.Xna.Framework.Media;     //mp3
using System.Diagnostics;

namespace assg
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font,ggfont;
        Texture2D mouseTexture, mouseTexture2;
        player PC;
        BackGround bg;
        BgLayer layer;
        Ground[] ground;
        Enemy[] enemy;
        Coin[] coin;
        int groundNum = 10,enemyNum = 5,coinNum = 5,life=5;

        Vector2 bgPos, bgVel;
        float downspeed = 0.1f, score=0,temp=0f, Volume=0.5f;
        double frameElapsedTime, frameTimeStep = 1000;
        SoundEffect coineff;
        SoundEffect bombashot;
        SoundEffect scream;
        Song bgm;
        SoundEffectInstance soundInstance;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
       
        protected override void Initialize()
        {
            //this.IsMouseVisible = true;
            layer = new BgLayer(this);
            Components.Add(layer);
            coin = new Coin[coinNum];
            for (int i = 0; i < coinNum; i++)
            {
                coin[i] = new Coin(this);
                Components.Add(coin[i]);
            }
            ground = new Ground[groundNum];
            for (int i = 0; i < groundNum; i++)
            {
                if (i % 3 == 0) ground[i] = new Ground1(this, i);
                if (i % 3 == 1) ground[i] = new Ground2(this, i);
                if (i % 3 == 2) ground[i] = new Ground3(this, i);
                Components.Add(ground[i]);
            }

            enemy = new Enemy[enemyNum];
            for (int i = 0; i < enemyNum; i++)
            {
                if (i % 2 == 0) enemy[i] = new Enemy1(this);
                if (i % 2 == 1) enemy[i] = new Enemy2(this);
                Components.Add(enemy[i]);
            }
            bg = new BackGround(this);
            Components.Add(bg);
            PC = new player(this);
            Components.Add(PC);
            
            bg.velocity.Y = downspeed;
            PC.bgVel.Y = downspeed;

            
            PC.bgVel = bg.velocity;
            base.Initialize();
        }


        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = this.Content.Load<SpriteFont>("fonts\\gamefont");
            ggfont = this.Content.Load<SpriteFont>("fonts\\gamefont2");
            mouseTexture = this.Content.Load<Texture2D>("ms1");
            mouseTexture2 = this.Content.Load<Texture2D>("ms2");

            bombashot = Content.Load<SoundEffect>("songs\\bomba");
            scream = Content.Load<SoundEffect>("songs\\scream");
            coineff = Content.Load<SoundEffect>("songs\\Coin");
            bgm = Content.Load<Song>("songs\\bgm");
            MediaPlayer.Volume = 0.2f;
            MediaPlayer.Play(bgm);
            MediaPlayer.IsRepeating = true;
        }


        protected override void UnloadContent()
        {

        }

        const int LIFE_MISS_DELAY = 1000;
        private double lastCollisionTime = 0;
        protected override void Update(GameTime gameTime)
        {
            UpdatemsInput();
            if (!ff)
            {
                bgPos = bg.position;
                PC.bgPosition = bgPos;


                if (life <= 0)
                {
                    PC.col = Color.White;
                    ff = true;
                    PC.isdie = true;
                }
                frameElapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (frameElapsedTime >= frameTimeStep)
                {
                    temp += 0.1f;
                    score += temp;
                    frameElapsedTime = 0;
                }

                if (life <= 0)
                {
                    PC.col = Color.White;
                    ff = true;
                    PC.isdie = true;
                }
                UpdateInput();
                base.Update(gameTime);

                if (CheckCollision()) PC.check();
                CheckCollision2();
                if (CheckCollision3())
                {
                    PC.col = Color.Red;
                    if (gameTime.TotalGameTime.TotalMilliseconds - lastCollisionTime > LIFE_MISS_DELAY)
                    {
                        soundInstance = scream.CreateInstance();
                        soundInstance.Volume = Volume;
                        soundInstance.Play();

                        life--;
                        lastCollisionTime = gameTime.TotalGameTime.TotalMilliseconds;
                    }
                }
                else
                    PC.col = Color.White;
                
                
               
                if ((PC.position.Y > GraphicsDevice.Viewport.Height - PC.frameRect.Height * PC.scale+20))
                {
                   life = -1;
                    soundInstance = scream.CreateInstance();
                    soundInstance.Volume = Volume;
                    soundInstance.Play();
                }
            }
            
            foreach(Enemy e in enemy)
            {
                if (e.position.X > GraphicsDevice.Viewport.Width + e.buffer || e.position.X < 0 - e.texture.Width - e.buffer)
                {
                    soundInstance = bombashot.CreateInstance();
                    soundInstance.Volume = Volume;
                    soundInstance.Play();
                    e.Initialize();
                }
                if (e.position.Y > GraphicsDevice.Viewport.Width + e.buffer)
                {
                    soundInstance = bombashot.CreateInstance();
                    soundInstance.Volume = Volume;
                    soundInstance.Play();
                    e.Initialize();
                }
            }
        }
        Vector2 NowmsPos, MarkmsPos1, MarkmsPos2;
        
        bool ff = false;
        Vector2 distance;
        int intdistance;
        ButtonState LastMouseButtonState = ButtonState.Released;
        private void UpdateInput()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState ks = Keyboard.GetState();
            
            if (ks.IsKeyDown(Keys.Escape)) Exit();
            /*
            if (ks.IsKeyDown(Keys.D1)) PC.isclick=true;
                else PC.isclick = false;
            if (ks.IsKeyDown(Keys.D2)) PC.isjump =true;
                else { PC.isjump = false; PC.tempcount = 0;}
            if (ks.IsKeyDown(Keys.D3)) PC.isdie = true;
                else PC.isdie = false;
                */

           
            if (!PC.isjump)
            {
                if (ms.LeftButton == ButtonState.Pressed && ms.LeftButton != LastMouseButtonState)
                {
                    PC.isclick = true;
                    MarkmsPos1 = NowmsPos;
                }
                else if (ms.LeftButton == ButtonState.Released && ms.LeftButton != LastMouseButtonState)
                {
                    PC.isclick = false;
                    MarkmsPos2 = NowmsPos;
                    distance = MarkmsPos1 - MarkmsPos2;
                    intdistance = (int)distance.Length();
                    if (!PC.isjump)
                        if (intdistance >50)
                        {
                            PC.isstay = false;
                            PC.setVel(distance);
                            //PC.istemp = true;
                            PC.isjump = true;
                        }

                }
            }
            LastMouseButtonState = ms.LeftButton;

        }
        MouseState ms;
        private void UpdatemsInput()
        {
            ms = Mouse.GetState();
            NowmsPos.X = ms.X; NowmsPos.Y = ms.Y;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Life :" + life, new Vector2(20, GraphicsDevice.Viewport.Height - 60), Color.Black);
            spriteBatch.DrawString(font, "score :"+ (int)score , new Vector2(20, GraphicsDevice.Viewport.Height - 90), Color.Black);
            if(PC.isclick)
                spriteBatch.Draw(mouseTexture2, NowmsPos, null, Color.White, 0f, Vector2.Zero, 2, SpriteEffects.FlipHorizontally, 0);
            else
                spriteBatch.Draw(mouseTexture, NowmsPos, null, Color.White, 0f, Vector2.Zero, 2, SpriteEffects.FlipHorizontally, 0);
            if (ff)
                spriteBatch.DrawString(ggfont, "Game Over",
                    new Vector2((GraphicsDevice.Viewport.Width - font.MeasureString("Game Over").X) / 2,
                    (GraphicsDevice.Viewport.Height - font.MeasureString("Game Over").Y) / 2), Color.Black);
            spriteBatch.End();
        }


        
        private bool CheckCollision()
        {
            Rectangle personRect1 = new Rectangle((int)PC.position.X, (int)PC.position.Y
                                                    , PC.frameRect.Width, PC.frameRect.Height);
            Matrix personTransform1 = Matrix.CreateTranslation(new Vector3(PC.position, 0.0f));

            foreach (Ground g in ground)
            {
                Rectangle groundRect;
                groundRect = new Rectangle(0, 0, g.texture.Width, g.texture.Height);
                
                Matrix transform = Matrix.CreateTranslation(new Vector3(-g.center, 0))
                                    //   * Matrix.CreateScale(block.Scale) 
                                    //   * Matrix.CreateRotationZ(g.rotateAngle)
                                    * Matrix.CreateTranslation(new Vector3(g.position, 0.0f));
                groundRect = CalculateBoundingRectangle(groundRect, transform);
                if (personRect1.Intersects(groundRect))
                    if (IntersectPixels(personTransform1, PC.frameRect, PC.texture.Width, ref PC.data,
                                        transform, g.texture.Width, g.texture.Height, ref g.data))

                        if (PC.velocity.Y > 0 && PC.isjump)

                            if (PC.position.Y > g.position.Y - (PC.frameRect.Height * PC.scale) - 20
                                    && PC.position.Y < g.position.Y - (PC.frameRect.Height * PC.scale) + 20)
                            {
                                PC.bgPosition = g.position;
                                PC.bgVel = g.velocity;
                                return true;
                            }
            }
            return false;
        }

        private void CheckCollision2()
        {
            Rectangle personRect1 = new Rectangle((int)PC.position.X, (int)PC.position.Y
                                                    , PC.frameRect.Width* PC.scale, PC.frameRect.Height* PC.scale) ;
            Matrix personTransform1 = Matrix.Identity* Matrix.CreateScale(PC.scale)* Matrix.CreateTranslation(new Vector3(PC.position, 0.0f)) ;

            foreach (Coin c in coin)
            {
                Rectangle coinRect;
                coinRect = new Rectangle(0, 0, c.texture.Width*c.scale, c.texture.Height * c.scale);
                
                Matrix transform = Matrix.CreateTranslation(new Vector3(0,0, 0))
                                       * Matrix.CreateScale(c.scale) 
                                    //   * Matrix.CreateRotationZ(c.rotateAngle)
                                    * Matrix.CreateTranslation(new Vector3(c.position, 0.0f));
                coinRect = CalculateBoundingRectangle(coinRect, transform);
                if (personRect1.Intersects(coinRect))
                    if (IntersectPixels(personTransform1, PC.frameRect, PC.texture.Width, ref PC.data,
                                        transform, c.texture.Width, c.texture.Height, ref c.data))
                    {
                        soundInstance = coineff.CreateInstance();
                        soundInstance.Volume = Volume;
                        soundInstance.Play();
                        getCoin();
                        c.Initialize();
                       
                    }
            }
            
        }

        private bool CheckCollision3()
        {
            Rectangle personRect1 = new Rectangle((int)PC.position.X, (int)PC.position.Y
                                                    , PC.frameRect.Width* PC.scale, PC.frameRect.Height* PC.scale);
            Matrix personTransform1 = Matrix.Identity * Matrix.CreateScale(PC.scale) * Matrix.CreateTranslation(new Vector3(PC.position, 0.0f));

            foreach (Enemy e in enemy)
            {
                Rectangle enemyRect;
                enemyRect = new Rectangle(0, 0, e.frameRect.Width* e.scale, e.texture.Height* e.scale);

                Matrix transform = Matrix.CreateTranslation(new Vector3(-e.center, 0))
                                       * Matrix.CreateScale(e.scale)
                                       * Matrix.CreateRotationZ(e.rotateAngle)
                                    * Matrix.CreateTranslation(new Vector3(e.position, 0.0f));
                enemyRect = CalculateBoundingRectangle(enemyRect, transform);
                if (personRect1.Intersects(enemyRect))
                    if (IntersectPixels(personTransform1, PC.frameRect, PC.texture.Width, ref PC.data,
                                        transform, e.texture.Width, e.texture.Height, ref e.data))
                    {
                        return true;

                    }
            }
            return false;
        }

        static bool IntersectPixels(Matrix transformA, Rectangle rectA, int widthA, ref Color[] dataA,
                                   Matrix transformB, int widthB, int heightB, ref Color[] dataB)
        {
            Matrix AToB = transformA * Matrix.Invert(transformB);
            Vector2 stepX = Vector2.TransformNormal(Vector2.UnitX, AToB);
            Vector2 stepY = Vector2.TransformNormal(Vector2.UnitY, AToB);

            Vector2 yPosInB = Vector2.Transform(Vector2.Zero, AToB);

            for (int yA = rectA.Top; yA < rectA.Bottom; yA++)
            {   
                Vector2 posInB = yPosInB; 
                for (int xA = rectA.Left; xA < rectA.Right; xA++)
                { 
                    int xB = (int)Math.Round(posInB.X);
                    int yB = (int)Math.Round(posInB.Y);

                    if (0 <= xB && xB < widthB && 0 <= yB && yB < heightB)
                    {
                        Color colorA = dataA[xA + yA * widthA]; Color colorB = dataB[xB + yB * widthB];
                        if (colorA.A != 0 && colorB.A != 0) return true;
                    }
                    posInB += stepX; 
                }
                yPosInB += stepY; 
            }
            return false;
        }

       

        public static Rectangle CalculateBoundingRectangle(Rectangle rectangle, Matrix transform)
        {

            Vector2 leftTop = new Vector2(rectangle.Left, rectangle.Top);
            Vector2 rightTop = new Vector2(rectangle.Right, rectangle.Top);
            Vector2 leftBottom = new Vector2(rectangle.Left, rectangle.Bottom);
            Vector2 rightBottom = new Vector2(rectangle.Right, rectangle.Bottom);

            Vector2.Transform(ref leftTop, ref transform, out leftTop);
            Vector2.Transform(ref rightTop, ref transform, out rightTop);
            Vector2.Transform(ref leftBottom, ref transform, out leftBottom);
            Vector2.Transform(ref rightBottom, ref transform, out rightBottom);

            Vector2 min = Vector2.Min(Vector2.Min(leftTop, rightTop), Vector2.Min(leftBottom, rightBottom));
            Vector2 max = Vector2.Max(Vector2.Max(leftTop, rightTop), Vector2.Max(leftBottom, rightBottom));

            return new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
        }

        void getCoin()
        {
            score += 50;
        }
    }
}


