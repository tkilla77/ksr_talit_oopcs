namespace geometry;

/** A scene encapsulates multiple figures and their behavior. */
public abstract class Scene {
    private Canvas canvas = new Canvas(640, 480);
    protected List<Figure> figures = new List<Figure>();

    /** Updates the scene state, e.g. by moving figures. */
    public abstract void Update(double totalSeconds, double elapsedSeconds);

    /** Draws the scene onto the canvas. */
    public virtual void Draw(Canvas canvas) {
        foreach (Figure fig in figures) {
            fig.Draw(canvas);
        }
    }
}