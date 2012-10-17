using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;


namespace DW.CompareElements.Entry
{

    /// <summary>
    /// Database Level Application
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    public class DBapp : IExternalDBApplication
    {

        /// <summary>
        /// Runs when Revit Starts Up
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public ExternalDBApplicationResult OnStartup(ControlledApplication app)
        {

            try
            {
                // Your Code Here

                // Return Success
                return ExternalDBApplicationResult.Succeeded;

            }
            catch
            {

                // Return Failure
                return ExternalDBApplicationResult.Failed;

            }

        }

        /// <summary>
        /// Runs when Revit Shuts Down
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public ExternalDBApplicationResult OnShutDown(ControlledApplication app)
        {
            // Return Success
            return ExternalDBApplicationResult.Succeeded;
        }

    }

}
