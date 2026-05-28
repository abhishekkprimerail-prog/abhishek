using System;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;

namespace AutoLayoutsPlugin.Models
{
    /// <summary>
    /// Represents a single viewport configuration
    /// </summary>
    public class ViewportInfo
    {
        public string Name { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public Point3d InsertionPoint { get; set; }
        public ObjectId ViewportId { get; set; }
        public int ViewportNumber { get; set; }
    }
}
