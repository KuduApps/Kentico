using System;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.FormControls;
using CMS.SiteProvider;

public partial class CMSModules_Friends_FormControls_FriendshipStatusSelector : FormEngineUserControl
{
    #region "Variables"

    private int mFriendshipStatusId = 0;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return base.Enabled;
        }
        set
        {
            base.Enabled = value;
            drpFriendshipStatusSelector.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets the field value.
    /// </summary>
    public override object Value
    {
        get
        {
        	 return FriendshipStatusID; 
        }
        set
        {
        	 FriendshipStatusID = ValidationHelper.GetInteger(value, 0); 
        }
    }


    /// <summary>
    /// Gets or sets the FriendshipStatus ID.
    /// </summary>
    public int FriendshipStatusID
    {
        get
        {
        	 return ValidationHelper.GetInteger(drpFriendshipStatusSelector.SelectedValue, 0); 
        }
        set
        {
            SelectValue(value.ToString());
            mFriendshipStatusId = value;
        }
    }

    #endregion


    protected override void CreateChildControls()
    {
        base.CreateChildControls();
        if (!StopProcessing)
        {
            ReloadData();
        }
    }
    

    /// <summary>
    /// Loads friendship status according to the control settings.
    /// </summary>
    public void ReloadData()
    {
        // Cleanup
        drpFriendshipStatusSelector.ClearSelection();
        drpFriendshipStatusSelector.SelectedValue = null;
        drpFriendshipStatusSelector.Items.Clear();

        // Bind dropdownlist
        foreach (string status in Enum.GetNames(typeof(FriendshipStatusEnum)))
        {
            FriendshipStatusEnum friendshipStatus =
                (FriendshipStatusEnum) Enum.Parse(typeof (FriendshipStatusEnum), status);
            int value = (int)friendshipStatus;
            if(friendshipStatus != FriendshipStatusEnum.None)
            {
                ListItem listItem = new ListItem(GetString("general." + status), value.ToString());
                drpFriendshipStatusSelector.Items.Add(listItem);
            }
        }

        // Try to preselect the value
        SelectValue(mFriendshipStatusId.ToString());
    }


    /// <summary>
    /// Tries to select the specified value in drpFriendshipStatusSelector.
    /// </summary>
    private void SelectValue(string value)
    {
        try
        {
            if (ValidationHelper.GetInteger(value, 0) != 0)
            {
                drpFriendshipStatusSelector.SelectedValue = value;
            }
        }
        catch { }
    }
}
