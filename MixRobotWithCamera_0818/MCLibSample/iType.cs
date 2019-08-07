using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct iIPoint
{
    public int x;
    public int y;
}

[StructLayout(LayoutKind.Sequential)]
public struct iDPoint
{
    public double x;
    public double y;
}

[StructLayout(LayoutKind.Sequential)]
public struct iFPoint
{
    public float x;
    public float y;
}

[StructLayout(LayoutKind.Sequential)]
public struct mRect
{
	public int  top;
	public int  down;
	public int  left;
	public int  right;
}

[StructLayout(LayoutKind.Sequential)]
public struct POINTGROUP
{
    public iIPoint Pos1;
    public iIPoint Pos2;
    public iIPoint Pos3;
    public iIPoint Pos4;
};

[StructLayout(LayoutKind.Sequential)]
public struct iMinBox
{
    public float X_center;
    public float Y_center;
    public float Height;
    public float width;
    public float Angle;
    public POINTGROUP POS;
};

[StructLayout(LayoutKind.Sequential)]
public struct ColMean	                                           
{
    public double R;
    public double G;
    public double B;
};

[StructLayout(LayoutKind.Sequential)]
public struct NCCFind
{
	public int		Width;
	public int		Height;

	public double     CX;
	public double     CY;
	public double	    Angle;
	public double	    Scale;
	public double	    Score;
}

[StructLayout(LayoutKind.Sequential)]
public struct ignore_Para
{
    public iDPoint Cp;
    public iDPoint Pt1;
    public iDPoint Pt2;
    public double InnerR;
    public double OuterR;
    public UInt32 TypeofRegion;
}

[StructLayout(LayoutKind.Sequential)]
public struct GeoFind
{
    public double x;
    public double y;
    public double Score;
    public double Angle;
    public double Scale;
}

[StructLayout(LayoutKind.Sequential)]
public struct iMCircle_Region
{
    public double Cx;
    public double Cy;
    public double InnerR;
    public double OuterR;
}


[StructLayout(LayoutKind.Sequential)]
public struct iCircle_Measured
{
    public iDPoint Cp;
    public double Radius;
    public double Roundness;
	public double PL_Difference;
}

[StructLayout(LayoutKind.Sequential)]
public struct iMLine_Region
{
    public iIPoint  Cp;
    public iIPoint  StP;
    public iIPoint  EnP;
    public UInt32   wid;
    public UInt32   hei;
    public double   angle;
}

[StructLayout(LayoutKind.Sequential)]
public struct iLine_Measured
{
    public iIPoint p1;
    public iIPoint p2;
    public double A;
    public double B;
    public double C;
}

[StructLayout(LayoutKind.Sequential)]
public  struct iObject_BlobFEA
{
	public long		area;
	public double  	XCenter;
	public double	YCenter;
	public iMinBox	MinRect;
}