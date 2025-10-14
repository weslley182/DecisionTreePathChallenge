# DecisionTreePath – Lowest-Value Leaf Path (C#)

## Problem
Given a balanced binary tree stored in an array (`root = index 0`, `left = 2*i+1`, `right = 2*i+2`), return the path from root to the **leaf with the lowest value** as a string of `L`/`R`.

- If array is empty → return `""`.
- Exactly one minimum among leaves is assumed.

## Approach (O(n))
1. Scan array; consider only leaf indices (no children in-bounds).
2. Track the minimum leaf value and its index.
3. Reconstruct path from leaf to root using parent index; push `L` or `R` into a stack; return reversed as string.

Time: `O(n)` scan + `O(log n)` path build.  
Space: `O(log n)` for the stack.

---

## Code
```csharp
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
```

---

## Line-by-Line Explanation

### Namespace & Usings
- `namespace DecisionTreePath;` — isolates the library scope.
- `using System;` — `ArgumentNullException`, `ReadOnlySpan<T>`.
- `using System.Collections.Generic;` — `Stack<char>`.

### Class
- `public static class DecisionTreePath` — static utility class; no state.

### Public API
```csharp
public static string LowestValueLeafPath(int[] arr)
{
    if (arr is null) throw new ArgumentNullException(nameof(arr));      // guard: null input
    if (arr.Length == 0) return string.Empty;                           // guard: empty => no path

    if (!TryFindMinLeafIndex(arr.AsSpan(), out var minLeafIndex))       // find min-leaf index
        return string.Empty;                                            // no leaf found (defensive)

    return BuildPathFromRoot(minLeafIndex);                             // build L/R path
}
```

### Scan for minimum leaf
```csharp
private static bool TryFindMinLeafIndex(ReadOnlySpan<int> data, out int minLeafIndex)
{
    minLeafIndex = -1;                         // sentinel for "not found"
    var minValue = int.MaxValue;               // current best value
    var n = data.Length;                       // cache length

    for (int i = 0; i < n; i++)                // linear scan O(n)
    {
        if (!IsLeaf(i, n)) continue;           // skip non-leaf nodes

        if (data[i] >= minValue) continue;     // not better than current min

        minValue = data[i];                    // update best
        minLeafIndex = i;                      // track index
    }

    return minLeafIndex != -1;                 // true if found
}
```

### Leaf detection
```csharp
private static bool IsLeaf(int index, int length)
{
    var left = (index * 2) + 1;                // left child index
    var right = left + 1;                      // right child index
    return left >= length && right >= length;  // both out-of-bounds => leaf
}
```

### Build path from root to leaf
```csharp
private static string BuildPathFromRoot(int index)
{
    if (index == 0) return string.Empty;       // root itself => empty path

    var steps = new Stack<char>();             // reverse collector

    while (index > 0)                          // climb to root
    {
        var parent = GetParent(index);         // compute parent
        steps.Push(IsLeftChildOf(index, parent) ? 'L' : 'R'); // L if left-child, else R
        index = parent;                        // move up
    }

    return new string(steps.ToArray());        // reverse to root→leaf order
}
```

### Parent / child helpers
```csharp
private static int GetParent(int index) => (index - 1) / 2;             // inverse of child formula

private static bool IsLeftChildOf(int childIndex, int parentIndex)
    => (parentIndex * 2) + 1 == childIndex;                              // true if matches left formula
```
---

## Notes
- Uses `ReadOnlySpan<int>` to avoid copies; safe and efficient.
- Guards for `null`/empty inputs.
- Deterministic tie behavior: only leaves compete; single minimum assumed by spec.
- Easily testable: unit tests can target `LowestValueLeafPath` with sample arrays.

## Example
Input: `[0,4,2,5,0,9,7,9,-4,2,-5,3,9,1,-11]` → Output: `"RRR"` (to `-11`).
