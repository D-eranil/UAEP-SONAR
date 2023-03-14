using CMS.DocumentEngine;
using CMS.Helpers;
using CMS.Membership;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using CMS.DataEngine;
using CMS.CustomTables;

public partial class RemoveDataFromCustomTable : System.Web.UI.Page
{
    private static string[] formats = new string[]
 {
        "MM/dd/yyyy hh:mm tt",
        "MM/dd/yyyy hh:mm",
        "M/dd/yyyy H:mm:ss tt",
        "MM/dd/yyyy",
        "M/dd/yyyy H:mm:ss"
 };
    private static DateTime ParseDate(string input)
    {
        
        return DateTime.ParseExact(input, formats, CultureInfo.InvariantCulture, DateTimeStyles.None);
    }

    string CurrentCultureName = CMS.Localization.LocalizationContext.CurrentCulture.CultureCode.ToLower();
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
       
            string customTableName = "";
            int lastCount = 1;
            string startCount = "";
            if (Request["customTableName"] != null && !string.IsNullOrEmpty(Request["customTableName"]) && !string.IsNullOrEmpty(Request["lastCount"]))
            {
                customTableName = Convert.ToString(Request["customTableName"]);
                lastCount = Int32.Parse(Request["lastCount"]);
                startCount = Convert.ToString(Request["startCount"]);
            }
			
            Response.Write(DeletCustomTableRecords(customTableName, lastCount, startCount) );
            

        
    }

    public string DeletCustomTableRecords(string customTableName, int lastCount, string startCount = "1" )
    {
        try
        {

            
            // Prepares the code name (class name) of the custom table from which the record will be deleted
            string customTableClassName = customTableName;


            // Gets the custom table
            DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(customTableClassName);
            if (customTable != null)
            {
                // Gets the first custom table record whose value in the 'ItemText' starts with 'New text'

                for (int i = 0; i < lastCount; i++)
                {
                    CustomTableItem item = CustomTableItemProvider.GetItems(customTableClassName).WhereStartsWith("ItemID", startCount).TopN(1).FirstOrDefault();


                    if (item != null)
                    {
                        // Deletes the custom table record from the database
                        item.Delete();
                    }
                }
                return ("Data have been clean");
            }
            return ("Custom table is not exist");

        }
        catch (Exception ex)
        {
            Response.Write(ex.InnerException);
            throw ex;
        }
    }
    
}

