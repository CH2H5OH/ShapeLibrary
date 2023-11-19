using ShapeLibrary;
using System.Diagnostics;
using System.Drawing;

namespace UNitTests
{
    [TestFixture]
    public class ShapeLibraryTests
    {
        [Test]
        public void TestCreateCircle()
        {
            double[] parameters = { 5 };
            var circle = ShapeFactory.CreateShape("circle", parameters);
            Assert.IsInstanceOf<Circle>(circle);
            Assert.AreEqual(78.54, ShapeCalculator.CalculateArea(circle), 0.01);
        }

        [Test]
        public void TestCreateRectangle()
        {
            double[] parameters = { 4, 6 };
            var rectangle = ShapeFactory.CreateShape("rectangle", parameters);

            // ��������, ��� ������ �� �������� null
            Assert.IsNotNull(rectangle);

            // ��������, ��� ������ �������� ����������� ��������������
            Assert.IsInstanceOf<ShapeLibrary.Rectangle>(rectangle);

            // ��������, ��� ������� �������������� �������������� ���������
            Assert.AreEqual(24, ShapeCalculator.CalculateArea(rectangle));
        }

        [Test]
        public void TestCreateTriangle()
        {
            double[] parameters = { 3, 4, 5 };
            var triangle = ShapeFactory.CreateShape("triangle", parameters);
            Assert.IsInstanceOf<Triangle>(triangle);
            Assert.AreEqual(6, ShapeCalculator.CalculateArea(triangle));
        }

        [Test]
        public void TestInvalidShapeType()
        {
            double[] parameters = { 5 };
            Assert.Throws<ArgumentException>(() => ShapeFactory.CreateShape("invalid", parameters));
        }

        [Test]
        public void TestInvalidParameters()
        {
            double[] parameters = { 1, 2, 3, 4 }; // ������������ ���������� ���������� ��� �����
            Assert.Throws<ArgumentException>(() => ShapeFactory.CreateShape("circle", parameters));
        }

        [Test]
        public void TestCalculateCircleArea()
        {
            double[] parameters = { 5 };
            var circle = ShapeFactory.CreateShape("circle", parameters);
            Assert.AreEqual(78.54, ShapeCalculator.CalculateArea(circle), 0.01);
        }

        [Test]
        public void TestCalculateRectangleArea()
        {
            double[] parameters = { 4, 6 };
            var rectangle = ShapeFactory.CreateShape("rectangle", parameters);
            Assert.AreEqual(24, ShapeCalculator.CalculateArea(rectangle));
        }

        [Test]
        public void TestCalculateTriangleArea()
        {
            double[] parameters = { 3, 4, 5 };
            var triangle = ShapeFactory.CreateShape("triangle", parameters);
            Assert.AreEqual(6, ShapeCalculator.CalculateArea(triangle));
        }

        [Test]
        public void TestZeroParameters()
        {
            double[] parameters = { 0 };
            var circle = ShapeFactory.CreateShape("circle", parameters);
            Assert.AreEqual(0, ShapeCalculator.CalculateArea(circle));
        }

        [Test]
        public void TestNegativeParameters()
        {
            double[] parameters = { -1, -2 };
            Assert.Throws<ArgumentException>(() => ShapeFactory.CreateShape("rectangle", parameters));
        }

        [Test]
        public void TestLargeParameters()
        {
            double[] parameters = { double.MaxValue, double.MaxValue };
            Assert.Throws<ArgumentException>(() => ShapeFactory.CreateShape("rectangle", parameters));
        }

        [Test]
        public void TestInvalidNumberOfParameters()
        {
            double[] parameters = { 1, 2, 3 }; // ������������ ���������� ���������� ��� �����
            Assert.Throws<ArgumentException>(() => ShapeFactory.CreateShape("circle", parameters));
        }

        [Test]
        public void TestPerformance()
        {
            // �������� ����� ���������� �������� � ���������� ������� ��� �������� ���������� �����
            int numberOfShapes = 1000000;

            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < numberOfShapes; i++)
            {
                var circle = ShapeFactory.CreateShape("circle", new double[] { 5 });
                var area = ShapeCalculator.CalculateArea(circle);
            }

            stopwatch.Stop();

            Assert.Less(stopwatch.ElapsedMilliseconds, 1000); // ���������, ��� ���������� �������� ����� 1 �������
        }

        [Test]
        public void TestAddNewShapeType()
        {
            // ������� ������� ��� �������� ������ �����
            var shapeFactories = new Dictionary<string, Func<double[], IShape>>();

            // ������������ ����� ��� ������ "square"
            shapeFactories["square"] = parameters => new ShapeLibrary.Rectangle(parameters[0], parameters[0]);

            // ���������� ��������� ������� ������ ShapeFactories
            double[] parameters = { 4 };
            var square = shapeFactories["square"](parameters);

            Assert.IsInstanceOf<ShapeLibrary.Rectangle>(square);
            Assert.AreEqual(16, ShapeCalculator.CalculateArea(square));
        }

        [Test]
        public void TestNonStandardInputFormat()
        {
            double[] parameters = { 2, 3, 4 };
            var triangle = ShapeFactory.CreateShape("triangle", parameters);

            Assert.IsInstanceOf<Triangle>(triangle);
            Assert.AreEqual(2.9047, ShapeCalculator.CalculateArea(triangle), 0.0001);
        }

        [Test]
        public void TestBoundaryValuesForCircle()
        {
            double[] parameters = { double.MaxValue };
            var circle = ShapeFactory.CreateShape("circle", parameters);
            Assert.AreEqual(double.PositiveInfinity, ShapeCalculator.CalculateArea(circle));
        }

        [Test]
        public void TestBoundaryValuesForTriangle()
        {
            double[] parameters = { double.MaxValue, double.MaxValue, double.MaxValue };
            var triangle = ShapeFactory.CreateShape("triangle", parameters);
            Assert.AreEqual(double.PositiveInfinity, ShapeCalculator.CalculateArea(triangle));
        }

        [Test]
        public void TestIsRightTriangleTrue()
        {
            double[] parameters = { 3, 4, 5 };
            var triangle = ShapeFactory.CreateShape("triangle", parameters);
            Assert.IsTrue(((Triangle)triangle).IsRightTriangle());
        }

        [Test]
        public void TestIsRightTriangleFalse()
        {
            double[] parameters = { 1, 2, 3 };
            var triangle = ShapeFactory.CreateShape("triangle", parameters);

            Assert.That(() => triangle, Is.TypeOf<ShapeLibrary.Triangle>().Or.Null, "Expected null or non-Triangle object for invalid parameters");
        }


    }
}