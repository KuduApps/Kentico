using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.Controls;
using CMS.CMSHelper;

public partial class CMSModules_Newsletters_Controls_TemplateFilter : CMSAbstractBaseFilterControl
{
    #region "Properties"

    /// <summary>
    /// Where condition.
    /// </summary>
    public override string WhereCondition
    {
        get
        {
            base.WhereCondition = GenerateWhereCondition();
            return base.WhereCondition;
        }
        set
        {
            base.WhereCondition = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!RequestHelper.IsPostBack())
        {
            this.Reload();
        }
    }


    /// <summary>
    /// Reloads control.
    /// </summary>
    protected void Reload()
    {
        filter.Items.Clear();
        filter.Items.Add(new ListItem(ResHelper.GetString("general.selectall"), "-1"));
        filter.Items.Add(new ListItem(ResHelper.GetString("NewsletterTemplate_List.OptIn"), "D"));
        filter.Items.Add(new ListItem(ResHelper.GetString("NewsletterTemplate_List.Issue"), "I"));
        filter.Items.Add(new ListItem(ResHelper.GetString("NewsletterTemplate_List.subscription"), "S"));
        filter.Items.Add(new ListItem(ResHelper.GetString("NewsletterTemplate_List.Unsubscription"), "U"));
    }


    /// <summary>
    /// Generates WHERE condition.
    /// </summary>
    private string GenerateWhereCondition()
    {
        switch (filter.SelectedValue)
        {
            // All templates
            default:
            case "-1":
                break;

            // Double opt-in templates
            case "D":
                return "TemplateType = 'D'";

            // Issue templates
            case "I":
                return "TemplateType = 'I'";

            // Subscription templates
            case "S":
                return "TemplateType = 'S'";

            // Unsubscription templates
            case "U":
                return "TemplateType = 'U'";
        }
        return null;
    }

    #endregion
}
