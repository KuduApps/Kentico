using System;
using System.Data;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.CMSHelper;
using CMS.WorkflowEngine;
using CMS.TreeEngine;

public partial class CMSModules_Content_CMSDesk_Validation_ValidationResults : CMSPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!QueryHelper.ValidateHash("hash"))
        {
            RedirectToAccessDenied(ResHelper.GetString("dialogs.badhashtitle"));
        }

        // Set CSS classes
        CurrentMaster.PanelFooter.CssClass = "FloatRight";

        // Add close button
        CurrentMaster.PanelFooter.Controls.Add(new LocalizedButton
        {
            ID = "btnClose",
            ResourceString = "general.close",
            EnableViewState = false,
            OnClientClick = "window.top.close(); return false;",
            CssClass = "SubmitButton"
        });

    }


    protected void Page_Load(object sender, EventArgs e)
    {
        int documentId = QueryHelper.GetInteger("docid", 0);
        string titlePart = null;
        if (documentId > 0)
        {
            TreeNode doc = DocumentHelper.GetDocument(documentId, null);
            if (doc != null)
            {
                titlePart = HTMLHelper.HTMLEncode(DataHelper.GetNotEmpty(doc.DocumentName, doc.NodeAliasPath));
            }
        }

        SetTitle("Design/Controls/Validation/warning.png", String.Format(GetString("validation.validationdialogresults"), titlePart), null, null);

        string key = QueryHelper.GetString("datakey", null);
        if (!String.IsNullOrEmpty(key))
        {
            viewDataSet.AdditionalContent = "";
            DataSet ds = WindowHelper.GetItem(key) as DataSet;
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                viewDataSet.DataSet = ds;
            }
        }

        ScriptHelper.RegisterDialogScript(Page);
    }
}
