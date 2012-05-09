using System;
using System.Web;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.Ecommerce;

public partial class CMSModules_Ecommerce_Pages_Tools_Header : CMSEcommercePage
{
    protected void Page_PreRender(object sender, EventArgs e)
    {
        string scriptRedir = ScriptHelper.GetScript(@" 
            function redir(url, target) 
            { 
                if (url != '') 
                { 
                    if (target != '') 
                    { 
                        if ((target == '_blank')||(target == '_new')) 
                        { 
		                    window.open(url); 
                            return true; 
                        }
                        else 
                        { 
		                    try 
		                    { 
			                    if ( !parent.frames[target].CheckChanges() ) return false; 
		                    } 
		                    catch (ex) {} 
		                    parent.frames[target].location.href = url; return true; } 
                        } 
                    else 
                    {
                        parent.location.href = url; 
                        return true; 
                    } 
                }  
                return true; 
            } ");

        ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(string), "RedirectScript", scriptRedir);

        string script = null;

        // Call the script for tab which is selected
        UIElementInfo selected = uiToolbarElem.SelectedUIElement;
        if (selected != null)
        {
            string url = selected.ElementTargetURL;

            // if target url contains javascript, execute this script
            if (url.StartsWith("javascript:", StringComparison.InvariantCultureIgnoreCase))
            {
                script = url.Remove(0, 11); // 11 - length of "javascript:"
            }
            else
            {
                script = "redir(" + ScriptHelper.GetString(URLHelper.ResolveUrl(url) + URLHelper.Url.Query) + ",'" + uiToolbarElem.TargetFrameset + "'); ";
            }
        }
        // No tab is visible
        else
        {
            script = "redir('" + URLHelper.ResolveUrl("~/CMSMessages/Information.aspx") + "?message=" + HttpUtility.UrlPathEncode(GetString("uiprofile.uinotavailable")) + "','" + uiToolbarElem.TargetFrameset + "'); ";
        }

        ScriptHelper.RegisterStartupScript(Page, typeof(string), "TabSelection", ScriptHelper.GetScript(script));
        ScriptHelper.RegisterTitleScript(this, GetString("cmsdesk.ui.ecommerce"));
    }
}
