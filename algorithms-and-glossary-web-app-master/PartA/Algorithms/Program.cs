using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartA
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintReversedString();

            PrintFifthLastItemLinkedList();

            Console.ReadKey();
        }

        private static void PrintReversedString()
        {
            var stringUtil = new StringUtil();
            const string stringToReverse = "cat and dog";
            Console.WriteLine($"Reversed string for {stringToReverse} is {stringUtil.Reverse(stringToReverse)}");
        }

        private static void PrintFifthLastItemLinkedList()
        {
            var customLinkedList = new CustomLinkedList<int>();
            customLinkedList.Add(2);
            customLinkedList.Add(3);
            customLinkedList.Add(4);
            customLinkedList.Add(5);
            customLinkedList.Add(6);
            customLinkedList.Add(7);
            customLinkedList.Add(8);
            customLinkedList.Add(9);
            customLinkedList.Add(10);
            customLinkedList.Add(11);

            Console.WriteLine($"5th element from the tail end is {customLinkedList.GetFifthElementFromTailEnd()}");
        }
    }

}
