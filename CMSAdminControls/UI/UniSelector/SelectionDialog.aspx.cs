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

public partial class CMSAdminControls_UI_UniSelector_SelectionDialog : CMSModalPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Try to get parameters...
        string identificator = QueryHelper.GetString("params", null);
        Hashtable parameters = (Hashtable)WindowHelper.GetItem(identificator);

        // ... and validate hash
        if ((QueryHelper.ValidateHash("hash", "selectedvalue")) && (parameters != null))
        {
            selectionDialog.LocalizeItems = QueryHelper.GetBoolean("localize", true);
            // Load resource prefix
            string resourcePrefix = ValidationHelper.GetString(parameters["ResourcePrefix"], "general");

            // Set the page title
            string titleText = GetString(resourcePrefix + ".selectitem|general.selectitem");

            CurrentMaster.Title.TitleText = titleText;
            Page.Title = titleText;

            string imgPath = ValidationHelper.GetString(parameters["IconPath"], null);
            if (String.IsNullOrEmpty(imgPath))
            {
                string objectType = ValidationHelper.GetString(parameters["ObjectType"], null);

                CurrentMaster.Title.TitleImage = GetObjectIconUrl(objectType, null);
            }
            else
            {
                CurrentMaster.Title.TitleImage = imgPath;
            }

            // Cancel button
            btnCancel.ResourceString = "general.cancel";
            btnCancel.Attributes.Add("onclick", "return US_Cancel();");

            // Ok button
            btnOk.ResourceString = "general.ok";
            btnOk.Attributes.Add("onclick", "return US_Submit();");

            SelectionModeEnum selectionMode = (SelectionModeEnum)parameters["SelectionMode"];

            // Show the OK button if needed
            switch (selectionMode)
            {
                case SelectionModeEnum.Multiple:
                case SelectionModeEnum.MultipleTextBox:
                case SelectionModeEnum.MultipleButton:
                    {
                        btnOk.Visible = true;
                    }
                    break;
            }
        }
        else
        {
            // Redirect to error page
            URLHelper.Redirect(ResolveUrl("~/CMSMessages/Error.aspx?title=" + ResHelper.GetString("dialogs.badhashtitle") + "&text=" + ResHelper.GetString("dialogs.badhashtext")));
        }
    }
}