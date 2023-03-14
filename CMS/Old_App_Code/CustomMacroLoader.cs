using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.MacroEngine;
using CMS.Helpers;

namespace CMSApp.Old_App_Code
{
    public class CustomMacroLoader : MacroMethodContainer
    {
        [MacroMethod(typeof(string), "Combines two strings, or appends a culture suffix when called with one parameter.", 1)]
        [MacroMethodParam(0, "param1", typeof(string), "First part of the string.")]
        [MacroMethodParam(1, "param2", typeof(string), "Second part of the string (optional).")]
        //testing
        public static object MyMethod(EvaluationContext context, params object[] parameters)
        {
            return context;
        }
    }
}