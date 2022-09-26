using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMoney
{
    [Serializable]
    public class MoneyArgumentException : ArgumentException
    {
        public MoneyArgumentException() : base() { }

        public MoneyArgumentException(string param, int value) : base($"{param} used with value of {value} is invalid in finances. ") { }

        public MoneyArgumentException(string message) : base(message) { }

        public MoneyArgumentException(string message, Exception innerException) : base(message, innerException) { }
    }
}
