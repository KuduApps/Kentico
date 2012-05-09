using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.UIControls;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_CMSDesk_New_NewCultureVersion : CMSContentPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Register progrees script
        ScriptHelper.RegisterProgress(Page);

        // Check if the culture is valid
        CheckPreferredCulture(true);

        ltlScript.Text += ScriptHelper.GetScript(
            "var radCopyElem = document.getElementById('" + radCopy.ClientID + "');\n" +
                "var lstCulturesElem = document.getElementById('" + lstCultures.ClientID + "');\n"
            );

        // Setup page title text and image
        CurrentMaster.Title.TitleText = GetString("Content.NewCultureVersionTitle") + " (" + HTMLHelper.HTMLEncode(CMSContext.PreferredCultureCode) + ")";
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Content/Menu/new.png");

        lblInfo.Text = GetString("ContentNewCultureVersion.Info");
        radCopy.Text = GetString("ContentNewCultureVersion.Copy");
        radEmpty.Text = GetString("ContentNewCultureVersion.Empty");

        radCopy.Attributes.Add("onclick", "ShowSelection();");
        radEmpty.Attributes.Add("onclick", "ShowSelection()");

        btnOk.Text = GetString("ContentNewCultureVersion.Create");
        btnOk.Attributes.Add("onclick", "NewDocument(" + Request.QueryString["nodeid"] + "); return false;");

        // Current Node ID
        int nodeId = QueryHelper.GetInteger("nodeid", 0);

        if (nodeId > 0)
        {
            // Fill in the existing culture versions
            TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
            TreeNode node = tree.SelectSingleNode(nodeId, TreeProvider.ALL_CULTURES);
            if (node != null)
            {
                if (!CMSContext.CurrentUser.IsAuthorizedToCreateNewDocument(node.NodeParentID, node.NodeClassName))
                {
                    pnlNewVersion.Visible = false;
                    lblInfo.Visible = false;
                    lblError.Visible = true;
                    lblError.Text = GetString("accessdenied.notallowedtocreatenewcultureversion");
                }
                else
                {
                    SiteInfo si = SiteInfoProvider.GetSiteInfo(node.NodeSiteID);
                    if (si != null)
                    {
                        DataSet nodes = tree.SelectNodes(si.SiteName, node.NodeAliasPath, TreeProvider.ALL_CULTURES, false, null, null, null, 1, false);
                        foreach (DataRow nodeCulture in nodes.Tables[0].Rows)
                        {
                            CultureInfo ci = CultureInfoProvider.GetCultureInfo(nodeCulture["DocumentCulture"].ToString());
                            if (ci != null)
                            {
                                ListItem li = new ListItem();
                                li.Text = ResHelper.LocalizeString(ci.CultureName);
                                li.Value = nodeCulture["DocumentID"].ToString();
                                lstCultures.Items.Add(li);
                            }
                        }

                        if (lstCultures.Items.Count > 0)
                        {
                            lstCultures.SelectedIndex = 0;
                        }
                        if (!CMSContext.CurrentUser.IsCultureAllowed(CMSContext.PreferredCultureCode, si.SiteName))
                        {
                            pnlNewVersion.Visible = false;
                            lblInfo.Visible = false;
                            lblError.Visible = true;
                            lblError.Text = GetString("transman.notallowedcreate");
                        }
                    }
                }
            }
            else
            {
                pnlNewVersion.Visible = false;
                lblInfo.Text = GetString("Content.DocumentNotExistsInfo");

                // Setup page title text and image
                CurrentMaster.Title.TitleText = GetString("Content.DocumentNotExists");
                CurrentMaster.Title.TitleImage = GetImageUrl("Others/Messages/error.png");
            }
        }
    }


    /// <summary>
    /// Adds the script to the output request window.
    /// </summary>
    /// <param name="script">Script to add</param>
    public override void AddScript(string script)
    {
        ltlScript.Text += ScriptHelper.GetScript(script);
    }
}
