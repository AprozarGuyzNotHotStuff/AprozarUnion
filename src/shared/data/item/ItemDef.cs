namespace AprozarUnion.Shared.Data.Item;

public sealed class ItemDef
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public int PriceBani { get; set; }
    public string Unit { get; set; } = "";
    public int DayStart { get; set; }
    public int DayEnd { get; set; }
}