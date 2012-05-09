using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Empty web service template.
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WebService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    /// <summary>
    /// Returns the data from DB.
    /// </summary>
    /// <param name="parameter">String parameter for sql command</param>
    [WebMethod]
    public DataSet GetDataSet(string parameter)
    {
        // INSERT YOUR WEB SERVICE CODE AND RETURN THE RESULTING DATASET

        return null;
    }


    [System.Web.Services.WebMethod(MessageName = "GetCompletionList")]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetCompletionList(string prefixText, int count)
    {
        // INSERT YOUR WEB SERVICE CODE AND RETURN THE RESULTING STRING ARRAY
        
        return null;
    }


    [System.Web.Services.WebMethod(MessageName = "GetCompletionListContext")]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetCompletionList(string prefixText, int count, string contextKey) 
    {
        // INSERT YOUR WEB SERVICE CODE AND RETURN THE RESULTING STRING ARRAY

        return null;
    }

}
