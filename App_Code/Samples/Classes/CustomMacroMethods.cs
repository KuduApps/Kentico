using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CMS.GlobalHelper;

/// <summary>
/// Example of custom module with custom macro methods registration.
/// </summary>
public static class CustomMacroMethods
{
    /// <summary>
    /// Registers all blog methods to macro resolver.
    /// </summary>
    public static void RegisterMethods()
    {
        MacroMethods.RegisterMethod("MyMethod", MyMethod, typeof(string), "Returns concatenation of two strings.", null, 1, new object[,] { { "param1", typeof(string), "First string to concatenate." }, { "param2", typeof(string), "Second string to concatenate." } }, null);
        MacroMethods.RegisterMethod("MyMethodSnippet", MyMethod, typeof(string), "Calls MyMethod on lower case current user name.", null, 0, null, null, new List<Type>() { typeof(string) }, "MyMethod(ToLower(CurrentDocument.DocumentName), |);", false);

        // Call MacroMethods.RegisterMethod for your own custom methods here with following parameters:
        // 1. parameter: Method name
        // 2. parameter: Method delegate (wrapper method)
        // 3. parameter: Return type of the method
        // 4. parameter: Comment for the method
        // 5. parameter: Formatting string for "human readable" translation of the method call (optional, you do not have to specify this)
        // 6. parameter: Minimal number of parameters needed to call the method (mimimal overload)
        // 7. parameter: Parameter definition in format {{name, type, comment}, {name, type, comment}}
        // 8. parameter: A list of special parameters needed to be supplied by resolver (these parameters are automatically passed by MacroResolver as the first parameters to the wrapper method)
        // 9. parameter: List of types for which the method is applicable (set to null for all types to be allowed)
        // 10. parameter: Code snippet which is used in AutoCompletion when TAB is pressed (for determining the cursor position use pipe)
    }


    #region "Macro methods implementation"

    /// <summary>
    /// Concatenates the given string with " default" string.
    /// </summary>
    /// <param name="param1">String to be concatenated with " default"</param>
    public static string MyMethod(string param1)
    {
        return MyMethod(param1, "default");
    }


    /// <summary>
    /// Concatenates two strings.
    /// </summary>
    /// <param name="param1">First string to concatenate</param>
    /// <param name="param2">Second string to concatenate</param>
    public static string MyMethod(string param1, string param2)
    {
        return param1 + " " + param2;
    }

    // Add your own custom methods here

    #endregion


    #region "MacroResolver wrapper methods"

    /// <summary>
    /// Wrapper method of MyMethod suitable for MacroResolver.
    /// </summary>
    /// <param name="parameters">Parameters of the method</param>
    public static object MyMethod(params object[] parameters)
    {
        switch (parameters.Length)
        {
            case 1:
                // Overload with one parameter
                return MyMethod(ValidationHelper.GetString(parameters[0], ""));

            case 2:
                // Overload with two parameters
                return MyMethod(ValidationHelper.GetString(parameters[0], ""), ValidationHelper.GetString(parameters[1], ""));

            default:
                // No other overload is supported
                throw new NotSupportedException();
        }
    }


    // Add wrappers for MacroResolver for your own custom methods here
    // The signature of wrapper methods has to be "public static object MyMethodName(params object[] parameters)"

    #endregion
}
