using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.FormEngine;
using CMS.DataEngine;
using CMS.EventLog;
using CMS.CMSHelper;
using CMS.URLRewritingEngine;

public partial class CMSModules_CustomTables_CustomTable_Edit_Fields : CMSCustomTablesPage
{
    #region "Variables"

    protected DataClassInfo dci = null;
    protected string className = null;
    private EventLogProvider mEventLog = null;
    private FormInfo mFormInfo = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Event log.
    /// </summary>
    public EventLogProvider EventLog
    {
        get
        {
            if (mEventLog == null)
            {
                mEventLog = new EventLogProvider();
            }
            return mEventLog;
        }
    }


    /// <summary>
    /// Form info.
    /// </summary>
    public FormInfo FormInfo
    {
        get
        {
            if ((mFormInfo == null) && (dci != null))
            {
                mFormInfo = FormHelper.GetFormInfo(dci.ClassName, true);
            }
            return mFormInfo;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        int classId = QueryHelper.GetInteger("customtableid", 0);
        dci = DataClassInfoProvider.GetDataClass(classId);
        // Set edited object
        EditedObject = dci;
        CurrentMaster.BodyClass += " FieldEditorBody";

        // Class exists
        if (dci != null)
        {
            className = dci.ClassName;
            if (dci.ClassIsCoupledClass)
            {
                FieldEditor.Visible = true;
                FieldEditor.ClassName = className;
                FieldEditor.Mode = FieldEditorModeEnum.CustomTable;
                FieldEditor.OnFieldNameChanged += FieldEditor_OnFieldNameChanged;

                btnGUID.Text = GetString("customtable.GenerateGUID");
                btnGUID.Click += btnGUID_Click;
            }
            else
            {
                lblError.Text = GetString("customtable.ErrorNoFields");
            }
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Class exists
        if (dci != null)
        {
            // Hide top menu and show only if needed
            pnlTopMenu.Visible = false;

            if (dci.ClassIsCoupledClass)
            {
                // GUID column is not present
                if ((FormInfo.GetFormField("ItemGUID") == null))
                {
                    lblGUID.Text = GetString("customtable.GUIDColumMissing");
                    pnlTopMenu.Visible = true;
                    pnlClassField.CssClass = "FieldClassEditor";
                    plcGUID.Visible = true;
                }
            }

            if (!RequestHelper.IsPostBack() && QueryHelper.GetBoolean("gen", false))
            {
                pnlTopMenu.Visible = true;
                lblError.Visible = true;
                lblError.CssClass = "InfoLabel";
                lblError.Text = GetString("customtable.GUIDFieldGenerated");
            }
        }
    }


    void FieldEditor_OnFieldNameChanged(object sender, string oldFieldName, string newFieldName)
    {
        if (dci != null)
        {
            // Rename field in layout(s)
            FormHelper.RenameFieldInFormLayout(dci.ClassID, oldFieldName, newFieldName);
        }
    }


    void btnGUID_Click(object sender, EventArgs e)
    {
        try
        {
            // Create GUID field
            FormFieldInfo ffiGuid = new FormFieldInfo();

            // Fill FormInfo object
            ffiGuid.Name = "ItemGUID";
            ffiGuid.Caption = "GUID";
            ffiGuid.DataType = FormFieldDataTypeEnum.GUID;
            ffiGuid.DefaultValue = "";
            ffiGuid.Description = "";
            ffiGuid.FieldType = FormFieldControlTypeEnum.CustomUserControl;
            ffiGuid.Settings["controlname"] = Enum.GetName(typeof(FormFieldControlTypeEnum), FormFieldControlTypeEnum.LabelControl).ToLower();
            ffiGuid.PrimaryKey = false;
            ffiGuid.System = true;
            ffiGuid.Visible = false;
            ffiGuid.Size = 0;
            ffiGuid.AllowEmpty = false;

            FormInfo.AddFormField(ffiGuid);

            // Update table structure - columns could be added
            bool old = TableManager.UpdateSystemFields;
            TableManager.UpdateSystemFields = true;
            string schema = FormInfo.GetXmlDefinition();
            TableManager.UpdateTableBySchema(dci.ClassTableName, schema);
            TableManager.UpdateSystemFields = old;

            // Update xml schema and form definition
            dci.ClassFormDefinition = schema;
            dci.ClassXmlSchema = TableManager.GetXmlSchema(dci.ClassTableName);

            dci.Generalized.LogEvents = false;

            // Save the data
            DataClassInfoProvider.SetDataClass(dci);

            dci.Generalized.LogEvents = true;

            // Generate default queries
            SqlGenerator.GenerateDefaultQueries(dci, true, false);

            // Clear cached data
            CMSObjectHelper.RemoveReadOnlyObjects(CustomTableItemProvider.GetObjectType(className), true);
            CustomTableItemProvider.Remove(className, true);
            // Clear the object type hashtable
            ProviderStringDictionary.ReloadDictionaries(className, true);

            // Clear the classes hashtable
            ProviderStringDictionary.ReloadDictionaries("cms.class", true);

            // Clear class strucures
            ClassStructureInfo.Remove(className, true);

            // Ensure GUIDs for all items
            CustomTableItemProvider tableProvider = new CustomTableItemProvider();
            tableProvider.UpdateSystemFields = false;
            tableProvider.LogSynchronization = false;
            DataSet dsItems = tableProvider.GetItems(className, null, null);
            if (!DataHelper.DataSourceIsEmpty(dsItems))
            {
                foreach (DataRow dr in dsItems.Tables[0].Rows)
                {
                    CustomTableItem item = new CustomTableItem(dr, className, tableProvider);
                    item.ItemGUID = Guid.NewGuid();
                    item.Update();
                }
            }

            // Log event
            UserInfo currentUser = CMSContext.CurrentUser;
            EventLog.LogEvent(EventLogProvider.EVENT_TYPE_INFORMATION, DateTime.Now, "Custom table", "GENERATEGUID", currentUser.UserID, currentUser.UserName, 0, null, null, string.Format(ResHelper.GetAPIString("customtable.GUIDGenerated", "Field 'ItemGUID' for custom table '{0}' was created and GUID values were generated."), dci.ClassName), 0, null);

            URLHelper.Redirect(URLHelper.AddParameterToUrl(URLRewriter.CurrentURL, "gen", "1"));
        }
        catch (Exception ex)
        {
            lblError.Visible = true;
            lblError.Text = GetString("customtable.ErrorGUID") + ex.Message;

            // Log event
            EventLog.LogEvent("Custom table", "GENERATEGUID", ex);
        }
    }
}
