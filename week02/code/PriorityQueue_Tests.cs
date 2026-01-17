using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.
// Defect(s) Found: 
// - Dequeue() method didn't actually remove items from queue (missing _queue.RemoveAt)
// - For loop had wrong bounds (_queue.Count - 1 instead of _queue.Count)  
// - Used >= comparison which broke FIFO order for same priorities
// - Needed > comparison to maintain insertion order for same priorities
[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add items with different priorities and dequeue them
    // Expected Result: Highest priority items are dequeued first
    // Defect(s) Found: 
    public void TestPriorityQueue_BasicPriority()
    {
        var priorityQueue = new PriorityQueue();

        // Add items with different priorities
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("High", 3);
        priorityQueue.Enqueue("Medium", 2);

        // Highest priority should come out first
        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Add multiple items with same highest priority
    // Expected Result: Items with same priority are dequeued in FIFO order
    // Defect(s) Found: 
    public void TestPriorityQueue_SamePriorityFIFO()
    {
        var priorityQueue = new PriorityQueue();

        // Add multiple items with same high priority
        priorityQueue.Enqueue("First", 5);
        priorityQueue.Enqueue("Second", 5);
        priorityQueue.Enqueue("Third", 3); // Lower priority

        // Same priority items should come out in FIFO order
        Assert.AreEqual("First", priorityQueue.Dequeue());
        Assert.AreEqual("Second", priorityQueue.Dequeue());
        Assert.AreEqual("Third", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Try to dequeue from empty queue
    // Expected Result: InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: 
    public void TestPriorityQueue_EmptyQueueException()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail($"Unexpected exception of type {e.GetType()} caught: {e.Message}");
        }
    }

    [TestMethod]
    // Scenario: Complex mix of priorities and same-priority items
    // Expected Result: Correct priority order with FIFO for same priorities
    // Defect(s) Found: 
    public void TestPriorityQueue_ComplexScenario()
    {
        var priorityQueue = new PriorityQueue();

        // Add items in mixed order
        priorityQueue.Enqueue("A", 2);
        priorityQueue.Enqueue("B", 5); // Highest priority
        priorityQueue.Enqueue("C", 1); // Lowest priority
        priorityQueue.Enqueue("D", 5); // Same high priority as B
        priorityQueue.Enqueue("E", 3);
        priorityQueue.Enqueue("F", 5); // Same high priority as B and D

        // Expected order: B (first high), D (second high), F (third high), E, A, C
        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("D", priorityQueue.Dequeue());
        Assert.AreEqual("F", priorityQueue.Dequeue());
        Assert.AreEqual("E", priorityQueue.Dequeue());
        Assert.AreEqual("A", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Negative priorities
    // Expected Result: Higher numbers have higher priority (negative numbers are low priority)
    // Defect(s) Found: 
    public void TestPriorityQueue_NegativePriorities()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Low", -5);
        priorityQueue.Enqueue("Medium", 0);
        priorityQueue.Enqueue("High", 2);

        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    // Add more test cases as needed below.
}

// Defect(s) Found: Loop condition (_queue.Count - 1) prevented checking the last item in the queue

// Defect(s) Found: Using >= instead of > caused FIFO order to be violated for items with same priority

// Defect(s) Found: Items were not actually removed from the queue after dequeuing

// etc...