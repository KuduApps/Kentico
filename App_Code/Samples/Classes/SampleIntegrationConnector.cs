using System;

using CMS.EventLog;
using CMS.SettingsProvider;
using CMS.Synchronization;
using CMS.SynchronizationEngine;
using CMS.TreeEngine;

/// <summary>
/// Integration connector demonstrating simpliest functionality.
/// </summary>
public class SampleIntegrationConnector : BaseIntegrationConnector
{
    #region "Initialization (subscribing)"

    /// <summary>
    /// Initialize connector name and register subscriptions.
    /// </summary>
    public override void Init()
    {
        // Initialize connector name (it has to match the code name of connector stored in DB)
        ConnectorName = GetType().Name;

        // Create subscription for all user objects
        SubscribeToObjects(TaskProcessTypeEnum.AsyncSnapshot, PredefinedObjectType.USER);

        // Create subscription for all documents (on all sites)
        SubscribeToAllDocuments(TaskProcessTypeEnum.AsyncSimpleSnapshot, TaskTypeEnum.All);

        // Or prepare the subscriptions manually
        //ObjectIntegrationSubscription objSubscription = new ObjectIntegrationSubscription(ConnectorName, TaskProcessTypeEnum.AsyncSnapshot, TaskTypeEnum.All, null, PredefinedObjectType.USER, null);
        //SubscribeTo(objSubscription);
        //DocumentIntegrationSubscription docSubscription = new DocumentIntegrationSubscription(ConnectorName, TaskProcessTypeEnum.AsyncSimpleSnapshot, TaskTypeEnum.All, null, null, null, null);
        //SubscribeTo(docSubscription);

        // Please see implementation of ProcessInternalTaskAsync overloads (and eventually comments for the rest of the methods)
    }

    #endregion


    #region "Internal (outcoming) tasks"

    /// <summary>
    /// Processes given object according to task type.
    /// It is expected that you use TranslateColumnsToExternal method before you process the task.
    /// The TranslateColumnsToExternal needs GetExternalObjectID and GetExternalDocumentID to be implemented.
    /// It traverses the given object and tries to translate foreign keys to match external (your) application.
    /// </summary>
    /// <param name="infoObj">Info object to process</param>
    /// <param name="translations">Translation helper object containing translations for given object</param>
    /// <param name="taskType">Type of task</param>
    /// <param name="dataType">Type of data</param>
    /// <param name="siteName">Name of site</param>
    /// <param name="errorMessage">Possible error message</param>
    /// <returns>Result of processing</returns>
    public override IntegrationProcessResultEnum ProcessInternalTaskAsync(GeneralizedInfo infoObj, TranslationHelper translations, TaskTypeEnum taskType, TaskDataTypeEnum dataType, string siteName, out string errorMessage)
    {
        try
        {
            // If object is of 'user' type
            // You can also use following condition: (((BaseInfo)infoObj) is CMS.SiteProvider.UserInfo)
            if (infoObj.ObjectType == PredefinedObjectType.USER)
            {
                bool log = false;
                // Create simple message
                string message = "User with username '" + infoObj.ObjectCodeName + "' has been";
                switch (taskType)
                {
                    case TaskTypeEnum.CreateObject:
                        log = true;
                        message += " created.";
                        break;

                    case TaskTypeEnum.UpdateObject:
                        log = true;
                        message += " updated.";
                        break;

                    case TaskTypeEnum.DeleteObject:
                        log = true;
                        message += " deleted.";
                        break;
                }
                if (log)
                {
                    EventLogProvider eventLog = new EventLogProvider();
                    // Log the message
                    eventLog.LogEvent(EventLogProvider.EVENT_TYPE_INFORMATION, DateTime.Now, ConnectorName, taskType.ToString(), 0, null, 0, null, null, message, 0, null);
                }
            }
            errorMessage = null;
            return IntegrationProcessResultEnum.OK;
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
            return IntegrationProcessResultEnum.Error;
        }
        finally
        {
            // Clear translations cached during TranslateColumnsToExternal which internally calls GetExternalObjectID, GetExternalDocumentID
            // This call is optional but recommended in the case where eg. collision of code names can occur
            ClearInternalTranslations();
        }
    }


    /// <summary>
    /// Processes given document according to task type.
    /// It is expected that you use TranslateColumnsToExternal method before you process the task.
    /// The TranslateColumnsToExternal needs GetExternalObjectID and GetExternalDocumentID to be implemented.
    /// It traverses the given object and tries to translate foreign keys to match external (your) application.
    /// </summary>
    /// <param name="node">Document to process</param>
    /// <param name="translations">Translation helper object containing translations for given document</param>
    /// <param name="taskType">Type of task</param>
    /// <param name="dataType">Type of data</param>
    /// <param name="siteName">Name of site</param>
    /// <param name="errorMessage">Possible error message</param>
    /// <returns>Result of processing</returns>
    public override IntegrationProcessResultEnum ProcessInternalTaskAsync(TreeNode node, TranslationHelper translations, TaskTypeEnum taskType, TaskDataTypeEnum dataType, string siteName, out string errorMessage)
    {
        try
        {
            bool log = false;
            // Create simple message
            string message = "Document named '" + node.NodeName + "' located at '" + node.NodeAliasPath + "' has been";
            switch (taskType)
            {
                case TaskTypeEnum.CreateDocument:
                    log = true;
                    message += " created.";
                    break;

                case TaskTypeEnum.UpdateDocument:
                    log = true;
                    message += " updated.";
                    break;

                case TaskTypeEnum.DeleteDocument:
                    log = true;
                    message += " deleted.";
                    break;
            }
            if (log)
            {
                EventLogProvider eventLog = new EventLogProvider();
                // Log the message
                eventLog.LogEvent(EventLogProvider.EVENT_TYPE_INFORMATION, DateTime.Now, ConnectorName, taskType.ToString(), 0, null, node.NodeID, node.DocumentName, null, message, node.NodeSiteID, null);
            }
            errorMessage = null;
            return IntegrationProcessResultEnum.OK;
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
            return IntegrationProcessResultEnum.Error;
        }
        finally
        {
            // Clear translations cached during TranslateColumnsToExternal which internally calls GetExternalObjectID, GetExternalDocumentID
            // This call is optional but recommended in the case where eg. collision of code names can occur
            ClearInternalTranslations();
        }
    }


    /// <summary>
    /// Suitable for implementation of synchronous outcoming object processing. Identifiers of object are in their original state.
    /// </summary>
    /// <param name="infoObj">Object to process</param>
    /// <param name="taskType">Type of task</param>
    /// <param name="siteName">Name of site</param>
    /// <param name="errorMessage">Possible error message</param>
    /// <returns>Processing result</returns>
    public override IntegrationProcessResultEnum ProcessInternalTaskSync(GeneralizedInfo infoObj, TaskTypeEnum taskType, string siteName, out string errorMessage)
    {
        // Synchronous processing of objects (meaning processing is executed in the context of CMS right away)
        // (task is not being logged and processed afterwards in separate thread as in ProcessInternalTaskAsync method)
        errorMessage = null;
        return IntegrationProcessResultEnum.OK;
    }


    /// <summary>
    /// Suitable for implementation of synchronous outcoming document processing. Identifiers of object are in their original state.
    /// </summary>
    /// <param name="node">Document to process</param>
    /// <param name="taskType">Type of task</param>
    /// <param name="siteName">Name of site</param>
    /// <param name="errorMessage">Possible error message</param>
    /// <returns>Processing result</returns>
    public override IntegrationProcessResultEnum ProcessInternalTaskSync(TreeNode node, TaskTypeEnum taskType, string siteName, out string errorMessage)
    {
        // Synchronous processing of documents (meaning processing is executed in the context of CMS right away)
        // (task is not being logged and processed afterwards in separate thread as in ProcessInternalTaskAsync method)
        errorMessage = null;
        return IntegrationProcessResultEnum.OK;
    }


    /// <summary>
    /// Based on parameters this method will find out identifier of the object matching external application.
    /// </summary>
    /// <param name="objectType">Type of object</param>
    /// <param name="codeName">Code name of object</param>
    /// <param name="siteName">Site name of object</param>
    /// <param name="parentType">Type of parent object</param>
    /// <param name="parentId">Parent object identifier</param>
    /// <param name="groupId">Group identifier</param>
    /// <returns>Object identifier for external usage</returns>
    public override int GetExternalObjectID(string objectType, string codeName, string siteName, string parentType, int parentId, int groupId)
    {
        // Based on given parameters you should be able to identify object in your (external) application and return its identifier
        return 0;
    }


    /// <summary>
    /// Based on parameters this method will find out identifier of the document matching external application.
    /// </summary>
    /// <param name="nodeGuid">Document unique identifier</param>
    /// <param name="cultureCode">Document culture code</param>
    /// <param name="siteName">Document site name</param>
    /// <param name="returnDocumentId">Whether to return document or node identifier</param>
    /// <returns>Document identifier for external usage</returns>
    public override int GetExternalDocumentID(Guid nodeGuid, string cultureCode, string siteName, bool returnDocumentId)
    {
        // Based on given parameters you should be able to identify document in your (external) application and return its identifier
        return 0;
    }

    #endregion


    #region "External (incoming) tasks"

    /// <summary>
    /// By supplying object type and identifier the method ensures filling output parameters needed for correct translation between external and internal object.
    /// </summary>
    /// <param name="id">Identifier of the object</param>
    /// <param name="objectType">Type of the object</param>
    /// <param name="codeName">Returns code name</param>
    /// <param name="siteName">Returns site name</param>
    /// <param name="parentId">Returns identifier of parent object</param>
    /// <param name="groupId">Returns identifier of object community group</param>
    public override void GetInternalObjectParams(int id, string objectType, out string codeName, out string siteName, ref int parentId, ref int groupId)
    {
        // Based on given identifier and object type you should be able to identify object in your (external) application and return its parameters (marked as 'out')
        codeName = null;
        siteName = null;
        parentId = 0;
        groupId = 0;
    }


    /// <summary>
    /// By supplying document identifier and class name the method ensures filling output parameters needed for correct translation between external and internal document.
    /// </summary>
    /// <param name="id">Identifier of the document</param>
    /// <param name="className">Class name of the document</param>
    /// <param name="nodeGuid">Returns document unique identifier</param>
    /// <param name="cultureCode">Returns culture code</param>
    /// <param name="siteName">Returns site name</param>
    public override void GetInternalDocumentParams(int id, string className, out Guid nodeGuid, out string cultureCode, out string siteName)
    {
        // Based on given identifier and object type you should be able to identify document in your (external) application and return its parameters (marked as 'out')
        nodeGuid = Guid.Empty;
        cultureCode = null;
        siteName = null;
    }


    /// <summary>
    /// Transforms given external object to internal (to TreeNode or GeneralizedInfo).
    /// </summary>
    /// <param name="obj">Object or document to transform</param>
    /// <param name="taskType">Type of task</param>
    /// <param name="dataType">Type of input data</param>
    /// <param name="siteName">Name of site</param>
    public override ICMSObject PrepareInternalObject(object obj, TaskTypeEnum taskType, TaskDataTypeEnum dataType, string siteName)
    {
        // This method is called withing IntegrationHelper.ProcessExternalTask and it provides you with space where you can easily transform
        // external object (possibly some kind of data container) to TreeNode (document) or GeneralizedInfo (object - eg. UserInfo)
        return null;
    }

    #endregion
}