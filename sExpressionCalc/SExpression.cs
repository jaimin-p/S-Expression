using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sExpressionCalc
{
    public static class SExpression
    {
        public static Dictionary<string, int> pre_calculated = new Dictionary<string, int> { };
        public static string str_input { get; private set ;}

        // str_input :  input string
        public static int calc(string str_input)
        {
            //check for empty or null input
            if (!string.IsNullOrEmpty(str_input))
            {
                //check if its integer 
                int n;
                if(int.TryParse(str_input, out n))
                {
                    return n;
                }

                // check for balaced parenthesis
                if (IsBalanced(str_input))
                {
                    while (str_input.Contains(")"))
                    {
                        // if its already calculated previously return answer
                        if (pre_calculated.Keys.Contains(str_input))
                        {
                            return pre_calculated[str_input];
                        }

                        // find first index of closing parenthesis
                        var right_bound = str_input.IndexOf(")");

                        // find relevant pair of opening paranthesis for previous opening parenthesis
                        var left_bound = HighestIndexOf(str_input.Substring(0,right_bound), "(");

                        int length = right_bound - left_bound;

                        //get expression inside that pair and evaluate it
                        var value = evaluate_single(str_input.Substring((left_bound + 1), length - 1));

                        //if opening parenthesis is at zero index , that means no nested value left for calculation
                        if (left_bound == 0)
                        {
                            return value;
                        }

                        //replace expression with value and continue
                        else
                        {
                            str_input = str_input.ReplaceAt(left_bound, length + 1, value.ToString());
                        }
                    }
                }
                else
                {
                    // invlid format
                    throw new FormatException();
                }
            }
            else
            {
                // null or empty value
                throw new FormatException();
            }

            return Convert.ToInt32(str_input);
        }

        // str_formatted : formatted input expression <FUNCTION EXPR EXPR>
        public static int evaluate_single(string str_formatted)
        {
            if (pre_calculated.Keys.Contains(str_formatted))
            {
                return pre_calculated[str_formatted];
            }

            int answer;
            // split expression into array of string
            var pieces = str_formatted.Split(' ');

            //first element indicates operator , second is para1 , thrid is para2
            if (pieces[0].ToLower() == "add")
            {
                 answer = Convert.ToInt32(pieces[1]) + Convert.ToInt32(pieces[2]);
            }

            else if (pieces[0].ToLower() == "multiply")
            {
                 answer = Convert.ToInt32(pieces[1]) * Convert.ToInt32(pieces[2]);
            }

            else
            {
                answer = Convert.ToInt32(str_formatted);
            }

            pre_calculated[str_formatted] = answer;
            return answer;

        }




        //// str - the source string
        //// index- the start location to replace at (0-based)
        //// length - the number of characters to be removed before inserting
        //// replace - the string that is replacing characters
        public static string ReplaceAt(this string str, int index, int length, string replace)
        {
            return str.Remove(index, Math.Min(length, str.Length - index)).Insert(index, replace);
        }

        // Find Max Index in given string
        public static int HighestIndexOf(this string str, string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");
            List<int> indexes = new List<int>();
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    return indexes.Max();
                indexes.Add(index);
            }
        }

        //Check For Balaced Parenthesis
        public static bool IsBalanced(string input)
        {
            if(string.IsNullOrEmpty(input))
            {
                return false;
            }

            else if(!input.ToLower().Contains("("))
            {
                return false;
            }

            Dictionary<char, char> bracketPairs = new Dictionary<char, char>() {{ '(', ')' }, };

            Stack<char> brackets = new Stack<char>();

            try
            {
                // Iterate through each character in the input string
                foreach (char c in input)
                {
                    // check if the character is one of the 'opening' brackets
                    if (bracketPairs.Keys.Contains(c))
                    {
                        // if yes, push to stack
                        brackets.Push(c);
                    }
                    else
                        // check if the character is one of the 'closing' brackets
                        if (bracketPairs.Values.Contains(c))
                    {
                        // check if the closing bracket matches the 'latest' 'opening' bracket
                        if (c == bracketPairs[brackets.First()])
                        {
                            brackets.Pop();
                        }
                        else
                            // if not, its an unbalanced string
                            return false;
                    }
                    else
                        // continue looking
                        continue;
                }
            }
            catch
            {
                // an exception will be caught in case a closing bracket is found, 
                // before any opening bracket.
                // that implies, the string is not balanced. Return false
                return false;
            }

            // Ensure all brackets are closed
            return brackets.Count() == 0 ? true : false;
        }
    }
}
