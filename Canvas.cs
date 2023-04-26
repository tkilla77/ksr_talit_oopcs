namespace geometry;

using SkiaSharp;
using System.IO;

public class Canvas {
    private SKBitmap bitmap;
    private SKCanvas canvas;

    public Canvas() {
        bitmap = new SKBitmap(640, 480);
        canvas = new SKCanvas(bitmap);
    }

    private SKPoint toPoint(Vector v) {
        return new SKPoint((float) v.X(), (float) v.Y());
    }

    public void Line(Vector from, Vector to) {
        SKPaint paint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 5
        };
        canvas.DrawLine(toPoint(from), toPoint(to), paint);
    }

    public void Save(string filename) {
        using (FileStream stream = File.Create(filename)) {
            SKData data = bitmap.Encode(SKEncodedImageFormat.Png, 95);
            ReadOnlySpan<byte> span = data.AsSpan();
            stream.Write(span);
        }
    }
}