using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;

public partial class CMSAPIExamples_Controls_APIExampleSection : CMSUserControl
{
    /// <summary>
    /// Title of the section.
    /// </summary>
    public string Title
    {
        get
        {
            return ltlTitle.Text;
        }
        set
        {
            ltlTitle.Text = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
