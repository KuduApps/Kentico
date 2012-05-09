using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Data;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;

/// <summary>
/// Summary description for TagSelectorService.
/// </summary>
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[WebService(Namespace = "CMS.WebService")]
[ScriptService]
public class TagSelectorService : WebService
{
    public TagSelectorService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    [ScriptMethod]
    public string[] TagsAutoComplete(string prefixText, int count, string contextKey)
    {
        string where = "(TagName LIKE N'" + SqlHelperClass.GetSafeQueryString(prefixText) + "%')";
        if (contextKey != null)
        {
            where += " AND (TagGroupID = " + ValidationHelper.GetInteger(contextKey, 0) + ")";
        }

        DataSet ds = TagInfoProvider.GetTags(where, "TagName", 20, "TagName");

        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            string[] output = new string[ds.Tables[0].Rows.Count];

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["TagName"].ToString().Contains(" "))
                {
                    output[i] = "'\"" + HttpUtility.HtmlDecode(ds.Tables[0].Rows[i]["TagName"].ToString()) + "\"'";
                }
                else
                {
                    output[i] = HttpUtility.HtmlDecode(ds.Tables[0].Rows[i]["TagName"].ToString());
                }
            }

            return output;
        }

        return null;
    }
}

