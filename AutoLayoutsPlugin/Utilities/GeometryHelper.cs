using Autodesk.AutoCAD.Geometry;
using System;

namespace AutoLayoutsPlugin.Utilities
{
    /// <summary>
    /// Geometry helper methods
    /// </summary>
    public class GeometryHelper
    {
        /// <summary>
        /// Calculates the center point of a rectangle
        /// </summary>
        public static Point3d GetRectangleCenter(Point3d minPoint, Point3d maxPoint)
        {
            return new Point3d(
                (minPoint.X + maxPoint.X) / 2,
                (minPoint.Y + maxPoint.Y) / 2,
                (minPoint.Z + maxPoint.Z) / 2
            );
        }

        /// <summary>
        /// Calculates width and height from two points
        /// </summary>
        public static void GetDimensions(Point3d point1, Point3d point2, out double width, out double height)
        {
            width = Math.Abs(point2.X - point1.X);
            height = Math.Abs(point2.Y - point1.Y);
        }

        /// <summary>
        /// Checks if a point is within a rectangle
        /// </summary>
        public static bool IsPointInRectangle(Point3d point, Point3d minPoint, Point3d maxPoint)
        {
            return point.X >= minPoint.X && point.X <= maxPoint.X &&
                   point.Y >= minPoint.Y && point.Y <= maxPoint.Y;
        }
    }
}
