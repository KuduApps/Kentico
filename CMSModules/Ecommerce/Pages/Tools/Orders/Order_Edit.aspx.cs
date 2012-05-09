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
using CMS.GlobalHelper;
using CMS.Ecommerce;

public partial class CMSModules_Ecommerce_Pages_Tools_Orders_Order_Edit : CMSOrdersPage
{
    protected override void OnPreInit(EventArgs e)
    {
        this.CustomerID = QueryHelper.GetInteger("customerid", 0);
        base.OnPreInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.CustomerID > 0)
        {
            this.rowsFrameset.Attributes["rows"] = TabsBreadFrameHeight + ", *";            
        }
        else
        {
           this.rowsFrameset.Attributes["rows"] = "96, *";
        }
    }
}
