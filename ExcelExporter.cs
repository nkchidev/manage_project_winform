using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ex = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.IO;

namespace ProjectStorage
{
    class ExcelExporter
    {
        static public int ALIGN_HCENTER = 1 << 0;
        static public int ALIGN_LEFT = 1 << 1;
        static public int ALIGN_RIGHT = 1 << 2;
        static public int ALIGN_VCENTER = 1 << 3;
        static public int ALIGN_CENTER = 1 << 4;

        public ex._Worksheet eSheet;
        public ex.Workbook eWork;
        public ex.Application ap;
        public ExcelExporter()
        {
            initDocument();
        }

        void initDocument()
        {
            ap = new ex.Application();
            eWork = ap.Workbooks.Add(Type.Missing);

            ex.Sheets sheets = ap.Sheets;
            eSheet = ((ex._Worksheet)(sheets[1]));

            eSheet.PageSetup.Orientation = ex.XlPageOrientation.xlPortrait;
            //eSheet.PageSetup.PaperSize = ex.XlPaperSize.xlPaperA4;
        }

        //==============================================
        static public void dataGridViewToExcel(DataGridView dataGridView)
        {
            dataGridView.SelectAll();
            DataObject dataObj = dataGridView.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);

            //======================
            ExcelExporter exporter = new ExcelExporter();

            int i = 1;
            foreach (DataGridViewColumn dgviewColumn in dataGridView.Columns)
            {
                // Excel work sheet indexing starts with 1
                if (dgviewColumn.Visible)
                {
                    exporter.eSheet.Cells[1, i] = dgviewColumn.Name;
                    ++i;
                }
            }
            //  now paste
            ex.Range CR = (ex.Range)exporter.eSheet.Cells[2, 1];
            CR.Select();
            exporter.eSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

            //  format cell color
            for (int rowIndex = 0; rowIndex < dataGridView.Rows.Count; rowIndex++)
            {
                DataGridViewRow dgRow = dataGridView.Rows[rowIndex];
                int ii = 0;
                for (int cellIndex = 0; cellIndex < dgRow.Cells.Count; cellIndex++)
                {
                    if (dataGridView.Columns[cellIndex].Visible)
                    {
                        System.Drawing.Color color = dgRow.Cells[cellIndex].Style.BackColor;
                        //if (color != System.Drawing.Color.White
                          //  && color.G > 0)
                        {
                            char c = (char)('A' + ii);
                            String from = "" + c + (rowIndex+2).ToString();
                            ex.Range r = exporter.getRange(from, from);
                            r.Cells.Interior.Color = color;
                        }

                        color = dgRow.Cells[cellIndex].Style.BackColor;

                        ii++;
                    }
                }
            }

            //  clear selection of the daatGrid
            dataGridView.ClearSelection();

            //  format something
            ex.Range range = exporter.getRange("A1", "Z1");
            range.Font.Bold = true;
            range.Font.Size = 8;

            int totalRow = dataGridView.Rows.Count;
            range = exporter.getRange("A2", "Z" + totalRow.ToString());
            range.Font.Size = 9;

            //  complete the document
            exporter.completeDocument("_");
        }

        public static void dataGridViewToExcel2(DataGridView dgView)
        {
            ExcelExporter exporter = new ExcelExporter();
            try
            {
                Microsoft.Office.Interop.Excel.Workbook currentWorkbook = exporter.eWork;
                Microsoft.Office.Interop.Excel._Worksheet currentWorksheet = exporter.eSheet;
                //currentWorksheet.Columns.ColumnWidth = 18;
                if (dgView.Rows.Count > 0)
                {
                    //currentWorksheet.Cells[1, 1] = DateTime.Now.ToString("s");
                    int i = 1;
                    foreach (DataGridViewColumn dgviewColumn in dgView.Columns)
                    {
                        // Excel work sheet indexing starts with 1
                        if (dgviewColumn.Visible){
                            currentWorksheet.Cells[2, i] = dgviewColumn.Name;
                            ++i;
                        }
                    }
                    Microsoft.Office.Interop.Excel.Range headerColumnRange = currentWorksheet.get_Range("A2", "Z2");
                    headerColumnRange.Font.Bold = true;
                    //headerColumnRange.Font.Color = 0xFF0000;
                    headerColumnRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    //headerColumnRange.EntireColumn.AutoFit();
                    int rowIndex = 0;
                    for (rowIndex = 0; rowIndex < dgView.Rows.Count; rowIndex++)
                    {
                        DataGridViewRow dgRow = dgView.Rows[rowIndex];
                        int ii = 0;
                        for (int cellIndex = 0; cellIndex < dgRow.Cells.Count; cellIndex++)
                        {
                            if (dgView.Columns[cellIndex].Visible)
                            {
                                currentWorksheet.Cells[rowIndex + 3, ii + 1] = dgRow.Cells[cellIndex].Value;
                                currentWorksheet.Cells[rowIndex + 3, ii + 1].NumberFormat = "@";
                                ii++;
                            }
                        }
                    }
                    Microsoft.Office.Interop.Excel.Range fullTextRange = currentWorksheet.get_Range("A2", "G" + (rowIndex + 1).ToString());
                    //fullTextRange.WrapText = true;
                    fullTextRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                }
                else
                {
                    string timeStamp = DateTime.Now.ToString("s");
                    timeStamp = timeStamp.Replace(':', '-');
                    timeStamp = timeStamp.Replace("T", "__");
                    currentWorksheet.Cells[1, 1] = timeStamp;
                    currentWorksheet.Cells[1, 2] = "No error occured";
                }
                /*
                using (SaveFileDialog exportSaveFileDialog = new SaveFileDialog())
                {
                    exportSaveFileDialog.Title = "Select Excel File";
                    exportSaveFileDialog.Filter = "Microsoft Office Excel Workbook(*.xlsx)|*.xlsx";

                    if (DialogResult.OK == exportSaveFileDialog.ShowDialog())
                    {
                        string fullFileName = exportSaveFileDialog.FileName;
                        // currentWorkbook.SaveCopyAs(fullFileName);
                        // indicating that we already saved the workbook, otherwise call to Quit() will pop up
                        // the save file dialogue box

                        currentWorkbook.SaveAs(fullFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, System.Reflection.Missing.Value, misValue, false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlUserResolution, true, misValue, misValue, misValue);
                        currentWorkbook.Saved = true;
                        MessageBox.Show("Exported successfully", "Exported to Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                 */
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            }
            exporter.completeDocument("_");
        }
        //==============================================

        void setAlign(int a, ex.Range r)
        {
            if ((a & ALIGN_CENTER) != 0)
            {
                r.HorizontalAlignment = ex.XlHAlign.xlHAlignCenter;
                r.VerticalAlignment = ex.XlHAlign.xlHAlignCenter;
            }
            if ((a & ALIGN_HCENTER) != 0)
            {
                r.HorizontalAlignment = ex.XlHAlign.xlHAlignCenter;
            }
            if ((a & ALIGN_VCENTER) != 0)
            {
                r.VerticalAlignment = ex.XlHAlign.xlHAlignCenter;
            }
            if ((a & ALIGN_RIGHT) != 0)
            {
                r.HorizontalAlignment = ex.XlHAlign.xlHAlignRight;
            }
            if ((a & ALIGN_LEFT) != 0)
            {
                r.HorizontalAlignment = ex.XlHAlign.xlHAlignLeft;
            }
        }

        public ex.Range writeLine(String fromCell, String toCell, String text, float fontSize, int align)
        {
            ex.Range r = eSheet.get_Range(fromCell, toCell);
            r.MergeCells = true;
            r.Value = text;
            r.Font.Size = fontSize;

            setAlign(align, r);

            return r;
        }

        public ex.Range writeLine(String fromCell, String toCell, String text, String fontname, float fontSize, int align)
        {
            ex.Range r = eSheet.get_Range(fromCell, toCell);
            r.MergeCells = true;
            r.Value = text;
            r.Font.Name = fontname;
            r.Font.Size = fontSize;

            setAlign(align, r);

            return r;
        }

        public ex.Range writeLine(String fromCell, String toCell, String text, float fontSize, int align, bool bold, bool underline)
        {
            ex.Range r = eSheet.get_Range(fromCell, toCell);
            r.MergeCells = true;
            r.Value = text;
            r.Font.Size = fontSize;

            setAlign(align, r);

            r.Font.Underline = underline;
            r.Font.Bold = bold;

            return r;
        }

        public ex.Range writeLine(String fromCell, String toCell, String text, String fontname, float fontSize, int align, bool bold, bool underline)
        {
            ex.Range r = eSheet.get_Range(fromCell, toCell);
            r.MergeCells = true;
            r.Value = text;
            r.Font.Name = fontname;
            r.Font.Size = fontSize;

            setAlign(align, r);

            r.Font.Underline = underline;
            r.Font.Bold = bold;

            return r;
        }

        public void setColumnWidth(String col, int width)
        {
            ex.Range r = eSheet.get_Range(col, col);
            r.Cells.ColumnWidth = width;
        }

        public ex.Range getRange(String topleftCol, String bottomrightColumn)
        {
            ex.Range r = eSheet.get_Range(topleftCol, bottomrightColumn);
            return r;
        }

        public void completeDocument(String fileprefix)
        {
            DateTime t = DateTime.Now;
            String filename = fileprefix + "_" + t.ToString("HHmmss_ddMMyyyy") + ".xls";

            eSheet.Name = "document";
            string path = System.IO.Directory.GetCurrentDirectory();
            if (!Directory.Exists(path + "\\baocao")) {
                Directory.CreateDirectory(path + "\\baocao");
            }
            eSheet.SaveAs(path + "\\baocao\\" + filename);
            eSheet.Activate();
            ap.Visible = true;
            System.Runtime.InteropServices.Marshal.ReleaseComObject(eWork);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(ap);
        }
    }
}
