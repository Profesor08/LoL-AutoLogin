using Microsoft.Win32;

namespace LoL_AutoLogin
{
    class Reg
    {
        private static string keyName = "HKEY_LOCAL_MACHINE\\SOFTWARE\\LolAutoLogin\\";

        public static object Get(string valueName)
        {
            return Registry.GetValue(keyName, valueName, null);
        }

        public static object Get(string valueName, object defaultValue)
        {
            return Registry.GetValue(keyName, valueName, defaultValue);
        }

        public static void Set(string valueName, object value)
        {
            Registry.SetValue(keyName, valueName, value);
        }
    }
}