using geometry;

using (Canvas canvas = new Canvas()) {
//    canvas.Line(new Vector(100,100), new Vector(200,200));

    Figure triangle = new Polygon(new Vector(100, 100), new Vector(100, 200), new Vector(200, 100));
    triangle.Draw(canvas);

    Figure square = new Square(new Vector(300,300), new Vector(100, 50));
    square.Draw(canvas);

    canvas.Save("example.png");
}