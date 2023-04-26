namespace geometry;

public abstract class Figure {
    protected abstract Vector[] Points();

    /**
     * Draws this figure onto canvas. The default implementation draws a line
     * between each consecutive pair of points of the figure.
     */
    public void Draw(Canvas canvas) {
        Vector[] points = Points();
        Vector last = points[points.Length - 1];
        foreach (Vector v in Points()) {
            canvas.Line(last, v);
            last = v;
        }
    }
}

public class Triangle : Figure {
    private Vector a, b, c;
    public Triangle(Vector a, Vector b, Vector c) {
        this.a = a;
        this.b = b;
        this.c = c;
    }

    protected override Vector[] Points() {
        return new Vector[] {a, b, c};
    }
}
