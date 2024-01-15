using ZA_check.WorkPoint;

namespace ZA_check;

public static class ChartCollection
{
    public static AirPerformance? Load(string? pathJsonFile)
    {

        var airPerformance = new AirPerformance()
        {
            CHART_DATA = new ChartData()
            {
                CURVES = new List<Curve>
                            {
                                new Curve
                                {
                                    ID = "curve1",
                                    DATA = new List<DataPoint>
                                    {
                                        new DataPoint
                                        {
                                            PSF = 1.0,
                                            QV = 2.0
                                        },
                                        new DataPoint
                                        {
                                            PSF = 3.0,
                                            QV = 4.0
                                        }
                                    }
                                },
                                new Curve
                                {
                                    ID = "curve2",
                                    DATA = new List<DataPoint>
                                    {
                                        new DataPoint
                                        {
                                            PSF = 5.0,
                                            QV = 6.0
                                        },
                                        new DataPoint
                                        {
                                            PSF = 7.0,
                                            QV = 8.0
                                        }
                                    }
                                }
                            },
                            LABEL = "Chart Label",
                            LABEL_X = "X Label",
                            LABEL_X2 = "X2 Label",
                            LABEL_Y = "Y Label",
                            X2_FORMAT = "X2 Format",
                            X2_UNIT_KORR_FACTOR = 1.0,
                            X_UNIT_FACTOR = 2,
                            X_UNIT_KORR_FACTOR = 3,
                            Y_UNIT_FACTOR = 4,
                            Y_UNIT_KORR_FACTOR = 5
            }

        };
        return airPerformance;
    }
}
