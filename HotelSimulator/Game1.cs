using System;
using System.Collections.Generic;
using System.Linq;
using HotelSimulator.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using HotelEvents;

namespace HotelSimulator
{
    /// <summary>
    ///     This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {

        private readonly GraphicsDeviceManager graphics;

        public Hotel hotel;

        private SpriteBatch spriteBatch;

        private CheckInListener _checkInListener;
        private CheckOutListener _checkOutListener;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content/Sprites";
        }

        /// <summary>
        ///     Allows the game to perform any initialization it needs to before starting to run.
        ///     This is where it can query for any required services and load any non-graphic
        ///     related content.  Calling base.Initialize will enumerate through any components
        ///     and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //add/create a new hotel
            hotel = new Hotel();

            //instantiate new listeners
            _checkInListener = new CheckInListener(ref hotel);
            _checkOutListener = new CheckOutListener(ref hotel);

            //register listeners to HotelEventManager
            HotelEventManager.Register(_checkInListener);
            HotelEventManager.Register(_checkOutListener);

            //start HotelEventManager
            HotelEventManager.Start();

            this.IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        ///     LoadContent will be called once per game and is the place to load
        ///     all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            graphics.PreferredBackBufferWidth = 900;
            graphics.PreferredBackBufferHeight = 700;
            graphics.ApplyChanges();
        }

        /// <summary>
        ///     UnloadContent will be called once per game and is the place to unload
        ///     game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            //unregister listeners
            HotelEventManager.Deregister(_checkInListener);
            HotelEventManager.Deregister(_checkOutListener);

            //stop HotelEventManager
            HotelEventManager.Stop();
            hotel.StopTimer();

        }

        /// <summary>
        ///     Allows the game to run logic such as updating the world,
        ///     checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        ///     This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            foreach (AbstractRoom r in hotel.Roomlist.ToList())
            {

                Texture2D temp = Content.Load<Texture2D>(r.sprite);

                if (r.DimensionY > 1)
                {
                    spriteBatch.Draw(temp, new Rectangle(r.PositionX * 60, (r.PositionY + 1) * -1 * 40 + 500, r.DimensionX * 60, r.DimensionY * 40), Color.White);
                }
                else
                {
                    spriteBatch.Draw(temp, new Rectangle(r.PositionX * 60, r.PositionY * -1 * 40 + 500, r.DimensionX * 60, r.DimensionY * 40), Color.White);
                }

            }



            foreach (AbstractHuman h in hotel.GuestList.ToList())
            {
                Texture2D temp = Content.Load<Texture2D>(h.sprite);

                spriteBatch.Draw(temp, new Rectangle(h.CurrentPosition.PositionX * 60, h.CurrentPosition.PositionY * -1 * 40 + 510, 19, 25), Color.White);

            }

            foreach (AbstractHuman m in hotel.Employees)
            {
                Texture2D temp = Content.Load<Texture2D>(m.sprite);

                spriteBatch.Draw(temp, new Rectangle(m.CurrentPosition.PositionX * 75, m.CurrentPosition.PositionY * -1 * 40 + 510, 19, 25), Color.White);
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}