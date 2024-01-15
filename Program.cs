
using System.Text.Json;
using ZA_check;
using ZA_check.Noise;
using ZA_check.WorkPoint;

/*Console.WriteLine("Введите данные по рабочей точке");
var inputDataWorkPoint = Console.ReadLine();
// Создание объекта для сериализации
var dataI = new { InputData = inputDataWorkPoint };
var json = JsonSerializer.Serialize(inputDataWorkPoint,
    new JsonSerializerOptions
    {
        WriteIndented = true
    }
);
File.WriteAllText("DataNoise.json", json);*/

/*Console.WriteLine("Введите наименование листа Excel");
var inputNameSheet = Console.ReadLine();
if (inputNameSheet == null)
{
    throw new Exception("Наименование листа Excel не может быть пустым");
}

// Создаем массив с информацией о файлах
var fileData = new[]
{
    new { FileName = "ChartData.json", DataType = typeof(AirPerformance) },
    new { FileName = "DataNoise.json", DataType = typeof(Acoustics) }
};

const string pathExcelFile = "D:\\3. Таблица ограничений параметров по подбору оборудования v1.3.xlsm";

// Проходимся по каждому файлу в цикле
foreach (var data in fileData)
{
    var filePath = Path.Combine(Directory.GetCurrentDirectory(), data.FileName);

    object chartData = null;
    switch (data.FileName)
    {
        case "ChartData.json":
            chartData = JsonLoader.Download<AirPerformance>(filePath, data.DataType);
            Console.WriteLine("Файл загружен");
            var excelWriter = new ExcelWriter<AirPerformance>(pathExcelFile, chartData as AirPerformance, inputNameSheet);
            break;
        case "DataNoise.json":
            chartData = JsonLoader.Download<Acoustics>(filePath, data.DataType);
            Console.WriteLine("Файл загружен");
            var writer = new ExcelWriter<Acoustics>(pathExcelFile, chartData as Acoustics, inputNameSheet);
            break;
    }

}*/

class Program
{
    static void Main(string[] args)
    {
        var sessionId = FanSelectionApi.GetSessionId();
        var responseString = FanSelectionApi.MakeRequest(sessionId);
        Console.WriteLine(responseString);
    }
}




