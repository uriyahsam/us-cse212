using System;
using System.Collections.Generic;
using System.Diagnostics;

public static class Arrays
{
    // PLAN for MultiplesOf:
    // 1. Create an array that can hold 'count' number of values.
    // 2. Loop from 0 up to count - 1.
    // 3. For each position i, store start * (i + 1) in the array.
    // 4. Return the array.

    public static double[] MultiplesOf(double number, int length)
    {
        // Step 1: Create a new array with the specified length
        double[] multiples = new double[length];
        
        // Step 2: Loop through each position in the array
        for (int i = 0; i < length; i++)
        {
            // Step 3: Calculate the multiple and store it in the array
            multiples[i] = number * (i + 1);
        }
        
        // Step 4: Return the completed array
        return multiples;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  
    /// The value of amount will be in the range of 1 to data.Count, inclusive.
    /// This function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // PLAN for RotateListRight:
        // 1. Calculate the split point: data.Count - amount
        // 2. Get the portion from the split point to the end
        // 3. Get the portion from the beginning to the split point
        // 4. Clear the original list
        // 5. Add the end portion first
        // 6. Add the beginning portion second
        
        // Step 1: Calculate where to split the list
        int splitIndex = data.Count - amount;
        
        // Step 2: Get the portion from the split point to the end
        List<int> endPortion = data.GetRange(splitIndex, amount);
        
        // Step 3: Get the portion from the beginning to the split point
        List<int> beginningPortion = data.GetRange(0, splitIndex);
        
        // Step 4: Clear the original list
        data.Clear();
        
        // Step 5: Add the end portion first
        data.AddRange(endPortion);
        
        // Step 6: Add the beginning portion second
        data.AddRange(beginningPortion);
    }
}
