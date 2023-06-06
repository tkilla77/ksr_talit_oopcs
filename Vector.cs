namespace geometry;

public class Vector {
    double[] components;
    public Vector(params double[] components) {
        this.components = components;
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

    public static Vector Add(Vector one, Vector two) {
        if (one.Dim != two.Dim) {
            throw new Exception("Vectors must have same number of dimensions!");
        }
        double[] components = new double[one.Dim];
        for (int i = 0; i < one.Dim; i++) {
            components[i] = one.Components[i] + two.Components[i];
        }
        return new Vector(components);
    }

    public static Vector operator+(Vector one, Vector two) {
        return Vector.Add(one, two);
    }

    public void Move(Vector v) {
        Vector sum = this + v;
        this.components = sum.components;
    }

}