using Xunit;
using EasyMoney;
using System.Globalization;

namespace EasyMoneyTest
{
    public class MoneyTests
    {

        [Theory]
        [InlineData(1,100)]
        [InlineData(1,-1)]
        public void InvalidCentsThrows(int integer, int cents)
        {
            try
            {
                Money x = new(integer, cents);
            }
            catch (Exception ex)
            {
                Assert.Equal(typeof(MoneyArgumentException), ex.GetType());
                Assert.Equal($"Cents used with value of {cents} is invalid in finances. ", ex.Message);
            }
        }

        [Theory]
        [InlineData(1.30)]
        [InlineData(291.18)]
        [InlineData(141.39)]
        [InlineData(12.99)]
        [InlineData(12.9923)]
        [InlineData(9.00)]
        public void ConstructorDoubleIsValid(double value)
        {
            Money actual = value;
            var x = value.ToString("0.00", CultureInfo.InvariantCulture).Split(".");
            Assert.Equal(int.Parse(x[0]),actual.Value.Item1);
            Assert.Equal(int.Parse(x[1]), actual.Value.Item2);
        }

        [Theory]
        [InlineData(1.30, 1.30)]
        [InlineData(291.18, 291.18)]
        [InlineData(141.39, 141.39)]
        [InlineData(12.99, 12.99)]
        [InlineData(12.9923, 12.99)]
        [InlineData(9.00, 9)]
        public void ToDoubleIsValid(double value, double expected)
        {
            Money actual = value;
            Assert.Equal(expected, actual.ToDouble());
        }


        [Theory]
        [InlineData(291.18, "R$", ',', "R$ 291,18")]
        [InlineData(141.39, "US$",'.', "US$ 141.39")]
        [InlineData(141.3912, "US$", '.', "US$ 141.39")]
        [InlineData(1299, "¥", '.', "¥ 1299.00")]
        public void ToStringIsValidWithFormatters(double value, string money, char pontuation, string expected)
        {
            Money actual = new(value);
            Assert.Equal(expected, actual.ToString(money, pontuation));
        }

        [Theory]
        [InlineData(291.18, "$ 291.18")]
        [InlineData(141.39, "$ 141.39")]
        [InlineData(141.3912, "$ 141.39")]
        [InlineData(1299, "$ 1299.00")]
        public void ToStringIsValidWithoutFormatters(double value, string expected)
        {
            Money actual = new(value);
            Assert.Equal(expected, actual.ToString());
        }
    }
}