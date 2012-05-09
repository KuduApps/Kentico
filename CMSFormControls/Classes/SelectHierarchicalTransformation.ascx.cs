using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.SettingsProvider;
using CMS.GlobalHelper;

public partial class CMSFormControls_Classes_SelectHierarchicalTransformation : FormEngineUserControl
{
    /// <summary>
    /// Gets or sets the field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return this.SelectTransformation.Value;
        }
        set
        {
            this.SelectTransformation.Value = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        SelectTransformation.IsLiveSite = this.IsLiveSite;
        SelectTransformation.NewDialogPath = "~/CMSModules/DocumentTypes/Pages/Development/HierarchicalTransformations_New.aspx";        

    }


}
