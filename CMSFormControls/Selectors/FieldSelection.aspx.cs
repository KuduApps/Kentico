using System;

using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSFormControls_Selectors_FieldSelection : DesignerPage
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check parameters
        if (!QueryHelper.ValidateHash("hash"))
        {
            ShowError("Validation of query hash failed.");
            pnlUpdate.Visible = false;
            btnOk.Visible = false;
        }
        else
        {
            // Register script for pendingCallbacks repair
            ScriptHelper.FixPendingCallbacks(Page);

            string parentClientId = QueryHelper.GetString("cid", string.Empty);

            ScriptHelper.RegisterStartupScript(this, typeof(string), "selectField", ScriptHelper.GetScript(
    @"
function SelectFields(drpId) {
    var drpElem = document.getElementById(drpId);
    var value = drpElem.options[drpElem.selectedIndex].value;
    var text = drpElem.options[drpElem.selectedIndex].text;
    if ((wopener != null) && (wopener.SetValue_" + parentClientId + @" != null)) {
        wopener.SetValue_" + parentClientId + @"(value, text);
    }
    window.close();
}"));

            // Setup uniselector
            selectionElem.UniSelector.SelectionMode = SelectionModeEnum.SingleDropDownList;
            selectionElem.DisplayClearButton = false;
            selectionElem.DropDownSingleSelect.AutoPostBack = true;
            selectionElem.UniSelector.OnSelectionChanged += UniSelector_OnSelectionChanged;
            selectionElem.IsLiveSite = false;
        }

        // Initialize master page
        CurrentMaster.Title.TitleText = GetString("attach.documenttypefield");
        CurrentMaster.Title.TitleImage = GetImageUrl("Design/Selectors/fieldselection.png");
        // Initialize buttons' functions
        btnOk.OnClientClick = "SelectFields('" + drpField.ClientID + "');  return false;";
        btnCancel.OnClientClick = "window.close();  return false;";

        // Bind fields dropdown list
        if (!RequestHelper.IsPostBack())
        {
            selectionElem.ReloadData(false);
            selectionElem.DropDownSingleSelect.SelectedIndex = 0;
            LoadFields();
        }
    }


    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        LoadFields();
    }

    #endregion


    #region "Private methods"

    private void LoadFields()
    {
        // Clear dropdown list
        drpField.Items.Clear();
        // Get dataclass info
        string className = ValidationHelper.GetString(selectionElem.Value, null);
        if (!string.IsNullOrEmpty(className))
        {
            DataClassInfo dci = DataClassInfoProvider.GetDataClass(className);

            // Get fields of type file
            FormInfo fi = FormHelper.GetFormInfo(dci.ClassName, false);
            FormFieldInfo[] ffi = fi.GetFields(FormFieldDataTypeEnum.DocumentAttachments);
            // Sort fields by caption
            Array.Sort(ffi, (ffi1, ffi2) => ffi1.Caption.CompareTo(ffi2.Caption));
            // Bind fields to dropdown list
            drpField.DataSource = ffi;
            drpField.DataTextField = "Caption";
            drpField.DataValueField = "Guid";
            drpField.DataBind();
            // Enable/disable controls
            bool fieldsFound = (drpField.Items.Count != 0);
            btnOk.Enabled = fieldsFound;
            drpField.Enabled = fieldsFound;
            UpdatePanels();
        }
        else
        {
            drpField.Enabled = false;
        }
    }


    private void UpdatePanels()
    {
        pnlUpdate.Update();
        pnlUpdateButtons.Update();
    }

    #endregion
}
