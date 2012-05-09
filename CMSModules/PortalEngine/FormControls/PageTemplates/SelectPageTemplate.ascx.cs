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

using CMS.GlobalHelper;
using CMS.PortalEngine;

public partial class CMSModules_PortalEngine_FormControls_PageTemplates_SelectPageTemplate : CMS.FormControls.FormEngineUserControl
{
    private PageTemplateInfo pti = null;
    private bool mShowOnlySiteTemplates = true;


    #region "Public properties"

    /// <summary>
    /// Enables or disables the control.
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
            this.btnClear.Enabled = value;
            this.txtTemplate.Enabled = value;
            this.btnSelect.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets page template ID.
    /// </summary>
    public int PageTemplateID
    {
        get
        {
            if (pti != null)
            {
                return pti.PageTemplateId;
            }

            return 0;
        }
        set
        {
            pti = PageTemplateInfoProvider.GetPageTemplateInfo(value);
        }
    }


    /// <summary>
    /// Gets or sets field value (Page template code name).
    /// </summary>
    public override object Value
    {
        get
        {
            pti = PageTemplateInfoProvider.GetPageTemplateInfo(ValidationHelper.GetInteger(this.hdnTemplateCode.Value, 0));
            if (pti != null)
            {
                return pti.CodeName;
            }

            return String.Empty;
        }
        set
        {
            string currentCodeName = ValidationHelper.GetString(value, "");
            pti = PageTemplateInfoProvider.GetPageTemplateInfo(currentCodeName);

            LoadData();
        }
    }


    /// <summary>
    /// Gets ClientID of the textbox with template.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return txtTemplate.ClientID;
        }
    }


    /// <summary>
    /// Gets or sets a value indicating whether to show site page templates only.
    /// </summary>
    public bool ShowOnlySiteTemplates
    {
        get
        {
            return mShowOnlySiteTemplates;
        }
        set
        {
            mShowOnlySiteTemplates = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (RequestHelper.IsPostBack())
        {
            int currentId = ValidationHelper.GetInteger(this.hdnTemplateCode.Value, 0);
            pti = PageTemplateInfoProvider.GetPageTemplateInfo(currentId);
        }

        ScriptHelper.RegisterDialogScript(this.Page);
        WindowHelper.Add("ShowOnlySiteTemplates", ShowOnlySiteTemplates);

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "SelectTemplate", ScriptHelper.GetScript(
            "function OpenSelectTemplateDialog (selectorId){modalDialog('" + ResolveUrl("~/CMSModules/PortalEngine/UI/Layout/PageTemplateSelector.aspx") + "?selectorid=' + selectorId + '&selectedPageTemplateId=' + document.getElementById(selectorId + '_hdnTemplateCode').value, 'PageTemplateSelection', '90%', '85%'); return false;} \n" +
            "function OnSelectPageTemplate(templateID, templateName, selectorId) { if (document.getElementById(selectorId + '_txtTemplate') != null) document.getElementById(selectorId + '_txtTemplate').value = templateName; if (document.getElementById(selectorId + '_hdnTemplateCode') != null) document.getElementById(selectorId + '_hdnTemplateCode').value = templateID; if ((templateName != null)&&(templateName != '')) setTimeout('BTN_Enable(\"' + selectorId + '_btnClear\");', 0);} \n" +
            "function ClearTemplate(selectorId) { document.getElementById(selectorId + '_txtTemplate').value = ''; document.getElementById(selectorId + '_hdnTemplateCode').value = 0; return false;} \n"
            ));

        btnSelect.OnClientClick = "return OpenSelectTemplateDialog('" + this.ClientID + "');";
        btnClear.Attributes.Add("onclick", "return ClearTemplate('" + this.ClientID + "');");

        btnSelect.Text = GetString("general.select");
        btnClear.Text = GetString("general.clear");
    }


    protected override void OnPreRender(EventArgs e)
    {
        LoadData();
        base.OnPreRender(e);
    }


    /// <summary>
    /// Loads the control data.
    /// </summary>
    public void LoadData()
    {
        if (pti != null) 
        {
            this.hdnTemplateCode.Value = pti.PageTemplateId.ToString();
            this.txtTemplate.Text = pti.DisplayName;
        }
        else
        {
            this.txtTemplate.Text = "";
            this.hdnTemplateCode.Value = "";
        }
    }
}

