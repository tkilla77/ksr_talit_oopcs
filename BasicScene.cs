using geometry;

class BasicScene : Scene {
    private double NextCircle = 2;
    public BasicScene() {
        Figure circle = new Circle(new Vector(100, 100), new Vector(40, 0));
        circle.SetColors("black", "red");
        figures.Add(circle);

        Figure triangle = new Polygon(new Vector(300, 250), new Vector(370, 200), new Vector(280, 180));
        triangle.SetColors("ff3", "33f");
        figures.Add(triangle);
    }
    public override void Update(double totalSeconds, double elapsedSeconds) {
        if (totalSeconds > NextCircle) {
            // Create new circles every so often...
            Figure triangle = new Circle(RandomVector(60), new Vector(30, 0));
            triangle.SetColors(RandomColor(), RandomColor());
            figures.Add(triangle);
            NextCircle += 2;
        }
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