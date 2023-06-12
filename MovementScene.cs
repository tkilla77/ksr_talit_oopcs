using geometry;

class MovementScene : Scene {
    private Figure Circle;
    private Vector Speed;
    public MovementScene() {
        Circle = new Circle(new Vector(100, 100), new Vector(40, 0));
        Circle.SetColors("black", "red");
        figures.Add(Circle);
        Speed = new Vector(15, 10);
    }
    public override void Update(double totalSeconds, double elapsedSeconds) {
        Vector translation = elapsedSeconds * Speed;
        Circle.Move(translation);
    }
}