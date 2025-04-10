using ToDo.Services;

namespace ToDoTests
{
    public class MathServiceTests
    {
        private MathService _unitUnderTesting;

        public MathServiceTests()
        {
            // Arrange
            if (_unitUnderTesting == null)
            {
                _unitUnderTesting = new MathService();
            }
        }

        [Fact]
        public void Add()
        {
            int a = 5;
            int b = 3;
            int expected = 8;
            // Act
            int result = _unitUnderTesting.Add(a, b);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Subtract()
        {
            int a = 5;
            int b = 3;
            int expected = 2;
            // Act
            int result = _unitUnderTesting.Subtract(a, b);
            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Multiply()
        {
            int a = 5;
            int b = 3;
            int expected = 15;
            // Act
            int result = _unitUnderTesting.Multiply(a, b);
            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Divide()
        {
            int a = 6;
            int b = 3;
            double expected = 2.0;
            // Act
            double result = _unitUnderTesting.Divide(a, b);
            // Assert
            Assert.Equal(expected, result);
        }
    }
}