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
using System.Text;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.IO;

public partial class CMSWebParts_Community_GroupRegistration : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Gets or sets the value that indicates whether form should be hidden after successful registration.
    /// </summary>
    public bool HideFormAfterRegistration
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("HideFormAfterRegistration"), false);
        }
        set
        {
            this.SetValue("HideFormAfterRegistration", value);
        }
    }


    /// <summary>
    /// Gets or sets text which should be displayed after successful registration.
    /// </summary>
    public string SuccessfullRegistrationText
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("SuccessfullRegistrationText"), this.groupRegistrationElem.SuccessfullRegistrationText);
        }
        set
        {
            this.SetValue("SuccessfullRegistrationText", value);
            this.groupRegistrationElem.SuccessfullRegistrationText = value;
        }
    }


    /// <summary>
    /// Emails of admins capable of approving the group.
    /// </summary>
    public string SendWaitingForApprovalEmailTo
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SendWaitingForApprovalEmailTo"), String.Empty);
        }
        set
        {
            this.SetValue("SendWaitingForApprovalEmailTo", value);
            this.groupRegistrationElem.SendWaitingForApprovalEmailTo = value;
        }
    }


    /// <summary>
    /// Gets or sets text which should be displayed after successful registration and waiting for approving.
    /// </summary>
    public string SuccessfullRegistrationWaitingForApprovalText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SuccessfullRegistrationWaitingForApprovalText"), this.groupRegistrationElem.SuccessfullRegistrationWaitingForApprovalText);
        }
        set
        {
            this.SetValue("SuccessfullRegistrationWaitingForApprovalText", value);
            this.groupRegistrationElem.SuccessfullRegistrationWaitingForApprovalText = value;
        }
    }


    /// <summary>
    /// If true, the group must be approved before it can be active.
    /// </summary>
    public bool CombineWithDefaultCulture
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CombineWithDefaultCulture"), false);
        }
        set
        {
            this.SetValue("CombineWithDefaultCulture", value);
        }
    }


    /// <summary>
    /// If true, the group must be approved before it can be active.
    /// </summary>
    public bool RequireApproval
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("RequireApproval"), false);
        }
        set
        {
            this.SetValue("RequireApproval", value);
        }
    }


    /// <summary>
    /// Alias path of the document structure which will be copied as the group content.
    /// </summary>
    public string GroupTemplateSourceAliasPath
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("GroupTemplateSourceAliasPath"), "");
        }
        set
        {
            this.SetValue("GroupTemplateSourceAliasPath", value);
        }
    }


    /// <summary>
    /// Alias where the group content will be created by copying the source template.
    /// </summary>
    public string GroupTemplateTargetAliasPath
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("GroupTemplateTargetAliasPath"), "");
        }
        set
        {
            this.SetValue("GroupTemplateTargetAliasPath", value);
        }
    }


    /// <summary>
    /// Gets or sets the document url under which will be accessible the profile of newly created group.
    /// </summary>
    public string GroupProfileURLPath
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("GroupProfileURLPath"), "");
        }
        set
        {
            this.SetValue("GroupProfileURLPath", value);
        }
    }


    /// <summary>
    /// Gets or sets the url, where is user redirected after registration.
    /// </summary>
    public string RedirectToURL
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("RedirectToURL"), "");
        }
        set
        {
            this.SetValue("RedirectToURL", value);
        }
    }


    /// <summary>
    /// Gets or sets the label text of display name field.
    /// </summary>
    public string GroupNameLabelText
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("GroupNameLabelText"), GetString("Groups.GroupName") + ResHelper.Colon);
        }
        set
        {
            this.SetValue("GroupNameLabelText", value);
            this.groupRegistrationElem.GroupNameLabelText = value;
        }
    }


    /// <summary>
    /// Indicates if group forum should be created.
    /// </summary>
    public bool CreateForum
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("CreateForum"), true);
        }
        set
        {
            SetValue("CreateForum", value);
            groupRegistrationElem.CreateForum = value;
        }
    }


    /// <summary>
    /// Indicates if group media library should be created.
    /// </summary>
    public bool CreateMediaLibrary
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("CreateMediaLibrary"), true);
        }
        set
        {
            SetValue("CreateMediaLibrary", value);
            groupRegistrationElem.CreateMediaLibrary = value;
        }
    }


    /// <summary>
    /// Indicates if search indexes should be created.
    /// </summary>
    public bool CreateSearchIndexes
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("CreateSearchIndexes"), true);
        }
        set
        {
            SetValue("CreateSearchIndexes", value);
            groupRegistrationElem.CreateSearchIndexes = value;
        }
    }

    #endregion


    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Reloads data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do nothing
        }
        else
        {
            if (CMSContext.CurrentSite != null)
            {
                this.groupRegistrationElem.SiteID = CMSContext.CurrentSite.SiteID;
            }

            this.groupRegistrationElem.HideFormAfterRegistration = this.HideFormAfterRegistration;
            this.groupRegistrationElem.SuccessfullRegistrationText = this.SuccessfullRegistrationText;
            this.groupRegistrationElem.SuccessfullRegistrationWaitingForApprovalText = this.SuccessfullRegistrationWaitingForApprovalText;
            this.groupRegistrationElem.GroupNameLabelText = this.GroupNameLabelText;
            this.groupRegistrationElem.CombineWithDefaultCulture = this.CombineWithDefaultCulture;
            this.groupRegistrationElem.RequireApproval = this.RequireApproval;
            this.groupRegistrationElem.GroupTemplateSourceAliasPath = this.GroupTemplateSourceAliasPath;
            this.groupRegistrationElem.GroupTemplateTargetAliasPath = this.GroupTemplateTargetAliasPath;
            this.groupRegistrationElem.GroupProfileURLPath = this.GroupProfileURLPath;
            this.groupRegistrationElem.RedirectToURL = this.RedirectToURL;
            this.groupRegistrationElem.SendWaitingForApprovalEmailTo = SendWaitingForApprovalEmailTo;
            this.groupRegistrationElem.CreateForum = CreateForum;
            this.groupRegistrationElem.CreateMediaLibrary = CreateMediaLibrary;
            this.groupRegistrationElem.CreateSearchIndexes = CreateSearchIndexes;
            this.groupRegistrationElem.IsLiveSite = true;
        }
    }
}
