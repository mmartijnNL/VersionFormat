using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace VersionFormat
{
    public static class StringExtensions
    {
        public static List<string> StringSections(this string input, char startChar = '{', char endChar = '}')
        {
            List<string> sections = new List<string>();
            Regex regex = new Regex(@"" + startChar + ".*?" + endChar + "");
            MatchCollection matches = regex.Matches(input);
            foreach (Match match in matches)
            {
                sections.Add(match.Value);
            }
            return sections;
        }
    }
}