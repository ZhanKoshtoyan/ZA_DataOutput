namespace ZA_check.WorkPoint;

public record Curve
{
    public  string? ID { get; init; }
    public  List<DataPoint>? DATA { get; init; }
}
