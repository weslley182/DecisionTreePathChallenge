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

    [Fact]
    public void LowestValueLeafPath_WhenLowestValueOnLeftSubtree_ShouldReturnLLL()
    {
        // min = -9 at index 7  => path: 0->1(L)->3(L)->7(L)
        int[] arr = { 0, 4, 2, 5, 0, 9, 7, -9, -4, 2, -5, 3, 9, 1, 8 };
        Assert.Equal("LLL", DecisionTreePath.LowestValueLeafPath(arr));
    }

    [Fact]
    public void LowestValueLeafPath_WhenLowestValueOnRightSubtree_ShouldReturnLLR()
    {
        // min = -8 at index 8  => path: 0->1(L)->3(L)->8(R)  => LLR
        int[] arr = { 0, 4, 2, 5, 0, 9, 7, 9, -8, 2, -5, 3, 9, 1, 11 };
        Assert.Equal("LLR", DecisionTreePath.LowestValueLeafPath(arr));
    }

    [Fact]
    public void LowestValueLeafPath_WhenLowestValueInMiddleSubtree_ShouldReturnRLL()
    {
        // min = -10 at index 11 => path: 0->2(R)->5(L)->11(L) => RLL
        int[] arr = { 0, 4, 2, 5, 0, 9, 7, 9, -4, 2, -5, -10, 9, 1, 11 };
        Assert.Equal("RLL", DecisionTreePath.LowestValueLeafPath(arr));
    }

    [Fact]
    public void LowestValueLeafPath_WhenLowestValueOnDeepLeft_ShouldReturnLLRR()
    {
        // min = -10 at index 18 => path: 0->1(L)->3(L)->8(R)->18(R) => LLRR
        int[] arr = { 0, 4, 2, 5, 0, 9, 7, 9, 2, 5, -8, 3, 9, 1, 6, 4, 5, 2, -10 };
        Assert.Equal("LLRR", DecisionTreePath.LowestValueLeafPath(arr));
    }

    [Fact]
    public void LowestValueLeafPath_WhenAllPositive_ShouldReturnLeftmostPath()
    {
        // min = 10 at index 3 => path: 0->1(L)->3(L) => LL
        int[] arr = { 5, 7, 8, 10, 11, 12, 13 };
        Assert.Equal("LL", DecisionTreePath.LowestValueLeafPath(arr));
    }
}

