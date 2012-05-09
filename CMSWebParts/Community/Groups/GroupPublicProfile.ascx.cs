using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;

using CMS.PortalControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Community;
using CMS.FormEngine;

public partial class CMSWebParts_Community_Groups_GroupPublicProfile : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Group name which profile should be displayed.
    /// </summary>
    public string GroupName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("GroupName"), "");
        }
        set
        {
            this.SetValue("GroupName", value);
        }
    }


    /// <summary>
    /// Name of the alternative form (ClassName.AlternativeFormName)
    /// Default value is Community.Group.DisplayProfile
    /// </summary>
    public string AlternativeFormName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("AlternativeFormName"), "Community.Group.DisplayProfile");
        }
        set
        {
            this.SetValue("AlternativeFormName", value);
        }
    }


    /// <summary>
    /// No profile text.
    /// </summary>
    public string NoProfileText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("NoProfileText"), "");
        }
        set
        {
            this.SetValue("NoProfileText", value);
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
            // Get group info
            GroupInfo gi = null;
            if (((this.GroupName == "") || this.GroupName == GroupInfoProvider.CURRENT_GROUP) && (CommunityContext.CurrentGroup != null))
            {
                gi = CommunityContext.CurrentGroup;
            }
            else
            {
                gi = GroupInfoProvider.GetGroupInfo(this.GroupName, CMSContext.CurrentSiteName);
            }

            if (gi != null)
            {
                // Get alternative form info
                AlternativeFormInfo afi = AlternativeFormInfoProvider.GetAlternativeFormInfo(AlternativeFormName);
                if (afi != null)
                {
                    // Initialize data form
                    formElem.Visible = true;
                    formElem.Info = gi;
                    formElem.AlternativeFormFullName = this.AlternativeFormName;
                    formElem.BasicForm.SubmitButton.Visible = false;
                    formElem.IsLiveSite = true;
                }
                else
                {
                    lblError.Text = String.Format(GetString("altform.formdoesntexists"),AlternativeFormName);
                    lblError.Visible = true;
                    plcContent.Visible = false;
                }
            }
            else
            {
                // Hide data form
                formElem.Visible = false;
                lblNoProfile.Visible = true;
                lblNoProfile.Text = this.NoProfileText;
            }
        }
    }
}
