using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSAdminControls_UI_PanelMenu_PanelMenuCategory : CMSUserControl
{
    #region "Variables"

    private string mCategoryName = null;

    #endregion

    #region "Properties"

    /// <summary>
    /// Category name.
    /// </summary>
    public string CategoryName
    {
        get
        {
            return this.mCategoryName;
        }
        set
        {
            this.mCategoryName = value;
        }
    }


    /// <summary>
    /// Category title.
    /// </summary>
    public string CategoryTitle
    {
        get
        {
            return this.lnkCategoryTitle.Text;
        }
        set
        {
            this.lnkCategoryTitle.Text = HTMLHelper.HTMLEncode(value);
            this.imgCategoryImage.AlternateText = HTMLHelper.HTMLEncode(value);
        }
    }


    /// <summary>
    /// Category URL.
    /// </summary>
    public string CategoryURL
    {
        get
        {
            return this.lnkCategoryTitle.NavigateUrl;
        }
        set
        {
            this.lnkCategoryTitle.NavigateUrl = value;
        }
    }


    /// <summary>
    /// Category image URL.
    /// </summary>
    public string CategoryImageUrl
    {
        get
        {
            return this.imgCategoryImage.ImageUrl;
        }
        set
        {
            this.imgCategoryImage.ImageUrl = value;
        }
    }


    /// <summary>
    /// Category tooltip.
    /// </summary>
    public string CategoryTooltip
    {
        get
        {
            return this.lnkCategoryTitle.ToolTip;
        }
        set
        {
            this.lnkCategoryTitle.ToolTip = value;
            this.imgCategoryImage.ToolTip = value;
        }
    }


    /// <summary>
    /// Category actions data {[n, 0] - Action name, [n, 1] - Action URL}.
    /// </summary>
    public string[,] CategoryActions
    {
        get;
        set;
    }

    #endregion


    #region "Page events"

    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.pnlCategory.Attributes.Add("onclick", String.Format("SelectRibbonButton({0});window.location.href = '{1}';", ScriptHelper.GetString(CategoryName), URLHelper.ResolveUrl(this.CategoryURL)));
        this.lnkCategoryTitle.Attributes.Add("onclick", String.Format("SelectRibbonButton({0});", ScriptHelper.GetString(CategoryName)));
        this.LoadCategoryActions();
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Creates hyperlinks for category actions.
    /// </summary>
    public void LoadCategoryActions()
    {
        if (this.CategoryActions != null)
        {
            int actionsCount = this.CategoryActions.GetLength(0);

            object[] actions = new object[actionsCount];

            for (int i = 0; i < actionsCount; i++)
            {
                string[] action = { HTMLHelper.HTMLEncode(this.CategoryActions[i, 0]), this.CategoryActions[i, 1], String.Format("SelectRibbonButton({0});", ScriptHelper.GetString(CategoryName)) };
                actions[i] = action;
            }

            this.rptCategoryActions.DataSource = actions;
            this.rptCategoryActions.DataBind();
        }
    }

    #endregion
}
