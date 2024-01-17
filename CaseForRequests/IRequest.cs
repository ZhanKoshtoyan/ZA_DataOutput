namespace ZA_check.CaseForRequests;

public interface IRequest
{
    public string Cmd {get; }
    public string CmdParam {get; }

    /*public double Qv {get; set; }

    public double Pf {get; set; }

    public string SessionId {get; set; }

    public int FanSize {get; set; }

    public double AirDensity {get; set; }

    public string ArticleNo {get; set; }*/

    public const string Username = "ZAFS48467";

    public const string Password = "ee1iio";

    public const string Language = "RU";

    public const string UnitSystem = "m";

    public const string SpecProducts = "PF_50";

    public const string ProductRange = "BR_14";

    public const int CurrentPhase = 3;

    public const int Voltage = 400;

    public const int NominalFrequency = 50;

    public const bool FullOctaveBand = true;

    public const bool InsertGeoData = true;

    public const bool InsertMotorData = true;

    public const bool InsertNominalValues = true;

    public const double AirDensity = 1.2D;

    public const double SearchTolerance = 10;

    /*public AbstractRequest(
        string cmd,
        string cmdParam,
        string qv,
        string pf,
        string sessionId,
        string articleNo,
        int fanSize,
        double airDensity
    )
    {
        _cmd = cmd;
        _cmdParam = cmdParam;
        _qv = qv;
        _pf = pf;
        _sessionId = sessionId;
        _articleNo = articleNo;
        _fanSize = fanSize;
        _airDensity = airDensity;
    }*/
}

