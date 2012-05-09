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

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;

public partial class CMSAdminControls_UI_Selectors_LoadGenerationSelector : CMSUserControl
{
    private bool mNoChangeOption = false;


    /// <summary>
    /// If true, no change option (-1) is added to the list.
    /// </summary>
    public bool NoChangeOption
    {
        get
        {
            return mNoChangeOption;
        }
        set
        {
            mNoChangeOption = value;
        }
    }


    /// <summary>
    /// Selected value.
    /// </summary>
    public int Value
    {
        get
        {
            return ValidationHelper.GetInteger(this.drpGeneration.SelectedValue, 0);
        }
        set
        {
            try
            {
                this.drpGeneration.SelectedValue = value.ToString();
            }
            catch
            {
            }
        }
    }


    protected void Page_Init(object sender, EventArgs e)
    {
        if (this.NoChangeOption)
        {
            this.drpGeneration.Items.Add(new ListItem(GetString("LoadGeneration.NoChange"), "-1"));
        }
        this.drpGeneration.Items.Add(new ListItem(GetString("LoadGeneration.First"), "0"));
        this.drpGeneration.Items.Add(new ListItem(GetString("LoadGeneration.Second"), "1"));
        this.drpGeneration.Items.Add(new ListItem(GetString("LoadGeneration.Third"), "2"));
    }
}
