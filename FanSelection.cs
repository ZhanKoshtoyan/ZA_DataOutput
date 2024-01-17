using System.Text.Json;
using ZA_check.CaseForRequests;

namespace ZA_check;

public static class FanSelectionApi
{
    private static readonly HttpClient Client = new HttpClient();
    private const string DllPath = "http://fanselect.net:8079/FSWebService";

    private static string ZaApiFanSelection(string requestString, string dllPath)
    {
        var content = new StringContent(requestString);
        var response = Client.PostAsync(dllPath, content).Result;
        return response.Content.ReadAsStringAsync().Result;
    }

    public static string GetSessionId()
    {
        var requestString = "{\"cmd\":\"create_session\", \"username\" : \"USERNAME\", \"password\" : \"PASSWORD\" }";
        var responseString = ZaApiFanSelection(requestString, DllPath);
        // Console.WriteLine(responseString); // Добавьте эту строку для отладки
        using (JsonDocument document = JsonDocument.Parse(responseString))
        {
            JsonElement root = document.RootElement;
            string sessionId = root.GetProperty("SESSIONID").GetString();
            return sessionId;
        }
    }

    public static string MakeRequest(string requestString)
    {
        /*var requestString = "{" +
            "'username' : 'ZAFS48467'," +
            "'password' : 'ee1iio'," +
            "'language' : 'RU'," +
            "'unit_system' : 'm'," +
            "'cmd' : 'get_chart_data'," +
            "'cmd_param' : 'air_performance'," +
            "'spec_products' : 'PF_50'," +
            "'product_range' : 'BR_14'," +
            "'qv' : '2500'," +
            "'pf' : '50'," +
            "'current_phase' : '3'," +
            "'voltage' : '400'," +
            "'nominal_frequency' : '50'," +
            "'sessionid' : '" + sessionId + "'," +
            "'full_octave_band' : 'true'," +
            "'insert_geo_data' : 'true'," +
            "'insert_motor_data' : 'true'," +
            "'insert_nominal_values' : 'true'," +
            "'fan_size' : '225'," +
            "'air_density' : '1.2'," +
            "'article_no' : '130614/0Z01'," +
            "}";*/
        var responseString = ZaApiFanSelection(requestString, DllPath);
        return responseString;
    }
}
