namespace ZA_check.WorkPoint;

public record ChartData
{
    public List<Curve>? CURVES { get; init; }
    public string? LABEL { get; set; }
    public string? LABEL_X { get; init; }
    public string? LABEL_X2 { get; set; }
    public string? LABEL_Y { get; init; }
    public string? X2_FORMAT { get; set; }
    public double X2_UNIT_KORR_FACTOR { get; set; }
    public int X_UNIT_FACTOR { get; set; }
    public int X_UNIT_KORR_FACTOR { get; set; }
    public int Y_UNIT_FACTOR { get; set; }
    public int Y_UNIT_KORR_FACTOR { get; set; }
}
