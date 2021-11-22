using System.Linq;
using System.Text;

namespace MathPuzzles
{
    public class Iban
    {
        public int CalculateChecksum(string identification, string countryCode)
        {
            var builder = new StringBuilder();
            foreach (var chr in identification.ToUpper().Concat(countryCode.ToUpper()))
            {
                builder.Append((chr >= 65 ? chr - 55 : chr - 48).ToString());
            }
            builder.Append("00");
            return 98 - Modulo(builder.ToString(), 97);
        }

        public bool ValidateChecksum(string iban)
        {
            var builder = new StringBuilder();
            foreach (var chr in iban.ToUpper().Substring(4).Concat(iban.ToUpper().Substring(0, 4)))
            {
                builder.Append((chr >= 65 ? chr - 55 : chr - 48).ToString());
            }
            return Modulo(builder.ToString(), 97) == 1;
        }

        public int Modulo(string number, int div)
        {
            int res = 0;
            for (int i = 0; i < number.Length; i++)
                res = (res * 10 + number[i] - '0') % div;
            return res;
        }
    }
}