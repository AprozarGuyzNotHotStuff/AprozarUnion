using AprozarUnion.Shared.Level.Store;

namespace AprozarUnion.shared.level.store;

public class Store
{
    public Queue queue { get; } = new Queue();

    public void tick()
    {
    }
}