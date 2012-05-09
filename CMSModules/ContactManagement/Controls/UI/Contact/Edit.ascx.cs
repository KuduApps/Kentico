using System;
using System.Data;
using System.Collections;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using System.Collections.Generic;

[System.Runtime.InteropServices.GuidAttribute("8EF3FF11-429C-4438-B922-A0884CED307F")]
public partial class CMSModules_ContactManagement_Controls_UI_Contact_Edit : CMSAdminEditControl
{
    #region "Variables and constants"

    private int mSiteID = 0;
    private bool mergedIntoSite = false;
    private bool mergedIntoGlobal = false;
    private ContactInfo parentContact = null;
    private bool isNew = false;

    /// <summary>
    /// URL of modal dialog window for contact editing.
    /// </summary>
    private const string CONTACT_DETAIL_DIALOG = "~/CMSModules/ContactManagement/Pages/Tools/Account/Contact_Detail.aspx";

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


    /// <summary>
    /// SiteID of current account.
    /// </summary>
    public int SiteID
    {
        get
        {
            return mSiteID;
        }
        set
        {
            mSiteID = value;

            if ((mSiteID > 0) && !CMSContext.CurrentUser.UserSiteManagerAdmin)
            {
                mSiteID = CMSContext.CurrentSiteID;
            }

            if (userSelector != null)
            {
                userSelector.ShowSiteFilter = true;
                userSelector.SiteID = mSiteID;
            }
            if (contactStatusSelector != null)
            {
                contactStatusSelector.SiteID = mSiteID;
            }
            if (campaignSelector != null)
            {
                campaignSelector.SiteID = mSiteID;
            }
        }
    }

    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        EditForm.OnAfterDataLoad += new EventHandler(EditForm_OnAfterDataLoad);
        EditForm.OnBeforeSave += new EventHandler(EditForm_OnBeforeSave);
        EditForm.OnAfterSave += new EventHandler(EditForm_OnAfterSave);

        InitHeaderActions();
    }


    void EditForm_OnAfterSave(object sender, EventArgs e)
    {
        // Refresh breadcrumbs after data are saved
        RaiseOnSaved();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Show/hide campaign selector
        fCampaignSelection.Visible = CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.WebAnalytics", "read");

        // Get parent contact
        if (mergedIntoSite)
        {
            parentContact = ContactInfoProvider.GetContactInfo(ValidationHelper.GetInteger(this.EditForm.Data["ContactMergedWithContactID"], 0));
            lblMergedInto.ResourceString = "om.contact.mergedintosite";
        }
        else if (mergedIntoGlobal)
        {
            parentContact = ContactInfoProvider.GetContactInfo(ValidationHelper.GetInteger(this.EditForm.Data["ContactGlobalContactID"], 0));
            lblMergedInto.ResourceString = "om.contact.mergedintoglobal";
        }
        else
        {
            lblMergedInto.Visible = false;
        }

        // Register scripts
        RegisterScripts();

        // Initialize properties
        pnlGeneral.GroupingText = GetString("general.general");
        pnlPersonal.GroupingText = GetString("om.contact.personal");
        pnlSettings.GroupingText = GetString("om.contact.settings");
        pnlAddress.GroupingText = GetString("general.address");
        pnlNotes.GroupingText = GetString("om.contact.notes");
        btnStamp.OnClientClick = "AddStamp('" + htmlNotes.CurrentEditor.ClientID + "'); return false;";

        // Initialize redirection URL
        string url = "Frameset.aspx?contactid={%EditedObject.ID%}&saved=1";
        url = URLHelper.AddParameterToUrl(url, "siteid", this.SiteID.ToString());
        if (ContactHelper.IsSiteManager)
        {
            url = URLHelper.AddParameterToUrl(url, "issitemanager", "1");
        }
        EditForm.RedirectUrlAfterCreate = url;

        if (!RequestHelper.IsPostBack() && QueryHelper.GetBoolean("split", false))
        {
            DisplayInfo(GetString("om.contact.splitted"));
        }

        if (userSelector.SiteID <= 0)
        {
            userSelector.WhereCondition = "UserName NOT LIKE N'public'";
            userSelector.ShowSiteFilter = true;
        }
        userSelector.ReloadData();
    }

    #endregion


    #region "UIform events"

    /// <summary>
    /// OnAfterDataLoad event handler.
    /// </summary>
    protected void EditForm_OnAfterDataLoad(object sender, EventArgs e)
    {
        if (!RequestHelper.IsPostBack())
        {
            countrySelector.CountryID = ValidationHelper.GetInteger(this.EditForm.Data["ContactCountryID"], 0);
            countrySelector.StateID = ValidationHelper.GetInteger(this.EditForm.Data["ContactStateID"], 0);
        }
        if ((this.EditForm.EditedObject != null) && (ValidationHelper.GetInteger(this.EditForm.EditedObject.GetValue("ContactID"), 0) != 0))
        {
            this.SiteID = ValidationHelper.GetInteger(this.EditForm.Data["ContactSiteID"], 0);
        }
        else
        {
            userSelector.SiteID = this.SiteID;
            contactStatusSelector.SiteID = this.SiteID;
            campaignSelector.SiteID = this.SiteID;
            chkMonitored.Value = 1;
            isNew = true;
        }
        if (userSelector.SiteID <= 0)
        {
            userSelector.WhereCondition = "UserName NOT LIKE N'public'";
        }

        userSelector.ReloadData();
        contactStatusSelector.ReloadData();
        campaignSelector.ReloadData();

        // Set time created
        cCreated.Text = DateTimeHelper.DateTimeToString(this.EditForm.EditedObject.GetDateTimeValue("ContactCreated", DateTimeHelper.ZERO_TIME));
    }


    /// <summary>
    /// OnBeforeSave event handler.
    /// </summary>
    void EditForm_OnBeforeSave(object sender, EventArgs e)
    {
        if (countrySelector.CountryID == 0)
        {
            this.EditForm.Data["ContactCountryID"] = DBNull.Value;
        }
        else
        {
            this.EditForm.Data["ContactCountryID"] = countrySelector.CountryID;
        }
        if (countrySelector.StateID == 0)
        {
            this.EditForm.Data["ContactStateID"] = DBNull.Value;
        }
        else
        {
            this.EditForm.Data["ContactStateID"] = countrySelector.StateID;
        }
        if (this.SiteID > 0)
        {
            this.EditForm.Data["ContactSiteID"] = this.SiteID;
        }
        else
        {
            this.EditForm.Data["ContactSiteID"] = DBNull.Value;
        }

        if (isNew)
        {
            // Set ContactIsAnonymous default value
            this.EditForm.Data["ContactIsAnonymous"] = true;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Registers JavaScripts on page.
    /// </summary>
    private void RegisterScripts()
    {
        string stamp = null;
        if (ContactHelper.IsSiteManager)
        {
            stamp = SettingsKeyProvider.GetStringValue("CMSCMStamp");
        }
        else
        {
            stamp = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSCMStamp");
        }
        ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(string), "AddStamp", ScriptHelper.GetScript(
@"function InsertHTML(htmlString, ckClientID)
{
    // Get the editor instance that we want to interact with.
    var oEditor = oEditor = window.CKEDITOR.instances[ckClientID];
    // Check the active editing mode.
    if (oEditor != null) {
        // Check the active editing mode.
        if (oEditor.mode == 'wysiwyg') {
            // Insert the desired HTML.
            oEditor.focus();
            oEditor.insertHtml(htmlString);        
        }
    }    
    return false;
}   

function AddStamp(ckClientID)
{
InsertHTML('<div>" + CMSContext.CurrentResolver.ResolveMacros(stamp).Replace("'", @"\'") + @"</div>', ckClientID);
}

function EditContact(contactID)
{
    modalDialog('" + ResolveUrl(CONTACT_DETAIL_DIALOG) + @"?contactid=' + contactID + '&isSiteManager=" + ContactHelper.IsSiteManager + @"', 'ContactParent', '1061px', '700px');
}

"));
        if (mergedIntoSite || mergedIntoGlobal)
        {
            string contactFullName = parentContact.ContactFirstName + " " + parentContact.ContactMiddleName;
            contactFullName = contactFullName.Trim() + " " + parentContact.ContactLastName;
            ltlButton.Text = string.Format(@" {0} <img class='UnigridActionButton' style='cursor:pointer;' onclick='EditContact({1});return false;' src='{2}' title='{3}'>",
                            HTMLHelper.HTMLEncode(contactFullName.Trim()),
                            parentContact.ContactID,
                            GetImageUrl("Design/Controls/UniGrid/Actions/contactdetail.png"),
                            GetString("om.contact.viewdetail"));
        }
    }


    /// <summary>
    /// Initializes header action control.
    /// </summary>
    private void InitHeaderActions()
    {
        // Find out if current contact is merged into another site or global contact
        mergedIntoSite = ValidationHelper.GetInteger(this.EditForm.Data["ContactMergedWithContactID"], 0) != 0;
        mergedIntoGlobal = ValidationHelper.GetInteger(this.EditForm.Data["ContactGlobalContactID"], 0) != 0;
        mergedIntoGlobal &= ContactHelper.AuthorizedModifyContact(UniSelector.US_GLOBAL_RECORD, false);

        if (!ContactHelper.IsSiteManager)
        {
            mergedIntoGlobal &= SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".CMSCMGlobalContacts");
        }

        string[,] actions;
        if (mergedIntoSite || mergedIntoGlobal)
        {
            actions = new string[2, 11];
        }
        else
        {
            actions = new string[1, 11];
        }

        // Initialize SAVE button
        actions[0, 0] = HeaderActions.TYPE_SAVEBUTTON;
        actions[0, 1] = GetString("General.Save");
        actions[0, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
        actions[0, 6] = "save";
        actions[0, 8] = "true";

        if (mergedIntoSite || mergedIntoGlobal)
        {
            // Initialize SPLIT button
            actions[1, 0] = HeaderActions.TYPE_SAVEBUTTON;
            actions[1, 1] = GetString("om.contact.splitfromparent");
            actions[1, 5] = GetImageUrl("CMSModules/CMS_ContactManagement/split.png");
            actions[1, 6] = "split";
            actions[1, 8] = "false";
        }
        ((CMSPage)Page).CurrentMaster.HeaderActions.LinkCssClass = "ContentSaveLinkButton";
        ((CMSPage)Page).CurrentMaster.HeaderActions.ActionPerformed += HeaderActions_ActionPerformed;
        ((CMSPage)Page).CurrentMaster.HeaderActions.Actions = actions;
    }


    /// <summary>
    /// Actions handler.
    /// </summary>
    protected void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        // Check permission
        ContactHelper.AuthorizedModifyContact(this.SiteID, true);

        switch (e.CommandName.ToLower())
        {
            // Save contact
            case "save":
                if (ValidateForm())
                {
                    EditForm.SaveData(null);
                }
                break;

            // Split from parent contact
            case "split":
                List<ContactInfo> mergedContact = new List<ContactInfo>();
                mergedContact.Add((ContactInfo)CMSPage.EditedObject);
                ContactHelper.Split(parentContact, mergedContact, false, false, false, false);
                ScriptHelper.RefreshTabHeader(this.Page, null);

                string url = URLHelper.AddParameterToUrl(URLHelper.CurrentURL, "split", "1");
                ScriptHelper.RegisterStartupScript(this.Page, typeof(string), "RefreshPage", ScriptHelper.GetScript("window.location.replace('" + url + "');"));
                break;
        }
    }


    /// <summary>
    /// Performs custom validation and displays error in top of the page.
    /// </summary>
    /// <returns>Returns true if validation is successful.</returns>
    protected bool ValidateForm()
    {
        // Validate name
        string errorMessage = new Validator().NotEmpty(txtLastName.Text.Trim(), GetString("om.contact.enterlastname")).Result;
        if (!String.IsNullOrEmpty(errorMessage.ToString()))
        {
            DisplayError(errorMessage);
            return false;
        }

        // Validates birthday
        if (!calBirthday.IsValid())
        {
            DisplayError(calBirthday.ValidationError);
            return false;
        }

        // Validates email
        if (!emailInput.IsValid())
        {
            DisplayError(emailInput.ValidationError);
            return false;
        }

        return true;
    }


    /// <summary>
    /// Displayes error message in header section of page.
    /// </summary>
    private void DisplayError(string errorMessage)
    {
        ((CMSPage)Page).ShowError(TextHelper.LimitLength(errorMessage, 200), errorMessage);
    }


    /// <summary>
    /// Displayes info label.
    /// </summary>
    /// <param name="infoMessage">Information message</param>
    private void DisplayInfo(string infoMessage)
    {
        ((CMSPage)Page).ShowInformation(infoMessage);
    }

    #endregion
}

