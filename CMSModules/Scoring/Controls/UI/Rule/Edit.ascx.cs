using System;
using System.Text;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.FormControls;
using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.OnlineMarketing;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.WebAnalytics;
using CMS.SettingsProvider;
using System.Collections.Generic;
using System.Collections;

public partial class CMSModules_Scoring_Controls_UI_Rule_Edit : CMSAdminEditControl
{

    #region "Variables"

    private int scoreId = 0;
    private FormFieldInfo[] fields;
    private FormFieldInfo selectedField;
    private RuleInfo rule = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Flag indicates whether atribute rule is selected.
    /// </summary>
    private bool AttributeRuleSelected
    {
        get
        {
            return (ValidationHelper.GetInteger(radType.SelectedValue, 1) == (int)RuleTypeEnum.Attribute);
        }
    }


    /// <summary>
    /// Gets or sets value indicating what item was selected in activity type drop-down list.
    /// </summary>
    private string PreviousActivityType
    {
        get
        {
            return ValidationHelper.GetString(ViewState["PreviousActivityType"], PredefinedActivityType.ABUSE_REPORT );
        }
        set
        {
            ViewState["PreviousActivityType"] = value;
        }
    }


    /// <summary>
    /// Gets or sets value indicating what item was selected in field type drop-down list.
    /// </summary>
    private int PreviousField
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["PreviousFilterControl"], 0);
        }
        set
        {
            ViewState["PreviousFilterControl"] = value;
        }
    }


    /// <summary>
    /// UIForm control used for editing objects properties.
    /// </summary>
    public UIForm UIFormControl
    {
        get
        {
            return this.EditForm;
        }
    }

    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        scoreId = QueryHelper.GetInteger("scoreid", 0);
        rule = RuleInfoProvider.GetRuleInfo(QueryHelper.GetInteger("ruleid", 0));
        EditForm.OnBeforeSave += new EventHandler(EditForm_OnBeforeSave);
        EditForm.RedirectUrlAfterSave = "Tab_Rules_Edit.aspx?ruleid={%EditedObject.ID%}&scoreid=" + scoreId + "&saved=1";
        InitHeaderActions();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        InitControls();
        if (AttributeRuleSelected)
        {
            InitAttributeRuleControls(PreviousField);
        }
        else
        {
            // Initialize basic form using previously selected activity type => view state will be loaded correctly
            InitActivityRuleControls(PreviousActivityType);
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        plcActivity.Visible = !AttributeRuleSelected;
        plcActivitySettings.Visible = !AttributeRuleSelected;
        plcAttribute.Visible = AttributeRuleSelected;

        if (AttributeRuleSelected)
        {
            if (formCondition.FieldLabels != null)
            {
                LocalizedLabel ll = (LocalizedLabel)formCondition.FieldLabels[selectedField.Name];
                if (ll != null)
                {
                    ll.ResourceString = "om.score.condition";
                }
            }
        }
    }

    #endregion


    #region "Control events events"

    /// <summary>
    /// UIForm OnBeforeSave event handler.
    /// </summary>
    protected void EditForm_OnBeforeSave(object sender, EventArgs e)
    {
        // Store parent score ID
        EditForm.Data["RuleScoreID"] = scoreId;

        // Store site ID
        if (EditForm.EditedObject.Generalized.ObjectID == 0)
        {
            EditForm.Data["RuleSiteID"] = CMSContext.CurrentSiteID;
        }

        // Store attribute or activity rule
        EditForm.Data["RuleType"] = ValidationHelper.GetInteger(radType.SelectedValue, 0);

        // For activity rule store validity
        if (!AttributeRuleSelected)
        {
            EditForm.Data["RuleValidity"] = validity.Validity;
            if (validity.Validity == ValidityEnum.Until)
            {
                if (validity.ValidUntil != DateTimeHelper.ZERO_TIME)
                {
                    EditForm.Data["RuleValidUntil"] = validity.ValidUntil;
                }
                else
                {
                    EditForm.Data["RuleValidUntil"] = null;
                }
            }
            else
            {
                EditForm.Data["RuleValidFor"] = validity.ValidFor;
                EditForm.Data["RuleValidUntil"] = null;
            }

            // Store contact column for attribute rule
            EditForm.Data["RuleParameter"] = ucActivityType.SelectedValue;

            // Store xml with where condition
            activityFormCondition.SaveData(null);
            string whereCond = "ActivityType='" + SqlHelperClass.GetSafeQueryString(ucActivityType.SelectedValue) + "'";
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, activityFormCondition.GetWhereCondition());
            EditForm.Data["RuleCondition"] = RuleHelper.GetConditionFromData(activityFormCondition.Data, whereCond, RuleTypeEnum.Activity, ucActivityType.SelectedValue);
        }
        // For attribute rule don't store validity
        else
        {
            EditForm.Data["RuleValidity"] = null;
            EditForm.Data["RuleValidUntil"] = null;
            EditForm.Data["RuleValidFor"] = null;

            // Store contact column for attribute rule
            EditForm.Data["RuleParameter"] = selectedField.Name;

            // Store xml with where condition
            formCondition.SaveData(null);
            EditForm.Data["RuleCondition"] = RuleHelper.GetConditionFromData(formCondition.Data, formCondition.GetWhereCondition(), RuleTypeEnum.Attribute, null);
        }

        ScoreInfo score = ScoreInfoProvider.GetScoreInfo(scoreId);
        if (score != null)
        {
            score.ScoreStatus = ScoreStatusEnum.New;
            ScoreInfoProvider.SetScoreInfo(score);
        }
    }


    /// <summary>
    /// Selected field changed
    /// </summary>
    protected void drpAttribute_SelectedIndexChanged(object sender, EventArgs e)
    {
        PreviousField = drpAttribute.SelectedIndex;
        selectedField = fields[PreviousField];
        FormInfo fi = new FormInfo(null);
        fi.AddFormField(selectedField);
        LoadForm(formCondition, fi, null);
    }


    /// <summary>
    /// Selected activity type changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void ucActivityType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        // Init rule condition when changing activity type
        if (rule != null)
        {
            rule.RuleCondition = null;
        }
        // Initialize form using new (current) activity type
        PreviousActivityType = ucActivityType.SelectedValue;
        InitActivityRuleControls(PreviousActivityType);
    }


    #endregion


    #region "General methods"

    /// <summary>
    /// Initializes controls (and preselects rule type).
    /// </summary>
    private void InitControls()
    {
        if (!RequestHelper.IsPostBack())
        {
            radType.Items.Add(new ListItem(GetString("om.score.attribute"), ((int)RuleTypeEnum.Attribute).ToString()));
            radType.Items.Add(new ListItem(GetString("om.score.activity"), ((int)RuleTypeEnum.Activity).ToString()));

            RuleTypeEnum rt = RuleTypeEnum.Attribute;
            if (EditForm.EditedObject != null)
            {
                // Initialize attribute type from edited object (if any)
                RuleInfo ri = (RuleInfo)EditForm.EditedObject;
                rt = ri.RuleType;

                // Init rule validity controls
                if (EditForm.Data["RuleValidity"] != null)
                {
                    string strValidity = ValidationHelper.GetString(EditForm.Data["RuleValidity"], null);
                    validity.Validity = (ValidityEnum)Enum.Parse(typeof(ValidityEnum), strValidity);
                    if (validity.Validity == ValidityEnum.Until)
                    {
                        validity.ValidUntil = ValidationHelper.GetDateTime(EditForm.Data["RuleValidUntil"], DateTimeHelper.ZERO_TIME);
                    }
                    else
                    {
                        validity.ValidFor = ValidationHelper.GetInteger(EditForm.Data["RuleValidFor"], 0);
                    }
                }
            }

            radType.Items[0].Selected = radType.Items[1].Selected = false;
            for (int i = radType.Items.Count - 1; i >= 0; i--)
            {
                bool selected = ValidationHelper.GetInteger(radType.Items[i].Value, 0) == (int)rt;
                radType.Items[i].Selected = selected;
                if (selected)
                {
                    radType.SelectedValue = Convert.ToString((int)rt);
                }
            }
        }
        txtMaxPoints.Enabled = ValidationHelper.GetBoolean(chkRecurring.Value, false);
        drpAttribute.SelectedIndexChanged += new EventHandler(drpAttribute_SelectedIndexChanged);
        pnlGeneral.GroupingText = GetString("general.general");
        pnlActivity.GroupingText = GetString("om.score.activityvalidity");
        pnlSettings.GroupingText = GetString("om.score.rulesettings");
    }


    /// <summary>
    /// Initializes controls for attributre rule.
    /// </summary>
    private void InitAttributeRuleControls(int selectedFieldIndex)
    {
        FormInfo filterFieldsForm = FormHelper.GetFormInfo(OnlineMarketingObjectType.CONTACT + ".ScoringAttributeRule", false);
        fields = filterFieldsForm.GetFields(true, false);

        // Sort alphabetically
        Array.Sort(fields, delegate(FormFieldInfo a, FormFieldInfo b) { return String.Compare(ResHelper.LocalizeString(a.Caption), ResHelper.LocalizeString(b.Caption)); });

        if (filterFieldsForm != null)
        {
            if ((fields != null) && (fields.Length > 0) && (drpAttribute.Items.Count <= 0))
            {
                for (int i = 0; i < fields.Length; i++)
                {
                    drpAttribute.Items.Add(new ListItem(fields[i].Caption, fields[i].Name));
                }
            }
        }

        if ((EditForm.EditedObject != null) && !RequestHelper.IsPostBack())
        {
            drpAttribute.SelectedValue = ValidationHelper.GetString(EditForm.Data["RuleParameter"], null);
            selectedFieldIndex = drpAttribute.SelectedIndex;
            PreviousField = selectedFieldIndex;
        }

        selectedField = fields[selectedFieldIndex];
        FormInfo fi = new FormInfo(null);
        fi.AddFormField(selectedField);

        LoadForm(formCondition, fi, null);
    }


    /// <summary>
    /// Initializes controls for activity rule.
    /// </summary>
    private void InitActivityRuleControls(string selectedActivityType)
    {
        ucActivityType.OnSelectedIndexChanged += new EventHandler(ucActivityType_OnSelectedIndexChanged);

        // Init activity selector from  edited object if any
        string activityType = selectedActivityType;
        if ((EditForm.EditedObject != null) && !RequestHelper.IsPostBack())
        {
            ucActivityType.Value = ValidationHelper.GetString(EditForm.Data["RuleParameter"], PredefinedActivityType.ABUSE_REPORT);
            activityType = ucActivityType.SelectedValue;
            PreviousActivityType = activityType;
        }

        // List of ignored columns
        string ignoredColumns = "|activitytype|activitysiteid|activityguid|activityactivecontactid|activityoriginalcontactid|pagevisitid|pagevisitactivityid|searchid|searchactivityid|";

        // List of activities with "ActivityValue"
        StringBuilder sb = new StringBuilder();
        sb.Append("|"); sb.Append(PredefinedActivityType.PURCHASE);
        sb.Append("|"); sb.Append(PredefinedActivityType.PURCHASEDPRODUCT);
        sb.Append("|"); sb.Append(PredefinedActivityType.RATING);
        sb.Append("|"); sb.Append(PredefinedActivityType.POLL_VOTING);
        sb.Append("|"); sb.Append(PredefinedActivityType.PRODUCT_ADDED_TO_SHOPPINGCART);
        sb.Append("|");
        string showActivityValueFor = sb.ToString();

        // Get columns from OM_Activity (i.e. base table for all activities)
        ActivityTypeInfo ati = ActivityTypeInfoProvider.GetActivityTypeInfo(activityType);

        FormInfo fi = new FormInfo(null);

        // Get columns from additional table (if any) according to selected activity type (page visit, search)
        FormInfo additionalFieldsForm = null;
        bool extraFieldsAtEnd = true;

        switch (activityType)
        {
            case PredefinedActivityType.PAGE_VISIT:
            case PredefinedActivityType.LANDING_PAGE:
                // Page visits
                additionalFieldsForm = FormHelper.GetFormInfo(OnlineMarketingObjectType.PAGEVISIT, false);
                break;

            case PredefinedActivityType.INTERNAL_SEARCH:
            case PredefinedActivityType.EXTERNAL_SEARCH:
                // Search
                additionalFieldsForm = FormHelper.GetFormInfo(OnlineMarketingObjectType.SEARCH, false);
                extraFieldsAtEnd = false;
                break;
        }

        // Get the activity form elements
        FormInfo filterFieldsForm = FormHelper.GetFormInfo(OnlineMarketingObjectType.ACTIVITY, true);
        ArrayList elements = filterFieldsForm.GetFormElements(true, false);
        
        FormCategoryInfo newCategory = null;
        
        string caption = null;
        string captionKey = null;

        foreach (var elem in elements)
        {
            if (elem is FormCategoryInfo)
            {
                // Form category
                newCategory = (FormCategoryInfo)elem;
            }
            else if (elem is FormFieldInfo)
            {
                // Form field
                FormFieldInfo ffi = (FormFieldInfo)elem;

                // Skip ignored columns
                if (ignoredColumns.IndexOf("|" + ffi.Name.ToLower() + "|") >= 0)
                {
                    continue;
                }

                string controlName = null;
                if (!ffi.PrimaryKey && (fi.GetFormField(ffi.Name) == null))
                {
                    // Set default filters
                    switch (ffi.DataType)
                    {
                        case FormFieldDataTypeEnum.Text:
                        case FormFieldDataTypeEnum.LongText:
                            controlName = "textfilter";
                            ffi.Settings["OperatorFieldName"] = ffi.Name + ".operator";
                            break;

                        case FormFieldDataTypeEnum.DateTime:
                            controlName = "datetimefilter";
                            ffi.Settings["SecondDateFieldName"] = ffi.Name + ".seconddatetime";
                            break;

                        case FormFieldDataTypeEnum.Integer:
                        case FormFieldDataTypeEnum.LongInteger:
                            controlName = "numberfilter";
                            ffi.Settings["OperatorFieldName"] = ffi.Name + ".operator";
                            break;

                        case FormFieldDataTypeEnum.GUID:
                            continue;
                    }

                    // For item ID and detail ID fields use control defined in activity type
                    if (String.Compare(ffi.Name, "ActivityItemID", true) == 0)
                    {
                        if (ati.ActivityTypeMainFormControl == null)
                        {
                            continue;
                        }

                        if (ati.ActivityTypeMainFormControl != String.Empty)
                        {
                            // Check if user defined control exists
                            FormUserControlInfo fui = FormUserControlInfoProvider.GetFormUserControlInfo(ati.ActivityTypeMainFormControl);
                            if (fui != null)
                            {
                                controlName = ati.ActivityTypeMainFormControl;
                            }
                        }

                        // Set detailed caption
                        captionKey = "activityitem." + activityType;
                        caption = GetString(captionKey);
                        if (!caption.Equals(captionKey, StringComparison.InvariantCultureIgnoreCase))
                        {
                            ffi.Caption = caption;
                        }
                    }
                    else if (String.Compare(ffi.Name, "ActivityItemDetailID", true) == 0)
                    {
                        if (ati.ActivityTypeDetailFormControl == null)
                        {
                            continue;
                        }

                        if (ati.ActivityTypeDetailFormControl != String.Empty)
                        {
                            // Check if user defined control exists
                            FormUserControlInfo fui = FormUserControlInfoProvider.GetFormUserControlInfo(ati.ActivityTypeDetailFormControl);
                            if (fui != null)
                            {
                                controlName = ati.ActivityTypeDetailFormControl;
                            }
                        }

                        // Set detailed caption
                        captionKey = "activityitemdetail." + activityType;
                        caption = GetString(captionKey);
                        if (!caption.Equals(captionKey, StringComparison.InvariantCultureIgnoreCase))
                        {
                            ffi.Caption = caption;
                        }
                    }
                    else if (String.Compare(ffi.Name, "ActivityNodeID", true) == 0)
                    {
                        // Document selector for NodeID
                        controlName = "selectdocument";
                    }
                    else if (String.Compare(ffi.Name, "ActivityCulture", true) == 0)
                    {
                        // Culture selector for culture
                        controlName = "sitecultureselector";
                    }
                    else if (String.Compare(ffi.Name, "ActivityValue", true) == 0)
                    {
                        // Show activity value only for relevant activity types
                        if (!ati.ActivityTypeIsCustom && (showActivityValueFor.IndexOf("|" + activityType + "|",  StringComparison.InvariantCultureIgnoreCase) < 0))
                        {
                            continue;
                        }
                    }

                    if (controlName != null)
                    {
                        // SKU selector for product
                        ffi.Settings["controlname"] = controlName;
                        if (String.Compare(controlName, "skuselector", StringComparison.InvariantCultureIgnoreCase) == 0)
                        {
                            ffi.Settings["allowempty"] = true;
                        }
                    }

                    // Ensure the category
                    if (newCategory != null)
                    {
                        fi.AddFormCategory(newCategory);

                        newCategory = null;

                        // // Extra fields at the beginning
                        if (!extraFieldsAtEnd && (additionalFieldsForm != null))
                        {
                            AddExtraFields(ignoredColumns, fi, additionalFieldsForm);

                            additionalFieldsForm = null;
                        }
                    }

                    fi.AddFormField(ffi);
                }
            }
        }

        // Extra fields at end
        if (extraFieldsAtEnd && (additionalFieldsForm != null))
        {
            // Ensure the category for extra fields
            if (newCategory != null)
            {
                fi.AddFormCategory(newCategory);

                newCategory = null;
            }

            AddExtraFields(ignoredColumns, fi, additionalFieldsForm);
        }

        LoadForm(activityFormCondition, fi, activityType);
    }


    /// <summary>
    /// Adds the extra activity fields to the form
    /// </summary>
    /// <param name="ignoredColumns">Ignored columns</param>
    /// <param name="fi">Form info with the result</param>
    /// <param name="additionalFieldsForm">Extra fields</param>
    private static void AddExtraFields(string ignoredColumns, FormInfo fi, FormInfo additionalFieldsForm)
    {
        if (additionalFieldsForm != null)
        {
            // Additional fields
            var formFields = additionalFieldsForm.GetFields(true, false);
            foreach (FormFieldInfo ffi in formFields)
            {
                if (ignoredColumns.IndexOf("|" + ffi.Name.ToLower() + "|") >= 0)
                {
                    continue;
                }
                ffi.Settings["controlname"] = "textfilter";
                fi.AddFormField(ffi);
            }
        }
    }


    /// <summary>
    /// Loads basicform with filter controls.
    /// </summary>
    /// <param name="bf">BasicForm control</param>
    /// <param name="fi">Form definition</param>
    /// <param name="activityType">Activity type in case the rule type is activity</param>
    private void LoadForm(BasicForm bf, FormInfo fi, string activityType)
    {
        bf.FormInformation = fi;
        bf.Data = RuleHelper.GetDataFromCondition((RuleInfo)rule, fi.GetDataRow().Table, ref activityType);
        bf.SubmitButton.Visible = false;
        bf.SiteName = CMSContext.CurrentSiteName;
        bf.FieldLabelCellCssClass = "ContactLabel";
        bf.FieldLabelCssClass = null;

        if (rule != null)
        {
            bf.Mode = FormModeEnum.Update;
        }

        bf.ReloadData();
    }


    /// <summary>
    /// Actions handler.
    /// </summary>
    protected void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            // Save contact
            case "save":
                if (ValidateForm())
                {
                    EditForm.SaveData(null);
                }
                break;
        }
    }


    /// <summary>
    /// Initializes header action control.
    /// </summary>
    private void InitHeaderActions()
    {
        string[,] actions;
        actions = new string[1, 11];

        // Initialize SAVE button
        actions[0, 0] = HeaderActions.TYPE_SAVEBUTTON;
        actions[0, 1] = GetString("General.Save");
        actions[0, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
        actions[0, 6] = "save";
        actions[0, 8] = "true";

        ((CMSPage)Page).CurrentMaster.HeaderActions.LinkCssClass = "ContentSaveLinkButton";
        ((CMSPage)Page).CurrentMaster.HeaderActions.ActionPerformed += HeaderActions_ActionPerformed;
        ((CMSPage)Page).CurrentMaster.HeaderActions.Actions = actions;
    }


    /// <summary>
    /// Performs custom validation and displays error in top of the page.
    /// </summary>
    /// <returns>Returns true if validation is successful.</returns>
    protected bool ValidateForm()
    {
        // Validate validity field
        string errorMessage = validity.Validate();
        if (!string.IsNullOrEmpty(errorMessage))
        {
            DisplayError(errorMessage);
            return false;
        }

        // Validate maximum value field
        if (ValidationHelper.GetBoolean(chkRecurring.Value, false))
        {
            string maxValueStr = txtMaxPoints.Text.Trim();
            if (!String.IsNullOrEmpty(maxValueStr))
            {
                if (!ValidationHelper.IsInteger(maxValueStr))
                {
                    DisplayError(GetString("om.score.enterintegervalue"));
                    return false;
                }

                // Check that maximum value is greater than value
                int value = ValidationHelper.GetInteger(txtValue.Text, 0);
                int maxValue = ValidationHelper.GetInteger(txtMaxPoints.Text, 0);
                if ((maxValue > 0) && (value > maxValue) || (maxValue < 0) && (value < maxValue))
                {
                    DisplayError(GetString("om.score.smallmaxvalue"));
                    return false;
                }
            }
        }

        // Check the attribute form
        if (!formCondition.ValidateData())
        {
            return false;
        }

        // Check the attribute form
        if (!activityFormCondition.ValidateData())
        {
            return false;
        }
        return true;
    }


    /// <summary>
    /// Displayes error message in header section of page.
    /// </summary>
    private void DisplayError(string errorMessage)
    {
        ((CMSPage)Page).ShowError(TextHelper.LimitLength(errorMessage, 200), errorMessage);
    }


    /// <summary>
    /// Displayes info label.
    /// </summary>
    /// <param name="infoMessage">Information message</param>
    private void DisplayInfo(string infoMessage)
    {
        ((CMSPage)Page).ShowInformation(infoMessage);
    }

    #endregion
}