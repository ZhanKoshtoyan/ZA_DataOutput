namespace ZA_check.CaseForRequests;

public class Methods
{
    public static string RequestString(
        string cmd,
        string cmdParam,
        double qv,
        double pf,
        string sessionId,
        int fanSize,
        string
            articleNo,
        double
            airDensity = 1.2D
    ) =>
        "{" +
        $"'username' : '{IRequest.Username}'," +
        $"'password' : '{IRequest.Password}'," +
        $"'language' : '{IRequest.Language}'," +
        $"'unit_system' : '{IRequest.UnitSystem}'," +
        $"'cmd' : '{cmd}'," +
        $"'cmd_param' : '{cmdParam}'," +
        $"'spec_products' : '{IRequest.SpecProducts}'," +
        $"'product_range' : '{IRequest.ProductRange}'," +
        $"'qv' : '{qv}'," +
        $"'pf' : '{pf}'," +
        $"'current_phase' : '{IRequest.CurrentPhase}'," +
        $"'voltage' : '{IRequest.Voltage}'," +
        $"'nominal_frequency' : '{IRequest.NominalFrequency}'," +
        $"'sessionid' : '{sessionId}'," +
        $"'full_octave_band' : '{IRequest.FullOctaveBand}'," +
        $"'insert_geo_data' : '{IRequest.InsertGeoData}'," +
        $"'insert_motor_data' : '{IRequest.InsertMotorData}'," +
        $"'insert_nominal_values' : '{IRequest.InsertNominalValues}'," +
        $"'fan_size' : '{fanSize}'," +
        $"'air_density' : '{airDensity}'," +
        $"'article_no' : '{articleNo}'," +
        "}";
}
