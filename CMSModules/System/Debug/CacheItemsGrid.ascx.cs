using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using CMS.UIControls;
using CMS.Controls;
using CMS.GlobalHelper;

public partial class CMSModules_System_Debug_CacheItemsGrid : CMSUserControl, IUniPageable, IPostBackEventHandler
{
    #region "Variables"

    protected int mTotalItems = 0;

    protected bool mShowDummyItems = false;

    protected IEnumerable<string> mAllItems = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Event fired when some cache item is deleted.
    /// </summary>
    public event EventHandler OnItemDeleted;
    

    /// <summary>
    /// All cache items array.
    /// </summary>
    public IEnumerable<string> AllItems
    {
        get
        {
            return mAllItems;
        }
        set
        {
            mAllItems = value;
        }
    }


    /// <summary>
    /// If true, grid shows the dummy items.
    /// </summary>
    public bool ShowDummyItems
    {
        get
        {
            return mShowDummyItems;
        }
        set
        {
            mShowDummyItems = value;
        }
    }


    /// <summary>
    /// Returns the pager control.
    /// </summary>
    public UniGridPager PagerControl
    {
        get
        {
            return pagerItems;
        }
    }


    /// <summary>
    /// Returns total number of items.
    /// </summary>
    public int TotalItems
    {
        get
        {
            return mTotalItems;
        }
    }

    #endregion


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        this.pagerItems.PagedControl = this;
        //this.pagerItems.UniPager.PageControl = "cacheItemsGrid";
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Register delete script
        string script =
            "function DeleteCacheItem(key) { if (confirm(" + ScriptHelper.GetString(GetString("general.confirmdelete")) + ")) { document.getElementById('" + this.hdnKey.ClientID + "').value = key; " + this.Page.ClientScript.GetPostBackEventReference(this, "delete") + " } }\n" +
            "function Refresh() { " + this.Page.ClientScript.GetPostBackEventReference(this, "refresh") + " }\n" +
            "function Show(key) { var url = '" + ResolveUrl("System_ViewObject.aspx?source=cache&key=") + "' + key; window.open(url); }";

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "DeleteCacheItem", ScriptHelper.GetScript(script));
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.pagerItems.UniPager.CurrentPage = 1;
    }


    public void ReloadData()
    {
        if (this.ShowDummyItems)
        {
            this.lblKey.Text = GetString("Administration-System.CacheInfoDummyKey");
            this.plcData.Visible = false;
        }
        else
        {
            this.lblKey.Text = GetString("Administration-System.CacheInfoKey");
            this.lblData.Text = GetString("Administration-System.CacheInfoData");
            this.lblExpiration.Text = GetString("Administration-System.CacheInfoExpiration");
            this.lblPriority.Text = GetString("Administration-System.CacheInfoPriority");

            this.plcData.Visible = true;
            this.plcContainer.Visible = CacheHelper.DebugCache;
        }

        this.lblAction.Text = GetString("General.Action");

        // Build the table
        StringBuilder sb = new StringBuilder();

        // Prepare the indexes for paging
        int pageSize = this.pagerItems.CurrentPageSize;

        int startIndex = (this.pagerItems.CurrentPage - 1) * pageSize + 1;
        int endIndex = startIndex + pageSize;

        // Process all items
        int i = 0;
        bool all = (endIndex <= startIndex);

        string search = this.txtFilter.Text;

        if (mAllItems != null)
        {
            if (ShowDummyItems)
            {
                // Process dummy keys
                foreach (string key in mAllItems)
                {
                    if (key.IndexOf(search, StringComparison.InvariantCultureIgnoreCase) >= 0)
                    {
                        if (!String.IsNullOrEmpty(key))
                        {
                            // Process the key
                            object value = HttpRuntime.Cache[key];
                            CacheItemContainer container = null;

                            // Handle the container
                            if (value is CacheItemContainer)
                            {
                                container = (CacheItemContainer)value;
                                value = container.Data;
                            }

                            if (value == CacheHelper.DUMMY_KEY)
                            {
                                i++;
                                if (all || (i >= startIndex) && (i < endIndex))
                                {
                                    RenderItem(sb, key, container, value, true, i);
                                }
                            }
                        }
                    }
                }

            }
            else
            {
                // Process only normal keys
                foreach (string key in mAllItems)
                {
                    if (key.IndexOf(search, StringComparison.InvariantCultureIgnoreCase) >= 0)
                    {
                        // Process the key
                        object value = HttpRuntime.Cache[key];
                        CacheItemContainer container = null;

                        // Handle the container
                        if (value is CacheItemContainer)
                        {
                            container = (CacheItemContainer)value;
                            value = container.Data;
                        }

                        if (value != CacheHelper.DUMMY_KEY)
                        {
                            i++;
                            if (all || (i >= startIndex) && (i < endIndex))
                            {
                                RenderItem(sb, key, container, value, false, i);
                            }
                        }
                    }
                }
            }
        }

        mTotalItems = i;
        this.plcItems.Visible = (i > 0);
        this.lblInfo.Visible = (i <= 0);
        if (!RequestHelper.IsPostBack())
        {
            this.pnlSearch.Visible = (i > 10);
        }

        ltlCacheInfo.Text = sb.ToString();

        // Call page binding event
        if (OnPageBinding != null)
        {
            OnPageBinding(this, null);
        }
    }


    /// <summary>
    /// Renders the particular cache item.
    /// </summary>
    protected void RenderItem(StringBuilder sb, string key, CacheItemContainer container, object value, bool dummy, int i)
    {
        // Add the key row
        string cssClass = ((i % 2) == 0) ? "OddRow" : "EvenRow";

        sb.Append("<tr class=\"");
        sb.Append(cssClass);
        sb.Append("\"><td style=\"white-space: nowrap;\">");

        // Get the action
        GetDeleteAction(key, sb);

        sb.Append("</td><td style=\"white-space: nowrap;\"><span title=\"" + key.Replace("&", "&amp;") + "\">");
        string keyTag = TextHelper.LimitLength(key, 100);
        sb.Append(keyTag.Replace("&", "&amp;"));
        sb.Append("</span></td>");

        if (!dummy)
        {
            sb.Append("<td style=\"white-space: nowrap;\">");

            // Render the value
            if (value != null)
            {
                sb.Append("<a href=\"#\" onclick=\"Show('" + Server.UrlEncode(key) + "')\"");
                sb.Append("><img class=\"UniGridActionButton\" src=\"");
                sb.Append(ResolveUrl(GetImageUrl("Design/Controls/UniGrid/Actions/View.png")));
                sb.Append("\" style=\"border: none;\" alt=\"");
                sb.Append(GetString("General.View"));
                sb.Append("\" title=\"");
                sb.Append(GetString("General.View"));
                sb.Append("\" />");
                sb.Append("</a> ");
                if ((value == null) || (value == DBNull.Value))
                {
                    sb.Append("null");
                }
                else
                {
                    sb.Append(HttpUtility.HtmlEncode(DataHelper.GetObjectString(value, 100)));
                }
            }
            else
            {
                sb.Append("null");
            }

            sb.Append("</td>");

            if (CacheHelper.DebugCache)
            {
                // Expiration
                sb.Append("<td style=\"white-space: nowrap;\">");
                if (container != null)
                {
                    if (container.AbsoluteExpiration != System.Web.Caching.Cache.NoAbsoluteExpiration)
                    {
                        sb.Append(container.AbsoluteExpiration);
                    }
                    else
                    {
                        sb.Append(container.SlidingExpiration);
                    }
                }
                sb.Append("</td>");

                // Expiration
                sb.Append("<td style=\"white-space: nowrap;\">");
                if (container != null)
                {
                    sb.Append(container.Priority);
                }
                sb.Append("</td>");
            }
        }

        sb.Append("</tr>");
    }


    /// <summary>
    /// Gets the action.
    /// </summary>
    /// <param name="key">Cache key</param>
    /// <param name="sb">StringBuilder with the output</param>
    protected void GetDeleteAction(string key, StringBuilder sb)
    {
        sb.Append("<input type=\"image\" onclick=\"DeleteCacheItem(" + ScriptHelper.GetString(key) + "); return false;\" class=\"UniGridActionButton\" src=\"");
        sb.Append(ResolveUrl(GetImageUrl("Design/Controls/UniGrid/Actions/Delete.png")));
        sb.Append("\" style=\"border: none;margin:0px 3px;\" alt=\"");
        sb.Append(GetString("General.Delete"));
        sb.Append("\" title=\"");
        sb.Append(GetString("General.Delete"));
        sb.Append("\" />");
    }


    #region "IUniPageable Members"

    /// <summary>
    /// Pager data item object.
    /// </summary>
    public object PagerDataItem
    {
        get
        {
            return null;
        }
        set
        {
        }
    }


    /// <summary>
    /// Pager control.
    /// </summary>
    public UniPager UniPagerControl
    {
        get;
        set;
    }


    /// <summary>
    /// Occurs when the control bind data.
    /// </summary>
    public event EventHandler<EventArgs> OnPageBinding;


    /// <summary>
    /// Occurs when the pager change the page and current mode is postback => reload data
    /// </summary>
    public event EventHandler<EventArgs> OnPageChanged;


    /// <summary>
    /// Evokes control databind.
    /// </summary>
    public void ReBind()
    {
        if (OnPageChanged != null)
        {
            OnPageChanged(this, null);
        }
    }


    /// <summary>
    /// Gets or sets the number of result. Enables proceed "fake" datasets, where number 
    /// of results in the dataset is not correspondent to the real number of results
    /// This property must be equal -1 if should be disabled
    /// </summary>
    public int PagerForceNumberOfResults
    {
        get
        {
            return mTotalItems;
        }
        set
        {
        }
    }

    #endregion


    #region "IPostBackEventHandler Members"

    /// <summary>
    /// Processes the postback.
    /// </summary>
    /// <param name="eventArgument">Event argument</param>
    public void RaisePostBackEvent(string eventArgument)
    {
        // Delete the item
        string key = this.hdnKey.Value;
        if (!String.IsNullOrEmpty(key))
        {
            CacheHelper.Remove(key);

            // Raise the OnItemDeleted event
            if (OnItemDeleted != null)
            {
                OnItemDeleted(this, null);
            }
        }
    }

    #endregion
}
