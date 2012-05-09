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

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Notifications;
using CMS.UIControls;

public partial class CMSModules_Notifications_Development_Gateways_Gateway_Edit : CMSNotificationsPage
{
    protected int gatewayId = 0;   

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get query strings
        if (gatewayId == 0)
        {
            gatewayId = ValidationHelper.GetInteger(Request.QueryString["gatewayid"], 0);
        }

        if (QueryHelper.GetInteger("saved", 0) == 1)
        {
            lblInfo.Visible = true;
            lblInfo.Text = GetString("general.changessaved");
        }        

        // initializes breadcrumbs 		
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("notifications.gateways");
        pageTitleTabs[0, 1] = "~/CMSModules/Notifications/Development/Gateways/Gateway_List.aspx";
        pageTitleTabs[0, 2] = "";
        pageTitleTabs[1, 0] = GetString("notifications.gateway.newitem");
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";

        // Get resource strings
        btnOk.Text = GetString("general.ok");
        lblSettings.Text = GetString("notifications.gateway.settings");

        valDisplayName.ErrorMessage = GetString("notifications.gateway.requiresdisplayname");
        valClassName.ErrorMessage = GetString("notifications.gateway.requiresclassname");
        valCodeName.ErrorMessage = GetString("general.requirescodename");
        valAssemblyName.ErrorMessage = GetString("notifications.gateway.requiresassemblyname");        

        if (!RequestHelper.IsPostBack())
        {
            // Edit
            if (gatewayId > 0)
            {
                NotificationGatewayInfo ngi = NotificationGatewayInfoProvider.GetNotificationGatewayInfo(gatewayId);
                EditedObject = ngi;

                if (ngi != null)
                {
                    txtAssemblyName.Text = ngi.GatewayAssemblyName;
                    txtClassName.Text = ngi.GatewayClassName;
                    txtCodeName.Text = ngi.GatewayName;
                    txtDescription.Text = ngi.GatewayDescription;
                    txtDisplayName.Text = ngi.GatewayDisplayName;

                    chkEnabled.Checked = ngi.GatewayEnabled;
                    chkSupportsHTML.Checked = ngi.GatewaySupportsHTMLText;
                    chkSupportsSubject.Checked = ngi.GatewaySupportsEmail;
                    chkSupportsPlain.Checked = ngi.GatewaySupportsPlainText;

                    pageTitleTabs[1, 0] = ngi.GatewayDisplayName;
                }
            }
        }

        // Set up page title control
        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
    }


    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Load or create new NotificationGatewayInfo
        NotificationGatewayInfo ngi = null;

        if (gatewayId > 0)
        {
            ngi = NotificationGatewayInfoProvider.GetNotificationGatewayInfo(gatewayId);
            EditedObject = ngi;
        }
        else
        {
            ngi = new NotificationGatewayInfo();
            ngi.GatewayGUID = Guid.NewGuid();
        }

        // Check code name
        if ((ngi.GatewayName != txtCodeName.Text))
        {
            NotificationGatewayInfo testNgi = NotificationGatewayInfoProvider.GetNotificationGatewayInfo(txtCodeName.Text);
            if ((testNgi != null) && (testNgi.GatewayID != ngi.GatewayID))
            {
                lblError.Text = GetString("general.uniquecodenameerror");
                lblError.Visible = true;
                return;
            }
        }

        string result = ValidateForm();

        if (result != "")
        {
            lblError.Visible = true;
            lblError.Text = result;
            return;
        }

        // Update values
        ngi.GatewayAssemblyName = txtAssemblyName.Text.Trim();
        ngi.GatewayClassName = txtClassName.Text.Trim();
        ngi.GatewayDescription = txtDescription.Text.Trim();
        ngi.GatewayDisplayName = txtDisplayName.Text.Trim();
        ngi.GatewayName = txtCodeName.Text.Trim();       
        ngi.GatewaySupportsEmail = chkSupportsSubject.Checked;
        ngi.GatewaySupportsHTMLText = chkSupportsHTML.Checked;
        ngi.GatewaySupportsPlainText = chkSupportsPlain.Checked;
        ngi.GatewayEnabled = chkEnabled.Checked;

        NotificationGatewayInfoProvider.SetNotificationGatewayInfo(ngi);

        gatewayId = ngi.GatewayID;
        URLHelper.Redirect("Gateway_Edit.aspx?saved=1&gatewayid=" + gatewayId.ToString());
    }


    private string ValidateForm()
    {
        // Validate codename        
        string result = new Validator().IsCodeName(txtCodeName.Text.Trim(), GetString("general.invalidcodename")).Result;
        if (result != "")
        {
            return result;
        }

        // Validate assembly name
        result = new Validator().IsCodeName(txtAssemblyName.Text.Trim(), GetString("general.invalidassemblyname")).Result;
        if (result != "")
        {
            return result;
        }

        // Validate classname
        result = new Validator().IsCodeName(txtClassName.Text.Trim(), GetString("general.invalidclassname")).Result;        
        return result;
    }
}
