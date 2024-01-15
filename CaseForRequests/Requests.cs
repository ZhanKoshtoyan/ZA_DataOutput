namespace ZA_check.CaseForRequests;

public class Requests
{
    public Requests(Values values)
    {

    }

    public enum Values
    {
        WorkPoint,
        Noise
    }

    public readonly string[] Names =
    {
        "0 = Запрос рабочей точки",
        "1 = Запрос шумовых характеристик Lw5"
    };
}
