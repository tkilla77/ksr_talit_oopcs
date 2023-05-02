namespace geometry;

public abstract class Figure {
    protected abstract Vector[] Points();

    /**
     * Draws this figure onto canvas. The default implementation draws a line
     * between each consecutive pair of points of the figure.
     */
    public abstract void Draw(Canvas canvas);
}

public class Polygon : Figure {
    private Vector[] points;

    public Polygon(params Vector[] points) {
        this.points = points;
    }
    protected override Vector[] Points() {
        return points;
    }

    public override void Draw(Canvas canvas) {
        Vector[] points = Points();
        Vector last = points[points.Length - 1];
        foreach (Vector v in Points()) {
            canvas.Line(last, v);
            last = v;
        }
    }
}

public class Square : Polygon {
    private Vector a, b;
    public Square(Vector a, Vector b) {
        this.a = a;
        this.b = b;
    }

    protected override Vector[] Points() {
        Vector perpendicular = new Vector(-b.Components[1], b.Components[0]);
        return new Vector[] {a, a+b, a+b+perpendicular, a+perpendicular};
    }
}
