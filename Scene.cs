namespace geometry;

/** A scene encapsulates multiple figures and their behavior. */
public abstract class Scene {
    private Canvas canvas;
    protected List<Figure> figures = new List<Figure>();

    public int Width { get; }
    public int Height { get; }

    protected Scene() : this(640, 480) {}

    protected Scene(int width, int height) {
        Width = width;
        Height = height;
        canvas = new Canvas(width, height);
    }

    /** Updates the scene state, e.g. by moving figures. */
    public abstract void Update(double totalSeconds, double elapsedSeconds);

    /** Draws the scene onto the canvas. */
    public virtual void Draw(Canvas canvas) {
        foreach (Figure fig in figures) {
            fig.Draw(canvas);
        }
    }
}