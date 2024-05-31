using System;
using System.Linq;
using System.Text.RegularExpressions;

public class LogParser
{
    public bool IsValidLine(string text) => new Regex(@"^\[(TRC|DBG|INF|WRN|ERR|FTL)]").IsMatch(text);

    public string[] SplitLogLine(string text) => Regex.Matches(text, @"(?<=>|^).*?(?=<|$)").Select(match => match.Value).ToArray();

    public int CountQuotedPasswords(string lines) => 
        lines.Split("\n").Where(match => Regex.IsMatch(match, @"(?i)"".*password.*""")).Count();
    public string RemoveEndOfLineText(string line) => Regex.Replace(line, @"end-of-line\d*", "");

    public string[] ListLinesWithPasswords(string[] lines) => 
        lines.Select(match => 
            Regex.IsMatch(match, @"(?i)password[^ ]+") ? 
            $"{Regex.Match(match, @"(?i)password[^ ]+")}: {match}" : 
            $"--------: {match}")
            .ToArray();
}
