using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.WebAnalytics;

public partial class CMSModules_WebAnalytics_Controls_UI_Conversion_Edit : CMSAdminEditControl
{
    #region "Variables"

    String oldConversionName = String.Empty;

    #endregion


    #region "Properties"

    /// <summary>
    /// UIForm control used for editing objects properties.
    /// </summary>
    public UIForm UIFormControl
    {
        get
        {
            return this.EditForm;
        }
    }


    /// <summary>
    /// Indicates if the control should perform the operations.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return base.StopProcessing;
        }
        set
        {
            base.StopProcessing = value;
            this.EditForm.StopProcessing = value;
        }
    }


    /// <summary>
    /// Indicates if the control is used on the live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return base.IsLiveSite;
        }
        set
        {
            base.IsLiveSite = value;
            EditForm.IsLiveSite = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        EditForm.OnBeforeSave += new EventHandler(EditForm_OnBeforeSave);
        EditForm.OnAfterSave += new EventHandler(EditForm_OnAfterSave);
        bool modalDialog = QueryHelper.GetBoolean("modaldialog", false);

        if (modalDialog)
        {
            EditForm.SubmitButton.Visible = false;
            EditForm.RedirectUrlAfterCreate = "";
        }
    }


    void EditForm_OnAfterSave(object sender, EventArgs e)
    {
        ConversionInfo ci = EditForm.EditedObject as ConversionInfo;
        // If code name has changed (on existing object) => Rename all analytics statistics data.
        if ((ci != null) && (ci.ConversionName != oldConversionName) && (oldConversionName != String.Empty))
        {
            ConversionInfoProvider.RenameConversionStatistics(oldConversionName, ci.ConversionName, CMSContext.CurrentSiteID);
        }
    }


    void EditForm_OnBeforeSave(object sender, EventArgs e)
    {
        ConversionInfo ci = EditForm.EditedObject as ConversionInfo;
        if (ci != null)
        {
            ci.ConversionSiteID = CMSContext.CurrentSiteID;
            oldConversionName = ci.ConversionName;
        }
    }


    /// <summary>
    /// Saves the data
    /// </summary>
    /// <param name="redirect">If true, use server redirect after successfull save</param>
    public bool Save(bool redirect)
    {
        string selectorID = QueryHelper.GetString("selectorID", String.Empty);

        bool ret = EditForm.SaveData("");

        // If saved - redirect with ConversionID parameter
        if ((ret) && (redirect))
        {
            ConversionInfo ci = (ConversionInfo)EditForm.EditedObject;
            if (ci != null)
            {
                URLHelper.Redirect("edit.aspx?conversionid=" + ci.ConversionID + "&saved=1&modaldialog=true&selectorID=" + selectorID);
            }
        }

        return ret;
    }

    #endregion
}

