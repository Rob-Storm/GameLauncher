using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Linq;

namespace MGTest
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SpriteFont font;

        public string[] check;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            check = Environment.GetCommandLineArgs();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Font");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.DrawString(font,"You opened the program!",new Vector2(200, 200), Color.Black);

            foreach (string args in Environment.GetCommandLineArgs().Skip(1)) 
            {
                _spriteBatch.DrawString(font, $"Args: {args}", new Vector2(200, 250), Color.Black);

                for (int i = 0; i < check.Length; i++)
                {
                    if (check[i] == "crash")
                    {
                        int x = 1, y = 0;

                        //Crashes the program to test launcher error handling

                        x /= y;
                    }
                }
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}