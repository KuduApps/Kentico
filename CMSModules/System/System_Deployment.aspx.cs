using System;
using System.Data;
using System.Text.RegularExpressions;

using CMS.GlobalHelper;
using CMS.IO;
using CMS.PortalEngine;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_System_System_Deployment : SiteManagerPage
{
    private static Regex codeFileRegex = RegexHelper.GetRegex("CodeFile=\"([^\"]*)\"", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static Regex inheritsRegex = RegexHelper.GetRegex("Inherits=\"([^\"]*)\"", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (AzureHelper.IsRunningOnAzure)
        {
            lblInfo.Text = GetString("Deployment.AzureDisabled");
            btnSaveAll.Enabled = false;
        }
        
        lblInfo.Text = GetString("Deployment.SaveAllInfo");
        btnSaveAll.Text = GetString("Deployment.SaveAll");
        
    }


    protected void btnSaveAll_Click(object sender, EventArgs e)
    {
        try
        {
            // Save all the document transformations
            DataSet ds = DataClassInfoProvider.GetAllDataClass();
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    int classId = ValidationHelper.GetInteger(dr["ClassID"], 0);
                    string className = ValidationHelper.GetString(dr["ClassName"], "");
                    bool isDocumentType = ValidationHelper.GetBoolean(dr["ClassIsDocumentType"], false);

                    if (isDocumentType)
                    {
                        ProcessTransformations(classId, className);
                    }
                }
            }

            // Save all the custom table transformations
            ds = DataClassInfoProvider.GetCustomTableClasses(null, null, 0, "ClassID,ClassName,ClassIsCustomTable");
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    int classId = ValidationHelper.GetInteger(dr["ClassID"], 0);
                    string className = ValidationHelper.GetString(dr["ClassName"], "");
                    bool isCustomTable = ValidationHelper.GetBoolean(dr["ClassIsCustomTable"], false);

                    if (isCustomTable)
                    {
                        ProcessTransformations(classId, className);
                    }
                }
            }

            // Save all the layouts
            ds = LayoutInfoProvider.GetAllLayouts();
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string layoutName = ValidationHelper.GetString(dr["LayoutCodeName"], "");
                    string layoutCode = ValidationHelper.GetString(dr["LayoutCode"], "");

                    int checkedOutByUserId = ValidationHelper.GetInteger(dr["LayoutCheckedOutByUserID"], 0);
                    string checkedOutMachineName = ValidationHelper.GetString(dr["LayoutCheckedOutMachineName"], "");

                    if ((checkedOutByUserId == 0) || (checkedOutMachineName.ToLower() != HTTPHelper.MachineName.ToLower()))
                    {
                        string filename = LayoutInfoProvider.GetLayoutUrl(layoutName, null);

                        // Prepare the code
                        string code = layoutCode;
                        code = LayoutInfoProvider.AddLayoutDirectives(code);

                        SiteManagerFunctions.SaveCodeFile(filename, code);
                    }
                }
            }

            // Save all the page template layouts
            ds = PageTemplateInfoProvider.GetAllTemplates();
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string templateName = ValidationHelper.GetString(dr["PageTemplateCodeName"], "");
                    string templateLayout = ValidationHelper.GetString(dr["PageTemplateLayout"], "");
                    bool isReusable = ValidationHelper.GetBoolean(dr["PageTemplateIsReusable"], false);

                    int checkedOutByUserId = ValidationHelper.GetInteger(dr["PageTemplateLayoutCheckedOutByUserID"], 0);
                    string checkedOutMachineName = ValidationHelper.GetString(dr["PageTemplateLayoutCheckedOutMachineName"], "");
                    bool isPortalTemplate = ValidationHelper.GetBoolean(dr["PageTemplateIsPortal"], false);
                    string pageTemplateType = ValidationHelper.GetString(dr["PageTemplateType"], "");
                    bool isDashboard = pageTemplateType.Equals(PageTemplateInfoProvider.GetPageTemplateTypeCode(PageTemplateTypeEnum.Dashboard));

                    if ((isPortalTemplate || isDashboard) && !String.IsNullOrEmpty(templateLayout) && ((checkedOutByUserId == 0) || (checkedOutMachineName.ToLower() != HTTPHelper.MachineName.ToLower())))
                    {
                        string filename = null;
                        if (isReusable)
                        {
                            filename = PageTemplateInfoProvider.GetLayoutUrl(templateName, null);
                        }
                        else
                        {
                            filename = PageTemplateInfoProvider.GetAdhocLayoutUrl(templateName, null);
                        }

                        // Prepare the code
                        string code = templateLayout;
                        code = LayoutInfoProvider.AddLayoutDirectives(code);

                        SiteManagerFunctions.SaveCodeFile(filename, code);
                    }
                }
            }

            // Save all the web part layouts
            ds = WebPartLayoutInfoProvider.GetWebPartLayouts(null, null);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string webPartLayoutCodeName = ValidationHelper.GetString(dr["WebPartLayoutCodeName"], "");
                    string webPartLayoutCode = ValidationHelper.GetString(dr["WebPartLayoutCode"], "");

                    WebPartInfo wpi = WebPartInfoProvider.GetWebPartInfo(ValidationHelper.GetInteger(dr["WebPartLayoutWebPartID"], 0));

                    if (wpi != null)
                    {
                        int checkedOutByUserId = ValidationHelper.GetInteger(dr["WebPartLayoutCheckedOutByUserID"], 0);
                        string checkedOutMachineName = ValidationHelper.GetString(dr["WebPartLayoutCheckedOutMachineName"], "");

                        if (!String.IsNullOrEmpty(webPartLayoutCode) && ((checkedOutByUserId == 0) || (checkedOutMachineName.ToLower() != HTTPHelper.MachineName.ToLower())))
                        {
                            // Get layout filename
                            string filename = WebPartLayoutInfoProvider.GetWebPartLayoutUrl(wpi.WebPartName, webPartLayoutCodeName, "");
                            // Get path to the code file
                            string codeFilePath = URLHelper.GetVirtualPath(filename) + ".cs";

                            // Get path to the original code behind file
                            string originalCodeFilePath = String.Empty;
                            if (codeFileRegex.IsMatch(webPartLayoutCode, 0))
                            {
                                originalCodeFilePath = codeFileRegex.Match(webPartLayoutCode).Result("$1");
                            }

                            // Get original class name
                            string originalClassName = String.Empty;
                            if (inheritsRegex.IsMatch(webPartLayoutCode))
                            {
                                originalClassName = inheritsRegex.Match(webPartLayoutCode).Result("$1");
                            }

                            if (codeFileRegex.IsMatch(webPartLayoutCode))
                            {
                                webPartLayoutCode = codeFileRegex.Replace(webPartLayoutCode, "CodeFile=\"" + codeFilePath + "\"");
                            }

                            if (inheritsRegex.IsMatch(webPartLayoutCode))
                            {
                                webPartLayoutCode = inheritsRegex.Replace(webPartLayoutCode, "Inherits=\"$1_Web_Deployment\"");
                            }

                            // Read original codefile and change classname
                            string codeFileCode = String.Empty;
                            if (!String.IsNullOrEmpty(originalCodeFilePath) && FileHelper.FileExists(originalCodeFilePath))
                            {
                                codeFileCode = File.ReadAllText(Server.MapPath(originalCodeFilePath));
                                codeFileCode = codeFileCode.Replace(originalClassName, originalClassName + "_Web_Deployment");

                                // Save code file
                                SiteManagerFunctions.SaveCodeFile(filename, webPartLayoutCode);

                                // Save code behind file
                                SiteManagerFunctions.SaveCodeFile(codeFilePath, codeFileCode);
                            }
                        }
                    }
                }
            }

            lblResult.Text = GetString("Deployment.ObjectsSaved");
        }
        catch (Exception ex)
        {
            CMS.EventLog.EventLogProvider ep = new CMS.EventLog.EventLogProvider();
            ep.LogEvent("System deployment", "E", ex);

            lblError.Visible = true;
            lblError.Text = ex.Message;
        }
    }


    private void ProcessTransformations(int classId, string className)
    {
        // Get the transformations
        DataSet transformationsDS = TransformationInfoProvider.GetTransformations(classId);
        if (!DataHelper.DataSourceIsEmpty(transformationsDS))
        {
            foreach (DataRow transformationRow in transformationsDS.Tables[0].Rows)
            {
                // Get the type
                string type = ValidationHelper.GetString(transformationRow["TransformationType"], "ascx");
                TransformationTypeEnum transformationType = TransformationInfoProvider.GetTransformationTypeEnum(type);

                // Only export ASCX transformations
                if (transformationType == TransformationTypeEnum.Ascx)
                {
                    string transformationName = ValidationHelper.GetString(transformationRow["TransformationName"], "");
                    string transformationCode = ValidationHelper.GetString(transformationRow["TransformationCode"], "");

                    int checkedOutByUserId = ValidationHelper.GetInteger(transformationRow["TransformationCheckedOutByUserID"], 0);
                    string checkedOutMachineName = ValidationHelper.GetString(transformationRow["TransformationCheckedOutMachineName"], "");

                    if ((checkedOutByUserId == 0) || (checkedOutMachineName.ToLower() != HTTPHelper.MachineName.ToLower()))
                    {
                        string filename = TransformationInfoProvider.GetTransformationUrl(className + "." + transformationName, null, TransformationTypeEnum.Ascx);

                        // Prepare the code
                        string code = transformationCode;
                        code = TransformationInfoProvider.AddTransformationDirectives(code, true);

                        SiteManagerFunctions.SaveCodeFile(filename, code);
                    }
                }
            }
        }
    }
}
