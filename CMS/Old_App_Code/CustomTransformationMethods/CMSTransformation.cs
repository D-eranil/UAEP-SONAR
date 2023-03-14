using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//namespace CMSApp.Old_App_Code.CustomTransformationMethods
//{
//    public class CMSTransformation
//    {
//    }
//}
namespace CMS.Controls
{
    /// <summary>
    /// Extends the CMSTransformation partial class.
    /// </summary>
    public partial class CMSTransformation
    {

        /// <summary>
        /// Trims text values to the specified length.
        /// </summary>
        /// <param name="txtValue">Original text to be trimmed</param>
        /// <param name="leftChars">Number of characters to be returned</param>
        public string CustomTrimText(object txtValue, int leftChars)
        {
            // Checks that text is not null.
            if (txtValue == null | txtValue == DBNull.Value)
            {
                return "";
            }
            else
            {
                string txt = (string)txtValue;

                // Returns a substring if the text is longer than specified.
                if (txt.Length <= leftChars)
                {
                    return txt;
                }
                else
                {
                    return txt.Substring(0, leftChars) + "...";
                }
            }
        }

    }
}