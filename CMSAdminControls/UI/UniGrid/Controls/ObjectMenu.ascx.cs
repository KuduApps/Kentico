using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using CMS.UIControls;
using CMS.Controls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.DataEngine;
using CMS.Synchronization;

public partial class CMSAdminControls_UI_UniGrid_Controls_ObjectMenu : CMSContextMenuControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterDialogScript(this.Page);
        ScriptHelper.RegisterApplicationConstants(this.Page);

        // Get the object type
        string param = this.ContextMenu.Parameter;
        string objectType = null;
        bool groupObject = false;
        if (param != null)
        {
            string[] parms = param.Split(';');
            objectType = parms[0];
            if (parms.Length == 2)
            {
                groupObject = ValidationHelper.GetBoolean(parms[1], false);
            }
        }

        // Get empty info
        GeneralizedInfo obj = null;
        if (objectType != null)
        {
            obj = CMSObjectHelper.GetReadOnlyObject(objectType);
            // Get correct info for listings
            if (obj.TypeInfo.Inherited)
            {
                obj = CMSObjectHelper.GetReadOnlyObject(obj.TypeInfo.OriginalObjectType);
            }
            obj.ObjectGroupID = groupObject ? 1 : 0;
        }

        if (obj == null)
        {
            this.Visible = false;
            return;
        }

        CurrentUserInfo curUser = CMSContext.CurrentUser;
        string curSiteName = CMSContext.CurrentSiteName;

        string menuId = this.ContextMenu.MenuID;

        // Relationships
        if (obj.TypeInfo.HasObjectRelationships)
        {
            this.iRelationships.ImageUrl = UIHelper.GetImageUrl(this.Page, "Design/Controls/UniGrid/Actions/Relationships.png");
            this.iRelationships.Text = ResHelper.GetString("General.Relationships");
            this.iRelationships.Attributes.Add("onclick", "ContextRelationships(GetContextMenuParameter('" + menuId + "'));");
        }
        else
        {
            this.iRelationships.Visible = false;
        }

        // Export
        if (obj.TypeInfo.AllowSingleExport)
        {
            if (curUser.IsAuthorizedPerResource("cms.globalpermissions", "ExportObjects", curSiteName))
            {
                this.iExport.ImageUrl = UIHelper.GetImageUrl(this.Page, "Design/Controls/UniGrid/Actions/ExportObject.png");
                this.iExport.Text = ResHelper.GetString("General.Export");
                this.iExport.Attributes.Add("onclick", "ContextExportObject(GetContextMenuParameter('" + menuId + "'), false);");
            }
            else
            {
                this.iExport.Visible = false;
            }

            if (obj.GUIDColumn != TypeInfo.COLUMN_NAME_UNKNOWN)
            {
                if (curUser.IsAuthorizedPerResource("cms.globalpermissions", "BackupObjects", curSiteName))
                {
                    this.iBackup.ImageUrl = UIHelper.GetImageUrl(this.Page, "Design/Controls/UniGrid/Actions/BackupObject.png");
                    this.iBackup.Text = ResHelper.GetString("General.Backup");
                    this.iBackup.Attributes.Add("onclick", "ContextExportObject(GetContextMenuParameter('" + menuId + "'), true);");
                }
                else
                {
                    this.iBackup.Visible = false;
                }

                if (curUser.IsAuthorizedPerResource("cms.globalpermissions", "RestoreObjects", curSiteName))
                {
                    this.iRestore.ImageUrl = UIHelper.GetImageUrl(this.Page, "Design/Controls/UniGrid/Actions/RestoreObject.png");
                    this.iRestore.Text = ResHelper.GetString("General.Restore");
                    this.iRestore.Attributes.Add("onclick", "ContextRestoreObject(GetContextMenuParameter('" + menuId + "'), true);");
                }
                else
                {
                    this.iRestore.Visible = false;
                }
            }
            else
            {
                this.iBackup.Visible = false;
                this.iRestore.Visible = false;
            }
        }
        else
        {
            this.iExport.Visible = false;
            this.iBackup.Visible = false;
            this.iRestore.Visible = false;
        }

        // Versioning
        if (obj.AllowRestore && UniGridFunctions.ObjectSupportsDestroy(obj) && curUser.IsAuthorizedPerObject(PermissionsEnum.Destroy, obj.TypeInfo.OriginalObjectType, curSiteName))
        {
            iDestroy.ImageUrl = UIHelper.GetImageUrl(Page, "Design/Controls/UniGrid/Actions/Delete.png");
            iDestroy.Text = ResHelper.GetString("security.destroy");
            iDestroy.Attributes.Add("onclick", "ContextDestroyObject_" + ClientID + "(GetContextMenuParameter('" + menuId + "'))");
        }
        else
        {
            iDestroy.Visible = false;
        }

        bool ancestor = iRelationships.Visible;
        sep1.Visible = iExport.Visible && ancestor;
        ancestor |= iExport.Visible;
        sep2.Visible = (iBackup.Visible || iRestore.Visible) && ancestor;
        ancestor |= (iBackup.Visible || iRestore.Visible);
        sep3.Visible = iDestroy.Visible && ancestor;

        this.Visible = iRelationships.Visible || iExport.Visible || iBackup.Visible || iDestroy.Visible;

        if (Visible)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
function ContextRelationships(definition) {
   var url = applicationUrl + 'CMSModules/AdminControls/Pages/ObjectRelationships.aspx?objecttype=' + escape(definition[0]) + '&objectid=' + escape(definition[1]);
        modalDialog(url, ""relationships"", 900, 600);
    }
    
function ContextExportObject(definition, backup) {
   var query = ''; 
   if (backup) {
       query += '&backup=true';
   }
   modalDialog(applicationUrl + 'CMSModules/ImportExport/SiteManager/ExportObject.aspx?objectType=' + escape(definition[0]) + '&objectId=' + definition[1] + query, 'ExportObject', 750, 200);
}

function ContextRestoreObject(definition, backup) {
    var query = '';
    if (backup) {
       query += '&backup=true';
    }
    modalDialog(applicationUrl + 'CMSModules/ImportExport/SiteManager/RestoreObject.aspx?objectType=' + escape(definition[0]) + '&objectId=' + definition[1] + query, 'RestoreObject', 750, 350);
}");
            // Register general export scripts
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ObjectMenuExportScripts", sb.ToString(), true);

            sb = new StringBuilder();
            sb.Append(@"
function ContextDestroyObject_", ClientID, @"(definition)
{
   if(confirm('", ResHelper.GetString("objectversioning.destroyobjectconfirmation"), @"')) {
      if(UG_DestroyObj_", ContextMenu.ParentElementClientID, @") {
          var param = definition.toString().split(',');
          if((param != null) && (param.length == 2)) {
              UG_DestroyObj_", ContextMenu.ParentElementClientID, @"(param[1]);
          }
      }
   }
}");
            // Register destroy script for particular menu
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ObjectMenuDestroyScript_" + ClientID, sb.ToString(), true);
        }
    }
}
