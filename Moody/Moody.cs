using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Moody.Components;
using Moody.Engine;
using System.Linq;
using IDrawable = Moody.Engine.Interfaces.IDrawable;

namespace Moody
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Moody : Game
    {
        GraphicsDeviceManager graphics;
        Scene scene = new Scene();
        AssetLibrary library = new AssetLibrary();

        public Moody()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            Mouse.SetCursor(MouseCursor.Arrow);
            IsMouseVisible = true;

            StaticActor actor1 = new StaticActor
            {
                Transform = new Transform
                {
                    Position = Vector3.UnitZ * 10
                },
                DisplayName = "StaticActor1",
                MainModel = library.Models[0],
            };
            StaticActor actor2 = new StaticActor
            {
                Transform = new Transform
                {
                    Position = Vector3.UnitX * 10
                },
                DisplayName = "StaticActor2",
                MainModel = library.Models[0],
            };
            StaticActor actor3 = new StaticActor
            {
                Transform = new Transform
                {
                    Position = Vector3.UnitZ * -10
                },
                DisplayName = "StaticActor3",
                MainModel = library.Models[0],
            };
            StaticActor actor4 = new StaticActor
            {
                Transform = new Transform
                {
                    Position = Vector3.UnitX * -10
                },
                DisplayName = "StaticActor4",
                MainModel = library.Models[0],
            };
            scene.RegisterActor(actor1);
            scene.RegisterActor(actor2);
            scene.RegisterActor(actor3);
            scene.RegisterActor(actor4);

            FirstPersonCamera camera = new FirstPersonCamera();
            camera.AspectRatio = GraphicsDevice.Viewport.AspectRatio;
            scene.RegisterActor(camera);
            scene.ActiveCamera = camera;
            scene.InputDispatcher.ViewportDimensions = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            scene.Start();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Model monocube = Content.Load<Model>("MonoCube");
            library.Models.Add(monocube);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            scene.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach(Model model in library.Models)
            {
                foreach (ModelMesh mesh in model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.View = scene.ActiveCamera.ViewMatrix;
                        effect.Projection = scene.ActiveCamera.ProjectionMatrix;
                    }
                }
            }

            foreach (IDrawable actor in scene.RegisteredActors.Where(n=>n is IDrawable))
            {
                actor.Draw();
            }
            base.Draw(gameTime);
        }
    }
}