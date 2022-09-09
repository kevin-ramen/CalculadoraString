using System.Text.RegularExpressions;
namespace StringCalculator
{
    public class Calc
    {
        public List<double> CreateDoubleList(string numbers, dynamic delimiter)
        {
            List<double> addition = new List<double>();
            List<double> negatives = new List<double>();
            var splitString = numbers.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);

            foreach (var number in splitString)
            {
                //Check if its a negative number
                if (Regex.IsMatch(number, @"^-\d*"))
                {
                    negatives.Add(Convert.ToDouble(number));
                    continue;
                }
                //Check if its greater than 1000
                double num = Convert.ToDouble(number);
                if (num > 1000)
                    continue;
                addition.Add(num);
            }
            if (negatives.Count() != 0)
                throw new InvalidOperationException("Only Positive numbers are accepted:\n" + negatives.ToString());
            return addition;
        }

        public double add(string numbers)
        {
            //Empty = 0
            if (numbers.Equals("")) 
                return 0.0;

            List<double> list = new List<double>();
            string pattern = @"(?:\/\/.\n)";

            //Has the conditional pattern to use a custom delimiter
            if (Regex.IsMatch(numbers, pattern))
            {
                //Removing the conditional
                char delimiter = numbers[2];
                numbers = numbers.Remove(0, 4);
                bool useDelimiter = true;
                for (int i = 0; i < numbers.Length; i++)
                {
                    if (Char.IsDigit(numbers[i]) || numbers[i].Equals(delimiter))
                    {
                        continue;
                    }
                    useDelimiter = false;
                }
                if (useDelimiter) //Uses only the custom delimiter
                {
                    list = CreateDoubleList(numbers, delimiter);
                    return list.Sum();
                }
                else 
                    throw new InvalidOperationException("Use of different custom delimiters");
            }
            
            //Uses , or \n as delimiters
            list = CreateDoubleList(numbers, new char[] { ',', '\n' });
            return list.Sum();
        }

    }
}