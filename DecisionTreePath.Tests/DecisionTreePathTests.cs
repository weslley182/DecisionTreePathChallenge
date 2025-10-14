using Bogus;

namespace DecisionTreePath.Tests;

public class DecisionTreePathTests
{
    [Fact]
    public void LowestValueLeafPath_WhenArrayEmpty_ShouldReturnEmpty()
    {
        var result = DecisionTreePath.LowestValueLeafPath(new int[] { });
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void LowestValueLeafPath_WhenExampleInput_ShouldReturnCorrectPath()
    {
        int[] arr = { 0, 4, 2, 5, 0, 9, 7, 9, -4, 2, -5, 3, 9, 1, -11 };
        var result = DecisionTreePath.LowestValueLeafPath(arr);
        Assert.Equal("RRR", result);
    }

    [Fact]
    public void LowestValueLeafPath_WhenSingleElement_ShouldReturnEmpty()
    {
        var result = DecisionTreePath.LowestValueLeafPath(new[] { 10 });
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void LowestValueLeafPath_WhenGeneratedRandomArray_ShouldNotThrow()
    {
        var faker = new Faker();
        var arr = faker.Random.Int(5, 50);
        var values = new int[arr];
        for (int i = 0; i < arr; i++) values[i] = faker.Random.Int(-100, 100);

        var result = DecisionTreePath.LowestValueLeafPath(values);
        Assert.NotNull(result);
    }
}

