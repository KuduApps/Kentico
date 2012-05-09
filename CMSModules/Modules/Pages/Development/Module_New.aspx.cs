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

public partial class CMSModules_Modules_Pages_Development_Module_New : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // initialization of controls
        lbModuleDisplayName.Text = GetString("Administration-Module_New.ModuleDisplayName");
        lbModuleCodeName.Text = GetString("Administration-Module_New.ModuleCodeName");
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

        // initialization of the title
        string modules = GetString("Administration-Module_New.Modules");
        string currentModule = GetString("Administration-Module_New.CurrentModule");
        string title = GetString("Administration-Module_New.Title");

        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = modules;
        pageTitleTabs[0, 1] = "~/CMSModules/Modules/Pages/Development/Module_List.aspx";
        pageTitleTabs[0, 2] = "";
        pageTitleTabs[1, 0] = currentModule;
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";

        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
        this.CurrentMaster.Title.TitleText = title;
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Module/new.png");
        this.CurrentMaster.Title.HelpTopicName = "new_modulegenral_tab";
        this.CurrentMaster.Title.HelpName = "helpTopic";

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
    /// Handles btnOK's OnClick event - Save resource info.
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
            // finds if the resource code name is unique
            if (ResourceInfoProvider.GetResourceInfo(tbModuleCodeName.Text) == null)
            {
                //Save resource info
                ResourceInfo ri = new ResourceInfo();
                ri.ResourceName = tbModuleCodeName.Text;
                ri.ResourceDisplayName = tbModuleDisplayName.Text.Trim();
                ri.ShowInDevelopment = chkShowInDevelopment.Checked;
                ri.ResourceUrl = (ri.ShowInDevelopment ? txtResourceUrl.Text : "");

                ResourceInfoProvider.SetResourceInfo(ri);

                URLHelper.Redirect("Module_Edit_Frameset.aspx?moduleID=" + ri.ResourceId + "&saved=1");
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("Administration-Module_New.UniqueCodeName");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = result;
        }
    }
}
