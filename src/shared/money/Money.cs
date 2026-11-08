namespace AprozarUnion.Shared.Money;

public readonly record struct Money(int Bani)
{
    public static Money operator +(Money a, Money b) => new(a.Bani + b.Bani);
    public static Money operator -(Money a, Money b) => new(a.Bani - b.Bani);
    public static Money operator *(Money a, int n) => new(a.Bani * n);
    public override string ToString() => $"{Bani / 100}.{Bani % 100:D2} lei";
}