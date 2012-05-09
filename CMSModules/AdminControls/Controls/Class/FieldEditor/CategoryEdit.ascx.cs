using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;

public partial class CMSModules_AdminControls_Controls_Class_FieldEditor_CategoryEdit : CMSUserControl
{
    /// <summary>
    /// Value contained in category textbox.
    /// </summary>
    public string Value
    {
        get
        {
            return txtCategoryName.Text.Trim();
        }
        set
        {
            txtCategoryName.Text = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        pnlCategory.GroupingText = GetString("templatedesigner.section.category");
    }
}
