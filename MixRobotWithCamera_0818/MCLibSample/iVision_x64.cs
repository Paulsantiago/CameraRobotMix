using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MiM_iVision
{
    // Declaring iImage class
    public class iVision
    {
        const string x64dllName = "iVision_x64.dll";

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "iGetiMatchVersion")]
        public extern static String iGetiMatchVersion();

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "iGetiMatchVersionDate")]
        public extern static String iGetiMatchVersionDate();

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "iGetiFindVersion")]
        public extern static String iGetiFindVersion();

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "iGetiFindVersionDate")]
        public extern static String iGetiFindVersionDate();

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "iGetErrorText")]
        public extern static String iGetErrorText(E_iVision_ERRORS eError);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "iGetKeySerial")]
        public extern static E_iVision_ERRORS iGetKeySerial(ref int Serial);

    }

}