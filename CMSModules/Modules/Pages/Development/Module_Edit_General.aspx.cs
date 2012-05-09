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
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_Modules_Pages_Development_Module_Edit_General : SiteManagerPage
{
    protected int moduleId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        lbModuleDisplayName.Text = GetString("Administration-Module_New.ModuleDisplayName");
        lbModuleCodeName.Text = GetString("Administration-Module_New.ModuleCodeName");
        lblModuleDescription.Text = GetString("Administration-Module_New.ModuleDescription");
        lblResourceUrl.Text = GetString("Administration-Module_New.ModuleResourceUrl");
        lblShowIn.Text = GetString("adm.module.showindevelopment");
        btnOk.Text = GetString("general.ok");
        rfvModuleDisplayName.ErrorMessage = GetString("Administration-Module_New.ErrorEmptyModuleDisplayName");
        rfvModuleCodeName.ErrorMessage = GetString("Administration-Module_New.ErrorEmptyModuleCodeName");

        // Register script
        string script =
            "function ShowHideUrl(checked) { \n" +
            "  var pnl = document.getElementById('" + this.pnlResourceUrl.ClientID + "'); \n" +
            "  if (pnl != null) { \n" +
            "    pnl.style.display = (checked ? 'block' : 'none'); \n" +
            "  } \n" +
            "} \n";
        ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(string), "ShowHideResourceUrl", ScriptHelper.GetScript(script));

        this.chkShowInDevelopment.Attributes.Add("onclick", "ShowHideUrl(this.checked);");

        moduleId = QueryHelper.GetInteger("moduleID", 0);
        if (moduleId > 0)
        {
            if (!RequestHelper.IsPostBack())
            {
                LoadData();
            }
        }

        if (this.chkShowInDevelopment.Checked)
        {
            this.pnlResourceUrl.Style.Add("display", "block");
        }
        else
        {
            this.pnlResourceUrl.Style.Add("display", "none");
        }
    }


    /// <summary>
    /// Handles btnOK's OnClick event - Update resource info.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // finds whether required fields are not empty
        string result = new Validator().NotEmpty(tbModuleDisplayName.Text.Trim(), GetString("Administration-Module_New.ErrorEmptyModuleDisplayName")).NotEmpty(tbModuleCodeName.Text, GetString("Administration-Module_New.ErrorEmptyModuleCodeName"))
            .IsCodeName(tbModuleCodeName.Text, GetString("general.invalidcodename"))
            .Result;

        if (this.chkShowInDevelopment.Checked && String.IsNullOrEmpty(this.txtResourceUrl.Text.Trim()))
        {
            result = GetString("module_edit.emptyurl");
        }

        if (result == "")
        {
            // Check unique name
            ResourceInfo ri = ResourceInfoProvider.GetResourceInfo(tbModuleCodeName.Text);
            if ((ri == null) || (ri.ResourceId == moduleId))
            {
                // Get object
                if (ri == null)
                {
                    ri = ResourceInfoProvider.GetResourceInfo(moduleId);
                    if (ri == null)
                    {
                        ri = new ResourceInfo();
                    }
                }

                //Update resource info
                ri.ResourceId = moduleId;
                ri.ResourceName = tbModuleCodeName.Text;
                ri.ResourceDescription = txtModuleDescription.Text.Trim();
                ri.ResourceDisplayName = tbModuleDisplayName.Text.Trim();

                ri.ShowInDevelopment = chkShowInDevelopment.Checked;
                ri.ResourceUrl = (ri.ShowInDevelopment ? txtResourceUrl.Text : "");
                pnlResourceUrl.Style.Add("display", (ri.ShowInDevelopment ? "block" : "none"));

                ResourceInfoProvider.SetResourceInfo(ri);

                // Update root UIElementInfo of the module
                UIElementInfo elemInfo = UIElementInfoProvider.GetRootUIElementInfo(ri.ResourceId);
                if (elemInfo == null)
                {
                    elemInfo = new UIElementInfo();
                }
                elemInfo.ElementResourceID = ri.ResourceId;
                elemInfo.ElementDisplayName = ri.ResourceDisplayName;
                elemInfo.ElementName = ri.ResourceName.ToLower().Replace(".", "");
                elemInfo.ElementIsCustom = false;
                UIElementInfoProvider.SetUIElementInfo(elemInfo);

                lblInfo.Visible = true;
                lblInfo.Text = GetString("General.ChangesSaved");
            }
            else
            {
                lblInfo.Visible = false;
                lblError.Visible = true;
                lblError.Text = GetString("Administration-Module_New.UniqueCodeName");
            }
        }
        else
        {
            lblInfo.Visible = false;
            lblError.Visible = true;
            lblError.Text = result;
        }
    }


    /// <summary>
    /// Loads data of edited module from DB into TextBoxes.
    /// </summary>
    protected void LoadData()
    {
        ResourceInfo ri = ResourceInfoProvider.GetResourceInfo(moduleId);

        if (ri != null)
        {
            tbModuleCodeName.Text = ri.ResourceName;
            tbModuleDisplayName.Text = ri.ResourceDisplayName;
            txtModuleDescription.Text = ri.ResourceDescription;
            chkShowInDevelopment.Checked = ri.ShowInDevelopment;
            txtResourceUrl.Text = (ri.ShowInDevelopment ? ri.ResourceUrl : "");

            pnlResourceUrl.Style.Add("display", (ri.ShowInDevelopment ? "block" : "none"));
        }
    }
}
