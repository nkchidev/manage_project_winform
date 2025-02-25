using OfficeOpenXml.Table;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml.Style;

namespace ProjectStorage.Helpers
{
    public class TemplateExcel
    {
        public static void FillReport(string filename, string templatefilename, DataSet data)
        {
            FillReport(filename, templatefilename, data, new string[] { "%", "%" });
        }

        public static void FillReport(string filename, string templatefilename, DataSet data, string[] deliminator)
        {
            if (File.Exists(filename))
                File.Delete(filename);

            using (var file = new FileStream(filename, FileMode.CreateNew))
            {
                using (var temp = new FileStream(templatefilename, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (var xls = new ExcelPackage(file, temp))
                    {
                        foreach (var n in xls.Workbook.Names)
                        {
                            FillWorksheetData(data, n.Worksheet, n, deliminator);
                        }

                        foreach (var ws in xls.Workbook.Worksheets)
                        {
                            foreach (var n in ws.Names)
                            {
                                FillWorksheetData(data, ws, n, deliminator);
                            }
                        }

                        foreach (var ws in xls.Workbook.Worksheets)
                        {
                            foreach (var c in ws.Cells)
                            {
                                var s = "" + c.Value;
                                if (s.StartsWith(deliminator[0]) == false &&
                                    s.EndsWith(deliminator[1]) == false)
                                    continue;
                                s = s.Replace(deliminator[0], "").Replace(deliminator[1], "");
                                var ss = s.Split('.');
                                try
                                {
                                    c.Value = data.Tables[ss[0]].Rows[0][ss[1]];
                                }
                                catch { }
                            }
                        }

                        xls.Save();
                    }
                }
            }
        }

        //private static void FillWorksheetData(DataSet data, ExcelWorksheet ws, ExcelNamedRange n, string[] deliminator)
        //{
        //    if (data.Tables.Contains(n.Name) == false)
        //        return;

        //    var dt = data.Tables[n.Name];

        //    int row = n.Start.Row;

        //    var cn = new string[n.Columns];
        //    var st = new int[n.Columns];
        //    for (int i = 0; i < n.Columns; i++)
        //    {
        //        cn[i] = (n.Value as object[,])[0, i].ToString().Replace(deliminator[0], "").Replace(deliminator[1], "");
        //        if (cn[i].Contains("."))
        //            cn[i] = cn[i].Split('.')[1];
        //        st[i] = ws.Cells[row, n.Start.Column + i].StyleID;
        //    }

        //    foreach (DataRow r in dt.Rows)
        //    {
        //        for (int col = 0; col < n.Columns; col++)
        //        {
        //            if (dt.Columns.Contains(cn[col]))
        //                ws.Cells[row, n.Start.Column + col].Value = r[cn[col]];
        //            ws.Cells[row, n.Start.Column + col].StyleID = st[col];
        //        }
        //        row++;
        //    }

        //    // extend table formatting range to all rows
        //    foreach (var t in ws.Tables)
        //    {
        //        var a = t.Address;
        //        if (n.Start.Row.Between(a.Start.Row, a.End.Row) &&
        //            n.Start.Column.Between(a.Start.Column, a.End.Column))
        //        {
        //            ExtendRows(t, dt.Rows.Count - 1);
        //        }

        //    }
        //}

        private static void FillWorksheetData(DataSet data, ExcelWorksheet ws, ExcelNamedRange n, string[] deliminator)
        {
            if (data.Tables.Contains(n.Name) == false)
                return;

            var dt = data.Tables[n.Name];

            // Starting row in the Excel worksheet
            int row = n.Start.Row;

            // Column names and styles from the named range
            var cn = new string[n.Columns];
            var st = new int[n.Columns];
            var listNull = new List<int>();
            for (int i = 0; i < n.Columns; i++)
            {

                if((n.Value as object[,])[0, i] != null)
                {
                    try
                    {
                        cn[i] = (n.Value as object[,])[0, i].ToString().Replace(deliminator[0], "").Replace(deliminator[1], "");
                        if (cn[i].Contains("."))
                            cn[i] = cn[i].Split('.')[1];
                        st[i] = ws.Cells[row, n.Start.Column + i].StyleID;
                    }
                    catch (Exception)
                    {
                        continue;

                    }
                }
                else
                {
                    listNull.Add(i);
                }
              
              
            }

            // Loop through each DataRow in the DataTable
            int k = 0;
            foreach (DataRow r in dt.Rows)
            {
                if (ws.Name.Contains("1B"))
                {
                    ws.Row(23).Height = 35;
                }

                if (ws.Name.Contains("2A"))
                {
                    ws.Row(26).Height = 35;
                }
                if (ws.Name.Contains("2B"))
                {
                    ws.Row(15).Height = 35;
                }
                if (ws.Name.Contains("2C"))
                {
                    ws.Row(27).Height = 35;
                }



                // If the current row exceeds the existing number of rows in the worksheet, insert a new row
                if (k > 0)
                {
                    ws.InsertRow(row, 1); // Insert a new row at the current row position
                }

                for (int col = 0; col < n.Columns; col++)
                {
                    var isNull = listNull.Any(x => x == col);
                    if (!isNull)
                    {
                        if (dt.Columns.Contains(cn[col]))
                        {
                            ws.Cells[row, n.Start.Column + col].Value = r[cn[col]];
                            ws.Cells[row, n.Start.Column + col].StyleID = st[col]; // Preserve style
                        }
                    }


                }
               
                row++;
                k++;
            }
            if (ws.Name.Contains("2B"))
            {

                var range = ws.Cells[$"A15:P{15 + dt.Rows.Count}"];

                // Apply border to the entire range
                var border = range.Style.Border;

                // Set all borders (top, bottom, left, right) for the range
                border.Top.Style = ExcelBorderStyle.Thin;
                border.Bottom.Style = ExcelBorderStyle.Thin;
                border.Left.Style = ExcelBorderStyle.Thin;
                border.Right.Style = ExcelBorderStyle.Thin;


            }

            if (ws.Name.Contains("2C"))
            {

                var range = ws.Cells[$"A27:L{27 + dt.Rows.Count}"];

                // Apply border to the entire range
                var border = range.Style.Border;

                // Set all borders (top, bottom, left, right) for the range
                border.Top.Style = ExcelBorderStyle.Thin;
                border.Bottom.Style = ExcelBorderStyle.Thin;
                border.Left.Style = ExcelBorderStyle.Thin;
                border.Right.Style = ExcelBorderStyle.Thin;


            }


            // Extend table formatting range to all rows
            //foreach (var t in ws.Tables)
            //{
            //    var a = t.Address;
            //    if (n.Start.Row.Between(a.Start.Row, a.End.Row) &&
            //        n.Start.Column.Between(a.Start.Column, a.End.Column))
            //    {
            //        ExtendRows(t, dt.Rows.Count - 1); // Adjust the table size
            //    }
            //}
        }

        public static void ExtendRows(ExcelTable excelTable, int count)
        {

            var ad = new ExcelAddress(excelTable.Address.Start.Row,
                                      excelTable.Address.Start.Column,
                                      excelTable.Address.End.Row + count,
                                      excelTable.Address.End.Column);
            //Address = ad;
        }
    }

    public static class int_between
    {
        public static bool Between(this int v, int a, int b)
        {
            return v >= a && v <= b;
        }
    }
}

