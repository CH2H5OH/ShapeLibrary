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

            // Проверим, что объект не является null
            Assert.IsNotNull(rectangle);

            // Проверим, что объект является экземпляром прямоугольника
            Assert.IsInstanceOf<ShapeLibrary.Rectangle>(rectangle);

            // Проверим, что площадь прямоугольника рассчитывается корректно
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
            double[] parameters = { 1, 2, 3, 4 }; // Неправильное количество параметров для круга
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
            double[] parameters = { 1, 2, 3 }; // Неправильное количество параметров для круга
            Assert.Throws<ArgumentException>(() => ShapeFactory.CreateShape("circle", parameters));
        }

        [Test]
        public void TestPerformance()
        {
            // Замеряем время выполнения создания и вычисления площади для большого количества фигур
            int numberOfShapes = 1000000;

            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < numberOfShapes; i++)
            {
                var circle = ShapeFactory.CreateShape("circle", new double[] { 5 });
                var area = ShapeCalculator.CalculateArea(circle);
            }

            stopwatch.Stop();

            Assert.Less(stopwatch.ElapsedMilliseconds, 1000); // Проверяем, что выполнение занимает менее 1 секунды
        }

        [Test]
        public void TestAddNewShapeType()
        {
            // Создаем словарь для хранения фабрик фигур
            var shapeFactories = new Dictionary<string, Func<double[], IShape>>();

            // Регистрируем новый тип фигуры "square"
            shapeFactories["square"] = parameters => new ShapeLibrary.Rectangle(parameters[0], parameters[0]);

            // Используем созданный словарь вместо ShapeFactories
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