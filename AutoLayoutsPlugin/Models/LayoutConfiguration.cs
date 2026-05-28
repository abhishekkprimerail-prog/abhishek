using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.Geometry;

namespace AutoLayoutsPlugin.Models
{
    /// <summary>
    /// Configuration for layout creation
    /// </summary>
    public class LayoutConfiguration
    {
        public enum LayoutType
        {
            SingleViewport = 1,
            DualViewport = 2,
            Mixed = 3
        }

        public LayoutType Type { get; set; }
        public List<string> SheetNumbers { get; set; } = new List<string>();
        public List<string> SheetNumbers2 { get; set; } = new List<string>();
        
        public ViewportInfo FirstViewport { get; set; }
        public ViewportInfo SecondViewport { get; set; }
        
        public bool UseCommonDimensions { get; set; }
        public bool FirstReportOnTop { get; set; }
        
        public string TitleBlockLayer { get; set; }
        public string KeyPlanLayer { get; set; }
    }
}
