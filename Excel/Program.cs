using Excel;
using ExcelDataReader;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

ApplicationContext db = new ApplicationContext();
using (var stream = File.Open(@"C:\Users\Nikita.Poroshin\Desktop\Full.xlsx", FileMode.Open, FileAccess.Read))
{
    Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
    var reader = ExcelReaderFactory.CreateReader(stream, new ExcelReaderConfiguration()
    {
        FallbackEncoding = Encoding.GetEncoding(1252),

        Password = "password",

        AutodetectSeparators = new char[] { ',', ';', '\t', '|', '#' },

        LeaveOpen = false,

        AnalyzeInitialCsvRows = 0,
    });

    var result = reader.AsDataSet();

    var table = result.Tables[0];

    for (int i = 1; i < table.Rows.Count; i++)
    {

        var row = table.Rows[i].ItemArray;
        int paketNumberProkat = 0;
        try
        {
            paketNumberProkat = Convert.ToInt32(row[1]);
        }
        catch
        {
            paketNumberProkat = 0;
        }
        int paketNumberProkat1 = 0;
        try
        {
            paketNumberProkat1 = Convert.ToInt32(row[2]);
        }
        catch
        {
            paketNumberProkat1 = 0;
        }
        
        BaseGuid svpzzd = new BaseGuid()
        {
            Guid = Convert.ToString(row[0]),
            DESCRIPTION_TYPE_ID = paketNumberProkat,
            CODE = paketNumberProkat1,
            SHORT_NAME = Convert.ToString(row[3]),
            NAME = Convert.ToString(row[4]),
            BEGIN_DATE = Convert.ToString(row[5]),
            END_DATE = Convert.ToString(row[6]),
            NOTE = Convert.ToString(row[7]),
        };
        if (String.IsNullOrEmpty(svpzzd.SHORT_NAME) == true)
            svpzzd.SHORT_NAME = "-";
        if (String.IsNullOrEmpty(svpzzd.NAME) == true)
            svpzzd.NAME = "-";
        if (String.IsNullOrEmpty(svpzzd.BEGIN_DATE) == true)
            svpzzd.BEGIN_DATE = "-";
        if (String.IsNullOrEmpty(svpzzd.END_DATE) == true)
            svpzzd.END_DATE = "-";
        if (String.IsNullOrEmpty(svpzzd.NOTE) == true)
            svpzzd.NOTE = "-";
        db.Guid.Add(svpzzd);
    }
    db.SaveChanges();
}