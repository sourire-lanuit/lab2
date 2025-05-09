using System;

class Program
{
    static int Main(string[] args)
    {
        try
        {
            string? word = null;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-word" && i + 1 < args.Length)
                {
                    word = args[i + 1];
                    i++;
                }
            }

            if (string.IsNullOrWhiteSpace(word))
            {
                Console.Error.WriteLine("Error: Missing -word argument.");
                return 1;
            }
            
            string inputText = Console.In.ReadToEnd();

            int count = TextSearcher.CountOccurrences(inputText, word);
            Console.WriteLine(count);

            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
            return 2;
        }
    }
}
