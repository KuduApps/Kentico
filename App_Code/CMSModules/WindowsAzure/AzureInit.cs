using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// Intitializes Windows Azure web role when its running using full IIS.
/// </summary>
public class AzureInit
{
    #region "Variables"

    private static AzureInit mCurrent = null;
    private static object objLock = new object();

    #endregion


    #region "Properties"

    /// <summary>
    /// Returns Azure init object.
    /// </summary>
    public static AzureInit Current
    {
        get
        {
            if (mCurrent == null)
            {
                lock (objLock)
                {
                    if (mCurrent == null)
                    {
                        mCurrent = new AzureInit();
                    }
                }
            }

            return mCurrent;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Code executed on application starts.
    /// </summary>
    public void ApplicationStartInit()
    {
    }


    /// <summary>
    /// Code executed on BeginRequest event.
    /// </summary>
    public void BeginRequestInit()
    {
    }

    #endregion
}

