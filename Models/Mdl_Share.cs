using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProjectStorage
{
    class Mdl_Share
    {
        public static string MACHINE_NAME;
        public static string SERIAL_HDD;
        public static bool IS_LOGIN = false;
        public static bool IS_LOGOUT = false;
        public static bool CONNECTED_INTERNET = true;
        public static bool IS_EDIT_USER = false;
        public static bool IS_EDIT_CONFIG = false;
        public static string USERNAME_EDIT = "";
        public static string SERIAL_HDD_EDIT = "";
        public static string MACHINE_NAME_EDIT = "";       
        public static string TEMP_PATH = Path.GetTempPath() + Assembly.GetExecutingAssembly().GetName().Name + "\\";
        public static string TEMP_PATH2 = Path.GetTempPath() + Assembly.GetExecutingAssembly().GetName().Name;

        public static User currentUser;
        public static List<Permission> currentPermission;
        
    }
}
