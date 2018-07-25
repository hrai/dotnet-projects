using System.Text;

namespace PartA
{
    public class StringUtil
    {
        public string Reverse(string input)
        {
            var sb = new StringBuilder();
            foreach (var inputChar in input)
            {
                sb.Insert(0, inputChar);
            }

            return sb.ToString();
        }
    }
}
