namespace ShapeLibrary
{
    public class ShapeFactory
    {
        private static readonly Dictionary<string, Func<double[], IShape>> ShapeFactories = new Dictionary<string, Func<double[], IShape>>
    {
        { "circle", parameters => new Circle(parameters[0]) },
        { "rectangle", parameters => new Rectangle(parameters[0], parameters[1]) },
        { "triangle", parameters => new Triangle(parameters[0], parameters[1], parameters[2]) }
    };

        public static IShape CreateShape(string shapeType, params double[] parameters)
        {
            if (shapeType.ToLower() == "triangle" && parameters.Length == 3)
            {
                try
                {
                    return ShapeFactories[shapeType.ToLower()](parameters);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else if (ShapeFactories.TryGetValue(shapeType.ToLower(), out var factory))
            {
                // Проверяем, что тип фигуры - прямоугольник
                if (shapeType.ToLower() == "rectangle")
                {
                    // Для прямоугольника ожидается два параметра
                    if (parameters.All(p => p >= 0 && p <= double.MaxValue) && parameters.Sum() <= double.MaxValue) // Проверяем, что все параметры неотрицательны, не превышают double.MaxValue и их сумма не превышает double.MaxValue
                    {
                        try
                        {
                            return factory(parameters);
                        }
                        catch (Exception)
                        {
                            // Если произошла ошибка при создании фигуры, вернем null
                            return null;
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Invalid parameters for rectangle.");
                    }
                }
                else
                {
                    var expectedParametersCount = factory.Method.GetParameters().Length;

                    // Проверяем, что количество параметров соответствует ожидаемому
                    if (expectedParametersCount == parameters.Length)
                    {
                        try
                        {
                            return factory(parameters);
                        }
                        catch (Exception)
                        {
                            // Если произошла ошибка при создании фигуры, вернем null
                            return null;
                        }
                    }
                    else
                    {
                        throw new ArgumentException($"Invalid number of parameters for {shapeType}. Expected: {expectedParametersCount}, Actual: {parameters.Length}");
                    }
                }
            }
            if (int.TryParse(shapeType, out _) && parameters.Length == 1)
            {
                try
                {
                    return new Circle(parameters[0]);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            throw new ArgumentException("Invalid parameters or shape type.");
        }

    }

}