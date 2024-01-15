namespace ZA_check.Noise;

public class NoiseChartData
{
    public string? AKUSTIK_TYP { get; set; }
    public List<NoiseCurve>? CURVES { get; set; }
    public string? LABEL { get; set; }
    public string? LABEL_X { get; set; }
    public string? LABEL_Y { get; set; }
    public int X_UNIT_KORR_FACTOR { get; set; }
}
