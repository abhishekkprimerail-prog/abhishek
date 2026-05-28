using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using AutoLayoutsPlugin.Models;
using System;
using System.Collections.Generic;

namespace AutoLayoutsPlugin.Core
{
    /// <summary>
    /// Main processor for layout creation workflow
    /// </summary>
    public class LayoutProcessor
    {
        private Document document;
        private Editor editor;
        private SheetNumberManager sheetNumberManager;
        private ViewportManager viewportManager;
        private LayoutCreator layoutCreator;
        private Logger logger = new Logger();

        public LayoutProcessor(Document doc)
        {
            document = doc;
            editor = doc.Editor;
            sheetNumberManager = new SheetNumberManager(doc);
            viewportManager = new ViewportManager(doc);
            layoutCreator = new LayoutCreator(doc);
        }

        /// <summary>
        /// Processes single viewport layout creation
        /// </summary>
        public bool ProcessSingleViewportLayout(LayoutConfiguration config)
        {
            try
            {
                if (config.SheetNumbers.Count == 0)
                {
                    editor.WriteMessage("\nNo sheet numbers selected.");
                    return false;
                }

                editor.WriteMessage($"\nProcessing {config.SheetNumbers.Count} sheets with single viewport...");

                int layoutCount = 0;
                foreach (var sheetNumber in config.SheetNumbers)
                {
                    string layoutName = $"Sheet-{sheetNumber}";
                    
                    // Create layout
                    var layoutId = layoutCreator.CreateLayout(layoutName);
                    if (layoutId == ObjectId.Null) continue;

                    layoutCount++;
                    editor.WriteMessage($"\nCreated layout: {layoutName}");
                }

                editor.WriteMessage($"\nSuccessfully created {layoutCount} layouts.");
                return true;
            }
            catch (Exception ex)
            {
                logger.Error($"Error processing single viewport layout: {ex.Message}");
                editor.WriteMessage($"\nError: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Processes dual viewport layout creation
        /// </summary>
        public bool ProcessDualViewportLayout(LayoutConfiguration config)
        {
            try
            {
                if (config.SheetNumbers.Count == 0 || config.SheetNumbers2.Count == 0)
                {
                    editor.WriteMessage("\nSheet numbers for both reports required.");
                    return false;
                }

                if (config.FirstViewport == null || config.SecondViewport == null)
                {
                    editor.WriteMessage("\nViewport information not configured.");
                    return false;
                }

                int minSheets = Math.Min(config.SheetNumbers.Count, config.SheetNumbers2.Count);
                editor.WriteMessage($"\nProcessing {minSheets} sheets with dual viewports...");

                int layoutCount = 0;
                for (int i = 0; i < minSheets; i++)
                {
                    string layoutName = $"Sheet-{config.SheetNumbers[i]}-{config.SheetNumbers2[i]}";
                    
                    // Create layout
                    var layoutId = layoutCreator.CreateLayout(layoutName);
                    if (layoutId == ObjectId.Null) continue;

                    layoutCount++;
                    editor.WriteMessage($"\nCreated layout: {layoutName}");
                }

                editor.WriteMessage($"\nSuccessfully created {layoutCount} layouts.");
                return true;
            }
            catch (Exception ex)
            {
                logger.Error($"Error processing dual viewport layout: {ex.Message}");
                editor.WriteMessage($"\nError: {ex.Message}");
                return false;
            }
        }
    }
}
