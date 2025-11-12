namespace MyGame.Core;

public class Main : Game
{
    // Resources for drawing.
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;


    public List<GameObject> Objects;

    public readonly static bool IsMobile = OperatingSystem.IsAndroid() || OperatingSystem.IsIOS();
    public readonly static bool IsDesktop = OperatingSystem.IsMacOS() || OperatingSystem.IsLinux() || OperatingSystem.IsWindows();

    public Main()
    {
        _graphics = new GraphicsDeviceManager(this);


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

        Objects = new List<GameObject>()
        {
            new Player(name: "player", position: new Vector2(100, 100))
            {
                new Sprite(Content.Load<Texture2D>("Player")),
                new Collider(size: new Point(32, 32), offset: new Point(0,32) ),
            },
            new GameObject(name: "test1",position: new Vector2(200, 400))
            {
                new Sprite(Content.Load<Texture2D>("Player")),
                new Collider()
            },
            new GameObject(name: "test2" ,position: new Vector2(250, 350))
            {
                new Sprite(Content.Load<Texture2D>("Player")),
                new Collider()
            },
            new GameObject(name: "test3" ,position: new Vector2(100, 400))
            {
                new Sprite(Content.Load<Texture2D>("Player")),
                new Collider()
            }
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
        foreach (var obj in Objects)
        {
            obj.Update(gameTime);
        }
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        // Clears the screen with the MonoGame orange color before drawing.
        GraphicsDevice.Clear(Color.MonoGameOrange);

        _spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

        foreach (var obj in Objects)
        {
            obj.Draw(_spriteBatch);
        }



        _spriteBatch.End();

        base.Draw(gameTime);
    }
}