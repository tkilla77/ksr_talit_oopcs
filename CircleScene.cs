namespace geometry;

public class CircleScene : Scene {
    private const double ScreenResolutionPpi = 163; // PixelsPerInch = PPI
    // 1 Inch = 2.54cm
    private const double PixelsPerMeter = ScreenResolutionPpi / 2.54 * 100;
    // Fraction of momentum maintained in collision direction.
    private const double CollisionElasticity = 0.9; // Preserve 90% of momentum.
    // Fraction of momentum maintained in non-collision direction.
    private const double CollisionOtherDimension = 0.995;
    // Air resistanc coefficient in s^2/(m^2*pixel)
    private const double AirResistanceFactor = 1e-4;

    private Circle circle;
    private Vector speed = new Vector(0.5, 0); // m/s
    public CircleScene() {
        figures.Add(new Square(new Vector(300, 300), new Vector(50, 0)));
        circle = new Circle(new Vector(50, 50), new Vector(30, 0));
        circle.SetColors("000", "a0f0");
        figures.Add(circle);
    }

    public override void Update(double totalSeconds, double elapsedSeconds) {
        // Wall collision
        Vector center = circle.Center;
        double radius = circle.Radius;
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

        // Gravity
        speed.Components[1] += elapsedSeconds * 9.81;

        // Air resistance is proportional to speed and diameter.
        double airResistanceForce = AirResistanceFactor * circle.Radius * 2 * speed.Magnitude * speed.Magnitude;
        // Ignore mass of circle and just treat this a speed factor
        double airResistance = airResistanceForce / speed.Magnitude;
        speed *= (1 - airResistance);

        // Compute translation vector (pixels) from speed vector (metric)
        // Adjust by a factor of 5 because reasons...
        Vector translation = speed * elapsedSeconds * (PixelsPerMeter / 5);
        circle.Move(translation);

        // Clamp to surface
        circle.Center.Components[0] = Math.Max(circle.Radius, circle.Center.Components[0]);
        circle.Center.Components[0] = Math.Min(640 - circle.Radius, circle.Center.Components[0]);
        circle.Center.Components[1] = Math.Max(circle.Radius, circle.Center.Components[1]);
        circle.Center.Components[1] = Math.Min(480 - circle.Radius, circle.Center.Components[1]);
    }
}
