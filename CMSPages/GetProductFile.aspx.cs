using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.Ecommerce;
using CMS.ExtendedControls;
using CMS.SiteProvider;
using CMS.CMSHelper;

public partial class CMSPages_GetProductFile : AbstractCMSPage
{
    #region "Variables"

    private Guid token = Guid.Empty;

    #endregion


    #region "Page methods"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Get download token from URL
        this.token = QueryHelper.GetGuid("token", Guid.Empty);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        string errorMessage = null;

        // Get order item SKU file
        OrderItemSKUFileInfo oiskufi = OrderItemSKUFileInfoProvider.GetOrderItemSKUFileInfo(this.token);

        if (oiskufi != null)
        {
            // Get parent order item
            OrderItemInfo oii = OrderItemInfoProvider.GetOrderItemInfo(oiskufi.OrderItemID);

            if (oii != null)
            {
                // If download is not expired
                if ((oii.OrderItemValidTo.CompareTo(DateTimeHelper.ZERO_TIME) == 0) || (oii.OrderItemValidTo.CompareTo(DateTime.Now) > 0))
                {
                    // Get SKU file
                    SKUFileInfo skufi = SKUFileInfoProvider.GetSKUFileInfo(oiskufi.FileID);

                    if (skufi != null)
                    {
                        // Decide how to process the file based on file type
                        switch (skufi.FileType.ToLower())
                        {
                            case "metafile":
                                // Set parameters to current context
                                this.Context.Items["fileguid"] = skufi.FileMetaFileGUID;
                                this.Context.Items["disposition"] = "attachment";

                                // Perform server side redirect to download
                                this.Response.Clear();
                                this.Server.Transfer(URLHelper.ResolveUrl("~/CMSPages/GetMetaFile.aspx"));
                                this.Response.End();
                                return;
                        }
                    }
                }
                else
                {
                    // Set error message
                    errorMessage = ResHelper.GetString("getproductfile.expirederror");
                }
            }
        }

        // If error message not set
        if (String.IsNullOrEmpty(errorMessage))
        {
            // Set default error message
            errorMessage = ResHelper.GetString("getproductfile.existerror");
        }

        // Set error message to current context
        this.Context.Items["title"] = ResHelper.GetString("getproductfile.error");
        this.Context.Items["text"] = errorMessage;

        // Perform server side redirect to error page
        this.Response.Clear();
        this.Server.Transfer(URLHelper.ResolveUrl("~/CMSMessages/Error.aspx"));
        this.Response.End();
    }

    #endregion
}

