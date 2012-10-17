using System;
using System.Collections.Generic;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace DW.CompareElements
{

    /// <summary>
    /// Revit 2013 Command Class
    /// </summary>
    /// <remarks></remarks>
    [Transaction(TransactionMode.Manual)]
    public class cmd : IExternalCommand
    {
        /// <summary>
        /// Command Entry Point
        /// </summary>
        /// <param name="commandData">Input argument providing access to the Revit application and documents</param>
        /// <param name="message">Return message to the user in case of error or cancel</param>
        /// <param name="elements">Return argument to highlight elements on the graphics screen if Result is not Succeeded.</param>
        /// <returns>Cancelled, Failed or Succeeded</returns>
        public Result Execute(ExternalCommandData commandData,
                              ref string message,
                              ElementSet elements)
        {
            try
            {
                // Add Your Code Here

                // Return Success
                return Result.Succeeded;

            }
            catch (Exception ex)
            {

                // Failure Message
                message = ex.Message;
                return Result.Failed;

            }
        }
    }
}
