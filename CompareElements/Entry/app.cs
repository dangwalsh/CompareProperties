using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace DW.CompareElements
{
    /// <summary>
    /// Revit 2013 API Application Class
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    class app : IExternalApplication
    {

        static string m_Path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        static UIControlledApplication m_uiApp;

        /// <summary>
        /// Load an Embedded Resource Image
        /// </summary>
        /// <param name="SourceName">String path to Resource Image</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private ImageSource LoadPngImgSource(string SourceName)
        {

            try
            {
                // Assembly
                Assembly m_assembly = Assembly.GetExecutingAssembly();

                // Stream
                Stream m_icon = m_assembly.GetManifestResourceStream(SourceName);

                // Decoder
                PngBitmapDecoder m_decoder = new PngBitmapDecoder(m_icon,
                                 BitmapCreateOptions.PreservePixelFormat,
                                 BitmapCacheOption.Default);

                // Source
                ImageSource m_source = m_decoder.Frames[0];
                return (m_source);

            }
            catch
            {
            }

            // Fail
            return null;

        }

        /// <summary>
        /// Add the Ribbon Item and Panel
        /// </summary>
        /// <param name="a"></param>
        /// <remarks></remarks>
        public void AddRibbonPanel(UIControlledApplication a)
        {
            try
            {
                // First Create the Tab
                a.CreateRibbonTab("Case Design Inc.");
            }
            catch
            {
                // Might already exist...
            }

            // Tools
            AddButton("Panel",
                      "Tool_Name",
                      "Text",
                      "?_16.png",
                      "?_32.png",
                      m_Path + "\\?.dll",
                      "?.cmd",
                      "Description");
        } // End AddRibbonPanel

        /// <summary>
        /// Add a button to a Ribbon Tab
        /// </summary>
        /// <param name="Rpanel">The name of the ribbon panel</param>
        /// <param name="ButtonName">The Name of the Button</param>
        /// <param name="ButtonText">Command Text</param>
        /// <param name="ImagePath16">Small Image</param>
        /// <param name="ImagePath32">Large Image</param>
        /// <param name="dllPath">Path to the DLL file</param>
        /// <param name="dllClass">Full qualified class descriptor</param>
        /// <param name="Tooltip">Tooltip to add to the button</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool AddButton(string Rpanel,
                               string ButtonName,
                               string ButtonText,
                               string ImagePath16,
                               string ImagePath32,
          string dllPath,
          string dllClass, string Tooltip)
        {
            try
            {
                // The Ribbon Panel
                RibbonPanel m_RibbonPanel = null;

                // Find the Panel within the Case Tab        
                List<RibbonPanel> m_RP = new List<RibbonPanel>();
                m_RP = m_uiApp.GetRibbonPanels("Case Design Inc.");
                foreach (RibbonPanel x in m_RP)
                {
                    if (x.Name.ToUpper() == Rpanel.ToUpper())
                    {
                        m_RibbonPanel = x;
                    }
                }

                // Create the Panel if it doesn't Exist
                if (m_RibbonPanel == null)
                {
                    m_RibbonPanel = m_uiApp.CreateRibbonPanel("Case Design Inc.", Rpanel);
                }

                // Create the Pushbutton Data
                PushButtonData m_PushButtonData = new PushButtonData(ButtonName, ButtonText, dllPath, dllClass);
                if (!string.IsNullOrEmpty(ImagePath16))
                {
                    try
                    {
                        m_PushButtonData.Image = LoadPngImgSource(ImagePath16);
                    }
                    catch
                    {
                    }
                }
                if (!string.IsNullOrEmpty(ImagePath32))
                {
                    try
                    {
                        m_PushButtonData.LargeImage = LoadPngImgSource(ImagePath32);
                    }
                    catch
                    {
                    }
                }
                m_PushButtonData.ToolTip = Tooltip;

                // Add the button to the tab
                m_RibbonPanel.AddItem(m_PushButtonData);

            }
            catch
            {
            }

            return true;
        }

        /// <summary>
        /// Fires off when Revit Session Starts
        /// </summary>
        /// <param name="a">An object that is passed to the external application which contains the controlled application.</param>
        /// <returns>Return the status of the external application. A result of Succeeded means that the external application successfully started. Cancelled can be used to signify that the user cancelled the external operation at some point. If false is returned then Revit should inform the user that the external application failed to load and the release the internal reference.</returns>
        public Result OnStartup(UIControlledApplication a)
        {
            try
            {
                // The Shared uiApp variable
                m_uiApp = a;

                // Add the Ribbon Panel!
                AddRibbonPanel(a);

                // Return Success
                return Result.Succeeded;

            }
            catch
            {
                // Return Failure
                return Result.Failed;

            }
        }

        /// <summary>
        /// Fires off when Revit Session Ends
        /// </summary>
        /// <param name="a">An object that is passed to the external application which contains the controlled application.</param>
        /// <returns>Return the status of the external application. A result of Succeeded means that the external application successfully shutdown. Cancelled can be used to signify that the user cancelled the external operation at some point. If false is returned then the Revit user should be warned of the failure of the external application to shut down correctly.</returns>
        public Result OnShutdown(UIControlledApplication a)
        {
            // Return Success
            return Result.Succeeded;
        }
    }
}
