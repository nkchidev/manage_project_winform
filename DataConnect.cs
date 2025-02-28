using System.Data;
using System.Data.SqlClient;
using DevComponents.DotNetBar;
using System.Windows.Forms;

namespace ProjectStorage
{
    class DataConnect : DataTable
    {
        //Khai bao cac thuoc tinh

        SqlConnection _nConnection;
        private static string NStrConnection = null;//"Server=(local);database=quanlycongtrinh;uid=sa;pwd=123";//!Q2w3e4r5t";
        SqlCommand _nCmd;
        SqlDataAdapter _nDad;

        SqlTransaction mTransaction;
        public void transStartTransaction()
        {
            mTransaction = _nConnection.BeginTransaction();
        }

        public bool transExecuteNonQuery(SqlCommand cmd)
        {
            try
            {
                cmd.Transaction = mTransaction;
                cmd.Connection = _nConnection;

                int rows = cmd.ExecuteNonQuery();

                return rows > 0;
            }
            catch
            {
                return false;
            }

            return false;
        }

        public void transCommit()
        {
            mTransaction.Commit();
        }
        public void transRollback()
        {
            mTransaction.Rollback();
        }

        //Load cau SQL

        public DataTable Doc(SqlCommand cmd)
        {
            _nCmd = cmd;
            try
            {
                //Ket noi cau lenh cmd voi doi tuong ket noi
                _nCmd.Connection = _nConnection;

                _nDad = new SqlDataAdapter {SelectCommand = _nCmd};

                //Nap cau lenh command vao DataAdapter

                //Xoa Database
                Clear();

                //Nap DataBase
                _nDad.Fill(this);

            }
            catch
            {
                //MessageBoxEx.Show("Không thể thực thi câu lệnh SQL này!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }

            return this;
        }

        public DataTable queryTable(SqlCommand cmd)
        {
            _nCmd = cmd;
            try
            {
                //Ket noi cau lenh cmd voi doi tuong ket noi
                _nCmd.Connection = _nConnection;

                _nDad = new SqlDataAdapter { SelectCommand = _nCmd };

                //Nap cau lenh command vao DataAdapter
                DataTable dt = new DataTable();

                //Nap DataBase
                _nDad.Fill(dt);

                return dt;
            }
            catch
            {
                MessageBoxEx.Show("Không thể thực thi câu lệnh SQL này!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        //Mo ket noi
        public void Mo()
        {
            //Nap cau lenh ket noi
            string path = System.IO.Directory.GetCurrentDirectory();
            path = path + "\\" + "databaseconfig.txt";
            try
            {
                //if (NStrConnection == null)
                {
                    NStrConnection = path;
                }

                using (System.IO.StreamReader sr = new System.IO.StreamReader(NStrConnection))
                {
                    NStrConnection = sr.ReadToEnd();
                }
            }
            catch
            {
                MessageBox.Show("File not found: " + path);
            }

            _nConnection = new SqlConnection(NStrConnection);
            _nConnection.Open();
        }

        public void Dong()
        {
            //Dong ket noi
            _nConnection.Close();
        }

        public bool ThucThi(SqlCommand cmd)
        {
            try
            {
                cmd.Connection = _nConnection;
                //Thuc hien cau lenh SQL 

                cmd.ExecuteScalar();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public int executeCMD(SqlCommand cmd)
        {
            try
            {
                cmd.Connection = _nConnection;
                //Thuc hien cau lenh SQL 

                object o = cmd.ExecuteScalar();
                System.Int32 id = (System.Int32)o;

                return id;
            }
            catch
            {
                return 0;
            }
        }

        public bool update(SqlCommand cmd)
        {
            try
            {
                cmd.Connection = _nConnection;
                //Thuc hien cau lenh SQL 

                int rows = cmd.ExecuteNonQuery();

                return rows > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool execute(SqlCommand cmd)
        {
            try
            {
                cmd.Connection = _nConnection;
                int rows = cmd.ExecuteNonQuery();

                return rows > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
