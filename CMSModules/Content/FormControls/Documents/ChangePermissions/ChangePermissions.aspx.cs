using System;

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSModules_Content_FormControls_Documents_ChangePermissions_ChangePermissions : CMSModalPage
{
    #region "Properties"

    private static int NodeID
    {
        get
        {
            return QueryHelper.GetInteger("nodeid", 0);
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Set master page
        CurrentMaster.Title.TitleText = GetString("selectsinglepath.setpermissions");
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Settings/Categories/CMS_Security/module.png");
        if (QueryHelper.ValidateHash("hash"))
        {
            if (NodeID == 0)
            {
                lblInfo.Text = GetString("content.documentnotexists");
                securityElem.Visible = false;
            }
            else
            {

                // Setup security control
                securityElem.DisplayButtons = false;
                securityElem.NodeID = NodeID;

                // Setup ok button
                btnOk.Click += btnOk_Click;
            }
        }
        else
        {
            securityElem.Visible = false;
        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(lblInfo.Text))
        {
            lblInfo.Visible = true;
        }
    }

    #endregion


    #region "Button handling"

    protected void btnOk_Click(object sender, EventArgs e)
    {
        securityElem.Save();
    }

    #endregion
}
