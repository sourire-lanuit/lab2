using System;
using System.Text.RegularExpressions;

public static class TextSearcher
{
    public static int CountOccurrences(string text, string word)
    {
        if (text == null) throw new ArgumentNullException(nameof(text));
        if (word == null) throw new ArgumentNullException(nameof(word));

        string pattern = $@"\b{Regex.Escape(word)}\b";
        return Regex.Matches(text, pattern).Count;
    }
}
