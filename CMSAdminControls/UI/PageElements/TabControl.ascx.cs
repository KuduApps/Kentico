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

public partial class CMSAdminControls_UI_PageElements_TabControl : CMSUserControl
{
    private string[,] mTabItems;
    public int mSelectedTab;


    /// <summary>
    /// 2-dimensional array of tabs. (0, 0) contains the caption, (0, 1) contains id of related region.
    /// </summary>
    public string[,] TabItems
    {
        get
        {
            return mTabItems;
        }
        set
        {
            mTabItems = value;
        }
    }


    /// <summary>
    /// Index of selected tab.
    /// </summary>
    public int SelectedTab
    {
        get
        {
            return mSelectedTab;
        }
        set
        {
            mSelectedTab = value;
        }
    }

    
    protected void Page_Load(object sender, EventArgs e)
    {
        BasicTabControlMenu.Tabs = new string[TabItems.GetUpperBound(0) + 1, 4];
        BasicTabControlMenu.SelectedTab = SelectedTab;
        BasicTabControlMenu.UseClientScript = true;
        string tabs = "";
        for (int i = 0; i < TabItems.GetUpperBound(0) + 1; i++)
        {
            //Creates list of all IDs from TabItems
            if ( i > 0 )
            {
            	tabs += ", ";
            }
            tabs += "'" + TabItems[i, 1] + "'";

            BasicTabControlMenu.Tabs[i, 0] = TabItems[i, 0];
            string showId = "'" + TabItems[i, 1] + "'";

            //In on-click event we hide all tabs content at first. Then we show tab content corresponding to current TabItems item.
            string showTabContentScript = "hideAllTabs();showSelectedTab(" + showId + ");";// 
            BasicTabControlMenu.Tabs[i, 1] = showTabContentScript;

            BasicTabControlMenu.Tabs[i, 2] = "#";
        }
        
        //Creates array of all IDs. We'll use it to hide all corresponding spans.
        Response.Write("<script> basicTabControlMenuTabs = new Array(" + tabs + "); </script>");
    }


    /// <summary>
    /// Returns id related to selected tab.
    /// </summary>
    public string GetSelectedId()
    {
        return TabItems[SelectedTab, 1];
    }
}
