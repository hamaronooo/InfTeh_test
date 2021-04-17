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
    public class ExplorerController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult _PartialFilePathBlock(int? currentFolderid)
        {
            if (currentFolderid == null)
                return PartialView(new List<Folder>());

            var currFolder = db.Folders.FirstOrDefault(m => m.folderid == currentFolderid);
            List<Folder> Folders = new List<Folder>();
            Folders.Add(currFolder);
            GetFoldersStructure(Folders, currFolder);

            return PartialView(Folders);
        }

        public ActionResult _PartialSearchForm()
        {
            return PartialView();
        }

        public ActionResult SearchFiles(string searchInput)
        {
            return PartialView("_PartialSearchResults", new FilesWorker().GetFilesByDisplaynameOrExt(searchInput.Trim()));
        }

        #region HelpMethods

        private List<Folder> GetFoldersStructure(List<Folder> folders, Folder currentFolder)
        {
            if(currentFolder.parent_folderid == null)
                return folders;

            var offset = db.Folders.FirstOrDefault(m => m.folderid == currentFolder.parent_folderid);
            if (offset != null)
                folders.Insert(0,offset);
            return GetFoldersStructure(folders, offset);
        }

        public ActionResult _PartialFolderContent(int folderid)
        {
            NavigationModel model = new NavigationModel();

            if (folderid != null)
            {
                model.ParentFolder = db.Folders.FirstOrDefault(m => m.folderid == folderid);
                model.Folders = db.Folders.Where(m => m.parent_folderid == folderid);
            }
            else
            {
                model.ParentFolder = new Folder() { folderid = 0 };
                model.Folders = db.Folders.Where(m => m.parent_folderid == null);
            }

            model.Files = new FilesWorker().GetFilesByFolderID(folderid);

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

            return PartialView("_PartialFolderContent", model);
        }



        #endregion
    }
}