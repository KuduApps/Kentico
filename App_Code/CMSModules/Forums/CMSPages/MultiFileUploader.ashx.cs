using System;
using System.Collections.Generic;
using System.Web;

using CMS.ExtendedControls;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.IO;
using CMS.Forums;
using CMS.GlobalHelper;

namespace MultiFileUploader
{
    /// <summary>
    /// Multifile forums uploader class for Http handler.
    /// </summary>
    public class ForumsUploader : IHttpHandler
    {
        #region "Public Methods"

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                // Get arguments passed via query string
                UploaderHelper args = new UploaderHelper(context);
                String appPath = context.Server.MapPath("~/");
                DirectoryHelper.EnsureDiskPath(args.FilePath, appPath);

                if (args.Canceled)
                {
                    // Remove file from server if canceled
                    args.CleanTempFile();
                }
                else
                {
                    args.ProcessFile();
                    if (args.Complete)
                    {
                        if (args.IsForumAttachmentUpload)
                        {
                            HandleForumUpload(args, context);
                        }
                        args.CleanTempFile();
                    }
                }
            }
            catch (Exception ex)
            {
                // Send error message
                context.Response.Write(String.Format(@"0|{0}", HTMLHelper.EnsureLineEnding(ex.Message, " ")));
                context.Response.Flush();
            }
        }

        #endregion


        #region "Private Methods"

        /// <summary>
        /// Provides operations necessary to create and store new cms file.
        /// </summary>
        /// <param name="args">Upload arguments.</param>
        /// <param name="context">HttpContext instance.</param>
        private void HandleForumUpload(UploaderHelper args, HttpContext context)
        {
            ForumInfo fi = null;

            try
            {
                args.IsExtensionAllowed();

                if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.forums", CMSAdminControl.PERMISSION_MODIFY))
                {
                    throw new Exception("Current user is not granted with modify permission per 'cms.forums' resource.");
                }

                fi = ForumInfoProvider.GetForumInfo(args.ForumArgs.PostForumID);
                if (fi != null)
                {
                    ForumGroupInfo fgi = ForumGroupInfoProvider.GetForumGroupInfo(fi.ForumGroupID);
                    if (fgi != null)
                    {
                        ForumAttachmentInfo fai = new ForumAttachmentInfo(args.FilePath, 0, 0, 0)
                        {
                            AttachmentPostID = args.ForumArgs.PostID,
                            AttachmentSiteID = fgi.GroupSiteID
                        };
                        ForumAttachmentInfoProvider.SetForumAttachmentInfo(fai);
                    }
                }
            }
            catch (Exception ex)
            {
                args.Message = ex.Message;
            }
            finally
            {
                if (!string.IsNullOrEmpty(args.AfterSaveJavascript))
                {
                    args.AfterScript = String.Format(@"
                    if (window.{0} != null) {{
                        window.{0}()
                    }} else if ((window.parent != null) && (window.parent.{0} != null)) {{
                        window.parent.{0}() 
                    }}", args.AfterSaveJavascript);
                }
                else
                {
                    args.AfterScript = String.Format(@"
                    if (window.InitRefresh_{0})
                    {{
                        window.InitRefresh_{0}('{1}', false, false);
                    }}
                    else {{ 
                        if ('{1}' != '') {{
                            alert('{1}');
                        }}
                    }}", args.ParentElementID, ScriptHelper.GetString(args.Message.Trim(), false));
                }

                args.AddEventTargetPostbackReference();
                context.Response.Write(args.AfterScript);
                context.Response.Flush();
            }
        }

        #endregion


        #region "Properties"

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #endregion
    }
}