using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.Synchronization;

public partial class CMSModules_Settings_FormControls_SelectObjectRestoring : FormEngineUserControl
{
    #region "Properties"

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
            this.rblObjectsRestoring.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return string.IsNullOrEmpty(this.rblObjectsRestoring.SelectedValue) ? "VERSIONEDOBJECTS" : this.rblObjectsRestoring.SelectedValue;
        }
        set
        {
            this.EnsureChildControls();
            this.rblObjectsRestoring.SelectedValue = ValidationHelper.GetString(value, "VERSIONEDOBJECTS");
        }
    }


    /// <summary>
    /// Returns ClientID of the RadioButtonList with object restoring options.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return this.rblObjectsRestoring.ClientID;
        }
    }

    #endregion


    #region "Page Events"

    protected void Page_Load(object sender, EventArgs e)
    {
    }


    protected override void CreateChildControls()
    {
        base.CreateChildControls();
        if (!StopProcessing)
        {
            ReloadData();
        }
    }

    #endregion


    #region "Private Methods"

    /// <summary>
    /// Loads the child controls at run-time.
    /// </summary>
    private void ReloadData()
    {
        if ((this.rblObjectsRestoring.Items == null) || (this.rblObjectsRestoring.Items.Count <= 0))
        {
            this.rblObjectsRestoring.Items.Add(new ListItem(GetString("general.no"), ObjectVersionManager.RESTORE_NONE));
            this.rblObjectsRestoring.Items.Add(new ListItem(GetString("objectversioning.versionedobjects"), ObjectVersionManager.RESTORE_VERSIONEDOBJECTS));
            this.rblObjectsRestoring.Items.Add(new ListItem(GetString("objecttasks.__all__"), ObjectVersionManager.RESTORE_ALL));
        }
        this.rblObjectsRestoring.SelectedValue = this.Value.ToString();
    }

    #endregion
}
