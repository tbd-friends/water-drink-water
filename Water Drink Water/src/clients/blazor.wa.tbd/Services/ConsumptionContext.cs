namespace blazor.wa.tbd.Services;

public class ConsumptionContext
{
    public int Percentage { get; set; } = 0;
    public event Action? OnStateChange;

    public void SetPercentage(int percentage)
    {
        Percentage = percentage;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnStateChange?.Invoke();
}