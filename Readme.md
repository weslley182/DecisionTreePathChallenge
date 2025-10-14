# Decision Tree Path – Problem Statement (with Diagram)

## Task
Return the path to the **lowest-value leaf** in a **balanced binary tree**.

## Input Format
- The tree is provided as an **integer array** in **level-order** (breadth-first).  
- The **root** node is at index **0**.  
- For a node at index `i`:
  - **Left child** is at `2*i + 1`  
  - **Right child** is at `2*i + 2`

## Output Format
- Return a **string** composed only of the characters:
  - `L` — move to the **left** child
  - `R` — move to the **right** child
- The string **describes the path starting from the root** and ending at the leaf that contains the **minimum value among all leaves**.

## Notes
- You may assume the tree is balanced enough to be represented by the given array.
- There will be **exactly one** minimum value among all **leaf nodes** (no ties on leaves).
- If the input array is empty, return an empty string `""`.

---

## Example

**Input array (level-order):**
```
[0, 4, 2, 5, 0, 9, 7, 9, -4, 2, -5, 3, 9, 1, -11]
```

**Leaf nodes (values):**
```
9, -4, 2, -5, 3, 9, 1, -11
```

**Minimum leaf:** `-11` (index 14)

**Path from root to this leaf:** `RRR`  
- 0 → **R** → 2  
- 2 → **R** → 7  
- 7 → **R** → -11

**Return:**
```
"RRR"
```

---

## Tree Diagram (Mermaid)

```mermaid
graph TD
    A0((0))
    A1((4)) -->|L| A0
    A2((2)) -->|R| A0
    
    A3((5)) -->|L| A1
    A4((0)) -->|R| A1
    A5((9)) -->|L| A2
    A6((7)) -->|R| A2
    
    A7((9)) -->|L| A3
    A8((-4)) -->|R| A3
    A9((2)) -->|L| A4
    A10((-5)) -->|R| A4
    A11((3)) -->|L| A5
    A12((9)) -->|R| A5
    A13((1)) -->|L| A6
    A14((-11)) -->|R| A6
    
    %% highlight path RRR
    linkStyle 2 stroke:red,stroke-width:3px
    linkStyle 6 stroke:red,stroke-width:3px
    linkStyle 14 stroke:red,stroke-width:3px
    
    style A0 fill:#ffe6e6,stroke:red,stroke-width:2px
    style A2 fill:#ffe6e6,stroke:red,stroke-width:2px
    style A6 fill:#ffe6e6,stroke:red,stroke-width:2px
    style A14 fill:#ffcccc,stroke:red,stroke-width:3px
```

---

## Clarifications
- “Describing the path starting from the root” means the first character corresponds to the move from index `0` to one of its children, and so on until the target leaf.
- Only **leaf nodes** (nodes without children inside the array bounds) participate in the minimum comparison.
- The array may contain negative values.
