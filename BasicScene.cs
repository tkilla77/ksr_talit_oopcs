using geometry;

class BasicScene : Scene {
    private Figure Circle;
    private Vector Speed;
    public BasicScene() {
        Circle = new Circle(new Vector(100, 100), new Vector(40, 0));
        Circle.SetColors("black", "red");
        figures.Add(Circle);
        Speed = new Vector(15, 10);
    }
    public override void Update(double totalSeconds, double elapsedSeconds) {
        Vector translation = elapsedSeconds * Speed;
        Circle.Move(translation);
    }

    /** Creates a random vector within the canvas, no closer than margin to the borders. */
    private Vector RandomVector(double margin) {
        int x = Random.Shared.Next((int) margin, (int) (Width-margin));
        int y = Random.Shared.Next((int) margin, (int) (Height-margin));
        return new Vector(x, y);
    }

    /** Creates a random 3-digit RGB color between 0x000 and 0xfff. */
    private string RandomColor() {
        int color = Random.Shared.Next(0, 0xfff);
        return color.ToString("x3");
    }
}