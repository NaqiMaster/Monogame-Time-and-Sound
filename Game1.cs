using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Time_and_Sound
{
    public class Game1 : Game
    {
        SoundEffect explode;
        MouseState mouseState;
        float seconds;
        SpriteFont timeFont;
        Texture2D bomb;
        Rectangle bombRec;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();

            this.Window.Title = "Time & Sound";
         
            
            bombRec = new Rectangle(50, 50, 700, 400);

            seconds = 0;
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            timeFont = Content.Load<SpriteFont>("Time");
            bomb = Content.Load<Texture2D>("bomb");
            explode = Content.Load<SoundEffect>("explosion");
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
                seconds = 0f;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here



            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (seconds >= 15)
            {
                explode.Play();
                seconds = 0f;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(bomb, bombRec, Color.White);
            _spriteBatch.DrawString(timeFont, (15 - seconds).ToString("00.0"), new Vector2(270, 200), Color.Black);

            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}