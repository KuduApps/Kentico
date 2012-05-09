using System;

using CMS.UIControls;

public partial class CMSModules_Workflows_Workflow_Edit : SiteManagerPage
{
    protected string headerTargetUrl = "";
    protected string contentTargetUrl = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["showtab"]))
        {
            if (Request.QueryString["showtab"].ToLower() == "scopes")
            {
                contentTargetUrl = "Workflow_Scopes.aspx?";
            }
            contentTargetUrl = (Request.QueryString["showtab"].ToLower() == "steps") ? "Workflow_Steps.aspx?" : "Workflow_General.aspx?";
            headerTargetUrl = "Workflow_Header.aspx?showtab=" + Request.QueryString["showtab"].ToLower() + "&";
        }
        else
        {
            contentTargetUrl = "Workflow_General.aspx?";
            headerTargetUrl = "Workflow_Header.aspx?";
        }

        if (!string.IsNullOrEmpty(Request.QueryString["saved"]) && (Request.QueryString["saved"] != "0"))
        {
            contentTargetUrl += "saved=1&";
        }
        else
        {
            contentTargetUrl += "saved=0&";
        }

        if (!string.IsNullOrEmpty(Request.QueryString["workflowid"]))
        {
            contentTargetUrl += "workflowid=" + Request.QueryString["workflowid"].ToLower();
            headerTargetUrl += "workflowid=" + Request.QueryString["workflowid"].ToLower();
        }
    }
}
