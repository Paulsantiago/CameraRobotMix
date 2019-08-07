using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MiM_iVision
{
    public class iMatch
    {
        const string x64dllName = "iMatch_x64.dll";

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "CreateNCCMatch")]
        public extern static IntPtr CreateNCCMatch();

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DestroyNCCMatch")]
        public extern static E_iVision_ERRORS DestroyNCCMatch(IntPtr objptr);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "CreateNCCModel")]
        public extern static E_iVision_ERRORS CreateNCCModel(IntPtr Img, IntPtr model, bool mask_used = false);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "MatchNCCModel")]
        public extern static E_iVision_ERRORS MatchNCCModel(IntPtr Img, IntPtr model);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "LoadiMatchModel")]
        public extern static E_iVision_ERRORS LoadiMatchModel(IntPtr model, string path);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "SaveiMatchModel")]
        public extern static E_iVision_ERRORS SaveiMatchModel(IntPtr model, string path);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iDrawiMatchResults")]
        public extern static E_iVision_ERRORS iDrawiMatchResults(IntPtr model, IntPtr hDC, double Scale);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iGetNCCMatchResults")]
        public extern static E_iVision_ERRORS iGetNCCMatchResults(IntPtr model, int index, ref NCCFind data);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iGetNCCMatchNum")]
        public extern static E_iVision_ERRORS iGetNCCMatchNum(IntPtr model, ref Int32 Nums);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iSetDontCareThreshold")]
        public extern static E_iVision_ERRORS iSetDontCareThreshold(IntPtr model, int val);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iGetDontCareThreshold")]
        public extern static E_iVision_ERRORS iGetDontCareThreshold(IntPtr model, ref Int32 val);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iSetMinScore")]
        public extern static E_iVision_ERRORS iSetMinScore(IntPtr model, double val);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iGetMinScore")]
        public extern static E_iVision_ERRORS iGetMinScore(IntPtr model, ref double val);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iSetAngle")]
        public extern static E_iVision_ERRORS iSetAngle(IntPtr model, double val1, double val2);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iGetAngle")]
        public extern static E_iVision_ERRORS iGetAngle(IntPtr model, ref double val1, ref double val2);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iSetIsRotated")]
        public extern static E_iVision_ERRORS iSetIsRotated(IntPtr model, bool flag);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iGetIsRotated")]
        public extern static E_iVision_ERRORS iGetIsRotated(IntPtr model, ref bool flag);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iSetScale")]
        public extern static E_iVision_ERRORS iSetScale(IntPtr model, double val1, double val2);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iGetScale")]
        public extern static E_iVision_ERRORS iGetScale(IntPtr model, ref double val1, ref double val2);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iSetIsScaled")]
        public extern static E_iVision_ERRORS iSetIsScaled(IntPtr model, bool flag);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iGetIsScaled")]
        public extern static E_iVision_ERRORS iGetIsScaled(IntPtr model, ref bool flag);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iSetIsDontArea")]
        public extern static E_iVision_ERRORS iSetIsDontArea(IntPtr model, bool flag);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iGetIsDontArea")]
        public extern static E_iVision_ERRORS iGetIsDontArea(IntPtr model, ref bool flag);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iSetOccurrence")]
        public extern static E_iVision_ERRORS iSetOccurrence(IntPtr model, int val);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iGetOccurrence")]
        public extern static E_iVision_ERRORS iGetOccurrence(IntPtr model, ref int val);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iGetModelWidth")]
        public extern static E_iVision_ERRORS iGetModelWidth(IntPtr model, ref int val);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iGetModelHeight")]
        public extern static E_iVision_ERRORS iGetModelHeight(IntPtr model, ref int val);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iSetMinReduceArea")]
        public extern static E_iVision_ERRORS iSetMinReduceArea(IntPtr model, int val);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iGetMinReduceArea")]
        public extern static E_iVision_ERRORS iGetMinReduceArea(IntPtr model, ref int val);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iIsPatternLearn")]
        public extern static E_iVision_ERRORS iIsPatternLearn(IntPtr model);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iIsColorModel")]
        public extern static E_iVision_ERRORS iIsColorModel(IntPtr model);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iVisitingKey")]
        public extern static E_iVision_ERRORS iVisitingKey();

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iGetKeyState")]
        public extern static E_iVision_ERRORS iGetKeyState();
    }
}
