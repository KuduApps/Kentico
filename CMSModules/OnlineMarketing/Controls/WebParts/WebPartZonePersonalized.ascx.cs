using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.PortalEngine;
using CMS.DataEngine;

public partial class CMSModules_OnlineMarketing_Controls_WebParts_WebPartZonePersonalized : CMSUserControl, IWebPartZoneProperties
{
    #region "Variables"

    protected string zoneId = QueryHelper.GetString("zoneid", "");
    protected int templateId = QueryHelper.GetInteger("templateid", 0);
    protected int variantId = QueryHelper.GetInteger("variantid", 0);
    protected VariantModeEnum variantMode = VariantModeFunctions.GetVariantModeEnum(QueryHelper.GetString("variantmode", string.Empty));

    #endregion


    #region "Page methods"

    /// <summary>
    /// Init event handler.
    /// </summary>
    protected override void OnInit(EventArgs e)
    {
        // When displaying an existing variant of a zone, get the variant mode for its original zone
        if (variantId > 0)
        {
            PageTemplateInfo pti = PageTemplateInfoProvider.GetPageTemplateInfo(templateId);
            if ((pti != null) && ((pti.TemplateInstance != null)))
            {
                // Get the original zone and retrieve its variant mode
                WebPartZoneInstance zoneInstance = pti.TemplateInstance.GetZone(zoneId);
                if ((zoneInstance != null) && (zoneInstance.VariantMode != VariantModeEnum.None))
                {
                    variantMode = zoneInstance.VariantMode;
                }
            }
        }

        base.OnInit(e);
    }


    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (variantMode == VariantModeEnum.MVT)
        {
            // Display MVT edit dialog
            mvtEditElem.UIFormControl.EditedObject = BaseAbstractInfoProvider.GetInfoById(PredefinedObjectType.MVTVARIANT, QueryHelper.GetInteger("variantid", 0));
            mvtEditElem.UIFormControl.ParentObject = BaseAbstractInfoProvider.GetInfoById(PredefinedObjectType.PAGETEMPLATE, QueryHelper.GetInteger("templateid", 0));
            mvtEditElem.Visible = true;
            mvtEditElem.UIFormControl.SubmitButton.Visible = false;
            mvtEditElem.UIFormControl.ReloadData();
        }
        else if (variantMode == VariantModeEnum.ContentPersonalization)
        {
            // Display Content personalization edit dialog
            cpEditElem.UIFormControl.EditedObject = BaseAbstractInfoProvider.GetInfoById(PredefinedObjectType.CONTENTPERSONALIZATIONVARIANT, QueryHelper.GetInteger("variantid", 0));
            cpEditElem.UIFormControl.ParentObject = BaseAbstractInfoProvider.GetInfoById(PredefinedObjectType.PAGETEMPLATE, QueryHelper.GetInteger("templateid", 0));
            cpEditElem.Visible = true;
            cpEditElem.UIFormControl.SubmitButton.Visible = false;
            cpEditElem.UIFormControl.ReloadData();
        }
    }


    /// <summary>
    /// PreRender event handler.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Show/Hide info and error labels
        lblInfo.Visible = !string.IsNullOrEmpty(lblInfo.Text);
        lblError.Visible = !string.IsNullOrEmpty(lblError.Text);
    }


    /// <summary>
    /// Saves the zone variant.
    /// </summary>
    public bool Save()
    {
        bool result = false;
        if (variantMode == VariantModeEnum.MVT)
        {
            if (mvtEditElem.ValidateData())
            {
                result = mvtEditElem.UIFormControl.SaveData(null);
            }
                        
            if (!string.IsNullOrEmpty(mvtEditElem.UIFormControl.ErrorLabel.Text))
            {
                lblError.Text = mvtEditElem.UIFormControl.ErrorLabel.Text;
                lblError.Visible = true;
            }
        }
        else if (variantMode == VariantModeEnum.ContentPersonalization)
        {
            if (cpEditElem.ValidateData())
            {
                result = cpEditElem.UIFormControl.SaveData(null);
            }
            
            if (!string.IsNullOrEmpty(cpEditElem.UIFormControl.ErrorLabel.Text))
            {
                lblError.Text = cpEditElem.UIFormControl.ErrorLabel.Text;
                lblError.Visible = true;
            }
        }
        if (result)
        {
            lblInfo.Text = ResHelper.GetString("general.changessaved");
        }

        return result;
    }

    #endregion
}
