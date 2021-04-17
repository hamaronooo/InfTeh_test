using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
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
            return PartialView("_PartialSearchResults", GetFilesByDisplaynameOrExt(searchInput.Trim()));
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

        private List<Models.DataContext.File> GetFilesByDisplaynameOrExt(string searchInput)
        {
            List<File> result = new List<File>();
            result = db.Files
                .Where(m => m.displayname.Contains(searchInput) 
                            || m.FileExtension.displayname.Contains(searchInput))
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