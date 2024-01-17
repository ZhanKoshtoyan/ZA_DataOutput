using System.Text.Json;
using ZA_check.WorkPoint;
using ZA_check.ToPrint;

namespace ZA_check;

public static class JsonLoader
{
    public static readonly string PathJsonFile = Path.Combine(Directory.GetCurrentDirectory(), "ChartData.json");
    public static string? Response { get; private set; }

    public static void Upload<T>(T? fanData, string pathJsonFile)
    {
        if (fanData == null)
        {
            throw new ArgumentNullException(nameof(fanData));
        }

        if (pathJsonFile == null)
        {
            throw new ArgumentNullException(nameof(pathJsonFile));
        }

        if (File.Exists(pathJsonFile))
        {
            Console.WriteLine("Файл уже существует. Хотите перезаписать его? (Y/N)");
            Response = Console.ReadLine();
            if (Response?.ToUpper() != "Y")
            {
                return;
            }
        }

        var options = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            WriteIndented = true
        };

        /*await using (var fileStream = new FileStream(pathJsonFile, FileMode.Create))
        {
            await JsonSerializer.SerializeAsync(fileStream, fanData, options);
        }

        Console.WriteLine(JsonSerializer.Serialize(fanData));
        */

        var json = JsonSerializer.Serialize(fanData, options);
        var file = File.CreateText(pathJsonFile);
        file.WriteLine(json);
        Console.WriteLine(json);
        file.Close();
    }

    public static T Download<T>(string pathJsonFile, Type dataType)
    {
        var options = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            WriteIndented = true
        };

        var jsonData = default(T);
        if (File.Exists(pathJsonFile))
        {
            try
            {
                using var streamJson = File.OpenRead(pathJsonFile);
                {
                    jsonData = (T)JsonSerializer.Deserialize(streamJson, dataType, options);
                    Console.WriteLine(jsonData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Файл *.json не найден");
        }

        if (jsonData != null)
        {
            var toPrint = new ToPrint<T>(jsonData);
        }
        else
        {
            throw new Exception("Ошибка: список пуст");
        }

        return jsonData;
    }
}
