namespace geometry;

public class Vector {
    double x;
    double y;
    public Vector(double x, double y) {
        this.x = x;
        this.y = y;
    }

    public double X() {
        return x;
    }

    public double Y() {
        return y;
    }

    public double Length() {
        return Math.Sqrt(x*x + y*y);
    }


    public override string ToString() {
        return $"[{x}, {y}] (Len: {Length()})";
    }

    public void Move(Vector translation) {
        this.x += translation.x;
        this.y += translation.y;
    }
}