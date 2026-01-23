using System;
using System.Collections;
using System.Collections.Generic;

public class BinarySearchTree : IEnumerable<int>
{
    private class Node
    {
        public int Data { get; set; }
        public Node? Left { get; set; }
        public Node? Right { get; set; }

        public Node(int data)
        {
            Data = data;
        }

        // Problem 1: Insert Unique Values Only
        public bool Insert(int value)
        {
            if (value == Data)
            {
                // Duplicate found - don't insert
                return false;
            }
            
            if (value < Data)
            {
                if (Left == null)
                {
                    Left = new Node(value);
                    return true;
                }
                else
                {
                    return Left.Insert(value);
                }
            }
            else
            {
                if (Right == null)
                {
                    Right = new Node(value);
                    return true;
                }
                else
                {
                    return Right.Insert(value);
                }
            }
        }

        // Problem 2: Contains
        public bool Contains(int value)
        {
            if (value == Data)
            {
                return true;
            }
            
            if (value < Data)
            {
                if (Left == null)
                {
                    return false;
                }
                return Left.Contains(value);
            }
            else
            {
                if (Right == null)
                {
                    return false;
                }
                return Right.Contains(value);
            }
        }

        // Problem 4: Get Height
        public int GetHeight()
        {
            int leftHeight = Left?.GetHeight() ?? 0;
            int rightHeight = Right?.GetHeight() ?? 0;
            
            return 1 + Math.Max(leftHeight, rightHeight);
        }
    }

    private Node? _root;

    /// <summary>
    /// Insert a new node in the BST.
    /// </summary>
    public void Insert(int value)
    {
        // Create new node
        Node newNode = new(value);
        
        // If the list is empty, then point both head and tail to the new node.
        if (_root is null)
        {
            _root = newNode;
        }
        // If the list is not empty, then only head will be affected.
        else
        {
            // Problem 1: The Insert method in Node returns false for duplicates
            // We don't need to use the return value, but duplicates won't be inserted
            _root.Insert(value);
        }
    }

    /// <summary>
    /// Check to see if the tree contains a certain value
    /// </summary>
    /// <param name="value">The value to look for</param>
    /// <returns>true if found, otherwise false</returns>
    public bool Contains(int value)
    {
        return _root != null && _root.Contains(value);
    }

    /// <summary>
    /// Yields all values in the tree
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        // call the generic version of the method
        return GetEnumerator();
    }

    /// <summary>
    /// Iterate forward through the BST
    /// </summary>
    public IEnumerator<int> GetEnumerator()
    {
        var numbers = new List<int>();
        TraverseForward(_root, numbers);
        foreach (var number in numbers)
        {
            yield return number;
        }
    }

    private void TraverseForward(Node? node, List<int> values)
    {
        if (node is not null)
        {
            TraverseForward(node.Left, values);
            values.Add(node.Data);
            TraverseForward(node.Right, values);
        }
    }

    /// <summary>
    /// Iterate backward through the BST.
    /// </summary>
    public IEnumerable Reverse()
    {
        var numbers = new List<int>();
        TraverseBackward(_root, numbers);
        foreach (var number in numbers)
        {
            yield return number;
        }
    }

    // Problem 3: Traverse Backwards
    private void TraverseBackward(Node? node, List<int> values)
    {
        if (node is not null)
        {
            // Right subtree first (larger values)
            TraverseBackward(node.Right, values);
            
            // Current node
            values.Add(node.Data);
            
            // Left subtree last (smaller values)
            TraverseBackward(node.Left, values);
        }
    }

    /// <summary>
    /// Get the height of the tree
    /// </summary>
    public int GetHeight()
    {
        if (_root is null)
            return 0;
        return _root.GetHeight();
    }

    public override string ToString()
    {
        return "<Bst>{" + string.Join(", ", this) + "}";
    }
}

public static class IntArrayExtensionMethods {
    public static string AsString(this IEnumerable array) {
        return "<IEnumerable>{" + string.Join(", ", array.Cast<int>()) + "}";
    }
}
