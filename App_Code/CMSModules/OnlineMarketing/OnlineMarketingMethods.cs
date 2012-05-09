using System;
using System.Collections.Generic;
using System.Web;

using CMS.GlobalHelper;
using CMS.OnlineMarketing;

/// <summary>
/// Online marketing methods - wrapping methods for macro resolver.
/// </summary>
public static class OnlineMarketingMethods
{
    /// <summary>
    /// Registers all online marketing methods to macro resolver.
    /// </summary>
    public static void RegisterMethods()
    {
        MacroMethods.RegisterMethod("LastActivityOfType", LastActivityOfType, typeof(ActivityInfo), "Returns contact's last activity of specified activity type.", GetMethodFormat("LastActivityOfType"), 2, new object[,] { { "Contact", typeof(object), "Contact info object." }, { "activityType", typeof(string), "Name of the activity type, optional." } }, null, new List<Type>() { typeof(ContactInfo) });
        MacroMethods.RegisterMethod("FirstActivityOfType", FirstActivityOfType, typeof(ActivityInfo), "Returns contact's first activity of specified activity type.", GetMethodFormat("FirstActivityOfType"), 2, new object[,] { { "Contact", typeof(object), "Contact info object." }, { "activityType", typeof(string), "Name of the activity type, optional." } }, null, new List<Type>() { typeof(ContactInfo) });
        MacroMethods.RegisterMethod("IsInContactGroup", IsInContactGroup, typeof(bool), "Returns true if contact is in contact group.", GetMethodFormat("IsInContactGroup"), 2, new object[,] { { "Contact", typeof(object), "Contact info object." }, { "groupName", typeof(string), "Name of the contact group to test whether contact is in." } }, null, new List<Type>() { typeof(ContactInfo) });
        MacroMethods.RegisterMethod("GetScore", GetScore, typeof(int), "Returns contact's points in specified score on current site.", GetMethodFormat("GetScore"), 2, new object[,] { { "Contact", typeof(object), "Contact info object." }, { "scoreName", typeof(string), "Name of the score to get contact's points of." } }, null, new List<Type>() { typeof(ContactInfo) });
        MacroMethods.RegisterMethod("GetEmailDomain", GetEmailDomain, typeof(string), "Returns e-mail domain name.", GetMethodFormat("GetEmailDomain"), 1, new object[,] { { "email", typeof(string), "E-mail address." } }, null, null);
    }


    /// <summary>
    /// Returns the method format for registration into the macro methods hashtable.
    /// </summary>
    /// <param name="method">Method name</param>
    private static string GetMethodFormat(string method)
    {
        return "{name} applied to {args}";
    }


    /// <summary>
    /// Returns contact's last activity of specified activity type.
    /// </summary>
    /// <param name="parameters">ID of current contact; Name of the activity type</param>
    public static object LastActivityOfType(params object[] parameters)
    {
        switch (parameters.Length)
        {
            case 1:
                return OnlineMarketingFunctions.LastActivityOfType(parameters[0], null);

            case 2:
                return OnlineMarketingFunctions.LastActivityOfType(parameters[0], parameters[1]);

            default:
                throw new NotSupportedException();
        }
    }


    /// <summary>
    /// Returns contact's first activity of specified activity type.
    /// </summary>
    /// <param name="parameters">ID of current contact; Name of the activity type</param>
    public static object FirstActivityOfType(params object[] parameters)
    {
        switch (parameters.Length)
        {
            case 1:
                return OnlineMarketingFunctions.FirstActivityOfType(parameters[0], null);

            case 2:
                return OnlineMarketingFunctions.FirstActivityOfType(parameters[0], parameters[1]);

            default:
                throw new NotSupportedException();
        }
    }


    /// <summary>
    /// Returns TRUE if the contact is in specified contact group on current site.
    /// </summary>
    /// <param name="parameters">ID of current contact; Name of the contact group</param>
    public static object IsInContactGroup(params object[] parameters)
    {
        switch (parameters.Length)
        {
            case 2:
                return OnlineMarketingFunctions.IsInContactGroup(parameters[0], parameters[1]);

            default:
                throw new NotSupportedException();
        }
    }


    /// <summary>
    /// Returns contact's points in specified score on current site.
    /// </summary>
    /// <param name="parameters">ID of current contact; Name of the score</param>
    public static object GetScore(params object[] parameters)
    {
        switch (parameters.Length)
        {
            case 2:
                return OnlineMarketingFunctions.GetScore(parameters[0], parameters[1]);

            default:
                throw new NotSupportedException();
        }
    }


    /// <summary>
    /// Returns e-mail domain name.
    /// </summary>
    /// <param name="parameters">E-mail address</param>
    public static object GetEmailDomain(params object[] parameters)
    {
        switch (parameters.Length)
        {
            case 1:
                return OnlineMarketingFunctions.GetEmailDomain(parameters[0]);

            default:
                throw new NotSupportedException();
        }
    }
}