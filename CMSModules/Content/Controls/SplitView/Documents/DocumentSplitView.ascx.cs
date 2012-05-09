using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;

public partial class CMSModules_Content_Controls_SplitView_Documents_DocumentSplitView : CMSUserControl
{
    #region "Properties"

    /// <summary>
    /// URL of the first frame.
    /// </summary>
    public string Frame1Url
    {
        get
        {
            return splitView.Frame1Url;
        }
        set
        {
            splitView.Frame1Url = value;
        }
    }


    /// <summary>
    /// URL of the second frame.
    /// </summary>
    public string Frame2Url
    {
        get
        {
            return splitView.Frame2Url;
        }
        set
        {
            splitView.Frame2Url = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        string url = QueryHelper.GetString("splitUrl", null);
        if (!string.IsNullOrEmpty(url))
        {
            url = HttpUtility.UrlDecode(url);
        }

        // Tollbar URL
        string toolbarUrl = "~/CMSModules/Content/CMSDesk/SplitView/Toolbar.aspx";
        int nodeId = ValidationHelper.GetInteger(URLHelper.GetQueryValue(url, "nodeid"), 0);
        if (nodeId > 0)
        {
            toolbarUrl = URLHelper.AddParameterToUrl(toolbarUrl, "nodeid", nodeId.ToString());
        }

        // Separator URL 
        splitView.SeparatorUrl = "~/CMSModules/Content/CMSDesk/SplitView/Separator.aspx";
        splitView.ToolbarUrl = toolbarUrl;
        splitView.ToolbarHeight = 35;
    }

    #endregion
}
