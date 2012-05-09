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

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;

public partial class CMSModules_Membership_FormControls_Users_MultipleCategoriesSelector : FormEngineUserControl
{
    /// <summary>
    /// Gets coma separated ID of selected categories.
    /// </summary>
    public override object Value
    {
        get
        {
            // Return string of categories ID
            return this.categorySelector.Value;
        }
        set
        {
            base.Value = value;
            this.categorySelector.Value = ValidationHelper.GetString(value, "");
        }
    }


    /// <summary>
    /// Indicates if control is used on live site.
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
            this.categorySelector.IsLiveSite = value;
        }
    }


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.Form.OnAfterDataLoad += Form_OnAfterDataLoad;
        this.Form.OnAfterSave += Form_OnAfterSave;
    }


    void Form_OnAfterSave(object sender, EventArgs e)
    {
        this.categorySelector.Save();
    }


    void Form_OnAfterDataLoad(object sender, EventArgs e)
    {
        // Set document ID
        int documentID = ValidationHelper.GetInteger(this.Form.Data.GetValue("DocumentID"), 0);
        if (documentID > 0)
        {
            this.categorySelector.DocumentID = documentID;
        }
        // Set user ID
        if (CMSContext.CurrentUser != null)
        {
            this.categorySelector.UserID = CMSContext.CurrentUser.UserID;
        }
    }
}
