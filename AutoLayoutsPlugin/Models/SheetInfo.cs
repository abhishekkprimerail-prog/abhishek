using System;
using Autodesk.AutoCAD.Geometry;

namespace AutoLayoutsPlugin.Models
{
    /// <summary>
    /// Represents sheet information
    /// </summary>
    public class SheetInfo
    {
        public string SheetNumber { get; set; }
        public string SheetName { get; set; }
        public Point3d SheetFrameCenter { get; set; }
        public double FrameWidth { get; set; }
        public double FrameHeight { get; set; }
    }
}
