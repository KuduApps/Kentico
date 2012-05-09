using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.OnlineMarketing;

public partial class CMSModules_ContactManagement_FormControls_ContactSelectorDialog : CMSModalPage
{
    #region "Variables"

    private int siteId = -1;
    private Hashtable mParameters;
    private string where = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Stop processing flag.
    /// </summary>
    public bool StopProcessing
    {
        get
        {
            return gridElem.StopProcessing;
        }
        set
        {
            gridElem.StopProcessing = value;
        }
    }


    /// <summary>
    /// Hashtable containing dialog parameters.
    /// </summary>
    private Hashtable Parameters
    {
        get
        {
            if (mParameters == null)
            {
                string identificator = QueryHelper.GetString("params", null);
                mParameters = (Hashtable)WindowHelper.GetItem(identificator);
            }
            return mParameters;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!QueryHelper.ValidateHash("hash") || Parameters == null)
        {
            StopProcessing = true;
            return;
        }

        siteId = ValidationHelper.GetInteger(Parameters["siteid"], 0);
        where = ValidationHelper.GetString(Parameters["where"], null);

        // Check read permission
        if (ContactHelper.AuthorizedReadContact(siteId, true))
        {
            if (siteId == UniSelector.US_GLOBAL_RECORD)
            {
                CurrentMaster.Title.TitleText = GetString("om.contact.selectglobal");
            }
            else
            {
                CurrentMaster.Title.TitleText = GetString("om.contact.selectsite");
            }

            CurrentMaster.Title.TitleImage = GetImageUrl("Objects/OM_Contact/object.png");
            Page.Title = CurrentMaster.Title.TitleText;

            imgNew.ImageUrl = GetImageUrl("Objects/OM_Contact/add.png");
            btnNew.Click += new EventHandler(btn_Click);
            btnNew.CommandArgument = null;

            if (siteId > 0)
            {
                gridElem.WhereCondition = "(ContactMergedWithContactID IS NULL AND ContactSiteID = " + siteId + ")";
            }
            else
            {
                gridElem.WhereCondition = "(ContactGlobalContactID IS NULL AND ContactSiteID IS NULL)";
            }
            gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, "ContactID NOT IN (SELECT ContactID FROM OM_Contact WHERE " + where + ")");
            gridElem.OnExternalDataBound += gridElem_OnExternalDataBound;
            gridElem.ShowActionsMenu = false;
            if (!RequestHelper.IsPostBack())
            {
                gridElem.Pager.DefaultPageSize = 10;
            }
        }
    }



    /// <summary>
    /// Unigrid external databound handler.
    /// </summary>
    protected object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName)
        {
            case "ContactFullNameJoined":
                LinkButton btn = new LinkButton();
                DataRowView drv = parameter as DataRowView;
                btn.Text = HTMLHelper.HTMLEncode(ValidationHelper.GetString(drv["ContactFullNameJoined"], null));
                btn.Click += new EventHandler(btn_Click);
                btn.CommandArgument = ValidationHelper.GetString(drv["ContactID"], null);
                return btn;
        }
        return parameter;
    }


    /// <summary>
    /// Contact status selected event handler.
    /// </summary>
    protected void btn_Click(object sender, EventArgs e)
    {
        int contactID = ValidationHelper.GetInteger(((LinkButton)sender).CommandArgument, 0);
        string script = ScriptHelper.GetScript(@"
wopener.SelectValue_" + ValidationHelper.GetString(Parameters["clientid"], string.Empty) + @"(" + contactID + @");
window.close();
");

        ScriptHelper.RegisterStartupScript(this.Page, typeof(string), "CloseWindow", script);
    }

    #endregion
}
