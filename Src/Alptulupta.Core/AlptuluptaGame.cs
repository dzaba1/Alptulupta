using System;
using System.Collections.Generic;
using System.Linq;
using Alptulupta.Contracts;
using Alptulupta.Core.Shapes;
using Alptulupta.KeyboardListener;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Alptulupta.Core
{
    internal sealed class AlptuluptaGame : Game, IGame
    {
        private readonly GraphicsDeviceManager graphics;
        private readonly IShapeFactory shapeFactory;
        private readonly IKeyboardHelper keyboardHelper;
        private readonly IMouseHelper mouseHelper;
        private readonly IGamepadHelper gamepadHelper;

        private SpriteBatch spriteBatch;
        private readonly List<StaticShape> shapes = new List<StaticShape>();
        private readonly List<IUpdateable> updateables = new List<IUpdateable>();
        private KeyboardInterceptor keyboardInterceptor;

        public AlptuluptaGame(IShapeFactory shapeFactory,
            IKeyboardHelper keyboardHelper,
            IMouseHelper mouseHelper,
            IGamepadHelper gamepadHelper)
        {
            this.shapeFactory = shapeFactory;
            this.keyboardHelper = keyboardHelper;
            this.mouseHelper = mouseHelper;
            this.gamepadHelper = gamepadHelper;

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            updateables.Add(keyboardHelper);
            updateables.Add(mouseHelper);
            updateables.Add(gamepadHelper);
        }

        protected override void Initialize()
        {
            keyboardInterceptor = new KeyboardInterceptor();
            keyboardInterceptor.OnKeyAction += OnKeyboardIntercept;
#if RELEASE
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.ToggleFullScreen();
#endif

            base.Initialize();
        }

        public Vector2 ScreenSize => new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();

            if (keyboardInterceptor != null)
            {
                keyboardInterceptor.OnKeyAction -= OnKeyboardIntercept;
                keyboardInterceptor.Dispose();
            }

            spriteBatch?.Dispose();

            ClearShapes();
        }

        private void ClearShapes()
        {
            foreach (var shape in shapes)
            {
                shape.Dispose();
            }

            shapes.Clear();
        }

        private bool OnKeyboardIntercept(object sender, RawKeyEventArgs args)
        {
            return Constants.VkCodesToBlock.Contains(args.VkCode);
        }

        private bool IsExit()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Z) && Keyboard.GetState().IsKeyDown(Keys.F4);
        }

        private bool IsSomethingPressed()
        {
            var keyboardAndMouse = keyboardHelper.GetNewPressed().Any() || mouseHelper.GetNewPressed() != 0;

            var gamepads = false;
            foreach (var gamepadButtons in gamepadHelper.GetNewButtonsPressed())
            {
                if (gamepadButtons.Value != 0)
                {
                    gamepads = true;
                    gamepadHelper.Vibrate(gamepadButtons.Key);
                }
            }

            return keyboardAndMouse || gamepads;
        }

        protected override void Update(GameTime gameTime)
        {
            if (IsExit())
            {
                Exit();
            }

            foreach (var updateable in updateables)
            {
                updateable.Update(gameTime);
            }

            if (IsSomethingPressed())
            {
                shapes.Add(shapeFactory.Random(ScreenSize));
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Z) && Keyboard.GetState().IsKeyDown(Keys.F1))
            {
                ClearShapes();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();

            foreach (var drawable in shapes)
            {
                drawable.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }
    }
}
