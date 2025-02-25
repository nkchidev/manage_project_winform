
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectStorage
{
    public class ExcelResponse
    {
        public string URI { set; get; }
        public string SheetNameFirst { set; get; }
        public List<string> listSheetName { get; set; }
        public DataSet DATA { set; get; }
    }
    public class ExcelHelper
    {


        public async Task<ExcelResponse> ImportExcelToDataTableAsync(string fileName = "", bool useHeaderRow = true, bool writeColumnAsExel = true, bool useVisibleSheet = true)
        {
            if (fileName == "")
            {
                using (var ofd = new OpenFileDialog() { Filter = "Excel File|*.xlsx;*.xls|Excel Workbook|*.xlsx|Excel Workbook 97-2003|*.xls", ValidateNames = true })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        var responses = await ReadExcelFromFileAsync(ofd.FileName, useHeaderRow, writeColumnAsExel, useVisibleSheet);
                        return responses;
                    }
                }

            }
            else
            {
                var responses = await ReadExcelFromFileAsync(fileName, useHeaderRow, writeColumnAsExel, useVisibleSheet);
                return responses;
            }
            return null;

        }

        private Task<ExcelResponse> ReadExcelFromFileAsync(string fileNameExcel, bool useHeaderRow = true, bool writeColumnAsExel = true, bool useVisibleSheet = true)
        {
            return Task<ExcelResponse>.Factory.StartNew(() =>
            {
                using (var stream = File.Open(fileNameExcel, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    IExcelDataReader reader;
                    if (Path.GetExtension(fileNameExcel).Equals(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }

                    var excelResponse = new ExcelResponse();
                    excelResponse.URI = fileNameExcel;
                    var ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {

                        FilterSheet = (tableReader, sheetIndex) => useVisibleSheet ? tableReader.VisibleState == "visible" : true,


                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = useHeaderRow
                        }
                    });
                    excelResponse.DATA = ds;
                    var listSheet = new List<string>();
                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        var tableName = ds.Tables[i].TableName;
                        listSheet.Add(tableName);
                    }
                    // đánh lại tên cột like excel A, B, C --- Z
                    if (writeColumnAsExel)
                    {
                        foreach (DataTable table in ds.Tables)
                        {
                            int i = 1;
                            foreach (DataColumn dataColumn in table.Columns)
                            {
                                dataColumn.ColumnName = GetExcelColumnName(i);
                                i++;
                            }
                        }
                    }
                    excelResponse.listSheetName = listSheet;
                    excelResponse.SheetNameFirst = ds.Tables[0].TableName;
                    return excelResponse;

                }

            });
        }

        public DataTable RenameFieldNameColumnLikeExcel(DataTable table)
        {
            int i = 1;
            foreach (DataColumn dataColumn in table.Columns)
            {
                dataColumn.ColumnName = GetExcelColumnName(i);
                i++;
            }
            return table;
        }

        private string GetExcelColumnName(int columnNumber)
        {
            string columnName = "";

            while (columnNumber > 0)
            {
                int modulo = (columnNumber - 1) % 26;
                columnName = Convert.ToChar('A' + modulo) + columnName;
                columnNumber = (columnNumber - modulo) / 26;
            }

            return columnName;
        }

        public class GroupExcel
        {
            public string GroupName { get; set; }
            public int StartPosition { get; set; }
            public DataTable dataTable { get; set; }
        }
        ///........................................... SỬ DỤNG ĐOẠN NÀY ................................................
        public DataTable GetTableFromRangeAddress(DataSet dataSet, string sheetName, RangeExcel rangeExcel)
        {
            var dataTable = dataSet.Tables[sheetName];
            return GetRangeDataTableFromTableParent(dataTable, rangeExcel);
        }

        public DataTable GetTableFromAddress(DataSet dataSet, string sheetName, string colName, string captionBegin, int numCol = -1, bool isHeader = true)
        {
            var dataTable = dataSet.Tables[sheetName];
            return GetTableFromFromFileCellAddress(dataTable, colName, captionBegin, numCol, isHeader);
        }

        public List<GroupExcel> GetListTableFromGroupAddress(DataSet dataSet, string sheetName, RangeExcel rangeExcel, string stringTextStop = "")
        {
            var dataTable = dataSet.Tables[sheetName];
            return GetRangeDataTableFromTableParentHasGroup(dataTable, rangeExcel, stringTextStop);
        }

        public DataTable ParseListGroupToDataTable(List<GroupExcel> groupExcels, string nameGroupColumn = "GroupName")
        {
            var removeTableEmpty = groupExcels.Where(x => x.dataTable.Rows.Count > 0).ToList();

            if (removeTableEmpty.Count > 0)
            {
                var dataTable = removeTableEmpty.First().dataTable.Clone();
                var dataColumn = new DataColumn(nameGroupColumn);
                dataTable.Columns.Add(dataColumn);
                //dataColumn.SetOrdinal(0);

                foreach (var item in removeTableEmpty)
                {
                    var dataTable2 = item.dataTable;
                    var dataColumn2 = new DataColumn(nameGroupColumn);
                    dataTable2.Columns.Add(dataColumn2);
                    //dataColumn2.SetOrdinal(0);
                    foreach (DataRow row in dataTable2.Rows)
                    {
                        row[nameGroupColumn] = item.GroupName;
                    }
                    dataTable.Merge(dataTable2);
                }
                return dataTable;
            }
            else
            {
                return null;
            }
        }

        public string ConvertNumberToCellAddress(int irow, int icol)
        {
            var newRow = irow + 1; // do trong excel tính đi từ 1 không phải từ 0
            var newCol = icol + 1;
            return GetExcelColumnName(newCol) + newRow;
        }

        public object GetDataAddressCell(DataSet dataSet, string sheetName, string rangeExcel)
        {
            var rangeData = GetDetailColumnRowExcelByRange(rangeExcel);
            var dataTable = dataSet.Tables[sheetName];
            return GetValueAtOneCell(dataTable, rangeData);
        }
        public object GetDataAddressCell(DataSet dataSet, string sheetName, RangeExcel rangeExcel)
        {
            var dataTable = dataSet.Tables[sheetName];
            return GetValueAtOneCell(dataTable, rangeExcel);
        }
        ///........................................... END SỬ DỤNG ĐOẠN NÀY ................................................


        private DataTable GetTableFromFromFileCellAddress(DataTable dataTable, string colName, string captionBegin, int numCol = -1, bool isHeader = true)
        {
            var tableResult = new DataTable();
            //find postion need get Table data
            int position = -1;
            int posTitleTable = 0;
            var beginStart = NumberFromExcelColumn(colName);

            var tempCol = numCol;

            foreach (DataRow row in dataTable.Rows)
            {
                var text = row[colName].ToString();
                if (text.Trim() == captionBegin)
                {
                    position = dataTable.Rows.IndexOf(row);
                    // vị trí bắt đầu đọc 
                    posTitleTable = position + 1;

                    if (!isHeader)
                    {
                        position = position - 1;
                        posTitleTable = position + 1;
                    }
                    // Tự động tìm số cột
                    if (numCol == -1)
                    {
                        numCol = FindColNum(dataTable.Rows[posTitleTable], beginStart);
                    }

                    break;
                }
            }
            var maxlength = (numCol != tempCol) ? numCol : beginStart - 2 + tempCol;
            var maxlength2 = (numCol != tempCol) ? numCol + 1 : beginStart - 1 + tempCol;

            // tạo column cho table
            for (int i = beginStart - 1; i <= maxlength; i++)
            {
                var columnName = GetNextFileNameColumn(dataTable.Rows[posTitleTable][i].ToString(), tableResult);
                tableResult.Columns.Add(columnName);
            }

            for (int j = posTitleTable + 1; j < dataTable.Rows.Count; j++)
            {

                DataRow dr = tableResult.NewRow();
                int k = 0;

                for (int i = beginStart; i <= maxlength2; i++)
                {
                    var stringcol = GetExcelColumnName(i);
                    var captionText = dataTable.Rows[j][stringcol].ToString();
                    if (k == 0 && string.IsNullOrEmpty(captionText)) { return tableResult; };
                    dr[k] = captionText;
                    k++;
                }
                tableResult.Rows.Add(dr);
            }


            return tableResult;
        }


        private string GetNextFileNameColumn(string columnName, DataTable dataTable)
        {
            if (dataTable.Columns?.Count == 0 || dataTable == null || dataTable.Columns.IndexOf(columnName) == -1)
            {
                return columnName;
            }

            var nextColumnName = columnName;

            int i = 0;
            while (dataTable.Columns.IndexOf(nextColumnName) > -1)
            {
                if (i == 0)
                    nextColumnName = $"{columnName}({++i})";
                else
                    nextColumnName = nextColumnName.Replace("(" + i + ")", "(" + ++i + ")");
            }

            return nextColumnName;
        }


        private int FindColNum(DataRow dataRow, int beginStart)
        {
            int value = 0;
            for (int i = beginStart - 1; i < dataRow.ItemArray.Length - 1; i++)
            {
                var text = dataRow[i].ToString();
                if (string.IsNullOrEmpty(text))
                {
                    return i + 1;
                }
                i++;
                value = i;
            }
            return value;
        }

        private object GetValueAtOneCell(DataTable dataTable, RangeExcel rangeExcel)
        {
            var row = rangeExcel.rowStart - 1;
            var col = rangeExcel.colStart;
            return dataTable.Rows[row][col];
        }
        private List<GroupExcel> GetRangeDataTableFromTableParentHasGroup(DataTable parentTable, RangeExcel rangeExcel, string stringTextStop = "")
        {

            var tableResult = new DataTable();
            var data = GetRangeDataTableFromTableParentStopByString(parentTable, rangeExcel, stringTextStop);

            // kiểm tra table có bao nhiêu group            
            var listgroup = new List<GroupExcel>();
            foreach (DataRow row in data.Rows)
            {
                var excelPosition = Convert.ToInt32(row[0]);
                var group_name = row[1].ToString();
                var row2 = row[2];
                if (!string.IsNullOrEmpty(group_name) && string.IsNullOrEmpty(row2.ToString()))
                {
                    listgroup.Add(new GroupExcel() { GroupName = group_name, StartPosition = excelPosition });

                }
            }

            var new_dataTable = data.Clone();
            foreach (var item in listgroup)
            {
                rangeExcel.rowStart = item.StartPosition;
                DataTable table_group;
                table_group = new_dataTable.Clone();
                GetRangeDataTableFromTableParent(parentTable, rangeExcel, ref table_group);
                //new_dataTable.Merge(table_group);
                item.dataTable = table_group.Copy();
            }


            return listgroup;
        }

        private DataTable GetRangeDataTableFromTableParentStopByString(DataTable parentTable, RangeExcel rangeExcel, string stop)
        {
            var tableResult = new DataTable();
            tableResult.Columns.Add("RowExcel");
            for (int i = rangeExcel.colStartNumber; i <= rangeExcel.colEndNumber; i++)
            {
                var stringcol = GetExcelColumnName(i);
                var captionText = parentTable.Rows[rangeExcel.rowStart - 1][stringcol].ToString();

                var columnName = GetNextFileNameColumn(captionText, tableResult);
                tableResult.Columns.Add(columnName);
            }
            //set datarow
            for (int j = rangeExcel.rowStart; j < rangeExcel.rowEnd; j++)
            {

                DataRow dr = tableResult.NewRow();
                dr[0] = j + 1;
                int k = 0;
                for (int i = rangeExcel.colStartNumber; i <= rangeExcel.colEndNumber; i++)
                {
                    var stringcol = GetExcelColumnName(i);
                    var captionText = parentTable.Rows[j][stringcol].ToString();
                    if (!string.IsNullOrEmpty(stop))
                    {
                        if (k == 0 && captionText.ToLower().Trim() == stop.ToLower().Trim()) { return tableResult; };

                    }

                    dr[k + 1] = captionText;
                    k++;
                }
                tableResult.Rows.Add(dr);
            }

            return tableResult;
        }

        private void GetRangeDataTableFromTableParent(DataTable parentTable, RangeExcel rangeExcel, ref DataTable tableResult)
        {
            tableResult.Columns.RemoveAt(0);

            for (int j = rangeExcel.rowStart; j < rangeExcel.rowEnd; j++)
            {

                DataRow dr = tableResult.NewRow();
                int k = 0;
                for (int i = rangeExcel.colStartNumber; i <= rangeExcel.colEndNumber; i++)
                {
                    var stringcol = GetExcelColumnName(i);
                    var captionText = parentTable.Rows[j][stringcol].ToString();
                    if (k == 0 && string.IsNullOrEmpty(captionText)) { return; };
                    dr[k] = captionText;
                    k++;
                }
                tableResult.Rows.Add(dr);
            }

            ////remove empty row 
            //DataTable removeEmptyRowTable = tableResult.Rows
            //     .Cast<DataRow>()
            //     .Where(row => !row.ItemArray.All(f => f is DBNull))
            //     .CopyToDataTable();


        }

        private DataTable GetRangeDataTableFromTableParent(DataTable parentTable, RangeExcel rangeExcel)
        {
            var tableResult = new DataTable();
            for (int i = rangeExcel.colStartNumber; i <= rangeExcel.colEndNumber; i++)
            {
                var stringcol = GetExcelColumnName(i);
                var captionText = parentTable.Rows[rangeExcel.rowStart - 1][stringcol].ToString();
                var columnName = GetNextFileNameColumn(captionText, tableResult);
                tableResult.Columns.Add(columnName);
            }
            //set datarow
            for (int j = rangeExcel.rowStart; j < rangeExcel.rowEnd; j++)
            {

                DataRow dr = tableResult.NewRow();
                int k = 0;
                for (int i = rangeExcel.colStartNumber; i <= rangeExcel.colEndNumber; i++)
                {
                    var stringcol = GetExcelColumnName(i);
                    var captionText = parentTable.Rows[j][stringcol].ToString();
                    if (k == 0 && string.IsNullOrEmpty(captionText)) { return tableResult; };
                    dr[k] = captionText;
                    k++;
                }
                tableResult.Rows.Add(dr);
            }

            return tableResult;
        }

        public RangeExcel GetDetailColumnRowExcelByRange(string input)
        {
            string colstart = "", colEnd = "";
            int rowStart = -1, rowEnd = -1;
            var arr = input.Trim().Split(':');
            var group1 = ParseColumnAndRowExcel(arr[0]);
            colstart = group1.Item1;
            rowStart = group1.Item2;
            if (arr.Length > 1)
            {
                var group2 = ParseColumnAndRowExcel(arr[1]);
                colEnd = group2.Item1;
                rowEnd = group2.Item2;
            }

            return new RangeExcel()
            {
                colStart = colstart,
                colEnd = colEnd,
                rowStart = rowStart,
                rowEnd = rowEnd
            };
        }

        private Tuple<string, int> ParseColumnAndRowExcel(string input)
        {
            var re = new Regex(@"([a-zA-Z]+)(\d+)");
            var result = re.Match(input);

            var alphaPart = Convert.ToString(result.Groups[1].Value);
            var numberPart = Convert.ToInt32(result.Groups[2].Value);
            return Tuple.Create(alphaPart, numberPart);
        }

        private int NumberFromExcelColumn(string column)
        {
            int retVal = 0;
            string col = column.ToUpper();
            for (int iChar = col.Length - 1; iChar >= 0; iChar--)
            {
                char colPiece = col[iChar];
                int colNum = colPiece - 64;
                retVal = retVal + colNum * (int)Math.Pow(26, col.Length - (iChar + 1));
            }
            return retVal;
        }

        public class RangeExcel
        {
            public string colStart { get; set; }
            public string colEnd { get; set; }
            public int colStartNumber
            {
                get
                {
                    return NumberFromExcelColumn(colStart);
                }
            }

            public int colEndNumber
            {
                get
                {
                    return NumberFromExcelColumn(colEnd);
                }
            }
            public int rowStart { get; set; }
            public int rowEnd { get; set; }

            private int NumberFromExcelColumn(string column)
            {
                int retVal = 0;
                string col = column.ToUpper();
                for (int iChar = col.Length - 1; iChar >= 0; iChar--)
                {
                    char colPiece = col[iChar];
                    int colNum = colPiece - 64;
                    retVal = retVal + colNum * (int)Math.Pow(26, col.Length - (iChar + 1));
                }
                return retVal;
            }
        }

    }


}
