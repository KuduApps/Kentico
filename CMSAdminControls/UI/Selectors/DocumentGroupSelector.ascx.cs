using System;

using CMS.SettingsProvider;
using CMS.FormControls;
using CMS.GlobalHelper;

public partial class CMSAdminControls_UI_Selectors_DocumentGroupSelector : FormEngineUserControl
{
    #region "Private variables"

    private int mValue = 0;
    private int mSiteId = 0;
    private int mNodeId = 0;
    #endregion


    #region "Public properties"
    
    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return mValue;
        }
        set
        {
            mValue = (int)value;
        }
    }


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
            EnsureChildControls();

            base.Enabled = value;
            txtGroups.Enabled = value;
            btnChange.Enabled = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize javascripts
        ScriptHelper.RegisterDialogScript(this.Page);
        btnChange.OnClientClick = "modalDialog('" + ResolveUrl("~/CMSAdminControls/UI/Dialogs/ChangeGroup.aspx") + "?nodeid="+mNodeId+"&siteid="+mSiteId+"&groupid="+Value+"','GroupSelector', 350, 200); return false;";
        ltlScript.Text = ScriptHelper.GetScript("function ReloadOwner(){" + Page.ClientScript.GetPostBackEventReference(btnHidden, string.Empty) +"}");

        int groupId = ValidationHelper.GetInteger(Value, 0);

        if (groupId > 0)
        {
            // Display current owner
            GeneralizedInfo gi = ModuleCommands.CommunityGetGroupInfo(groupId);
            if (gi != null)
            {
                txtGroups.Text =  ValidationHelper.GetString(gi.GetValue("GroupDisplayName"), String.Empty);
            }
        }
    }

    protected void btnHidden_Click(object sender, EventArgs e){}

    #endregion


    #region "Overidden methods"

    public override void SetValue(string propertyName, object value)
    {
        switch (propertyName.ToLower())
        {
            case "siteid":
                mSiteId = (int) value;
                break;

            case "nodeid":
                mNodeId = (int) value;
                break;
        }
        base.SetValue(propertyName, value);
    }


    public override object GetValue(string propertyName)
    {
        switch (propertyName.ToLower())
        {
            case "siteid":
                return mSiteId;

            case "nodeid":
                return mNodeId;
        }
        return base.GetValue(propertyName);
    }

    #endregion
}
