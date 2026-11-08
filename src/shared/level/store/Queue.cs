using System.Collections.Generic;
using AprozarUnion.Shared.Customer;
using Array = System.Array;

namespace AprozarUnion.Shared.Level.Store;

public class Queue
{
    private List<Customer.Customer> customersLine = new();
    
    public const int maxQueueSize = 7;

    public int Size => customersLine.Count;
    
    public bool IsFull => Size >= maxQueueSize;
}