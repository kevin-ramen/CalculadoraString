using System.Text.RegularExpressions;
namespace StringCalculator
{
    public class Calc
    {
        public List<double> CreateDoubleList(string numbers, dynamic delimiter)
        {
            //Check if its a negative number
            if (Regex.IsMatch(numbers, @"(?:-\d+)"))
            {
                var regex = Regex.Matches(numbers, @"(?:-\d+)").Cast<Match>()
                    .Select(m => m.Value).ToArray();
                throw new InvalidOperationException("Only Positive numbers are accepted:\n"
                    + String.Join(",",regex));
            }
            List<double> addition = new List<double>();
            var splitString = numbers.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
            foreach (var number in splitString)
            {
                //Check if its greater than 1000
                double num = Convert.ToDouble(number);
                if (num > 1000)
                    continue;
                addition.Add(num);
            }
            return addition;
        }

        public (string str, string[] delimiters) RegexManipulation(string numbers,string pattern)
        {
            string conditionalPattern = Regex.Match(numbers, pattern).ToString();//Extracting Conditional pattern from string
            string delimitersString = Regex.Replace(conditionalPattern, @"[(\n)\/]", "").ToString();//string of the form [delimiter][delimiter]...
            string[] delimiters = delimitersString.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);//spliting string 
            numbers = Regex.Replace(numbers, pattern, "");//trim string
            return (str:numbers, delimiters:delimiters);
        }

        public double IsValid(string str, string [] delimiters)
        {
            string regexPattern = "[^";
            foreach (var s in delimiters)//[^.{3}|,|;|\d]
            {
                if (s == delimiters[delimiters.Length - 1])
                {
                    regexPattern = regexPattern + s[0] + "{" + s.Length.ToString() + "}|0-9]";
                    continue;
                }
                regexPattern = regexPattern + s[0] + "{" + s.Length.ToString() + "}|";
            }
            if (Regex.IsMatch(str, regexPattern))
                throw new InvalidOperationException("Custom Delimiter not expected");
            return CreateDoubleList(str, delimiters).Sum();
                
        }
        public double add(string numbers)
        {
            if (numbers.Equals(""))//Empty = 0
                return 0.0;
            if (Regex.IsMatch(numbers, @"(?:\/\/(\[.+\])+\n)"))//Has the conditional pattern to multiple custom delimiters "//[.][.]\n"
            {
                var result = RegexManipulation(numbers, @"(?:\/\/(\[.+\])+\n)");
                return IsValid(result.str,result.delimiters);
            }
            if (Regex.IsMatch(numbers, @"(?:\/\/.+\n)"))//Has the conditional pattern to use a custom delimiter "//.\n"
            {
                //Removing conditionalPattern from string
                var result = RegexManipulation(numbers, @"(?:\/\/.+\n)");
                return IsValid(result.str, result.delimiters);
            }
            return CreateDoubleList(numbers, new char[] { ',', '\n' }).Sum();//Uses , or \n as delimiters
        }
    }
}