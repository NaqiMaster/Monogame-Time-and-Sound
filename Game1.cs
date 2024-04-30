using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Time_and_Sound
{
    public class Game1 : Game
    {
        //Naqi Master
        bool explosion;
        SoundEffect explode;
        SoundEffectInstance explodeInstance;
        MouseState mouseState;
        float seconds;
        SpriteFont timeFont, wireText,defuseText;
        Texture2D bomb, exploded;
        Rectangle bombRec, wires;

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

            wires = new Rectangle(480, 160, 300, 100);
            bombRec = new Rectangle(50, 50, 700, 400);
            explosion = false;

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
            explodeInstance = explode.CreateInstance();
            exploded = Content.Load<Texture2D>("Exploded");
            wireText = Content.Load<SpriteFont>("Wires");

        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (wires.Contains(mouseState.X, mouseState.Y))
                {
                    //Defused
                    Exit();
                }
                else if (bombRec.Contains(mouseState.X, mouseState.Y))
                {
                    //Reset timer
                    seconds = 0f;
                }

            }


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here



            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (seconds >= 15)
            {
                explodeInstance.Play();
                seconds = 0f;
                explosion = true;
                bombRec = new Rectangle(0, 0, 800, 500);


            }

            if (explodeInstance.State == SoundState.Stopped && explosion == true)
            {
                this.Exit();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            if (explosion == true)
            {
                _spriteBatch.Draw(exploded, bombRec, Color.White);

            }
            else
            {
                _spriteBatch.Draw(bomb, bombRec, Color.White);
                _spriteBatch.DrawString(timeFont, (15 - seconds).ToString("00.0"), new Vector2(270, 200), Color.Black);
                _spriteBatch.DrawString(wireText,"Find a way to defuse and exit the program!",new Vector2(100,12),Color.Black);
                _spriteBatch.Draw(bomb, wires, Color.White * 0);
            }


            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}