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

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_TagGroups_Controls_TagEdit : CMSUserControl
{
    #region "Private fields"

    private int mGroupID = 0;
    private int mSiteID = 0;
    private bool mIsEdit = false;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Indicates whether the control is used to edit existing group.
    /// </summary>
    public bool IsEdit
    {
        get
        {
            return this.mIsEdit;
        }
        set
        {
            this.mIsEdit = value;
        }
    }


    /// <summary>
    /// Indicates whether the previous tag group was saved.
    /// </summary>
    public bool WasSaved
    {
        get
        {
            if (QueryHelper.GetInteger("saved", 0) == 1) 
            {
                return true;
            }
            return false;
        }
    }

    #endregion


    #region "Public properties"

    /// <summary>
    /// ID of the currently processed tag group.
    /// </summary>
    public int GroupID
    {
        get
        {
            if (this.mGroupID == 0) 
            {
                this.mGroupID = QueryHelper.GetInteger("groupid", 0);
            }
            return mGroupID;
        }
        set
        {
            this.mGroupID = value;
        }
    }


    /// <summary>
    /// ID of site of the currently processed tag group.
    /// </summary>
    public int SiteID
    {
        get
        {
            return mSiteID;
        }
        set
        {
            this.mSiteID = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize control
        SetupControl();
    }


    #region "Event handlers"

    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Validate form entries
        string errorMessage = ValidateForm();

        if (errorMessage == "")
        {
            TagGroupInfo tgi = null;

            try
            {
                // Edit existing tag group
                if (this.GroupID > 0)
                {
                    tgi = TagGroupInfoProvider.GetTagGroupInfo(this.GroupID);
                }
                else
                {
                    tgi = new TagGroupInfo();
                }

                // Update tag group info fields                
                tgi.TagGroupDisplayName = this.txtDisplayName.Text;
                tgi.TagGroupName = this.txtCodeName.Text;
                tgi.TagGroupDescription = this.txtDescription.Text;
                tgi.TagGroupSiteID = this.SiteID;

                // If the new tag group is created set the default value for TagGroupIsAdHoc property 
                if (!this.IsEdit)
                {
                    tgi.TagGroupIsAdHoc = false;
                }

                // Update tag group info
                TagGroupInfoProvider.SetTagGroupInfo(tgi);

                // Redirect to edit page once the new tag group is created
                if (!this.IsEdit)
                {
                    string editUrl = "~/CMSModules/TagGroups/Pages/Development/TagGroup_Edit.aspx?groupid=" + tgi.TagGroupID.ToString() + "&siteid=" + this.SiteID.ToString() + "&saved=1";
                    URLHelper.Redirect(editUrl);
                }

                // Inform about success while editing
                this.lblError.Visible = false;
                this.lblInfo.Text = GetString("general.changessaved");
                this.lblInfo.Visible = true;

                // Refresh header
                ScriptHelper.RefreshTabHeader(this.Page, "general");
            }
            catch (Exception ex)
            {
                // Display error message
                this.lblInfo.Visible = false;
                this.lblError.Text = GetString("general.erroroccurred") + " " + ex.Message;
                this.lblError.Visible = true;
            }
        }
        else 
        {
            // Display error message
            this.lblInfo.Visible = false;
            this.lblError.Text = errorMessage;
            this.lblError.Visible = true;
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Initializes the controls.
    /// </summary>
    private void SetupControl()
    {
        // Set the error messages
        this.rfvDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
        this.rfvCodeName.ErrorMessage = GetString("general.requirescodename");

        // Handle existing tag group editing
        if (this.GroupID > 0)
        {
            if (!RequestHelper.IsPostBack())
            {
                HandleExisting();
            }
        }

        if (!RequestHelper.IsPostBack())
        {
            if (this.WasSaved)
            {
                // Inform about success while editing
                this.lblError.Visible = false;
                this.lblInfo.Text = GetString("general.changessaved");
                this.lblInfo.Visible = true;
            }
        }
    }


    /// <summary>
    /// Handles pre-actions required when editing existing tag group.
    /// </summary>
    private void HandleExisting()
    {
        // Get info on existing tag group
        TagGroupInfo tgi = TagGroupInfoProvider.GetTagGroupInfo(this.GroupID);

        // Prefill the fields with obtained data
        this.txtDisplayName.Text = tgi.TagGroupDisplayName;
        this.txtCodeName.Text = tgi.TagGroupName;
        this.txtDescription.Text = tgi.TagGroupDescription;        
    }


    /// <summary>
    /// Validates required entries.
    /// </summary>    
    /// <param name="newGroupName">Group name to check for unique</param>
    /// <param name="siteId">ID of the site tag group should be inserted</param>
    /// <returns>True if all the necessary data were entered, otherwise false is returned</returns>
    private bool CodeNameIsUnique(string newGroupName, int siteId)
    {
        // Gheck if the tag group already exist
        TagGroupInfo tagGroup = TagGroupInfoProvider.GetTagGroupInfo(newGroupName, siteId);
        if (tagGroup == null)
        {
            return true;
        }
        else
        {
            // If the existing tag group is updated the code name already exist
            if ((this.GroupID > 0) && (this.GroupID == tagGroup.TagGroupID))
            {
                return true;
            }

            return false;
        }
    }


    /// <summary>
    /// Validates the form entries.
    /// </summary>
    /// <returns>Empty string if validation passed otherwise error message is returned</returns>
    private string ValidateForm()
    {
        string errorMessage = string.Empty;
        // Trim all textboxes
        this.txtDisplayName.Text = this.txtDisplayName.Text.Trim();
        this.txtCodeName.Text = this.txtCodeName.Text.Trim();
        this.txtDescription.Text = this.txtDescription.Text.Trim();

        errorMessage = new Validator().IsCodeName(this.txtCodeName.Text, GetString("general.invalidcodename")).Result;

        if (string.IsNullOrEmpty(errorMessage))
        {
            this.rfvCodeName.Validate();
            this.rfvDisplayName.Validate();

            // If all the required fields entries were supplied
            if (this.rfvCodeName.IsValid & this.rfvDisplayName.IsValid)
            {
                // Check if the given code name is unique
                if (!CodeNameIsUnique(this.txtCodeName.Text, this.SiteID))
                {
                    errorMessage = GetString("general.codenameexists");

                    this.lblInfo.Visible = false;
                    this.lblError.Visible = true;

                    this.lblError.Text = errorMessage;
                }
            }
            else
            {
                errorMessage = this.rfvCodeName.ErrorMessage;
            }
        }

        return errorMessage;
    }

    #endregion
}
