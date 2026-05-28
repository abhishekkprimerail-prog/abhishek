using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AutoLayoutsPlugin.Core
{
    /// <summary>
    /// Manages viewport selection and configuration
    /// </summary>
    public class ViewportManager
    {
        private Document document;
        private Editor editor;
        private Logger logger = new Logger();

        public ViewportManager(Document doc)
        {
            document = doc;
            editor = doc.Editor;
        }

        /// <summary>
        /// Prompts user to select viewport width
        /// </summary>
        public double PromptViewportWidth()
        {
            try
            {
                editor.WriteMessage("\nSelect viewport width by clicking two points:");
                var ppo1 = new PromptPointOptions("First point:");
                var res1 = editor.GetPoint(ppo1);
                if (res1.Status != PromptStatus.OK) return 0;

                var ppo2 = new PromptPointOptions("Second point:");
                var res2 = editor.GetPoint(ppo2);
                if (res2.Status != PromptStatus.OK) return 0;

                double width = res1.Value.DistanceTo(res2.Value);
                editor.WriteMessage($"\nViewport Width: {width}");
                return width;
            }
            catch (Exception ex)
            {
                logger.Error($"Error getting viewport width: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Prompts user to select viewport height
        /// </summary>
        public double PromptViewportHeight()
        {
            try
            {
                editor.WriteMessage("\nSelect viewport height by clicking two points:");
                var ppo1 = new PromptPointOptions("First point:");
                var res1 = editor.GetPoint(ppo1);
                if (res1.Status != PromptStatus.OK) return 0;

                var ppo2 = new PromptPointOptions("Second point:");
                var res2 = editor.GetPoint(ppo2);
                if (res2.Status != PromptStatus.OK) return 0;

                double height = res1.Value.DistanceTo(res2.Value);
                editor.WriteMessage($"\nViewport Height: {height}");
                return height;
            }
            catch (Exception ex)
            {
                logger.Error($"Error getting viewport height: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Prompts user to select a viewport rectangle
        /// </summary>
        public Models.ViewportInfo PromptSelectViewport(string promptMessage = "Select viewport:")
        {
            var viewportInfo = new Models.ViewportInfo();

            try
            {
                var pmo = new PromptEntityOptions($"\n{promptMessage}");
                pmo.SetRejectMessage("Invalid entity. Please select a rectangle/viewport.");
                pmo.AddAllowedClass(typeof(Polyline), true);
                pmo.AddAllowedClass(typeof(Line), true);

                var res = editor.GetEntity(pmo);
                if (res.Status == PromptStatus.OK)
                {
                    using (var trans = document.TransactionManager.StartTransaction())
                    {
                        var entity = trans.GetObject(res.ObjectId, OpenMode.ForRead) as Entity;
                        viewportInfo.ViewportId = res.ObjectId;
                        
                        if (entity is Polyline poly)
                        {
                            var extents = poly.Bounds.Value;
                            viewportInfo.Width = extents.MaxPoint.X - extents.MinPoint.X;
                            viewportInfo.Height = extents.MaxPoint.Y - extents.MinPoint.Y;
                            viewportInfo.InsertionPoint = extents.MinPoint;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Error selecting viewport: {ex.Message}");
            }

            return viewportInfo;
        }
    }
}
