using System;
using System.Xml;
using System.Data;
using System.Text;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.Synchronization;
using CMS.SettingsProvider;

public partial class CMSModules_Integration_Pages_Administration_View : CMSIntegrationPage
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the task identifier.
    /// </summary>
    public int TaskID
    {
        get
        {
            return QueryHelper.GetInteger("taskid", 0);
        }
    }

    #endregion"


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMaster.Title.TitleText = GetString("Task.ViewHeader");
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Integration/tasks.png");

        IntegrationTaskInfo ti = IntegrationTaskInfoProvider.GetIntegrationTaskInfo(TaskID);
        // Set edited object
        EditedObject = ti;

        if (ti != null)
        {
            CurrentMaster.Title.TitleText += " (" + HTMLHelper.HTMLEncode(ti.TaskTitle) + ")";

            string direction = GetString(ti.TaskIsInbound ? "integration.inbound" : "integration.outbound");

            // Prepare task description
            StringBuilder sbTaskInfo = new StringBuilder();
            sbTaskInfo.Append("<table>");
            sbTaskInfo.Append("<tr><td class=\"Title Grid\">" + GetString("integration.taskdirection") + ":</td><td>" + direction + "</td></tr>");
            sbTaskInfo.Append("<tr><td class=\"Title Grid\">" + GetString("integration.tasktype") + ":</td><td>" + ti.TaskType + "</td></tr>");
            sbTaskInfo.Append("<tr><td class=\"Title Grid\">" + GetString("integration.tasktime") + ":</td><td>" + ti.TaskTime + "</td></tr>");
            sbTaskInfo.Append("</table>");

            string objectType = ti.TaskObjectType;
            if (ti.TaskNodeID > 0)
            {
                objectType = PredefinedObjectType.DOCUMENT;
            }
            viewDataSet.ObjectType = objectType;
            viewDataSet.DataSet = GetDataSet(ti.TaskData, ti.TaskType, ti.TaskObjectType);
            viewDataSet.AdditionalContent = sbTaskInfo.ToString();
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Returns the dataset loaded from the given document data.
    /// </summary>
    /// <param name="documentData">Document data to make the dataset from</param>
    /// <param name="taskType">Task type</param>
    /// <param name="taskObjectType">Task object type</param>
    protected virtual DataSet GetDataSet(string documentData, TaskTypeEnum taskType, string taskObjectType)
    {
        SyncHelper syncHelper = SyncHelper.GetInstance();
        syncHelper.OperationType = OperationTypeEnum.Synchronization;
        string className = CMSHierarchyHelper.GetNodeClassName(documentData, ExportFormatEnum.XML);
        DataSet ds = syncHelper.GetSynchronizationTaskDataSet(taskType, className, taskObjectType);

        XmlParserContext xmlContext = new XmlParserContext(null, null, null, XmlSpace.None);
        XmlReader reader = new XmlTextReader(documentData, XmlNodeType.Element, xmlContext);
        return DataHelper.ReadDataSetFromXml(ds, reader, null, null);
    }

    #endregion
}