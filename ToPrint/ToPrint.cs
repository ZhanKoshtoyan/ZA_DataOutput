using ZA_check.Noise;
using ZA_check.WorkPoint;

namespace ZA_check.ToPrint;

public class ToPrint<T>
{
    public ToPrint(T t)
    {
        switch (t)
        {
           case AirPerformance airPerformance:
               WorkPoint(airPerformance);
               break;
           case Acoustics acoustics:
                Noise(acoustics);
               break;
        }
    }

    private static void WorkPoint(AirPerformance? airPerformance)
    {
        // Пример: Вывод идентификаторов кривых
        if (airPerformance?.CHART_DATA?.CURVES != null)
        {
            Console.WriteLine($"Label_X: {airPerformance.CHART_DATA.LABEL_X}");
            Console.WriteLine($"Label_Y: {airPerformance.CHART_DATA.LABEL_Y}");
            foreach (var curve in airPerformance.CHART_DATA.CURVES)
            {
                Console.WriteLine(curve.ID);
                if (curve.DATA != null)
                {
                    foreach (var dataPoint in curve.DATA)
                    {
                        Console.WriteLine($"PSF: {dataPoint.PSF}");
                        Console.WriteLine($"QV: {dataPoint.QV}");
                    }
                }
            }
        }
    }

    private static void Noise(Acoustics? acoustics)
    {
        // Пример: Вывод идентификаторов кривых
        if (acoustics?.DATA?.CHART_DATA?.CURVES != null)
        {
            Console.WriteLine($"Label_X: {acoustics.DATA.CHART_DATA.LABEL_X}");
            Console.WriteLine($"Label_Y: {acoustics.DATA.CHART_DATA.LABEL_Y}");

            foreach (var curve in acoustics.DATA.CHART_DATA.CURVES)
            {
                Console.WriteLine(curve.ID);
                if (curve.DATA != null)
                {
                    foreach (var dataPoint in curve.DATA)
                    {
                        Console.WriteLine($"X: {dataPoint.X}");
                        Console.WriteLine($"Y: {dataPoint.Y}");
                    }
                }
            }
        }
    }
}
