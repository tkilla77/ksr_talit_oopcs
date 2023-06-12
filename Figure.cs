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
        canvas.Polygon(points, StrokeColor, FillColor);
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

    public Vector Center {
        get { return center; }
    }

    public double Radius {
        get { return radius.Magnitude; }
    }
}
