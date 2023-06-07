namespace geometry;

public class CircleScene : Scene {
    private Figure circle;
    private int direction = 1;
    private int switchTime = 1;
    public CircleScene() {
        circle = new Circle(new Vector(50, 240), new Vector(30, 0));
        circle.SetColors("000", "af00");
        figures.Add(circle);
        figures.Add(new Square(new Vector(300, 300), new Vector(50, 0)));
    }

    public override void Update(double totalSeconds, double elapsedSeconds) {
        circle.Move(new Vector(2, direction * totalSeconds));
        if (totalSeconds > switchTime) {
            direction *= -1;
            switchTime += 2;
        }
    }
}
