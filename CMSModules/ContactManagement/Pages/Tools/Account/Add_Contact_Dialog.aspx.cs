using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.OnlineMarketing;

public partial class CMSModules_ContactManagement_Pages_Tools_Account_Add_Contact_Dialog : CMSModalPage
{
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        RequireSite = false;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterWOpenerScript(Page);
        ScriptHelper.RegisterJQuery(Page);

        // Try to get parameters
        string identificator = QueryHelper.GetString("params", null);
        Hashtable parameters = (Hashtable)WindowHelper.GetItem(identificator);

        // Validate hash
        if ((QueryHelper.ValidateHash("hash", "selectedvalue")) && (parameters != null))
        {
            int siteID = ValidationHelper.GetInteger(parameters["SiteID"], -1);
            if (siteID != -1)
            {
                // Check permissions
                ContactHelper.AuthorizedReadContact(siteID, true);
                if (AccountHelper.AuthorizedModifyAccount(siteID, false) || ContactHelper.AuthorizedModifyContact(siteID, false))
                {
                    contactRoleSelector.SiteID = siteID;
                    contactRoleSelector.IsLiveSite = ValidationHelper.GetBoolean("IsLiveSite", false);
                    contactRoleSelector.UniSelector.DialogWindowName = "SelectContactRole";
                    contactRoleSelector.IsSiteManager = ValidationHelper.GetBoolean(parameters["IsSiteManager"], false);

                    selectionDialog.LocalizeItems = QueryHelper.GetBoolean("localize", true);

                    // Load resource prefix
                    string resourcePrefix = ValidationHelper.GetString(parameters["ResourcePrefix"], "general");

                    // Set the page title
                    string titleText = GetString(resourcePrefix + ".selectitem|general.selectitem");

                    // Validity group text
                    pnlRole.GroupingText = GetString(resourcePrefix + ".contactsrole");

                    CurrentMaster.Title.TitleText = titleText;
                    Page.Title = titleText;

                    string imgPath = ValidationHelper.GetString(parameters["IconPath"], null);
                    if (String.IsNullOrEmpty(imgPath))
                    {
                        string objectType = ValidationHelper.GetString(parameters["ObjectType"], null);

                        CurrentMaster.Title.TitleImage = GetObjectIconUrl(objectType, null);
                    }
                    else
                    {
                        CurrentMaster.Title.TitleImage = imgPath;
                    }

                    // Cancel button
                    btnCancel.ResourceString = "general.cancel";
                    btnCancel.Attributes.Add("onclick", "return US_Cancel();");
                }
                // No permission modify
                else
                {
                    CMSPage.RedirectToCMSDeskAccessDenied("CMS.ContactManagement", "ModifyAccount");
                }
            }
            else
            {
                // Redirect to error page
                URLHelper.Redirect(ResolveUrl("~/CMSMessages/Error.aspx?title=" + ResHelper.GetString("dialogs.badhashtitle") + "&text=" + ResHelper.GetString("dialogs.badhashtext")));
            }
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        btnOk.ResourceString = "general.ok";
        btnOk.Attributes.Add("onclick",
@"  var role = $j('#" + contactRoleSelector.DropDownList.ClientID + @"').val();
    if (wopener.setRole != null) wopener.setRole(role);
    US_Submit();
");
        base.OnPreRender(e);
    }


    protected override void OnPreRenderComplete(EventArgs e)
    {
        base.OnPreRenderComplete(e);
        if (selectionDialog.UniGrid.IsEmpty)
        {
            pnlRole.Visible = false;
        }
    }
}

