namespace geometry;

public class CircleScene : Scene {
    private Circle circle;
    private Vector speed = new Vector(5, -5); // pixels per second
    public CircleScene() {
        figures.Add(new Square(new Vector(300, 300), new Vector(50, 0)));
        circle = new Circle(new Vector(50, 50), new Vector(30, 0));
        circle.SetColors("000", "a0f0");
        figures.Add(circle);
    }

    public override void Update(double totalSeconds, double elapsedSeconds) {
        // Gravity
        speed.Components[1] += elapsedSeconds * 9.81;

        Vector center = circle.Center();
        double radius = circle.Radius();
        if (center.Components[1] <= radius || center.Components[1] >= 480 - radius) {
            speed.Components[1] *= -0.9; // mirror & dampen
        }
        if (center.Components[0] <= radius || center.Components[0] >= 680 - radius) {
            speed.Components[0] *= -0.9;
        }
        Vector translation = speed * elapsedSeconds * 30;
        circle.Move(translation);
    }
}
