namespace viewmodels;

public class PreferencesViewModel
{
    public int TargetFluidOunces { get; set; }
    public string TimeZoneId { get; set; } = null!;
    public int TimeZoneOffsetHours { get; init; }
}