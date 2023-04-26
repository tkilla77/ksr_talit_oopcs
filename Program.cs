using geometry;

Canvas canvas = new Canvas();
canvas.Line(new Vector(100,100), new Vector(200,200));

Figure triangle = new Triangle(new Vector(100, 100), new Vector(100, 200), new Vector(200, 100));
triangle.Draw(canvas);

canvas.Save("example.png");