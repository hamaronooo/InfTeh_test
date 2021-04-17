using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using InfTeh_test.Classes;
using InfTeh_test.DataContexts;
using InfTeh_test.Models;
using InfTeh_test.Models.DataContext;

namespace InfTeh_test.Controllers
{
    public class NavigationController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult GetNavigationBlock(int? parentFolderid)
        {
            NavigationModel model = new NavigationModel();

            if (parentFolderid != null)
            {
                model.ParentFolder = db.Folders.FirstOrDefault(m => m.folderid == parentFolderid);
                model.Folders = db.Folders.Where(m => m.parent_folderid == parentFolderid);
            }
            else
            {
                model.ParentFolder = new Folder(){ folderid = 0 };
                model.Folders = db.Folders.Where(m => m.parent_folderid == null);
            }

            model.Files = new FilesWorker().GetFilesByFolderID(parentFolderid);

            foreach (File file in model.Files)
            {
                string extName = file.FileExtension?.icon_filename ?? file.FileExtension.displayname + ".svg";
                string relativePath = "/Content/FileIcons/" + extName;
                string fullpath = HttpContext.Server.MapPath(relativePath);

                bool a = System.IO.File.Exists(fullpath);

                if (System.IO.File.Exists(fullpath))
                    file.IconFileName = extName;
                else file.IconFileName = "unknown.svg";
            }

            return PartialView("NavigationBlock", model);
        }

        public ActionResult _PartialFolderInfo(int? parentFolderid)
        {
            NavigationModel model = new NavigationModel();

            if (parentFolderid != null)
            {
                model.ParentFolder = db.Folders.FirstOrDefault(m => m.folderid == parentFolderid);
                model.Folders = db.Folders.Where(m => m.parent_folderid == parentFolderid);
            }
            else
            {
                model.ParentFolder = new Folder() { folderid = 0 };
                model.Folders = db.Folders.Where(m => m.parent_folderid == null);
            }

            model.Files = new FilesWorker().GetFilesByFolderID(parentFolderid);

            foreach (var file in model.Files)
            {
                string relativePath = "~/Content/FileIcons/" + file.FileExtension?.icon_filename;
                file.IconFileName = System.IO.File.Exists(HttpContext.Server.MapPath(relativePath))
                    ? file.FileExtension?.icon_filename
                    : "unknown.svg";
            }

            return PartialView(model);
        }



        #region HelpMethods

        private List<Models.DataContext.File> GetFilesByFolderID(int? folderid)
        {
            List<File> result = new List<File>();
            result = db.Files
                .Where(m => m.folderid == folderid)
                .Include(m => m.FileExtension)
                .Select(m => new
                {
                    fileid = m.fileid,
                    displayname = m.displayname,
                    description = m.description,
                    file_extensionid = m.file_extensionid,
                    folderid = m.folderid,
                    FileExtension = m.FileExtension
                }).ToList().Select(x => new File()
                {
                    fileid = x.fileid,
                    displayname = x.displayname,
                    description = x.description,
                    file_extensionid = x.file_extensionid,
                    folderid = x.folderid,
                    FileExtension = x.FileExtension
                }).ToList();

            return result;
        }

        #endregion
    }
}