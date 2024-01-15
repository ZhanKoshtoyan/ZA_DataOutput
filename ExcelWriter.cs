using OfficeOpenXml;
using ZA_check.Noise;
using ZA_check.WorkPoint;

namespace ZA_check;

public class ExcelWriter<T>
{
    public ExcelWriter(string? pathExcelFile, T? t, string nameSheet, int firstRow = 21)
    {
        switch (t)
        {
            case AirPerformance airPerformance:
                WorkPoint(pathExcelFile, airPerformance, nameSheet, firstRow);
                break;
            case Acoustics acoustics:
                Noise(pathExcelFile, acoustics, nameSheet, firstRow);
                break;
        }
    }

    private static void WorkPoint(string? pathExcelFile, AirPerformance? airPerformance, string nameSheet, int firstRow)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        // Открытие существующего файла
        var package = new ExcelPackage(
            new FileInfo(pathExcelFile ?? throw new InvalidOperationException("Файл не найден."))
        );

        // создание нового листа
        var worksheet = package.Workbook.Worksheets.Add(nameSheet);

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

    private static void Noise(string? pathExcelFile, Acoustics? acoustics, string nameSheet, int firstRow)
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
        if (acoustics?.DATA?.CHART_DATA?.CURVES != null)
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
                var curveData = acoustics.DATA.CHART_DATA.CURVES[i];

                /*worksheet.Cells[row-1, columnCurve].Value = curveData.ID;
                worksheet.Cells[row-3, columnCurve].Value = acoustics.CHART_DATA.LABEL_X;*/
                worksheet.Cells[row-1, columnCurve+1].Value = curveData.ID;
                worksheet.Cells[row-3, columnCurve+1].Value = acoustics.DATA.CHART_DATA.LABEL_Y;

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
}
