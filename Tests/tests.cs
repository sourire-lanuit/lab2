using Xunit;
using System.Diagnostics;
using System.Text;

public class TextSearcherTests
{
    [Fact]
    public void CountOccurrences_FindsCorrectCount()
    {
        string text = "hello world hello";
        string word = "hello";
        int result = TextSearcher.CountOccurrences(text, word);
        Assert.Equal(2, result);
    }

    [Fact]
    public void CountOccurrences_IsCaseSensitive()
    {
        string text = "Hello hello HeLLo";
        string word = "hello";
        int result = TextSearcher.CountOccurrences(text, word);
        Assert.Equal(1, result);
    }

    [Fact]
    public void CountOccurrences_MatchesWholeWords()
    {
        string text = "hell hello hellooo hello";
        string word = "hello";
        int result = TextSearcher.CountOccurrences(text, word);
        Assert.Equal(2, result);
    }

    [Fact]
    public void CountOccurrences_EmptyText_ReturnsZero()
    {
        string text = "";
        string word = "hello";
        int result = TextSearcher.CountOccurrences(text, word);
        Assert.Equal(0, result);
    }

    [Fact]
    public void CountOccurrences_NullText_Throws()
    {
        Assert.Throws<System.ArgumentNullException>(() => 
            TextSearcher.CountOccurrences(null, "word"));
    }

    [Fact]
    public void CountOccurrences_NullWord_Throws()
    {
        Assert.Throws<System.ArgumentNullException>(() => 
            TextSearcher.CountOccurrences("text", null));
    }
}

public class ProgramIntegrationTests
{
    private const string AppPath = @"..\..\..\..\App\bin\Debug\net9.0\App.exe";

    [Fact]
    public void ReturnsCorrectOutputAndExitCode_OnValidInput()
    {
        var result = RunApp("hello world hello", "-word hello");

        Assert.Equal("2", result.stdout.Trim());
        Assert.Equal(string.Empty, result.stderr);
        Assert.Equal(0, result.exitCode);
    }

    [Fact]
    public void ReturnsErrorAndNonZeroExitCode_WhenNoArguments()
    {
        var result = RunApp("some input", "");

        Assert.Contains("Error", result.stderr);
        Assert.NotEqual(0, result.exitCode);
    }

    [Fact]
    public void ReturnsZeroOccurrences_WhenWordNotFound()
    {
        var result = RunApp("this is a test", "-word hello");

        Assert.Equal("0", result.stdout.Trim());
        Assert.Equal(string.Empty, result.stderr);
        Assert.Equal(0, result.exitCode);
    }

    private (string stdout, string stderr, int exitCode) RunApp(string stdinText, string args)
    {
        var process = new Process();
        process.StartInfo.FileName = AppPath;
        process.StartInfo.Arguments = args;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.CreateNoWindow = true;

        var stdout = new StringBuilder();
        var stderr = new StringBuilder();

        process.OutputDataReceived += (s, e) => { if (e.Data != null) stdout.AppendLine(e.Data); };
        process.ErrorDataReceived += (s, e) => { if (e.Data != null) stderr.AppendLine(e.Data); };

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        using (var writer = process.StandardInput)
        {
            writer.Write(stdinText);
        }

        process.WaitForExit();

        return (stdout.ToString(), stderr.ToString(), process.ExitCode);
    }
}