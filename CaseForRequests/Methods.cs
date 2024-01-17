namespace ZA_check.CaseForRequests;

public static class Methods
{
    public static string RequestString(
        string cmd,
        string cmdParam,
        string sessionId,
        int fanSize,
        string articleNo,
        double qv = 0,
        double psf = 0,
        double pf = 0,
        double airDensity = IRequest.AirDensity,
        bool fullOctaveBand = IRequest.FullOctaveBand,
        bool insertGeoData = IRequest.InsertGeoData,
        bool insertMotorData = IRequest.InsertMotorData,
        bool insertNominalValues = IRequest.InsertNominalValues
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
        $"'psf' : '{psf}'," +
        $"'current_phase' : '{IRequest.CurrentPhase}'," +
        $"'voltage' : '{IRequest.Voltage}'," +
        $"'nominal_frequency' : '{IRequest.NominalFrequency}'," +
        $"'sessionid' : '{sessionId}'," +
        $"'full_octave_band' : '{fullOctaveBand.ToString()}'," +
        $"'insert_geo_data' : '{insertGeoData.ToString()}'," +
        $"'insert_motor_data' : '{insertMotorData.ToString()}'," +
        $"'insert_nominal_values' : '{insertNominalValues.ToString()}'," +
        $"'fan_size' : '{fanSize}'," +
        $"'air_density' : '{airDensity}'," +
        $"'article_no' : '{articleNo}'," +
        $"'search_tolerance' : '{IRequest.SearchTolerance}'," +
        "}";
}
