namespace geometry;

public class CircleScene : Scene {
    private Figure circle;
    public CircleScene() {
        circle = new Circle(new Vector(50, 50), new Vector(30, 0));
    }

    public override void Draw(Canvas canvas) {
        circle.Draw(canvas);
    }

    public override void Update(double totalSeconds, double elapsedSeconds) {
        circle.Move(new Vector(2, totalSeconds * totalSeconds));
    }
}