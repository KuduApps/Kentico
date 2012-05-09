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

using CMS.UIControls;

public partial class CMSMasterPages_LiveSite_EmptyPage : CMSLiveMasterPage
{
    /// <summary>
    /// Prepared for specifying the additional HEAD elements.
    /// </summary>
    public override Literal HeadElements
    {
        get
        {
            return this.ltlHeadElements;
        }
        set
        {
            this.ltlHeadElements = value;
        }
    }


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        this.PageStatusContainer = this.plcStatus;
    }
}
