using System.Globalization;

namespace EasyMoney
{
    public class Money
    {
        public Tuple<int, int> Value;

        public Money(int integer, int cents)
        {
            Value = Load(integer, cents);
        }

        public Money(double value)
        {
            Value = Load(value);
        }

        public Money(object? obj)
        {
            if (obj == null)
            Value = Load(Convert.ToInt64(obj));
        }

        public double ToDouble()
        {
            string s = $"{Value.Item1},{Value.Item2}";
            return double.Parse(s);        
        }

        private Tuple<int, int> Load(int integer, int cents)
        {
            if (cents > 99 || cents < 0) throw new MoneyArgumentException("Cents", cents);
            return new Tuple<int, int>(integer, cents);
        }

        private Tuple<int, int> Load(double value)
        {
            value = Math.Round(value, 2);
            string[] parts = value.ToString("0.00", CultureInfo.InvariantCulture).Split('.');
            return new Tuple<int, int>(int.Parse(parts[0]), int.Parse(parts[1]));
        }

        public string ToString(string money = "$", char pontuation = '.')
        {
            return $"{money} {Value.Item1}{pontuation}{Value.Item2:D2}";
        }
    }
}

