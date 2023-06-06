namespace geometry;

using SkiaSharp;
using System.IO;

public class Canvas : IDisposable {
    private SKBitmap bitmap;
    private SKCanvas canvas;

    public Canvas() : this(640, 480) {}
    public Canvas(int width, int height) {
        bitmap = new SKBitmap(640, 480);
        canvas = new SKCanvas(bitmap);
        canvas.Clear(SKColors.White);
    }

    private SKPoint toPoint(Vector v) {
        if (v.Dim != 2) {
            throw new System.Exception("Can only handle 2d points");
        }
        return new SKPoint((float) v.Components[0], (float)v.Components[1]);
    }

    public void Line(Vector from, Vector to) {
        SKPaint paint = new SKPaint {
            Style = SKPaintStyle.StrokeAndFill,
            StrokeCap = SKStrokeCap.Round,
            StrokeWidth = 5
        };
        canvas.DrawLine(toPoint(from), toPoint(to), paint);
    }

    public void Polygon(Vector[] points, string strokeColor, string fillColor) {
        canvas.DrawPath(ToPoints(points), MakeStrokeColor(strokeColor));
        canvas.DrawPath(ToPoints(points), MakeFillColor(fillColor));
    }

    public void Circle(Vector center, double radius, string strokeColor, string fillColor) {
        canvas.DrawCircle(toPoint(center).X, toPoint(center).Y, (float) radius, MakeStrokeColor(strokeColor));
        canvas.DrawCircle(toPoint(center).X, toPoint(center).Y, (float) radius, MakeFillColor(fillColor));
    }

    private SKPath ToPoints(Vector[] points) {
        SKPath path = new SKPath{FillType = SKPathFillType.EvenOdd};
        path.MoveTo(toPoint(points[0]));
        foreach (Vector v in points) {
            path.LineTo(toPoint(v));
        }
        path.Close();
        return path;
    }

    private SKPaint MakeFillColor(string fillColor) {
        return new SKPaint {
            Color = GetColor(fillColor),
            Style = SKPaintStyle.Fill,
        };
    }

    private SKPaint MakeStrokeColor(string strokeColor) {
        return new SKPaint {
            Color = GetColor(strokeColor),
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 5
        };
    }

    private SKColor GetColor(string color) {
        switch (color) {
            case "red" : return SKColors.Red;
            case "black": return SKColors.Black;
            default: return SKColor.Parse(color);
        }
        
    }

    public void Save(string filename) {
        using (FileStream stream = File.Create(filename)) {
            SKData data = bitmap.Encode(SKEncodedImageFormat.Png, 95);
            ReadOnlySpan<byte> span = data.AsSpan();
            stream.Write(span);
        }
    }

    public SKColor[] GetPixels() {
        return bitmap.Pixels;
    }

    public void Clear() {
        canvas.Clear();
        bitmap.Erase(SKColors.White);
    }
    
    public void Dispose() {
        canvas.Dispose();
        bitmap.Dispose();
    }
}