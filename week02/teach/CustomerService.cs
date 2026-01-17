/// <summary>
/// Maintain a Customer Service Queue. Allows new customers to be
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    public static void Run() {
        Console.WriteLine("Customer Service Queue Tests");

        // Test 1: Invalid queue size
        Console.WriteLine("Test 1: Invalid Queue Size");
        var cs1 = new CustomerService(0);
        Console.WriteLine(cs1);
        Console.WriteLine("=================");

        // Test 2: Queue full
        Console.WriteLine("Test 2: Queue Full");
        var cs2 = new CustomerService(1);
        cs2.AddNewCustomer();
        cs2.AddNewCustomer(); // should show error
        Console.WriteLine(cs2);
        Console.WriteLine("=================");

        // Test 3: Serve from empty queue
        Console.WriteLine("Test 3: Serve From Empty Queue");
        var cs3 = new CustomerService(2);
        cs3.ServeCustomer(); // should show error
        Console.WriteLine("=================");
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        _maxSize = maxSize <= 0 ? 10 : maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// </summary>
    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId}) : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.
    /// Put the new record into the queue.
    /// </summary>
    private void AddNewCustomer() {
        // FIX #1: Correct full-queue check
        if (_queue.Count >= _maxSize) {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();

        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();

        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer() {
        // FIX #2: Empty queue protection
        if (_queue.Count == 0) {
            Console.WriteLine("No Customers in Queue.");
            return;
        }

        // FIX #3: Retrieve before removing
        var customer = _queue[0];
        _queue.RemoveAt(0);
        Console.WriteLine(customer);
    }

    /// <summary>
    /// String representation of the queue (for debugging).
    /// </summary>
    public override string ToString() {
        return $"[size={_queue.Count} max_size={_maxSize} => " +
               string.Join(", ", _queue) + "]";
    }
}
