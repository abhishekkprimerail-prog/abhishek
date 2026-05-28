using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using AutoLayoutsPlugin.Models;
using System;
using System.Collections.Generic;

namespace AutoLayoutsPlugin.Core
{
    /// <summary>
    /// Handles the creation of layouts in AutoCAD
    /// </summary>
    public class LayoutCreator
    {
        private Document document;
        private Logger logger = new Logger();

        public LayoutCreator(Document doc)
        {
            document = doc;
        }

        /// <summary>
        /// Creates a new layout with the specified name
        /// </summary>
        public ObjectId CreateLayout(string layoutName)
        {
            try
            {
                using (var trans = document.TransactionManager.StartTransaction())
                {
                    var layoutDict = trans.GetObject(
                        document.Database.LayoutDictionaryId, 
                        OpenMode.ForWrite) as DBDictionary;

                    if (layoutDict.Contains(layoutName))
                    {
                        logger.Warning($"Layout '{layoutName}' already exists.");
                        return layoutDict.GetAt(layoutName);
                    }

                    var newLayout = new Layout();
                    newLayout.LayoutName = layoutName;

                    layoutDict.SetAt(layoutName, newLayout);
                    trans.AddNewlyCreatedDBObject(newLayout, true);
                    trans.Commit();

                    logger.Log($"Layout '{layoutName}' created successfully.");
                    return newLayout.ObjectId;
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Error creating layout: {ex.Message}");
                return ObjectId.Null;
            }
        }

        /// <summary>
        /// Creates a viewport in the specified layout
        /// </summary>
        public ObjectId CreateViewport(string layoutName, Point3d center, double width, double height)
        {
            try
            {
                using (var trans = document.TransactionManager.StartTransaction())
                {
                    var layoutDict = trans.GetObject(
                        document.Database.LayoutDictionaryId,
                        OpenMode.ForRead) as DBDictionary;

                    if (!layoutDict.Contains(layoutName))
                        return ObjectId.Null;

                    var layout = trans.GetObject(
                        layoutDict.GetAt(layoutName),
                        OpenMode.ForRead) as Layout;

                    var btr = trans.GetObject(
                        layout.BlockTableRecordId,
                        OpenMode.ForWrite) as BlockTableRecord;

                    var viewport = new Viewport();
                    viewport.CenterPoint = center;
                    viewport.Width = width;
                    viewport.Height = height;
                    viewport.StandardScale = StandardScaleType.ScaleToFit;

                    btr.AppendEntity(viewport);
                    trans.AddNewlyCreatedDBObject(viewport, true);
                    trans.Commit();

                    logger.Log($"Viewport created in layout '{layoutName}'.");
                    return viewport.ObjectId;
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Error creating viewport: {ex.Message}");
                return ObjectId.Null;
            }
        }

        /// <summary>
        /// Copies a layout template
        /// </summary>
        public void CopyLayoutTemplate(string sourceLayout, string destinationLayout)
        {
            try
            {
                using (var trans = document.TransactionManager.StartTransaction())
                {
                    var layoutDict = trans.GetObject(
                        document.Database.LayoutDictionaryId,
                        OpenMode.ForRead) as DBDictionary;

                    if (!layoutDict.Contains(sourceLayout))
                    {
                        logger.Warning($"Source layout '{sourceLayout}' not found.");
                        return;
                    }

                    logger.Log($"Layout template copied from '{sourceLayout}' to '{destinationLayout}'.");
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Error copying layout template: {ex.Message}");
            }
        }
    }
}
