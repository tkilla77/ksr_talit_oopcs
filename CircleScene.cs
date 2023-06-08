namespace geometry;

public struct Material {
    public static Material Air = new Material(1.2);
    public static Material Water = new Material(1000);
    public static Material Steel = new Material(7850);

    public Material(double density) {
        Density = density;
    }
    public double Density { get; }
}

public class Sphere {
    public static Sphere FromShapeAndMass(Circle figure, double mass) {
        double radius_si = figure.Radius / CircleScene.PixelsPerMeter;
        // Assume a 3d-sphere displacing the medium at given density
        double volume = Math.PI*4/3*Math.Pow(radius_si, 3);
        return new Sphere(figure, volume, mass);
    }

    public static Sphere SolidSphere(Circle circle, Material material) {
        double radius_si = circle.Radius / CircleScene.PixelsPerMeter;
        // Assume a 3d-sphere displacing the medium at given density
        double volume = Math.PI*4/3*Math.Pow(radius_si, 3);

        return new Sphere(circle, volume, volume * material.Density);
    }

    private Sphere(Circle figure, double volume, double mass) {
        Shape = figure;
        Volume = volume;
        Mass = mass;
        Speed = new Vector(0, 0);
    }

    public Circle Shape { get; }
    public double Mass { get; }
    public double Volume { get; }

    public Vector Speed { get; set; }
}

public class CircleScene : Scene {
    private const double ScreenResolutionPpi = 160; // PixelsPerInch = PPI
    // 1 Inch = 2.54cm
    // Scaling Factor
    private const double ScalingFactor = 10;
    internal const double PixelsPerMeter = ScreenResolutionPpi / 2.54 * 100 / ScalingFactor;
    // Fraction of momentum maintained in collision direction.
    private const double CollisionElasticity = 0.99;
    // Fraction of momentum maintained in non-collision direction.
    private const double CollisionOtherDimension = 0.999;
    // Drag coefficient of a sphere.
    private const double DragCoefficient = 0.5;
    // Air density at earth surface.
    private static Material Medium = Material.Air;

    private Sphere soapBubble, pingPongBall, steelBall;
    public CircleScene() {
        // filled with Helium...
        soapBubble = Sphere.SolidSphere(new Circle(new Vector(50, 150), new Vector(0.06 * PixelsPerMeter, 0)), Material.Air);
        soapBubble.Shape.SetColors("700f", "300a");
        soapBubble.Speed = new Vector(0.1, 0);
        figures.Add(soapBubble.Shape);

        pingPongBall = Sphere.FromShapeAndMass(new Circle(new Vector(600, 160), new Vector(0.02 * PixelsPerMeter, 0)), 0.0027);
        pingPongBall.Shape.SetColors("eff9", "eff9");
        pingPongBall.Speed = new Vector(-1.7, -5.7);
        figures.Add(pingPongBall.Shape);

        steelBall = Sphere.SolidSphere(new Circle(new Vector(600, 160), new Vector(0.01 * PixelsPerMeter, 0)), Material.Steel);
        steelBall.Shape.SetColors("aaa", "aaa");
        steelBall.Speed = new Vector(-1.2, -0.05);
        figures.Add(steelBall.Shape);
    }

    public override void Update(double totalSeconds, double elapsedSeconds) {
        UpdateSphere(soapBubble, elapsedSeconds);
        UpdateSphere(pingPongBall, elapsedSeconds);
        UpdateSphere(steelBall, elapsedSeconds);
    }

    private void UpdateSphere(Sphere sphere, double elapsedSeconds) {
        Circle shape = sphere.Shape;
        Vector speed = sphere.Speed;
       // Wall collision
        Vector center = shape.Center;
        double radius = shape.Radius;
        // Y - Vertical
        if (center.Components[1] <= radius && speed.Components[1] < 0
            || center.Components[1] >= 480 - radius && speed.Components[1] > 0) {
            speed.Components[0] *= CollisionOtherDimension; // dampen only
            speed.Components[1] *= -CollisionElasticity; // mirror & dampen
        }
        // X - horizontal
        if (center.Components[0] <= radius && speed.Components[0] < 0
            || center.Components[0] >= 640 - radius && speed.Components[0] > 0) {
            speed.Components[0] *= -CollisionElasticity; // mirror & dampen
            speed.Components[1] *= CollisionOtherDimension; // dampen only
        }

        // Air resistance is approximately proportional to speed^2 and diameter.
        double radius_si = shape.Radius / PixelsPerMeter;
        double dragForce = DragCoefficient * radius_si * Medium.Density * speed.Magnitude * speed.Magnitude;
        double dragAcceleration = dragForce / sphere.Mass;
        double factor = 2; // why? looks better!
        double dragMagnitude = elapsedSeconds * dragAcceleration / factor;
        double dragMultiplier = Math.Max(0, 1 - dragMagnitude / speed.Magnitude);
        speed *= dragMultiplier;

        // Gravity
        double gravity = 9.81;
        double gravitationalForce = sphere.Mass * gravity;
        // Buoyancy
        double buoyancy = sphere.Volume * Medium.Density; // kg
        double buoyancyForce = buoyancy * gravity;
        // Total of gravitation and buoyancy
        double totalForce = gravitationalForce - buoyancyForce;
        speed.Components[1] += elapsedSeconds * totalForce / sphere.Mass;

        // Compute translation vector (pixels) from speed vector (metric)
        Vector translation = speed * elapsedSeconds * PixelsPerMeter;
        shape.Move(translation);
        sphere.Speed = speed;

        // Clamp to surface
        shape.Center.Components[0] = Math.Max(shape.Radius, shape.Center.Components[0]);
        shape.Center.Components[0] = Math.Min(640 - shape.Radius, shape.Center.Components[0]);
        shape.Center.Components[1] = Math.Max(shape.Radius, shape.Center.Components[1]);
        shape.Center.Components[1] = Math.Min(480 - shape.Radius, shape.Center.Components[1]);
    }
}
