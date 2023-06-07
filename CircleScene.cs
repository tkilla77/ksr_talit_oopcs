namespace geometry;

public class CircleScene : Scene {
    private Figure circle;
    private int direction = 1;
    private int switchTime = 1;
    public CircleScene() {
        circle = new Circle(new Vector(50, 240), new Vector(30, 0));
    }

    public override void Draw(Canvas canvas) {
        circle.Draw(canvas);
    }

    public override void Update(double totalSeconds, double elapsedSeconds) {
        circle.Move(new Vector(2, direction * totalSeconds));
        if (totalSeconds > switchTime) {
            direction *= -1;
            switchTime += 2;
        }
    }
}