using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using geometry;
using SkiaSharp;

namespace surface;

public class Surface : Game {
    private GraphicsDeviceManager _graphics;
    private Texture2D _texture;
    private SpriteBatch _spriteBatch;
    private Canvas _canvas;
    private Figure _circle;
    private Color[] _buffer;
    private int _direction = 1;


    public Surface() {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.IsFullScreen = false;
        _graphics.PreferredBackBufferWidth = 640;
        _graphics.PreferredBackBufferHeight = 480;
        _graphics.ApplyChanges();
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

    }

    protected override void Initialize() {
        // Resources in Monogame land
        _texture = new Texture2D(GraphicsDevice, 640, 480);
        _buffer = new Color[_texture.Width * _texture.Height];
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Resources in our Figure land.
        _canvas = new Canvas(640, 480);
        _circle = new Circle(new Vector(50, 50), new Vector(30, 0));

        base.Initialize();
    }

    protected override void LoadContent() {
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime) {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // Make our scene move a bit.
        if (gameTime.TotalGameTime.TotalSeconds > 3) {
            _direction = -1;
        }
        _circle.Move(new Vector(1, _direction * gameTime.TotalGameTime.TotalSeconds));

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
        // Draw our scene - only the circle for now.
        _canvas.Clear();
        _circle.Draw(_canvas);

        // Convert to Monogame land.
        SkCanvasToTexture(_canvas, _texture);

        // Draw Texture as sprite batch.
        _spriteBatch.Begin();
        _spriteBatch.Draw(_texture, Vector2.Zero, Color.CornflowerBlue);
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void SkCanvasToTexture(Canvas canvas, Texture2D texture) {
        SKColor[] pixels = canvas.GetPixels();
        for (int i = 0; i < pixels.Length; i++) {
            _buffer[i] = SkToMonoColor(pixels, i);
        }
        texture.SetData(_buffer);
    }

    private static Color SkToMonoColor(SKColor[] pixels, int i) {
        return new Color(pixels[i].Red, pixels[i].Green, pixels[i].Blue, pixels[i].Alpha);
    }
}
