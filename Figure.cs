namespace geometry;

using System;
using System.Collections.Generic;
using SkiaSharp;

/** Any 2D figure. */
public abstract class Figure {
    public string StrokeColor = "black";
    public string FillColor = "red"; // Transparent

    public void SetColors(string strokeColor, string fillColor) {
        StrokeColor = strokeColor;
        FillColor = fillColor;
    }

    /**
     * Draws this figure onto canvas. The default implementation draws a line
     * between each consecutive pair of points of the figure.
     */
    public abstract void Draw(Canvas canvas);

    public virtual void Move(Vector vector) {
        foreach (Vector v in GetMovingPoints()) {
            v.Move(vector);
        }
    }

    /** Returns the set of points that need to be moved for translation. */
    protected abstract IEnumerable<Vector> GetMovingPoints();
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

    protected override IEnumerable<Vector> GetMovingPoints() {
        return Points();
    }

    public override void Draw(Canvas canvas) {
        Vector[] points = Points();
        canvas.Polygon(points, StrokeColor, FillColor);
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
        return new Vector[] { corner, corner + side, corner + side + perpendicular, corner + perpendicular };
    }
}

public class Circle : Figure {
    private Vector center;

    private Vector radius;

    public Circle(Vector center, Vector radius) {
        this.center = center;
        this.radius = radius;
    }

    public override void Draw(Canvas canvas) {
        canvas.Circle(center, radius.Magnitude, StrokeColor, FillColor);
    }

    protected override IEnumerable<Vector> GetMovingPoints() {
        return new Vector[] { center };
    }

    public Vector Center {
        get { return center; }
    }

    public double Radius {
        get { return radius.Magnitude; }
    }
}
