using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MiM_iVision
{
    // Declaring iImage class
    public class iImage
    {
        const string x64dllName = "iImage_x64.dll";

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "CreateGrayiImage")]
        public extern static IntPtr CreateGrayiImage();

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "CreateColoriImage")]
        public extern static IntPtr CreateColoriImage();

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "CreateGrayiImageEx")]
        public extern static IntPtr CreateGrayiImageEx(int wid, int hei);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "CreateColoriImageEx")]
        public extern static IntPtr CreateColoriImageEx(int wid, int hei);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iImageCopy")]
        public extern static E_iVision_ERRORS iImageCopy(IntPtr DestImg, IntPtr SrcImg);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DestroyiImage")]
        public extern static E_iVision_ERRORS DestroyiImage(IntPtr iImg);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iImageResize")]
        public extern static E_iVision_ERRORS iImageResize(IntPtr iImg, int width, int height);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "GetSubiImage")]
        public extern static E_iVision_ERRORS GetSubiImage(IntPtr DesiImg, IntPtr SrciImg, mRect ROI);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "GetWidth")]
        public extern static int GetWidth(IntPtr iImg);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "GetHeight")]
        public extern static int GetHeight(IntPtr iImg);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iImageIsNULL")]
        public extern static E_iVision_ERRORS iImageIsNULL(IntPtr iImg);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iGetBitmapAddress")]
        public extern static IntPtr iGetBitmapAddress(IntPtr iImg);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iPointerToiImage")]
        public extern static E_iVision_ERRORS iPointerToiImage(IntPtr iImg, IntPtr DataSrc, int wid, int hei);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iPointerFromiImage")]
        public extern static E_iVision_ERRORS iPointerFromiImage(IntPtr iImg, ref byte Destdata, int wid, int hei);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "iReadImage")]
        public extern static E_iVision_ERRORS iReadImage(IntPtr iImg, string filename);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "iSaveImage")]
        public extern static E_iVision_ERRORS iSaveImage(IntPtr iImg, string filename);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iVarPtr")]
        public extern static IntPtr iVarPtr(ref byte ptr);

        [DllImport(x64dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "iGetCameraAlignSize")]
        public extern static E_iVision_ERRORS iGetCameraAlignSize(IntPtr iImg, ref Int32 AlignWid, ref Int32 AlignHei);

    }

 
}