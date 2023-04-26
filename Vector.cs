namespace geometry;

public class Vector {
    double[] components;
    public Vector(params double[] components) {
        this.components = components;
    }

    public double X() {
        return x;
    }

    public double Y() {
        return y;
    }

    public double Magnitude {
        get {
            double total = 0;
            foreach (double c in Components) {
                total += c*c;
            }
            return Math.Sqrt(total);
        }
    }

    public int Dim {
        get {
            return components.Length;
        }
    }

    public double[] Components { get {return components;} }

    public override string ToString() {
        return $"{components} (Len: {Magnitude})";
    }

}