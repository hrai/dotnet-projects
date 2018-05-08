using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Fancy.Domain
{
    public class FancyService : IFancyService
    {
        private static readonly ConcurrentDictionary<int, long> fibonacciDictionary = new ConcurrentDictionary<int, long>();

        public long GetFibonacciNumberForPositiveIndex(long index)
        {
            if (index == 0)
                return 0;

            if (index == 1)
                return 1;

            int previousIndex = (int)(index - 1);
            int previous2Index = (int)(index - 2);

            if (!fibonacciDictionary.TryGetValue(previousIndex, out long previousValue))
            {
                previousValue = GetFibonacciNumberForPositiveIndex(previousIndex);
                fibonacciDictionary[previousIndex] = previousValue;
            }

            if (!fibonacciDictionary.TryGetValue(previous2Index, out long previous2Value))
            {
                previous2Value = GetFibonacciNumberForPositiveIndex(previous2Index);
                fibonacciDictionary[previous2Index] = previous2Value;
            }

            return previousValue + previous2Value;
        }

        public long GetFibonacciNumberForNegativeIndex(long index)
        {
            if (index == 0)
                return 0;

            if (index == -1)
                return -1;

            int previousIndex = (int)(index + 1);
            int previous2Index = (int)(index + 2);

            if (!fibonacciDictionary.TryGetValue(previousIndex, out long previousValue))
            {
                previousValue = GetFibonacciNumberForNegativeIndex(previousIndex);
                fibonacciDictionary[previousIndex] = previousValue;
            }

            if (!fibonacciDictionary.TryGetValue(previous2Index, out long previous2Value))
            {
                previous2Value = GetFibonacciNumberForNegativeIndex(previous2Index);
                fibonacciDictionary[previous2Index] = previous2Value;
            }

            return previousValue + previous2Value;
        }

        public string GetTriangleType(int? a, int? b, int? c)
        {
            if (IsValidLength(a) && IsValidLength(b) && IsValidLength(c))
            {
                if (IsEquilateral(a, b, c))
                    return "Equilateral";

                if (IsIsosceles(a.Value, b.Value, c.Value))
                    return "Isosceles";

                if (IsScalene(a.Value, b.Value, c.Value))
                    return "Scalene";
            }

            return "Error";
        }

        private bool IsIsosceles(long a, long b, long c)
        {
            return (a == b && a + b > c) || (b == c && b + c > a) || (c == a && c + a > b);
        }

        private bool IsEquilateral(int? a, int? b, int? c)
        {
            return (a == b && b == c);
        }
        private bool IsScalene(long a, long b, long c)
        {
            if ((a + b >= c && c >= a && a >= b) || (a + c >= b && b >= c && b >= a) ||
                (b + c >= a && a >= b && a >= c))
                return true;

            return false;
        }

        public string GetToken()
        {
            return new Guid("f20a636b-60f5-41c3-a671-c307af5ee56e").ToString();
        }

        public string GetReverseWords(string sentence)
        {
            if (string.IsNullOrWhiteSpace(sentence))
                return "";

            var charArray = sentence.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private bool IsValidLength(int? number)
        {
            return number > 0;
        }
    }
}
