namespace DecisionTreePath;

using System;
using System.Collections.Generic;

public static class DecisionTreePath
{
    /// <summary>
    /// Returns the L/R path from root (index 0) to the leaf with the lowest value.
    /// </summary>
    public static string LowestValueLeafPath(int[] arr)
    {
        if (arr is null) throw new ArgumentNullException(nameof(arr));
        if (arr.Length == 0) return string.Empty;

        if (!TryFindMinLeafIndex(arr.AsSpan(), out var minLeafIndex))
            return string.Empty;

        return BuildPathFromRoot(minLeafIndex);
    }

    private static bool TryFindMinLeafIndex(ReadOnlySpan<int> data, out int minLeafIndex)
    {
        minLeafIndex = -1;
        var minValue = int.MaxValue;
        var n = data.Length;

        for (int i = 0; i < n; i++)
        {
            if (!IsLeaf(i, n)) continue;
            if (data[i] >= minValue) continue;

            minValue = data[i];
            minLeafIndex = i;
        }

        return minLeafIndex != -1;
    }

    private static bool IsLeaf(int index, int length)
    {
        var left = (index * 2) + 1;
        var right = left + 1;
        return left >= length && right >= length;
    }

    private static string BuildPathFromRoot(int index)
    {
        if (index == 0) return string.Empty;

        var steps = new Stack<char>();

        while (index > 0)
        {
            var parent = GetParent(index);
            steps.Push(IsLeftChildOf(index, parent) ? 'L' : 'R');
            index = parent;
        }

        return new string(steps.ToArray());
    }

    private static int GetParent(int index) => (index - 1) / 2;

    private static bool IsLeftChildOf(int childIndex, int parentIndex)
        => (parentIndex * 2) + 1 == childIndex;
}
