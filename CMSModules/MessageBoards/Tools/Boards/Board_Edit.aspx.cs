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

public partial class CMSModules_MessageBoards_Tools_Boards_Board_Edit : CMSMessageBoardBoardsPage
{
    protected override void OnPreRender(EventArgs e)
    {
        rowsFrameset.Attributes["rows"] = TabsBreadFrameHeight + ", *";

        // External call
        if (QueryHelper.GetBoolean("changemaster", false))
        {
            rowsFrameset.Attributes["rows"] = "35, *";
        }
        base.OnPreRender(e);
    }
}
