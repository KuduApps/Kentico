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

public partial class CMSModules_Newsletters_Tools_Newsletters_Newsletter_Frameset : CMSNewsletterNewslettersPage
{
    protected string newsletterContentUrl = "Newsletter_Issue_List.aspx";
    protected string newsletterHeaderUrl = "Newsletter_Header.aspx?newsletterid=";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        int newsletterId = QueryHelper.GetInteger("newsletterid", 0);
        if (QueryHelper.GetInteger("saved", 0) > 0)
        {
            newsletterContentUrl = "Newsletter_Configuration.aspx?newsletterid=" + newsletterId.ToString() + "&saved=1";
            newsletterHeaderUrl += newsletterId.ToString() + "&saved=1";
        }
        else
        {
            newsletterContentUrl = "Newsletter_Issue_List.aspx?newsletterid=" + newsletterId.ToString();
            newsletterHeaderUrl += newsletterId.ToString();
        }
    }
}
