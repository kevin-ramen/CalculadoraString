using StringCalculator;
using System.Text.RegularExpressions;
Calc clc = new Calc();
Console.WriteLine("Holamundo");
Console.WriteLine(clc.add("1,2,3"));
Console.WriteLine(clc.add("//;\n1;2;3"));
Console.WriteLine(Regex.IsMatch("1,2,3", @"[^\d;\0]"));
