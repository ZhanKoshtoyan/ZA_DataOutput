namespace ZA_check.CaseForRequests;

public class WorkPointRequest : IRequest
{
    public WorkPointRequest(double qv, double pf, string sessionId, int fanSize, string articleNo, double
        airDensity = 1.2D)
    {
        Qv = qv;
        Pf = pf;
        SessionId = sessionId;
        FanSize = fanSize;
        AirDensity = airDensity;
        ArticleNo = articleNo;


    }

    public string Cmd => "get_chart_data";
    public string CmdParam => "air_performance";
    public double Qv { get; set; }
    public double Pf { get; set; }
    public string SessionId { get; set; }
    public int FanSize { get; set; }
    public double AirDensity { get; set; }
    public string ArticleNo { get; set; }

    public string Request => Methods.RequestString(Cmd, CmdParam, Pf, Pf, SessionId, FanSize, ArticleNo, AirDensity);

};

