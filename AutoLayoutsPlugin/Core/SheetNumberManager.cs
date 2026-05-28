using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoLayoutsPlugin.Core
{
    /// <summary>
    /// Manages sheet number selection and processing
    /// </summary>
    public class SheetNumberManager
    {
        private Document document;
        private Editor editor;
        private Logger logger = new Logger();

        public SheetNumberManager(Document doc)
        {
            document = doc;
            editor = doc.Editor;
        }

        /// <summary>
        /// Gets all sheet numbers from a specific layer
        /// </summary>
        public List<string> GetSheetNumbersFromLayer(string layerName)
        {
            var sheetNumbers = new List<string>();

            try
            {
                using (var trans = document.TransactionManager.StartTransaction())
                {
                    var blockTable = trans.GetObject(document.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                    var modelSpace = trans.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForRead) as BlockTableRecord;

                    foreach (ObjectId id in modelSpace)
                    {
                        var entity = trans.GetObject(id, OpenMode.ForRead) as Entity;
                        if (entity is DBText dbText && entity.Layer == layerName)
                        {
                            sheetNumbers.Add(dbText.TextString);
                        }
                        else if (entity is MText mText && entity.Layer == layerName)
                        {
                            sheetNumbers.Add(mText.Text);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Error getting sheet numbers from layer {layerName}: {ex.Message}");
            }

            return sheetNumbers;
        }

        /// <summary>
        /// Prompts user to select sheet numbers from model space
        /// </summary>
        public List<string> PromptSelectSheetNumbers()
        {
            var sheetNumbers = new List<string>();

            try
            {
                var pmo = new PromptEntityOptions("\nSelect sheet number (MText):");
                pmo.SetRejectMessage("Invalid entity. Please select MText.");
                pmo.AddAllowedClass(typeof(MText), true);

                var res = editor.GetEntity(pmo);
                if (res.Status == PromptStatus.OK)
                {
                    using (var trans = document.TransactionManager.StartTransaction())
                    {
                        var mText = trans.GetObject(res.ObjectId, OpenMode.ForRead) as MText;
                        if (mText != null)
                        {
                            sheetNumbers = GetSheetNumbersFromLayer(mText.Layer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Error prompting for sheet numbers: {ex.Message}");
            }

            return sheetNumbers;
        }
    }
}
