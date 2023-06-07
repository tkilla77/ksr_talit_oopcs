namespace geometry;

/** A scene encapsulates multiple figures and their behavior. */
public abstract class Scene {
    private Canvas canvas = new Canvas(640, 480);

    /** Updates the scene state, e.g. by moving figures. */
    public abstract void Update(double totalSeconds, double elapsedSeconds);

    /** Draws the scene onto the canvas. */
    public abstract void Draw(Canvas canvas);

}