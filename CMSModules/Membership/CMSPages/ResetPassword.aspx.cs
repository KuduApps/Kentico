using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;

public partial class CMSModules_Membership_CMSPages_ResetPassword : CMSPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageTitle title = this.CurrentMaster.Title;
        title.TitleImage = GetImageUrl("Objects/CMS_User/password.png");
        title.TitleText = GetString("passreset.title");
    }
}

