using System.Collections.Generic;
using AprozarUnion.Shared.Data.Item;

namespace AprozarUnion.Shared.Customer;

public class Customer
{
    public CustomerPatienceState state { get; set; } = CustomerPatienceState.REGULAR;
    public int patienceLevel { get; set; } = 60;
    public int frustratedThreshold = 30;
    
    public Dictionary<int, ItemDef> requestedItems { get; set; } = new Dictionary<int, ItemDef>();

    public void tick()
    {
        if (this.state == CustomerPatienceState.LEAVING) return;
        
        this.patienceLevel--;

        if (this.patienceLevel <= frustratedThreshold && this.state != CustomerPatienceState.FRUSTRATED)
        {
            this.state = CustomerPatienceState.FRUSTRATED;
        }

        if (this.patienceLevel <= 0)
        {
            leave();
        }
    }

    public void leave()
    {
        this.state = CustomerPatienceState.LEAVING;
    }
}

public enum CustomerPatienceState
{
    REGULAR,
    FRUSTRATED,
    LEAVING
}