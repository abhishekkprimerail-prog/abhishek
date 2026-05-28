using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
using AutoLayoutsPlugin.UI;
using AutoLayoutsPlugin.Core;
using System;

[assembly: CommandClass(typeof(AutoLayoutsPlugin.Commands))]
[assembly: ExtensionApplication(typeof(AutoLayoutsPlugin.App))]

namespace AutoLayoutsPlugin
{
    /// <summary>
    /// Main application class for the AutoCAD plugin
    /// </summary>
    public class App : IExtensionApplication
    {
        private static Logger logger = new Logger();

        public void Initialize()
        {
            try
            {
                logger.Log("AutoLayouts Plugin Initialized");
            }
            catch (Exception ex)
            {
                logger.Error($"Initialization Error: {ex.Message}");
            }
        }

        public void Terminate()
        {
            try
            {
                logger.Log("AutoLayouts Plugin Terminated");
            }
            catch (Exception ex)
            {
                logger.Error($"Termination Error: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Commands class for AutoCAD plugin
    /// </summary>
    public class Commands
    {
        private static Logger logger = new Logger();

        [CommandMethod("AUTOLAYOUTS")]
        public void AutoLayoutsCommand()
        {
            try
            {
                Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                Editor ed = doc.Editor;

                // Show the main dialog
                LayoutDialog dialog = new LayoutDialog();
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                logger.Error($"Command Error: {ex.Message}");
            }
        }
    }
}
