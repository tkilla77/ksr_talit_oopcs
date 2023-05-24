namespace geometry;

using SkiaSharp;
using System.IO;

public class Canvas : IDisposable {
    private SKBitmap bitmap;
    private SKCanvas canvas;

    public Canvas() {
        bitmap = new SKBitmap(640, 480);
        canvas = new SKCanvas(bitmap);
        canvas.Clear(SKColors.White);
    }

    private SKPaint MakePaint(SKColor color, SKPaintStyle style) {
        return new SKPaint {
            Style = style,
            StrokeCap = SKStrokeCap.Round,
            StrokeWidth = 5,
            Color = color,
        };
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

    public void Circle(Vector center, double radius, string strokeColor, string fillColor) {
        SKPaint strokePaint = new SKPaint {
            Color = GetColor(strokeColor),
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 5
        };

        canvas.DrawCircle(toPoint(center).X, toPoint(center).Y, (float) radius, strokePaint);
        SKPaint fillPaint = new SKPaint {
            Color = GetColor(fillColor),
            Style = SKPaintStyle.Fill,
        };
        canvas.DrawCircle(toPoint(center).X, toPoint(center).Y, (float) radius, fillPaint);
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
    
    public void Dispose() {
        canvas.Dispose();
        bitmap.Dispose();
    }
}