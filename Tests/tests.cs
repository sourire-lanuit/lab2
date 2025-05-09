using Xunit;

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
