namespace AprozarUnion.Shared.Level;

public class TickManager
{
    private const float TICK_RATE = (float)1 / 20;
    private const int MAX_TICKS_PER_SECOND = 5;
    
    private double timeAccumulator = 0;
    private ulong currentTick = 0;

    public bool DoTickThisFrame = false;

    public void updateTick(double delta)
    {
        DoTickThisFrame = false;
        this.timeAccumulator += delta;

        double maxAllowedTime = TICK_RATE * MAX_TICKS_PER_SECOND;
        if (this.timeAccumulator > maxAllowedTime)
        {
            this.timeAccumulator = maxAllowedTime + (this.timeAccumulator % TICK_RATE);
        }

        while (this.timeAccumulator >= TICK_RATE)
        {
            double currentTime = this.currentTick * TICK_RATE;
            DoTickThisFrame = true;

            this.currentTick++;
            this.timeAccumulator -= TICK_RATE;
        }
    }
}