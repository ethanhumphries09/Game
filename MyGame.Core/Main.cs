global using System;
global using System.Collections.Generic;
global using Microsoft.Xna.Framework;
global using Microsoft.Xna.Framework.Graphics;
global using Microsoft.Xna.Framework.Input;
using Engine;
using Engine.Components;

namespace MyGame.Core;
public class Main : Game
{
    // Resources for drawing.
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private GameObject gameObject;

    public readonly static bool IsMobile = OperatingSystem.IsAndroid() || OperatingSystem.IsIOS();
    public readonly static bool IsDesktop = OperatingSystem.IsMacOS() || OperatingSystem.IsLinux() || OperatingSystem.IsWindows();

    public Main()
    {
        _graphics = new GraphicsDeviceManager(this);

        // Share GraphicsDeviceManager as a service


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

        gameObject = new GameObject(position: new Vector2(100, 100))
        {
            new Sprite(Content.Load<Texture2D>("Player")),
            new Movement()
        };

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        // Exit the game if the Back button (GamePad) or Escape key (Keyboard) is pressed.
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic 
        gameObject.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        // Clears the screen with the MonoGame orange color before drawing.
        GraphicsDevice.Clear(Color.MonoGameOrange);

        _spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack);

        gameObject.Draw(_spriteBatch);



        _spriteBatch.End();

        base.Draw(gameTime);
    }
}