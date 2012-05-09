using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.Community;

public partial class CMSModules_Groups_Tools_MessageBoards_Boards_Board_Edit : CMSGroupMessageBoardsPage
{
    protected override void OnPreRender(EventArgs e)
    {
        // External call
        if (QueryHelper.GetBoolean("changemaster", false))
        {
            rowsFrameset.Attributes["rows"] = "34, *";
        }
        base.OnPreRender(e);
    }
}
