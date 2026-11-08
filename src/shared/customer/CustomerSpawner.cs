using AprozarUnion.Shared.Level.Store;
using Godot;

namespace AprozarUnion.Shared.Customer;

public class CustomerSpawner
{
    private int betweenInterval = 60;
    private Queue queue;

    public CustomerSpawner(Queue queue)
    {
        this.queue = queue;
    }
    
    public void tick()
    {
        if (this.queue.IsFull)
        {
            return;
        }
        
        betweenInterval--;

        if (betweenInterval < 0)
        {
            GD.Print("Spawned");
            betweenInterval = GD.RandRange(40, 120);
        }
    }
}