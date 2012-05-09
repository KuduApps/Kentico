using System;
using System.Data;
using System.Collections;
using System.Text.RegularExpressions;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.TreeEngine;
using CMS.CMSHelper;
using System.Text;


public partial class CMSModules_Content_Controls_Dialogs_Properties_DocCopyMoveProperties : ItemProperties
{
    #region "Enums"

    protected enum Action
    {
        Move = 0,
        Copy = 1,
        Link = 2,
        LinkDoc = 3
    }

    #endregion


    #region "Private properties"

    /// <summary>
    /// Returns current dialog action.
    /// </summary>
    private Action CurrentAction
    {
        get
        {
            try
            {
                return (Action)Enum.Parse(typeof(Action), Config.CustomFormatCode, true);
            }
            catch
            {
                return Action.Move;
            }
        }
    }


    /// <summary>
    /// Target node alias path.
    /// </summary>
    private int AliasPath
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["AliasPath"], 0);
        }
        set
        {
            ViewState["AliasPath"] = value;
        }
    }


    /// <summary>
    /// Target node ID.
    /// </summary>
    private int TargetNodeID
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["TargetNodeID"], 0);
        }
        set
        {
            ViewState["TargetNodeID"] = value;
        }
    }


    /// <summary>
    /// Preview URL.
    /// </summary>
    private string PreviewURL
    {
        get
        {
            return ValidationHelper.GetString(ViewState["PreviewURL"], string.Empty);
        }
        set
        {
            ViewState["PreviewURL"] = value;
        }
    }


    /// <summary>
    /// Preview extension.
    /// </summary>
    private string PreviewExt
    {
        get
        {
            return ValidationHelper.GetString(ViewState["PreviewExt"], string.Empty);
        }
        set
        {
            ViewState["PreviewExt"] = value;
        }
    }


    /// <summary>
    /// Dialog control identifier.
    /// </summary>
    public string Identifier
    {
        get
        {
            string identifier = ValidationHelper.GetString(ViewState["Identifier"], null);
            if (identifier == null)
            {
                identifier = Guid.NewGuid().ToString("N");
                ViewState["Identifier"] = identifier;
            }

            return identifier;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        string nodeIdsString = QueryHelper.GetString("sourcenodeids", string.Empty);

        // Load nodes alias paths to session (used in iFrame)
        if (!URLHelper.IsPostback() && !String.IsNullOrEmpty(nodeIdsString))
        {
            StringBuilder aliasPaths = new StringBuilder();
            TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
            DataSet ds = tree.SelectNodes(CMSContext.CurrentSiteName, TreeProvider.ALL_DOCUMENTS, TreeProvider.ALL_CULTURES, true, null, String.Format("NodeID IN ({0})", nodeIdsString.Trim('|').Replace("|", ",")), null, TreeProvider.ALL_LEVELS, false, 0, TreeProvider.SELECTNODES_REQUIRED_COLUMNS + ",NodeParentID, DocumentName, NodeAliasPath, NodeLinkedNodeID");
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                const string lineBreak = "<br />";

                // Create list of paths
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    aliasPaths.Append(ValidationHelper.GetString(dr["NodeAliasPath"], string.Empty));
                    if (ValidationHelper.GetInteger(dr["NodeLinkedNodeID"], 0) != 0)
                    {
                        aliasPaths.Append(UIHelper.GetDocumentMarkImage(Page, DocumentMarkEnum.Link));
                    }
                    aliasPaths.Append(lineBreak);
                }

                // Trim last line break
                if (aliasPaths.Length > lineBreak.Length)
                {
                    aliasPaths = aliasPaths.Remove(aliasPaths.Length - lineBreak.Length, lineBreak.Length);
                }
            }

            SessionHelper.SetValue("CopyMoveDocAliasPaths", aliasPaths);
            SessionHelper.Remove("CopyMoveDocCopyPermissions");
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Returns URL for the inner IFrame.
    /// </summary>
    private string GetFrameUrl(object nodeIds, object parentAlias, object targetId, object ext, object aliasPath, object multiple, object action, object parameters)
    {
        string frameUrl = ResolveUrl("~/CMSModules/Content/Controls/Dialogs/Properties/DocCopyMoveProperites.aspx");

        Hashtable properties = new Hashtable();
        // Fill properties table
        properties.Add("sourcenodeids", nodeIds);
        properties.Add("parentalias", parentAlias);
        properties.Add("targetid", targetId);
        properties.Add("ext", ext);
        properties.Add("aliaspath", aliasPath);
        properties.Add("multiple", multiple);
        properties.Add("parameters", parameters);
        properties.Add("output", CurrentAction);

        Hashtable param = WindowHelper.GetItem(parameters as string) as Hashtable;
        if (param != null)
        {
            // Transfer parameters to new hashtable for iframe
            foreach (string key in param.Keys)
            {
                properties[key] = param[key];
            }
        }

        if ((action != null) && (ValidationHelper.GetInteger(targetId, 0) > 0))
        {
            properties.Add("action", action);
        }
        WindowHelper.Add(Identifier, properties);

        frameUrl = URLHelper.AddParameterToUrl(frameUrl, "params", Identifier);
        frameUrl = URLHelper.AddParameterToUrl(frameUrl, "hash", QueryHelper.GetHash(frameUrl));
        return frameUrl;
    }

    #endregion


    #region "Overriden methods"

    public override void LoadSelectedItems(MediaItem item, Hashtable properties)
    {
        if (properties == null)
        {
            properties = new Hashtable();
        }

        string url = item.MediaType == MediaTypeEnum.Flash ? URLHelper.UpdateParameterInUrl(item.Url, "ext", "." + item.Extension.TrimStart('.')) : item.Url;

        properties[DialogParameters.DOC_NODEALIASPATH] = item.AliasPath;
        properties[DialogParameters.DOC_TARGETNODEID] = item.NodeID;
        properties[DialogParameters.URL_EXT] = item.Extension;
        properties[DialogParameters.URL_URL] = url;

        TargetNodeID = ValidationHelper.GetInteger(item.NodeID, 0);
        PreviewURL = url;
        PreviewExt = item.Extension;

        LoadProperties(properties);
    }


    /// <summary>
    /// Loads the properites into control.
    /// </summary>
    /// <param name="properties">Collection with properties</param>
    public override void LoadItemProperties(Hashtable properties)
    {
        LoadProperties(properties);
    }


    /// <summary>
    /// Loads the properites into control.
    /// </summary>
    /// <param name="properties">Collection with properties</param>
    public override void LoadProperties(Hashtable properties)
    {
        if (properties != null)
        {
            string url = GetFrameUrl(
                QueryHelper.GetText("sourcenodeids", string.Empty),
                QueryHelper.GetText("parentalias", string.Empty),
                properties[DialogParameters.DOC_TARGETNODEID],
                properties[DialogParameters.URL_EXT],
                properties[DialogParameters.DOC_NODEALIASPATH],
                QueryHelper.GetBoolean("multiple", false),
                null,
                QueryHelper.GetString("params", null));
            innerFrame.Attributes.Add("src", url);
        }
    }


    /// <summary>
    /// Returns all parameters of the selected item as name – value collection.
    /// </summary>
    public override Hashtable GetItemProperties()
    {
        string url = GetFrameUrl(
            QueryHelper.GetString("sourcenodeids", string.Empty),
            QueryHelper.GetString("parentalias", string.Empty),
            TargetNodeID,
            PreviewExt,
            AliasPath,
            QueryHelper.GetBoolean("multiple", false),
            true,
            QueryHelper.GetString("params", null));
        innerFrame.Attributes.Add("src", url);
        return null;
    }


    /// <summary>
    /// Clears the properties form.
    /// </summary>
    /// <param name="hideProperties"></param>
    public override void ClearProperties(bool hideProperties)
    {
        // Do nothing, inner frame handles it.
    }

    #endregion
}
