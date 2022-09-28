using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace EasyMoney
{
    public struct Money
    {
        public Tuple<int, int> Value = new(0,0);

        public Money(int integer, int cents) => Value = Load(integer, cents);

        public Money(double value) => Value = Load(value);

        public static implicit operator Money(double value) => new Money(value);

        private Tuple<int, int> Load(int integer, int cents)
        {
            if (cents > 99 || cents < 0) throw new MoneyArgumentException("Cents", cents);
            return new Tuple<int, int>(integer, cents);
        }

        private Tuple<int, int> Load(double value)
        {
            value = Math.Round(value, 2);
            string[] parts = value.ToString("0.00", CultureInfo.InvariantCulture).Split('.');
            return Load(int.Parse(parts[0]), int.Parse(parts[1]));
        }

        public double ToDouble() => double.Parse($"{Value.Item1},{Value.Item2}");

        public string ToString(string money = "$", char pontuation = '.')
        {
            return $"{money} {Value.Item1}{pontuation}{Value.Item2:D2}";
        }

        public static bool operator ==(Money x, Money y) => x.ToDouble() == y.ToDouble();

        public static bool operator !=(Money x, Money y) => !(x.ToDouble() == y.ToDouble());
    }
}

