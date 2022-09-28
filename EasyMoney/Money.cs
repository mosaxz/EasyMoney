using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace EasyMoney
{
    public struct Money
    {
        public Tuple<int, int> Value = new(0,0);

        public Money(int integer, int cents) => Value = Load(integer, cents);
        public Money(double value) => Value = Load(value);
        public Money(int value) => Value = Load(Convert.ToInt64(value));
        public Money(decimal value) => Value = Load(Convert.ToInt64(value));

        private static Tuple<int, int> Load(int integer, int cents)
        {
            if (cents > 99 || cents < 0) throw new MoneyArgumentException("Cents", cents);
            return new Tuple<int, int>(integer, cents);
        }

        private static Tuple<int, int> Load(double value)
        {
            value = Math.Round(value, 2);
            string[] parts = value.ToString("0.00", CultureInfo.InvariantCulture).Split('.');
            return Load(int.Parse(parts[0]), int.Parse(parts[1]));
        }

        public double ToDouble() 
        {
            string value2 = (Value.Item2 >= 10 ? Value.Item2.ToString() : "0" + Value.Item2);
            return double.Parse($"{Value.Item1},{value2}");
        }

        public string ToString(string money = "$", char pontuation = '.')
        {
            return $"{money} {Value.Item1}{pontuation}{Value.Item2:D2}";
        }

        public static implicit operator Money(int value) => new(value);
        public static implicit operator Money(double value) => new(value);
        public static implicit operator Money(decimal value) => new(value);
        public static bool operator ==(Money x, Money y) => x.ToDouble() == y.ToDouble();
        public static bool operator !=(Money x, Money y) => !(x.ToDouble() == y.ToDouble());
        public static Money operator +(Money x, Money y) => new(x.ToDouble() + y.ToDouble());
        public static Money operator -(Money x, Money y) => new(x.ToDouble() - y.ToDouble());
        public static Money operator *(Money x, Money y) => new(x.ToDouble() * y.ToDouble());
        public static Money operator /(Money x, Money y) => new(x.ToDouble() / y.ToDouble());
        public override bool Equals(object? obj) => obj is Money money && Equals(money);
        public override int GetHashCode() => HashCode.Combine(Value);
    }
}

