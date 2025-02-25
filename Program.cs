using ProjectStorage.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using VBSQLHelper;

namespace ProjectStorage
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var stringConn = File.ReadAllText("databaseconfig.txt");
            SQLHelper.CONNECTION_STRINGS = stringConn;
            
          //  Application.Run(new FormMDIMainApplication());
            Application.Run(FrmMain.Instance);
        }
    }
}