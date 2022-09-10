using StringCalculator;
using System.Text.RegularExpressions;
Calc clc = new Calc();

var regex = Regex.Matches("1,-2,-3,-4,5", @"(?:-\d+)").Cast<Match>()
    .Select(m => m.Value)
    .ToArray();
Console.WriteLine(string.Join(", ", regex));
Console.WriteLine(Regex.Matches("1,-2,-3,-4,5", @"(?:-\d+)").ToString());
//Console.WriteLine(clc.add("1,-2,-3,-4,5"));