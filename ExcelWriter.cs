using OfficeOpenXml;
using System.Globalization;
using System.Text.Json;
using ZA_check.CaseForRequests;
using ZA_check.NoiseLw;
using ZA_check.TotalNoiseLw;
using ZA_check.WorkPoint;

namespace ZA_check;

public class ExcelWriter<T>
{
    public ExcelWriter(string? pathExcelFile, T? t, string nameSheet, string sessionId, int intFanSize, string
    stringArticleNo, double airDensity = 1.2D, int
    firstRow = 21,
     bool
    outputNoiseData =
    false)
    {
        switch (t)
        {
            case AirPerformance airPerformance:
                WorkPoint(pathExcelFile, airPerformance, nameSheet, sessionId, intFanSize, stringArticleNo, airDensity,
                firstRow,
                outputNoiseData);
                break;
            case TotalAcousticsLw totalAcousticsLw:
                TotalNoiseLw(pathExcelFile, totalAcousticsLw, nameSheet, firstRow);
                break;
        }
    }

    private static void WorkPoint(
        string? pathExcelFile,
        AirPerformance? airPerformance,
        string nameSheet,
        string sessionId,
        int intFanSize,
        string stringArticleNo,
        double airDensity,
        int firstRow,
        bool outputNoiseData
    )
    {
        var arrQv = new List<double>(30);
        var arrPsf = new List<double>(30);
        double[,] arrWorkPoints =
            { };

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        // Открытие существующего файла
        var package = new ExcelPackage(
            new FileInfo(pathExcelFile ?? throw new InvalidOperationException("Файл не найден."))
        );

        // (Тернарная операция) Проверяем, существует ли лист с указанным именем
        var worksheet =
            package.Workbook.Worksheets.Any(sheet => sheet.Name == nameSheet)
                ?
                // Если лист существует, присваиваем его переменной worksheet
                package.Workbook.Worksheets[nameSheet]
                :
                // Если лист не существует, создаем новый лист
                package.Workbook.Worksheets.Add(nameSheet);

        package.Workbook.Worksheets.MoveToEnd(nameSheet);

        //Запускаем цикл заполнения данных из файла JSON в EXCEL
        if (airPerformance?.CHART_DATA?.CURVES != null)
        {
            const int columnMaxCurve = 2;
            const int columnMinCurve = 28;
            const int indexCurve = 0;

            //Запускаем цикл для кривых №1,2 (MAX; MIN);
            for (var i = indexCurve; i < 2; i++)
            {
                var row = firstRow;
                // Для первого перебора указываем столбец columnMaxCurve, для второго columnMinCurve
                var columnCurve = i == indexCurve ? columnMaxCurve : columnMinCurve;
                //Указываем имя кривой;
                var curveData = airPerformance.CHART_DATA.CURVES[i];

                worksheet.Cells[row-1, columnCurve].Value = curveData.ID;
                worksheet.Cells[row-3, columnCurve].Value = airPerformance.CHART_DATA.LABEL_X;
                worksheet.Cells[row-1, columnCurve+1].Value = curveData.ID;
                worksheet.Cells[row-3, columnCurve+1].Value = airPerformance.CHART_DATA.LABEL_Y;

                foreach (var dataPoint in curveData.DATA!)
                {
                    worksheet.Cells[row, columnCurve].Value = dataPoint.QV;
                    worksheet.Cells[row, columnCurve+1].Value = dataPoint.PSF;

                    if (i == 0)
                    {
                        arrQv.Add(dataPoint.QV);
                        arrPsf.Add(dataPoint.PSF);
                    }

                    row++;
                }

                arrWorkPoints = new double[arrQv.Count, 2];
                for (var arrRow = 0; arrRow < arrPsf.Count; arrRow++)
                {
                    for (var arrCol = 0; arrCol < arrQv.Count; arrCol++)
                    {
                        switch (arrRow)
                        {
                            case 0:
                                arrWorkPoints[arrCol, arrRow] = arrQv[arrCol];
                                break;
                            case 1:
                                arrWorkPoints[arrCol, arrRow] = arrPsf[arrCol];
                                break;
                        }
                    }
                }
            }
        }

        worksheet.Cells[15, 1].Value = nameSheet;

        var worksheet6 = package.Workbook.Worksheets[6];

        worksheet6.Cells[1,1,11,1].Copy(worksheet.Cells[1,1,11,1]);
        worksheet6.Cells[17,1,19,28].Copy(worksheet.Cells[17,1,19,28]);

        //--------------------------------------------------------------------
        if (outputNoiseData)
        {
            for (var workPointIndex = 0; workPointIndex <= arrWorkPoints.GetUpperBound(0); workPointIndex++)
            {
                var acousticRequest = Methods.RequestString("select",
                    "acoustics_lw5",
                    sessionId,
                    intFanSize,
                    stringArticleNo,
                    arrWorkPoints[workPointIndex, 0],
                    arrWorkPoints[workPointIndex, 1],
                    fullOctaveBand: false,
                    insertGeoData: false,
                    insertMotorData: false,
                    insertNominalValues: false,
                    airDensity: airDensity
                );
                var responseString = FanSelectionApi.MakeRequest(acousticRequest);
                Console.WriteLine(responseString);

                var acousticsLw = JsonSerializer.Deserialize<AcousticsLw>(responseString) ?? throw new
                    InvalidOperationException("Строка Json пуста. Невозможно получить объект AcousticsLw");

                var calcLw5Okt = acousticsLw.CALC_LW5_OKT ?? throw new InvalidOperationException("строка acousticsLw.DATA?.CALC_LW5_OKT пуста");
                List<double> fullOctaveBandLw5 = calcLw5Okt.Split(',')
                    .Select(s => double.Parse(s, CultureInfo.InvariantCulture))
                    .ToList();

                ExcelWriter<AcousticsLw>.NoiseLw(package, acousticsLw, nameSheet,
                    fullOctaveBandLw5, workPointIndex);
            }
        }
        //--------------------------------------------------------------------

        // Сохранение файла
        package.Save();
        // Закрытие файла
        package.Dispose();
    }

    private static void TotalNoiseLw(
        string? pathExcelFile,
        TotalAcousticsLw? totalAcousticsLw,
        string nameSheet,
        int firstRow
    )
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        // Открытие существующего файла
        var package = new ExcelPackage(
            new FileInfo(pathExcelFile ?? throw new InvalidOperationException("Файл не найден."))
        );

        // выбор листа листа
        var worksheet = package.Workbook.Worksheets[nameSheet];

        // package.Workbook.Worksheets.MoveToEnd(nameSheet);

        //Запускаем цикл заполнения данных из файла JSON в EXCEL
        if (totalAcousticsLw?.DATA?.CHART_DATA?.CURVES != null)
        {
            const int columnMaxCurve = 16;
            const int columnMinCurve = 28;
            const int indexCurve = 0;

            //Запускаем цикл для кривых №1,2 (MAX; MIN);
            for (var i = indexCurve; i < 1; i++)
            {
                var row = firstRow;
                // Для первого перебора указываем столбец columnMaxCurve, для второго columnMinCurve
                var columnCurve = i == indexCurve ? columnMaxCurve : columnMinCurve;
                //Указываем имя кривой;
                var curveData = totalAcousticsLw.DATA.CHART_DATA.CURVES[i];

                /*worksheet.Cells[row-1, columnCurve].Value = curveData.ID;
                worksheet.Cells[row-3, columnCurve].Value = acoustics.CHART_DATA.LABEL_X;*/
                worksheet.Cells[row-1, columnCurve+1].Value = curveData.ID;
                worksheet.Cells[row - 3, columnCurve + 1].Value = totalAcousticsLw.DATA.CHART_DATA.LABEL_Y;

                foreach (var dataPoint in curveData.DATA!)
                {
                    // worksheet.Cells[row, columnCurve].Value = dataPoint.X;
                    worksheet.Cells[row, columnCurve+1].Value = dataPoint.Y;
                    row++;
                }
            }
        }

        worksheet.Cells[15, 1].Value = nameSheet;

        var worksheet6 = package.Workbook.Worksheets[6];

        worksheet6.Cells[1,1,11,1].Copy(worksheet.Cells[1,1,11,1]);
        worksheet6.Cells[17,1,19,28].Copy(worksheet.Cells[17,1,19,28]);

        // Сохранение файла
        package.Save();
        // Закрытие файла
        package.Dispose();
    }

    private static void NoiseLw(
        ExcelPackage package,
        AcousticsLw? acousticsLw,
        string nameSheet,
        IReadOnlyList<double> fullOctaveBandLw5,
        int selectRow,
        int firstRow = 21
    )
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        /*// Открытие существующего файла
        var package = new ExcelPackage(
            new FileInfo(pathExcelFile ?? throw new InvalidOperationException("Файл не найден."))
        );*/

        // выбор листа листа
        var worksheet = package.Workbook.Worksheets[nameSheet];

        // package.Workbook.Worksheets.MoveToEnd(nameSheet);

        //Запускаем цикл заполнения данных из файла JSON в EXCEL
        if (acousticsLw?.CALC_LW5_OKT != null)
        {
            const int columnRotationSpeed = 8;
            const int columnLw5OctaveNoiseStart = 9;
            var row = firstRow + selectRow;

            //Указываем скорость вращения колеса;
            worksheet.Cells[row, columnRotationSpeed].Value = acousticsLw.ZA_N;

            for (int columnOctaveNoise = columnLw5OctaveNoiseStart, indexOctaveNoiseList = 0;
                indexOctaveNoiseList < fullOctaveBandLw5.Count;
                columnOctaveNoise++, indexOctaveNoiseList++)
            {
                //Записываем значение каждой октавы Lw5 в ячейку листа Excel;
                worksheet.Cells[row, columnOctaveNoise].Value = fullOctaveBandLw5[indexOctaveNoiseList];
            }
        }

        /*// Сохранение файла
        package.Save();
        // Закрытие файла
        package.Dispose();*/
    }
}
