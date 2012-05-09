using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.OnlineMarketing;

public partial class CMSModules_Scoring_Pages_Frameset : CMSScorePage
{
    protected string contentUrl = "Tab_Contacts.aspx";


    protected void Page_Load(object sender, EventArgs e)
    {
        // Get current query-string
        string querystring = URLHelper.GetQuery(URLHelper.CurrentURL);

        if (QueryHelper.GetBoolean("saved", false))
        {
            // Redirect to the General tab if new score was created
            contentUrl = "Tab_General.aspx" + querystring;
        }
        else
        {
            contentUrl += querystring;
        }
    }
}