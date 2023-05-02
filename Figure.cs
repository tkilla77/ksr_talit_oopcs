namespace geometry;

public abstract class Figure {
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
    protected virtual Vector[] Points() {
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
    private Vector corner, side;
    public Square(Vector corner, Vector side) {
        this.corner = corner;
        this.side = side;
    }

    protected override Vector[] Points() {
        Vector perpendicular = new Vector(-side.Components[1], side.Components[0]);
        return new Vector[] {corner, corner+side, corner+side+perpendicular, corner+perpendicular};
    }
}
