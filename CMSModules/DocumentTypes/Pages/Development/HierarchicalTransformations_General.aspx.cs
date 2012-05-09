using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;


public partial class CMSModules_DocumentTypes_Pages_Development_HierarchicalTransformations_General : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int transID = QueryHelper.GetInteger("transid", 0);
        if (transID != 0)
        {
            TransformationInfo ti = TransformationInfoProvider.GetTransformation(transID);
            ucTransf.TransInfo = ti;
        }
    }
}

