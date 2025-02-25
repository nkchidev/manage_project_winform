using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ProjectStorage
{
    class Utils
    {
        static public Double getDoubleValueOfRow(DataRow r, String field)
        {
            //try
            //{
            //    Double kl = (Double)r[field];
            //    return kl;
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            Double result = 0;
            Double.TryParse(r[field].ToString(), out result); 

            return result;
        }

        static public int getIntValueOfRow(DataRow r, String field)
        {
            try
            {
                int v = (int)r[field];
                return v;
            }
            catch (Exception e)
            {
            }

            return 0;
        }

        static public Int64 getBigIntValueOfRow(DataRow r, String field)
        {
            try
            {
                Int64 v = (Int64)r[field];
                return v;
            }
            catch (Exception e)
            {
            }

            return 0;
        }

        static public Int64 getBigIntValueOfRow(DataRow r, int field)
        {
            try
            {
                Int64 v = (Int64)r[field];
                return v;
            }
            catch (Exception e)
            {
            }

            return 0;
        }

        static public Double getDoubleValueOfRow(DataRow r, int field)
        {
            try
            {
                Double kl = (Double)r[field];
                return kl;
            }
            catch (Exception e)
            {
            }

            return 0;
        }

        static public int getIntValueOfRow(DataRow r, int field)
        {
            try
            {
                int v = (int)r[field];
                return v;
            }
            catch (Exception e)
            {
            }

            return 0;
        }


        static public Double convertStringToDouble(String s)
        {
            if (s == null || s.Length == 0)
                return 0;

           s = s.Replace(",", "");

            try
            {
                //return Double.Parse(s);
                return Convert.ToDouble(s);
            }
            catch (Exception e)
            {
            }

            return 0;
        }
        static public Double convertMoneyStringToDouble(String s)
        {
            if (s == null || s.Length == 0)
                return 0;

            s = s.Replace(".", "");

            try
            {
                return Double.Parse(s);
            }
            catch (Exception e)
            {
            }

            return 0;
        }

        static public String doubleToMoneyString(Double m)
        {
            try
            {
                //String s = m.ToString("0,0");

                Int64 im = (Int64)m;

                return formatNumber((long)m);
            }
            catch (Exception e)
            {
            }

            return "0";
        }

        static public String floatToString(float m)
        {
            try
            {
                String s = String.Format("{0:F4}", m);
                return s;
            }
            catch (Exception e)
            {
            }

            return "0";
        }

        public static String formatNumber(long number)
        {
            if (number == 0)
            {
                return "0";
            }
            //  2345678

            StringBuilder sb = new StringBuilder();
            byte[] tmp = new byte[30];
            int j = 0;
            int signal = 1;
            if (number < 0)
            {
                number = -number;
                signal = -1;
            }

            while (number > 0)
            {
                tmp[j++] = (byte)(number % 10);
                number = number / 10;
            }
            // 00013

            sb.Length = 0;
            if (signal == -1)
                sb.Append('-');
            for (int i = j - 1; i >= 0; i--)
            {
                sb.Append((char)('0' + tmp[i]));
                if ((i % 3) == 0 && i > 0)
                {
                    sb.Append('.');
                }
            }

            String s = "" + sb.ToString();

            return s;
        }

        static public String getStringOfRow(DataRow r, String field)
        {
            try
            {
                String s = (String)r[field];
                return s.Trim();
            }catch(Exception e)
            {
            }

            return "";
        }

        static public String getStringOfRow(DataRow r, int field)
        {
            try
            {
                String s = (String)r[field];
                return s.Trim();
            }
            catch (Exception e)
            {
            }

            return "";
        }

        static public String trim(String s)
        {
            if (s == null)
                return "";

            return s.Trim();
        }

        static public String dateToStringOfRow(DataRow r, int idx)
        {
            try
            {
                DateTime d = (DateTime)r[idx];

                return d.ToString("dd/MM/yyyy");
            }
            catch(Exception ex)
            {}

            return "";
        }

        static public String dateToStringOfRow(DataRow r, String idx)
        {
            try
            {
                DateTime d = (DateTime)r[idx];

                return d.ToString("dd/MM/yyyy");
            }
            catch (Exception ex)
            { }

            return "";
        }

        static public DateTime convert_ddMMyyyyStringToDateTime(String s)
        {
            try
            {
                string[] arr = s.Split(new char[]{'/'});
                int dd = Int32.Parse(arr[0]);
                int MM = Int32.Parse(arr[1]);
                int yyyy = Int32.Parse(arr[2]);

                DateTime dt = new DateTime(yyyy, MM, dd);
                return dt;
            }
            catch (Exception ex)
            {
            }

            return DateTime.Now;
        }

        static public DateTime getDateOfRow(DataRow r, int idx)
        {
            try
            {
                DateTime d = (DateTime)r[idx];
                return d;
            }
            catch(Exception e)
            {
            }
            return DateTime.Now;
        }

        static public DateTime getDateOfRow(DataRow r, String idx)
        {
            try
            {
                DateTime d = (DateTime)r[idx];
                return d;
            }
            catch (Exception e)
            {
            }
            return DateTime.Now;
        }

        static public void log(String error)
        {
            DateTime date = DateTime.Now;
            String sdate = date.ToString("HH:mm:ss ddMMyyyy");

            String fulline = sdate + " " + error;
            //  log to file here
        }

        static public String doubleToString(double v)
        {
            double retail = v - (int)v;
            if (retail == 0)
            {
                return "" + v;
            }

            String s = String.Format("{0:F}", v);
            return s;
        }

        static public Int64 sqlNumberToInt64(object o)
        {
            try
            {
                return Convert.ToInt64(o);
            }
            catch(Exception e)
            {
            }

            return 0;
        }

        static public int convertStringToInt(String s)
        {
            if (s == null || s.Length == 0)
                return 0;

            try
            {
                //return Int32.Parse(s);
                return Convert.ToInt32(s);
            }
            catch (Exception e)
            {
            }

            return 0;
        }
    }
}
