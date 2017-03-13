using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace LoL_AutoLogin
{
    class CustomFont
    {
        public static FontFamily Load(byte [] fontResource)
        {
            var pfc = new PrivateFontCollection();

            IntPtr pointer = Marshal.AllocCoTaskMem(fontResource.Length);
            Marshal.Copy(fontResource, 0, pointer, fontResource.Length);
            pfc.AddMemoryFont(pointer, fontResource.Length);
            Marshal.FreeCoTaskMem(pointer);

            return pfc.Families[0];
        }
    }
}
