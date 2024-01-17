using System.Globalization;
using System.Text.Json;
using ZA_check.CaseForRequests;
using ZA_check.NoiseLw;
using ZA_check.TotalNoiseLw;
using ZA_check.WorkPoint;

namespace ZA_check;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Введите типоразмер колеса");
        var stringFanSize = Console.ReadLine();

        var result = int.TryParse(stringFanSize, out var intFanSize);
        if (!result)
        {
            throw new ArgumentException(
                "Значение 'Исполнение вентилятора' не является числом."
            );
        }
        //-----------------------------------------------------------------------------------------------------------

        Console.WriteLine("Введите артикул вентилятора");
        var stringArticleNo = Console.ReadLine();

        if (string.IsNullOrEmpty(stringArticleNo))
        {
            throw new ArgumentException(
                "Значение 'Артикул вентилятора' не должно быть пустым."
            );
        }
        //-----------------------------------------------------------------------------------------------------------

        Console.WriteLine("Введите плотность воздуха (опционально)");
        var stringAirDensity = Console.ReadLine();

        result = double.TryParse(
            stringAirDensity?.Replace(".", ","),
            out var doubleAirDensity
        );

        if (!result && !string.IsNullOrEmpty(stringAirDensity))
        {
            throw new ArgumentException(
                "Значение 'Плотность воздуха' не является числом."
            );
        }

        doubleAirDensity = Math.Round(doubleAirDensity, 2);
        //-----------------------------------------------------------------------------------------------------------

        //Запрос аэродинамических характеристик
        var sessionId = FanSelectionApi.GetSessionId();
        var requestString = Methods.RequestString(
            "get_chart_data",
            "air_performance",
            sessionId,
            intFanSize = 225,
            stringArticleNo = "130614/0Z01",
            airDensity: doubleAirDensity
        );
        var responseString = FanSelectionApi.MakeRequest(requestString);
        Console.WriteLine(responseString);

        var chartData = JsonSerializer.Deserialize<AirPerformance>(responseString) ?? throw new
            InvalidOperationException("Строка Json пуста. Невозможно получить объект AirPerformance");

        var inputNameSheet = stringArticleNo.Replace("/", "_");


        /*// Создаем массив с информацией о файлах
        var fileData = new[]
        {
            new { FileName = "ChartData.json", DataType = typeof(AirPerformance) },
            // new {FileName = "DataNoise.json", DataType = typeof(TotalAcousticsLw) }
        };*/

        const string pathExcelFile = "D:\\3. Таблица ограничений параметров по подбору оборудования v1.3.xlsm";

        /*// Проходимся по каждому файлу в цикле
        foreach (var data in fileData)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), data.FileName);

            // object? chartData;
            switch (data.FileName)
            {
                case "ChartData.json":
                    chartData = JsonLoader.Download<AirPerformance>(filePath, data.DataType);
                    Console.WriteLine("Файл загружен");*/
                    var excelWriter = new ExcelWriter<AirPerformance>(pathExcelFile, chartData as AirPerformance,
                    inputNameSheet, sessionId, intFanSize, stringArticleNo, airDensity:doubleAirDensity, outputNoiseData: true);

                    /*break;
                case "DataNoise.json":
                    /*chartData = JsonLoader.Download<TotalAcousticsLw>(filePath, data.DataType);
                    Console.WriteLine("Файл загружен");
                    var writer =
                        new ExcelWriter<TotalAcousticsLw>(pathExcelFile, chartData as TotalAcousticsLw, inputNameSheet);#1#
                    break;
            }
        }*/
    }
}
