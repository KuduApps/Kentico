using System;
using System.Web.UI;

using CMS.GlobalHelper;
using CMS.FormControls;
using CMS.SiteProvider;
using CMS.PortalControls;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_FormControls_Tags_TagSelector : FormEngineUserControl
{
    #region "Variables"

    private bool mEnabled = true;

    #endregion


    #region "Properties"

    /// <summary>
    /// Enable/disable control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return mEnabled;
        }
        set
        {
            mEnabled = value;
            btnSelect.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets the field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return TagHelper.GetTagsForSave(txtTags.Text.Trim());
        }
        set
        {
            txtTags.Text = ValidationHelper.GetString(value, "");
        }
    }


    /// <summary>
    /// Tag Group ID.
    /// </summary>
    public int GroupId
    {
        get
        {
            int mGroupId = ValidationHelper.GetInteger(GetValue("GroupID"), 0);
            if ((mGroupId == 0) && (Form != null))
            {
                // When inserting new document
                if (Form.ParentObject != null)
                {
                    // Get path and groupID of the parent node
                    mGroupId = ((TreeNode)Form.ParentObject).DocumentTagGroupID;
                    // If nothing found try get inherited value
                    if (mGroupId == 0)
                    {
                        mGroupId = ValidationHelper.GetInteger(((TreeNode)Form.ParentObject).GetInheritedValue("DocumentTagGroupID", true), 0);
                    }
                }
                // When editing existing document
                else if (Form.EditedObject != null)
                {
                    // Get path and groupID of the parent node
                    mGroupId = ((TreeNode)Form.EditedObject).DocumentTagGroupID;
                    // If nothing found try get inherited value
                    if (mGroupId == 0)
                    {
                        mGroupId = ValidationHelper.GetInteger(((TreeNode)Form.EditedObject).GetInheritedValue("DocumentTagGroupID", true), 0);
                    }
                }
            }

            return mGroupId;
        }
        set
        {
            SetValue("GroupID", value);
        }
    }

    #endregion


    #region "Page events"

    /// <summary>
    /// Init event.
    /// </summary>
    protected override void OnInit(EventArgs e)
    {
        // Ensure the script manager
        PortalHelper.EnsureScriptManager(Page);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Ensure Script Manager is the first control on the page
        using (ScriptManager sMgr = ScriptManager.GetCurrent(Page))
        {
            if (sMgr != null)
            {
                sMgr.Services.Add(new ServiceReference("~/CMSModules/Content/FormControls/Tags/TagSelectorService.asmx"));
            }
        }

        // Register the dialog script
        ScriptHelper.RegisterDialogScript(Page);

        // Register tag script 
        ScriptHelper.RegisterStartupScript(this, typeof(string), "tagScript", ScriptHelper.GetScript(GetTagScript()));

        // Create script for valid inserting into textbox
        ScriptHelper.RegisterStartupScript(this, typeof(string), "tagSelectScript", @"
function itemSelected(source, eventArgs) {
    var txtBox = source.get_element();
    if (txtBox) {
        txtBox.value = eventArgs.get_text().replace(/\'""/,'""').replace(/""\'/,'""');
    }
}
function resetPosition(object, args) {
    var tb = object._element;
    var tbposition = findPosition(tb);

    var xposition = tbposition[0];
    var yposition = tbposition[1] + 22; // 22 = textbox height + a few pixels spacing

    var ex = object._completionListElement;
    if (ex) {
        $common.setLocation(ex, new Sys.UI.Point(xposition, yposition));
    }
}
function findPosition(obj) {
    var posX = 0, posY = 0;
    if (typeof (obj.offsetParent) != 'undefined') {
        posX += obj.offsetLeft;
        posY += obj.offsetTop;
        while (obj) {
            if ((obj.style.position != 'absolute') && (obj.style.position != 'relative') && (obj.style.position != 'fixed')) {
                posX += obj.offsetLeft;
                posY += obj.offsetTop;
                obj = obj.offsetParent;
            }
            else {
                obj = null;
            }
        }
    }
    else {
        posX = obj.x;
        posY = obj.y;
    }
    return [posX, posY];
}
", true);

        btnSelect.Text = GetString("general.select");
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        // Enable / Disable control
        txtTags.Enabled = Enabled;
        btnSelect.Enabled = Enabled;
        if (Enabled)
        {
            autoComplete.ContextKey = GroupId.ToString();
            btnSelect.OnClientClick = String.Format("tagSelect('{0}','{1}'); return false;", txtTags.ClientID, GroupId);
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Returns tag JS script.
    /// </summary>
    private string GetTagScript()
    {
        string baseUrl = IsLiveSite ? "~/CMSFormControls/LiveSelectors/TagSelector.aspx" : "~/CMSFormControls/Selectors/TagSelector.aspx";

        // Build script with modal dialog opener and set textbox functions
        return String.Format(@"
function tagSelect(id,group){{
    var textbox = document.getElementById(id);
    if (textbox != null){{
        var tags = encodeURIComponent(textbox.value.replace(/\|/,""-"").replace(/%/,""-"")).replace(/&/,""%26"");
        modalDialog('{0}?textbox='+ id +'&group='+ group +'&tags=' + tags, 'TagSelector', 570, 670);
    }}
}}
function setTagsToTextBox(textBoxId,tagString){{
    if (textBoxId != '') {{
        var textbox = document.getElementById(textBoxId);
        if (textbox != null){{
            textbox.value = decodeURI(tagString);
        }}
    }}
}}", ResolveUrl(baseUrl));
    }

    #endregion
}
