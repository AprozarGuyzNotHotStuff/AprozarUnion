using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Godot;

namespace AprozarUnion.Shared.Data.Item;

public static class ItemDatabase
{
    private static List<ItemDef> _items = new();
    private static readonly JsonSerializerOptions Opts = new() { PropertyNameCaseInsensitive = true };

    public static IReadOnlyList<ItemDef> All => _items;

    public static void Load(string path = "res://data/items/items.json")
    {
        using var file = Godot.FileAccess.Open(path, Godot.FileAccess.ModeFlags.Read);
        if (file == null) { GD.PushError($"Cannot open {path}"); return; }
        _items = JsonSerializer.Deserialize<List<ItemDef>>(file.GetAsText(), Opts) ?? new();
    }

    public static IEnumerable<ItemDef> AvailableOn(int day) =>
        _items.Where(i => day >= i.DayStart && day <= i.DayEnd);

    public static ItemDef? Get(string id) => _items.FirstOrDefault(i => i.Id == id);
}