using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame.Core;

public class Main : Game
{
    // Resources for drawing.
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public readonly static bool IsMobile = OperatingSystem.IsAndroid() || OperatingSystem.IsIOS();
    public readonly static bool IsDesktop = OperatingSystem.IsMacOS() || OperatingSystem.IsLinux() || OperatingSystem.IsWindows();

    public Main()
    {
        _graphics = new GraphicsDeviceManager(this);
        
        // Share GraphicsDeviceManager as a service.
        Services.AddService<GraphicsDeviceManager>(_graphics);
        

        Content.RootDirectory = "Content";

        // Configure screen orientations.
        _graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
    }

    protected override void Initialize()
    {


        base.Initialize();
    }

    /// <summary>
    /// Loads game content, such as textures and particle systems.
    /// </summary>
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Services.AddService<SpriteBatch>(_spriteBatch);


        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        // Exit the game if the Back button (GamePad) or Escape key (Keyboard) is pressed.
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        // Clears the screen with the MonoGame orange color before drawing.
        GraphicsDevice.Clear(Color.MonoGameOrange);

        _spriteBatch.Begin();

        _spriteBatch.Draw(Content.Load<Texture2D>("Player"), new Vector2(100, 100), Color.White);




        _spriteBatch.End();

        base.Draw(gameTime);
    }
}