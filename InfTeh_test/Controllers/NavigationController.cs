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

            return PartialView(model);
        }
    }
}