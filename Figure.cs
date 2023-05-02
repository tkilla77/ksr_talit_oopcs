namespace geometry;

/** Any 2D figure. */
public abstract class Figure {
    /**
     * Draws this figure onto canvas. The default implementation draws a line
     * between each consecutive pair of points of the figure.
     */
    public abstract void Draw(Canvas canvas);
}

/** A closed polygon. */
public class Polygon : Figure {
    private Vector[] points;

    public Polygon(params Vector[] points) {
        this.points = points;
    }
    /** The sequence of points to be connected. */
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

/** A square is also a polygon, but we save some space by only storing a corner and a side vector. */
public class Square : Polygon {
    private Vector corner, side;
    public Square(Vector corner, Vector side) {
        this.corner = corner;
        this.side = side;
    }

    protected override Vector[] Points() {
        // other side is perpendicular to side: [-y, x]
        Vector perpendicular = new Vector(-side.Components[1], side.Components[0]);
        return new Vector[] {corner, corner+side, corner+side+perpendicular, corner+perpendicular};
    }
}
