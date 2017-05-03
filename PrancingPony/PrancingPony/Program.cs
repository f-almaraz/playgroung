using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PrancingPony
{
    class Program
    {
        private static Dictionary<string, DwarfCombination> masterList = new Dictionary<string, DwarfCombination>();
        static void Main(string[] args)
        {
            string[] names = { "Gimli", "Fili", "Ilif", "Ilmig", "Mark" };
            //string[] names = { "A", "B", "C", "D", "E" };

            getItemCombinations(names, 0, 2);

            Console.WriteLine(string.Format("{0} Scenario - We are all happy and drunk.", string.Join(string.Empty, names)));
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("--------------------------------------------------");

            foreach (string item in masterList.Keys)
            {
                Console.WriteLine(string.Format("{0} Scenario - {1}", string.Join(string.Empty, item), masterList[item].Notes));
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("--------------------------------------------------");
                nameCombinations(masterList[item].Names, 0, masterList[item].Names.Length-1);
            }

            Console.ReadLine();
        }

        private static bool isPalindrome(string[] names)
        {
            string value = string.Join(string.Empty, names).ToLower();
            return value.SequenceEqual(value.Reverse());
        }

        private static void nameCombinations(string[] names, int start, int end)
        {
            if (start == end)
            {
                Console.WriteLine(string.Format("{0} = palindrome {1}", string.Join(string.Empty, names), isPalindrome(names)));
            }
            else
            {
                for (int i = start; i <= end; i++)
                {
                    swap2(names, start, i);
                    nameCombinations(names, start + 1, end);
                    swap2(names, start, i);
                }
            }
        }

        private static void swap2(string[] array, int start, int end)
        {
            string temp;

            temp = array[start];
            array[start] = array[end];
            array[end] = temp;
            return;
        }

        private static void getItemCombinations(string[] names, int start, int minLength)
        {
            if (names.Length <= minLength)
            {
                return;
            }
            for (int i = start; i <= names.Length-1; i++)
            {
                string[] tnames = RemoveItemAt(names, i);
                DwarfCombination dc = new DwarfCombination()
                {
                    Names = tnames,
                    Notes = string.Format("The Smaug joins the party and only {0} survive.", string.Join(",", tnames))
                };
                masterList[string.Join(string.Empty, tnames)] = dc;
                getItemCombinations(tnames, 0, minLength);
            }
        }

        private static string[] RemoveItemAt(string[] names, int index)
        {
            if (index >= names.Length)
            {
                return names;
            }
            string[] result = new string[names.Length - 1];
            Array.Copy(names, 0, result, 0, index);
            if (index < names.Length - 1) { 
                Array.Copy(names, index + 1, result, index, (names.Length - (index + 1)));
            }
            return result;
        }
    }

    public class DwarfCombination
    {
        public string[] Names { get; set; }

        public string Notes { get; set; }

    }
}

