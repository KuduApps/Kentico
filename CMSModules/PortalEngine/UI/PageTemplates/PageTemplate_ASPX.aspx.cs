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
using System.Text.RegularExpressions;
using System.Text;

using CMS.GlobalHelper;
using CMS.PortalEngine;
using CMS.PortalControls;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.EventLog;
using CMS.IO;

public partial class CMSModules_PortalEngine_UI_PageTemplates_PageTemplate_ASPX : SiteManagerPage
{
    protected int templateId = 0;
    string fileName = "";
    private string selectedSite = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.CurrentMaster.DisplaySiteSelectorPanel = true;

        this.lblInfo.Visible = false;
        this.lblError.Visible = false;

        // Get page template id from url
        templateId = ValidationHelper.GetInteger(Request.QueryString["templateid"], 0);
        PageTemplateInfo pti = PageTemplateInfoProvider.GetPageTemplateInfo(templateId);

        string templateName = this.txtName.Text.Trim();
        if (templateName == "")
        {
            if (radMaster.Checked)
            {
                templateName = "MainMenu";
            }
            else
            {
                templateName = "Template";
            }

            if (pti != null)
            {
                 templateName = ValidationHelper.GetIdentifier(pti.DisplayName);
            }

            this.txtName.Text = templateName;
        }

        // Set site selector        
        siteSelector.AllowAll = false;
        siteSelector.UseCodeNameForSelection = true;


        if (!RequestHelper.IsPostBack())
        {
            selectedSite = CMSContext.CurrentSiteName;
            siteSelector.Value = selectedSite;
        }
        else
        {
            selectedSite = ValidationHelper.GetString(siteSelector.Value, String.Empty);
        }

        string className = "CMSTemplates_" + selectedSite + "_" + templateName;
        fileName = templateName;

        lblCodeInfo.Text = GetString("pagetemplate_aspx.info");
        lblCodeBehindInfo.Text = GetString("pagetemplate_aspx.codebehindinfo");

        lblName.Text = GetString("pagetemplate_aspx.name");
        lblMaster.Text = GetString("pagetemplate_aspx.mastername");
        btnSave.Text = GetString("pagetemplate_aspx.save");
        btnRefresh.Text = GetString("pagetemplate_aspx.refresh");

        radSlave.Text = GetString("pagetemplate_aspx.slave");
        radMaster.Text = GetString("pagetemplate_aspx.master");
        radTemplate.Text = GetString("pagetemplate_aspx.template");
        radTemplateOnly.Text = GetString("pagetemplate_aspx.templateonly");

        this.plcMasterTemplate.Visible = this.radSlave.Checked;

        if (pti != null)
        {
            string codeBehind = "";
            string registerCode = "";
            string code = PortalHelper.GetPageTemplateASPXCode(pti, out registerCode);

            if (this.radTemplateOnly.Checked)
            {
                code = registerCode + code;
            }
            else if (this.radSlave.Checked)
            {
                fileName += ".aspx";

                codeBehind = File.ReadAllText(Server.MapPath("~/CMSModules/PortalEngine/UI/PageTemplates/ASPX/ChildTemplate.aspx.cs"));
                codeBehind = codeBehind.Replace("PageTemplates_ChildTemplate", className);

                string pageCode = File.ReadAllText(Server.MapPath("~/CMSModules/PortalEngine/UI/PageTemplates/ASPX/ChildTemplate.aspx"));
                pageCode = pageCode.Replace(" Inherits=\"PageTemplates_ChildTemplate\"", " Inherits=\"" + className + "\"");
                pageCode = pageCode.Replace(" CodeFile=\"ChildTemplate.aspx.cs\"", " CodeFile=\"" + fileName + ".cs\"");

                string master = "";
                if (this.txtMaster.Text.Trim() != "")
                {
                    master = " MasterPageFile=\"" + this.txtMaster.Text.Trim() + ".master\"";
                }
                pageCode = pageCode.Replace(" MasterPageFile=\"Template.master\"", master);

                pageCode = pageCode.Replace("<%--REGISTER--%>", registerCode);

                pageCode = pageCode.Replace("<%--CONTENT--%>", code);

                code = pageCode;
            }
            else if (this.radMaster.Checked)
            {
                fileName += ".master";

                codeBehind = File.ReadAllText(Server.MapPath("~/CMSModules/PortalEngine/UI/PageTemplates/ASPX/Template.master.cs"));
                codeBehind = codeBehind.Replace("PageTemplates_MasterTemplate", className);

                string pageCode = File.ReadAllText(Server.MapPath("~/CMSModules/PortalEngine/UI/PageTemplates/ASPX/Template.master"));
                pageCode = pageCode.Replace(" Inherits=\"PageTemplates_MasterTemplate\"", " Inherits=\"" + className + "\"");
                pageCode = pageCode.Replace(" CodeFile=\"Template.master.cs\"", " CodeFile=\"" + fileName + ".cs\"");

                pageCode = pageCode.Replace("<%@ Register Assembly=\"CMS.Controls\" Namespace=\"CMS.Controls\" TagPrefix=\"cc1\" %>", "");
                pageCode = pageCode.Replace("<%--REGISTER--%>", registerCode);

                code = code.Replace("<%--CONTENT--%>", "<asp:ContentPlaceHolder ID=\"plcMain\" runat=\"server\"></asp:ContentPlaceHolder>");

                pageCode = pageCode.Replace("<%--CONTENT--%>", code);

                code = pageCode;
            }
            else if (this.radTemplate.Checked)
            {
                fileName += ".aspx";

                codeBehind = File.ReadAllText(Server.MapPath("~/CMSModules/PortalEngine/UI/PageTemplates/ASPX/Template.aspx.cs"));
                codeBehind = codeBehind.Replace("PageTemplates_Template", className);

                string pageCode = File.ReadAllText(Server.MapPath("~/CMSModules/PortalEngine/UI/PageTemplates/ASPX/Template.aspx"));
                pageCode = pageCode.Replace("Inherits=\"PageTemplates_Template\"", "Inherits=\"" + className + "\"");
                pageCode = pageCode.Replace(" CodeFile=\"Template.aspx.cs\"", " CodeFile=\"" + fileName + ".cs\"");

                pageCode = pageCode.Replace("<%@ Register Assembly=\"CMS.Controls\" Namespace=\"CMS.Controls\" TagPrefix=\"cc1\" %>", "");
                pageCode = pageCode.Replace("<%--REGISTER--%>", registerCode);

                pageCode = pageCode.Replace("<%--CONTENT--%>", code);

                code = pageCode;
            }

            this.txtCode.Text = HTMLHelper.ReformatHTML(code);
            this.txtCodeBehind.Text = codeBehind;
        }

        lblSaveInfo.Text = String.Format(GetString("pagetemplate_aspx.saveinfo"), "~/CMSTemplates/" + selectedSite + "/" + fileName);
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string path = "~/CMSTemplates/" + selectedSite + "/" + fileName;
            path = Server.MapPath(path);

            string directory = path.Substring(0, path.LastIndexOf('\\'));
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (this.txtCode.Text.Trim() != "")
            {
                File.WriteAllText(path, this.txtCode.Text);
            }

            if (this.txtCodeBehind.Text.Trim() != "")
            {
                File.WriteAllText(path + ".cs", this.txtCodeBehind.Text);
            }

            this.lblInfo.Text = String.Format(GetString("pagetemplate_aspx.saved"), path);
            this.lblInfo.Visible = true;
        }
        catch (Exception ex)
        {
            this.lblError.Text = ex.Message;
            this.lblError.ToolTip = EventLogProvider.GetExceptionLogMessage(ex);
            this.lblError.Visible = true;
        }
    }
}
