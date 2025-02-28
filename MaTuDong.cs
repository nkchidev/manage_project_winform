using System.Data;

namespace ProjectStorage
{
    class MaTuDong
    {
        public string ma(DataTable tbl,int cot,string ma)
        {
            string xauDL;
            if (tbl.Rows.Count > 0)
            {
                try
                {
                    DataRow hangcuoi = tbl.Rows[tbl.Rows.Count - 1];
                    xauDL = hangcuoi[cot].ToString();
                    xauDL.Trim();
                    xauDL = xauDL.Substring(3);
                    int maso = int.Parse(xauDL);
                    maso++;
                    xauDL = ma + maso.ToString("0000");
                }
                catch
                {
                    xauDL = ma + "0000";
                }
            }
            else
                xauDL = ma + "0000";
            return xauDL;
        }
    }
}
