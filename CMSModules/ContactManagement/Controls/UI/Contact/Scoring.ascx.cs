using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.CMSHelper;

public partial class CMSModules_ContactManagement_Controls_UI_Contact_Scoring : CMSAdminListControl
{
    #region "Properties"

    /// <summary>
    /// Sets or gets contact ID to filter unigrid.
    /// </summary>
    public int? ContactID
    {
        get;
        set;
    }


    /// <summary>
    /// Sets or gets site ID to filter unigrid.
    /// </summary>
    public int? SiteID
    {
        get;
        set;
    }


    /// <summary>
    /// Returns inner unigrid.
    /// </summary>
    public UniGrid UniGrid
    {
        get
        {
            return gridElem;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (ContactID != null)
        {
            gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, "ContactID = " + ContactID);
        }
        if (SiteID != null)
        {
            gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, "ScoreSiteID = " + SiteID);
        }
        gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);
    }


    protected override void OnPreRender(EventArgs e)
    {
        RegisterScripts();
    }


    /// <summary>
    /// Puts javascript functions to page.
    /// </summary>
    private void RegisterScripts()
    {
        ScriptHelper.RegisterDialogScript(this.Page);

        string scriptBlock = string.Format(@"
            function ViewDetails(id) {{ modalDialog('{0}?scoreid=' + id, 'ScoreDetails', '1000px', '700px'); }}
            function Refresh()
            {{
                __doPostBack('" + pnlUpdate.ClientID + @"', '');
            }}",
            ResolveUrl(@"~/CMSModules/Scoring/Pages/Detail.aspx"));

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "Actions", scriptBlock, true);
    }


    object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName)
        {
            case "view":
                ImageButton viewBtn = (ImageButton)sender;
                viewBtn.OnClientClick = "ViewDetails(" + viewBtn.CommandArgument + "); return false;";
                break;
        }
        return null;
    }

    #endregion
}