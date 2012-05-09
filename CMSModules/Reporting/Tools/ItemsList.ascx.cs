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

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Reporting;
using CMS.UIControls;


public partial class CMSModules_Reporting_Tools_ItemsList : CMSUserControl
{
    #region "Variables"

    private string mEditUrl = "";
    private int mReportID = 0;
    private ReportInfo mReport = null;
    private ReportItemType mItemType = ReportItemType.Graph;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Url of the edit page.
    /// </summary>
    public string EditUrl
    {
        get
        {
            return mEditUrl;
        }
        set
        {
            mEditUrl = value;
        }
    }


    /// <summary>
    /// Gets or sets report id, which is used for loading the report if Report property is null.
    /// </summary>
    public int ReportID
    {
        get
        {
            return mReportID;
        }
        set
        {
            mReportID = value;
        }
    }


    /// <summary>
    /// Gets or sets the report object to load the items.
    /// </summary>
    public ReportInfo Report
    {
        get
        {
            return mReport;
        }
        set
        {
            mReport = value;
        }
    }


    /// <summary>
    /// Item type.
    /// </summary>
    public ReportItemType ItemType
    {
        get
        {
            return mItemType;
        }
        set
        {
            mItemType = value;
        }
    }

    #endregion


    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check 'Read' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.reporting", "Read"))
        {
            CMSReportingPage.RedirectToCMSDeskAccessDenied("cms.reporting", "Read");
        }
        if (Report != null)
        {
            brsItems.ReportID = Report.ReportID;
        }
        brsItems.ReportType = mItemType;
        brsItems.Display = false;
        brsItems.IsLiveSite = IsLiveSite;
    }


    /// <summary>
    /// Pre render.
    /// </summary>
    protected void Page_PreRender(object sender, EventArgs e)
    {
        Initialize();
        brsItems.ReloadItems();
    }


    /// <summary>
    /// Sets buttons actions.
    /// </summary>
    protected void Initialize()
    {
        ScriptHelper.RegisterDialogScript(this.Page);
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), @"       
        
        GetSelectedItem" + mItemType.ToString(), ScriptHelper.GetScript(
            "function getSelectedItem_" + mItemType.ToString() + "() { \n" +
            "   if (document.getElementById('" + brsItems.UniSelectorClientID + "') != null) { \n" +
            "      return document.getElementById('" + brsItems.UniSelectorClientID + "').value; " +
            "   } \n" +
            "   return 0; } \n\n" +
            "function DeleteItem_" + mItemType.ToString() + "() { \n" +
            "   if ((document.getElementById('" + brsItems.UniSelectorClientID + "') != null) && (document.getElementById('" + brsItems.UniSelectorClientID + "').value != '0')) { \n" +
            "       if (confirm(" + ScriptHelper.GetString(ResHelper.GetString("general.confirmdelete")) + ")) { \n" +
            "           document.getElementById('" + this.hdnItemId.ClientID + "').value = getSelectedItem_" + mItemType.ToString() + "();  " + this.Page.ClientScript.GetPostBackEventReference(btnHdnDelete, null) + " } \n" +
            "   } else { alert('" + ResHelper.GetString("Reporting_General.SelectObjectFirst") + "'); } \n" +
            "} \n\n" +
            "function InserMacro_" + mItemType.ToString() + "() { \n" +
            "   if ((document.getElementById('" + brsItems.UniSelectorClientID + "') != null) && (document.getElementById('" + brsItems.UniSelectorClientID + "').value != '0')) { \n" +
            "       InsertHTML('%%control:Report" + ItemType.ToString() + "?" + Report.ReportName + ".' + getSelectedItem_" + mItemType.ToString() + "() + '%%'); \n" +
            "   } else { alert('" + ResHelper.GetString("Reporting_General.SelectObjectFirst") + "'); } \n" +
            "} \n"
            ));


        string modalHeight = "760";
        string modalWidth = "1050";

        if (this.Report != null)
        {
            btnAdd.OnClientClick = "modalDialog('" + ResolveUrl(mEditUrl) + "?reportId=" + Report.ReportID + "','ReportItemEdit'," + modalWidth + "," + modalHeight + ");return false;";
            btnEdit.OnClientClick = "if (getSelectedItem_" + mItemType.ToString() + "() != '0') { modalDialog('" + ResolveUrl(mEditUrl) + "?reportId=" + Report.ReportID + "&itemName='+ getSelectedItem_" + mItemType.ToString() + "(),'ReportItemEdit'," + modalWidth + "," + modalHeight + "); } else { alert('" + ResHelper.GetString("Reporting_General.SelectObjectFirst") + "');} return false;";
            btnDelete.OnClientClick = "DeleteItem_" + mItemType.ToString() + "(); return false;";
            btnInsert.OnClientClick = "InserMacro_" + mItemType.ToString() + "(); return false;";

            btnPreview.OnClientClick = "if (getSelectedItem_" + mItemType.ToString() + "() != '0') { modalDialog('" + ResolveUrl(mEditUrl) + "?preview=true&reportId=" + Report.ReportID + "&itemName='+ getSelectedItem_" + mItemType.ToString() + "(),'ReportItemEdit'," + modalWidth + "," + modalHeight + "); } else { alert('" + ResHelper.GetString("Reporting_General.SelectObjectFirst") + "');} return false;";


        }
    }


    protected void btnHdnDelete_Click(object sender, EventArgs e)
    {
        // Check 'Modify' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.reporting", "Modify"))
        {
            CMSReportingPage.RedirectToCMSDeskAccessDenied("cms.reporting", "Modify");
        }

        string itemName = "";

        if (hdnItemId.Value != "")
        {
            itemName = Report.ReportName + "." + ValidationHelper.GetString(hdnItemId.Value, "");

            if ((mItemType == ReportItemType.Graph) || (mItemType == ReportItemType.HtmlGraph))
            {
                ReportGraphInfo rgi = ReportGraphInfoProvider.GetReportGraphInfo(itemName);

                if (rgi != null)
                {
                    ReportGraphInfoProvider.DeleteReportGraphInfo(rgi.GraphID);
                }
            }
            else if (mItemType == ReportItemType.Table)
            {
                ReportTableInfo rti = ReportTableInfoProvider.GetReportTableInfo(itemName);

                if (rti != null)
                {
                    ReportTableInfoProvider.DeleteReportTableInfo(rti.TableID);
                }
            }
            else if (mItemType == ReportItemType.Value)
            {
                ReportValueInfo rvi = ReportValueInfoProvider.GetReportValueInfo(itemName);

                if (rvi != null)
                {
                    ReportValueInfoProvider.DeleteReportValueInfo(rvi.ValueID);
                }
            }
        }
    }
}
