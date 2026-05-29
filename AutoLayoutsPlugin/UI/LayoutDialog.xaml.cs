using System;
using System.Windows;
using Autodesk.AutoCAD.ApplicationServices;
using AutoLayoutsPlugin.Core;
using AutoLayoutsPlugin.Models;

namespace AutoLayoutsPlugin.UI
{
    /// <summary>
    /// Main dialog for AutoLayouts plugin
    /// </summary>
    public partial class LayoutDialog : Window
    {
        private Document document;
        private Editor editor;
        private Logger logger = new Logger();
        private LayoutConfiguration config;

        public LayoutDialog()
        {
            InitializeComponent();
            document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            editor = document.Editor;
            config = new LayoutConfiguration();
        }

        private void OneViewportRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            config.Type = LayoutConfiguration.LayoutType.SingleViewport;
            UpdateUIForSingleViewport();
        }

        private void TwoViewportsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            config.Type = LayoutConfiguration.LayoutType.DualViewport;
            UpdateUIForDualViewport();
        }

        private void SelectSheetNumbersButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var sheetNumberManager = new SheetNumberManager(document);
                config.SheetNumbers = sheetNumberManager.PromptSelectSheetNumbers();
                SheetNumbersTextBlock.Text = $"Selected: {config.SheetNumbers.Count} sheet(s)";
            }
            catch (Exception ex)
            {
                logger.Error($"Error selecting sheet numbers: {ex.Message}");
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SelectFirstViewportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
                var viewportManager = new ViewportManager(document);
                config.FirstViewport = viewportManager.PromptSelectViewport("Select first viewport:");
                FirstViewportTextBlock.Text = $"Width: {config.FirstViewport.Width:F2}, Height: {config.FirstViewport.Height:F2}";
                this.Show();
            }
            catch (Exception ex)
            {
                logger.Error($"Error selecting viewport: {ex.Message}");
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Show();
            }
        }

        private void SelectSecondViewportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
                var viewportManager = new ViewportManager(document);
                config.SecondViewport = viewportManager.PromptSelectViewport("Select second viewport:");
                SecondViewportTextBlock.Text = $"Width: {config.SecondViewport.Width:F2}, Height: {config.SecondViewport.Height:F2}";
                this.Show();
            }
            catch (Exception ex)
            {
                logger.Error($"Error selecting viewport: {ex.Message}");
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Show();
            }
        }

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var processor = new LayoutProcessor(document);
                bool result = false;

                if (config.Type == LayoutConfiguration.LayoutType.SingleViewport)
                {
                    result = processor.ProcessSingleViewportLayout(config);
                }
                else if (config.Type == LayoutConfiguration.LayoutType.DualViewport)
                {
                    result = processor.ProcessDualViewportLayout(config);
                }

                if (result)
                {
                    MessageBox.Show("Layouts created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to create layouts. Check the command line for details.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Error executing layout creation: {ex.Message}");
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateUIForSingleViewport()
        {
            SecondViewportPanel.Visibility = Visibility.Collapsed;
            DualViewportControls.Visibility = Visibility.Collapsed;
        }

        private void UpdateUIForDualViewport()
        {
            SecondViewportPanel.Visibility = Visibility.Visible;
            DualViewportControls.Visibility = Visibility.Visible;
        }
    }
}
