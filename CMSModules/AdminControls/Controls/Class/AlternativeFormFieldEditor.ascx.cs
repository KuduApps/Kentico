using System;

using CMS.FormEngine;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_AdminControls_Controls_Class_AlternativeFormFieldEditor : CMSUserControl
{
    #region "Events"

    public event EventHandler OnBeforeSave;


    public event EventHandler OnAfterSave;

    #endregion


    #region "Variables"

    protected int mAlternativeFormId = 0;
    protected FieldEditorControlsEnum mDisplayedControls = FieldEditorControlsEnum.None;

    #endregion


    #region "Properties"


    /// <summary>
    /// Indicates if system fields (node and document fields) are enabled.
    /// </summary>
    public bool EnableSystemFields
    {
        get
        {
            return fieldEditor.EnableSystemFields;
        }
        set
        {
            fieldEditor.EnableSystemFields = value;
        }
    }


    /// <summary>
    /// Indicates if field visibility selector should be displayed.
    /// </summary>
    public bool ShowFieldVisibility
    {
        get
        {
            return fieldEditor.ShowFieldVisibility;
        }
        set
        {
            fieldEditor.ShowFieldVisibility = value;
        }
    }


    /// <summary>
    /// Form id (with alterantive form definition).
    /// </summary>
    public int AlternativeFormID
    {
        get
        {
            return mAlternativeFormId;
        }
        set
        {
            mAlternativeFormId = value;
        }
    }


    /// <summary>
    /// Specify set of controls which should be offered for field types.
    /// </summary>
    public FieldEditorControlsEnum DisplayedControls
    {
        get
        {
            return mDisplayedControls;
        }
        set
        {
            mDisplayedControls = value;
        }
    }


    /// <summary>
    /// Field editor mode.
    /// </summary>
    public FieldEditorModeEnum Mode
    {
        get
        {
            return fieldEditor.Mode;
        }
        set
        {
            fieldEditor.Mode = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        AlternativeFormInfo afi = AlternativeFormInfoProvider.GetAlternativeFormInfo(mAlternativeFormId);
        CMSPage.EditedObject = afi;

        if (afi != null)
        {
            DataClassInfo dci = DataClassInfoProvider.GetDataClass(afi.FormClassID);
            if (dci != null)
            {
                string formDef = dci.ClassFormDefinition;
                string coupledClassName = null;
                if (afi.FormCoupledClassID > 0)
                {
                    // If coupled class is defined combine form definitions
                    DataClassInfo coupledDci = DataClassInfoProvider.GetDataClass(afi.FormCoupledClassID);
                    if (coupledDci != null)
                    {
                        formDef = FormHelper.MergeFormDefinitions(formDef, coupledDci.ClassFormDefinition, true);
                        coupledClassName = coupledDci.ClassName;
                    }
                }

                // Merge class and alternative form definitions
                formDef = FormHelper.MergeFormDefinitions(formDef, afi.FormDefinition);
                
                // Initialize field editor mode and load form definition
                fieldEditor.IsAlternativeForm = true;
                fieldEditor.AlternativeFormFullName = afi.FullName;
                fieldEditor.FormDefinition = formDef;
                // Specify set of controls which should be offered for field types
                fieldEditor.DisplayedControls = mDisplayedControls;
                fieldEditor.ClassName = dci.ClassName;
                fieldEditor.CoupledClassName = coupledClassName;
                // Handle definition update (move up, move down, delete, OK button)
                fieldEditor.OnAfterDefinitionUpdate += fieldEditor_OnAfterDefinitionUpdate;
            }
            else
            {
                ShowErrorMessage();
            }
        }
    }


    /// <summary>
    /// After form definition update event handler.
    /// </summary>
    void fieldEditor_OnAfterDefinitionUpdate(object sender, EventArgs e)
    {
        // Perform OnBeforeSave if defined
        if (OnBeforeSave != null)
        {
            OnBeforeSave(this, EventArgs.Empty);
        }

        // Stop processing if set from outside
        if (StopProcessing)
        {
            return;
        }

        // Get alternative form info object and data class info object
        AlternativeFormInfo afi = AlternativeFormInfoProvider.GetAlternativeFormInfo(mAlternativeFormId);

        if (afi != null)
        {
            DataClassInfo dci = DataClassInfoProvider.GetDataClass(afi.FormClassID);

            if (dci != null)
            {
                string formDefinition = dci.ClassFormDefinition;

                if (afi.FormCoupledClassID > 0)
                {
                    // Combine form definitions of class and its coupled class
                    DataClassInfo coupledDci = DataClassInfoProvider.GetDataClass(afi.FormCoupledClassID);
                    if (coupledDci != null)
                    {
                        formDefinition = FormHelper.MergeFormDefinitions(formDefinition, coupledDci.ClassFormDefinition, true);
                    }
                }

                // Compare original and alternative form definitions - store differences only
                afi.FormDefinition = FormHelper.GetFormDefinitionDifference(formDefinition, fieldEditor.FormDefinition);
                // Update alternative form info in database
                AlternativeFormInfoProvider.SetAlternativeFormInfo(afi);
            }
            else
            {
                ShowErrorMessage();
            }
        }

        // Perform OnAfterSave if defined
        if (OnAfterSave != null)
        {
            OnAfterSave(this, EventArgs.Empty);
        }
    }


    /// <summary>
    /// Shows invalid id message.
    /// </summary>
    void ShowErrorMessage()
    {
        pnlError.Visible = true;
        fieldEditor.Visible = false;
    }

    #endregion
}