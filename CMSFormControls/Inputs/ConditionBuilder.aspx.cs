using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

using CMS.GlobalHelper;
using CMS.FormEngine;
using CMS.UIControls;

public partial class CMSFormControls_Inputs_ConditionBuilderDialog : DesignerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.RegisterESCScript = false;

        this.CurrentMaster.Title.TitleText = GetString("conditionbuilder.title");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_DocumentLibrary/Properties.png");

        this.btnInsert.Click += new EventHandler(btnInsert_Click);

        if (!RequestHelper.IsPostBack())
        {
            string condition = MacroResolver.RemoveDataMacroBrackets(QueryHelper.GetString("condition", ""));
            this.designerElem.Condition = condition;
        }
    }


    protected void btnInsert_Click(object sender, EventArgs e)
    {
        this.ltlScript.Text = ScriptHelper.GetScript("wopener.InsertMacroCondition(" + ScriptHelper.GetString(this.designerElem.Condition) + "); window.close();");
    }
}
