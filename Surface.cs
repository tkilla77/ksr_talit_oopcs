using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SkiaSharp;

namespace geometry;

public class Surface : Game {
    private GraphicsDeviceManager _graphics;
    private Texture2D _texture;
    private SpriteBatch _spriteBatch;
    private Canvas _canvas;
    private Color[] _buffer;
    private Scene _scene;

    public Surface(Scene scene) {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.IsFullScreen = false;
        _graphics.PreferredBackBufferWidth = 640;
        _graphics.PreferredBackBufferHeight = 480;
        _graphics.ApplyChanges();
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _scene = scene;
    }

    protected override void Initialize() {
        // Resources in Monogame land
        _texture = new Texture2D(GraphicsDevice, 640, 480);
        _buffer = new Color[_texture.Width * _texture.Height];
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        // Resources in our Figure land.
        _canvas = new Canvas(640, 480);

        base.Initialize();
    }

    protected override void LoadContent() {
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime) {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        _scene.Update(gameTime.TotalGameTime.TotalSeconds, gameTime.ElapsedGameTime.TotalSeconds);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
        // Draw our scene - only the circle for now.
        _canvas.Clear();
        _scene.Draw(_canvas);

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
