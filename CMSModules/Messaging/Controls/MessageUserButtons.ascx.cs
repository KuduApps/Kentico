using System;
using System.Web.UI;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Messaging;
using CMS.UIControls;

public partial class CMSModules_Messaging_Controls_MessageUserButtons : CMSUserControl
{
    #region "Private variables"

    private string mInformationText = string.Empty;
    private string mErrorText = string.Empty;
    private bool mDisplayInline = false;
    
    #endregion


    #region "Public properties"

    /// <summary>
    /// Related user.
    /// </summary>
    public int RelatedUserId
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["RelatedUserId"], 0);
        }
        set
        {
            ViewState["RelatedUserId"] = value;
        }
    }


    /// <summary>
    /// Information text.
    /// </summary>
    public string InformationText
    {
        get
        {
            return mInformationText;
        }
        set
        {
            mInformationText = value;
        }
    }


    /// <summary>
    /// Error text.
    /// </summary>
    public string ErrorText
    {
        get
        {
            return mErrorText;
        }
        set
        {
            mErrorText = value;
        }
    }


    /// <summary>
    /// Indicates if the control should be rendered as inline.
    /// </summary>
    public bool DisplayInline
    {
        get
        {
            return mDisplayInline;
        }
        set
        {
            mDisplayInline = value;
        }
    }
    
    #endregion


    #region "Page events"

    /// <summary>
    /// Page load.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event arguments</param>
    protected void Page_Load(object sender, EventArgs e)
    {
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!StopProcessing)
        {
            if (DisplayInline)
            {
                pnlButtons.Attributes.Add("style", "display: inline;");
            }
            ReloadData();
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Reload data.
    /// </summary>
    public void ReloadData()
    {
        if (RelatedUserId > 0)
        {
            btnAddToIgnoreList.OnClientClick = "return confirm(" + ScriptHelper.GetString(GetString("Messsaging.AddToIgnoreListConfirmation")) + ");";

            btnAddToContactList.ToolTip = GetString("Messsaging.AddToContactList");
            btnAddToIgnoreList.ToolTip = GetString("Messsaging.AddToIgnoreList");
            imgAddToContactList.AlternateText = GetString("Messsaging.AddToContactList");
            imgAddToIgnoreList.AlternateText = GetString("Messsaging.AddToIgnoreList");

            imgAddToContactList.ImageUrl = GetImageUrl("/CMSModules/CMS_Messaging/addtocontactlist.png");
            imgAddToIgnoreList.ImageUrl = GetImageUrl("/CMSModules/CMS_Messaging/addtoignorelist.png");

            pnlButtons.Visible = true;
            btnAddToContactList.Visible = true;
            btnAddToIgnoreList.Visible = true;

            // Hide btnAddToContactList if sender is already in contact list
            if (ContactListInfoProvider.IsInContactList(CMSContext.CurrentUser.UserID, RelatedUserId))
            {
                btnAddToContactList.Visible = false;
            }
            // Hide btnAddToIgnoreList if sender is already in ignore list
            if (IgnoreListInfoProvider.IsInIgnoreList(CMSContext.CurrentUser.UserID, RelatedUserId))
            {
                btnAddToIgnoreList.Visible = false;
            }
        }
        else
        {
            pnlButtons.Visible = false;
        }
    }

    #endregion


    #region "Button handling"

    protected void btnAddToIgnoreList_Click(object sender, EventArgs e)
    {
        try
        {
            // Current user ID
            int currentUserId = CMSContext.CurrentUser.UserID;

            // Add user to ignore list
            IgnoreListInfoProvider.AddToIgnoreList(currentUserId, RelatedUserId);

            InformationText = GetString("MessageUserButtons.IgnoreAdded");
        }
        catch (Exception ex)
        {
            ErrorText = ex.Message;
        }
    }


    protected void btnAddToContactList_Click(object sender, EventArgs e)
    {
        try
        {
            // Current user ID
            int currentUserId = CMSContext.CurrentUser.UserID;

            // Add user to contact list
            ContactListInfoProvider.AddToContactList(currentUserId, RelatedUserId);

            InformationText = GetString("MessageUserButtons.ContactAdded");
        }
        catch (Exception ex)
        {
            ErrorText = ex.Message;
        }
    }

    #endregion
}